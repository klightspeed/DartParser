#!/bin/bash

set -e

snapfiles=(
    app_snapshot.cc
    app_snapshot.h
    clustered_snapshot.cc
    clustered_snapshot.h
    dart.cc
    dart_api_impl.cc
    datastream.h
    image_snapshot.cc
    image_snapshot.h
    object.cc
    object.h
    raw_object.cc
    raw_object.h
    raw_object_snapshot.cc
    snapshot.cc
    snapshot.h
    snapshot_ids.h
    symbols.cc
    symbols.h
    class_id.h
    ../../tools/make_version.py
)

cd "$(dirname "${0}")"
scriptdir="${PWD}"
outdir="${PWD}/out"
dartdir="${1:-${PWD}/sdk}"
mkdir -p "${outdir}"

if [ ! -d "${dartdir}" ]; then
    git clone https://github.com/dart-lang/sdk.git "${dartdir}"
fi

cd "${dartdir}"

if [ ! -f "${outdir}/svn-commits.txt" ]; then
    git log --all |
        tr '\t' '\001' |
        sed -E 's@^commit ([0-9a-f]{40})$@\t\0@' |
        tr '\n\t' '\t\n' |
        sed -n -E 's@^commit ([0-9a-f]{40})\t.*git-svn-id: https://dart.googlecode.com/svn/branches/[^@]+[@]([0-9]+) .*@\1\t\2@p' \
        >"${outdir}/svn-commits.txt.tmp"
    mv "${outdir}/svn-commits.txt.tmp" "${outdir}/svn-commits.txt"
fi

if [ ! -f "${outdir}/git-tag-commits.txt" ]; then
    (
        cat "${outdir}/svn-commits.txt"
        echo
        git log --all |
            tr '\t' '\001' |
            sed -E 's@^commit ([0-9a-f]{40})@\t\0@' |
            tr '\n\t' '\t\n' |
            sed -n -E 's@^commit ([0-9a-f]{40})\t([^\t:]*: [^\t]*\t)*\t    Version ([0-9]\.[0-9]+\.[^\t ]*[0-9])[^\t]*\t.*\t    svn merge (-[rc] [0-9]+:|-c )([0-9]+) https://dart.googlecode.com/svn/branches/bleeding_edge [^\t]*\t.*\t    git-svn-id: http://dart.googlecode.com/svn/[^@]+[@][0-9]+ .*@tag=\3\t\5@p'
    ) |
        sort -V -k2 |
        sed -n -E '/^tag=/N;s/^tag=([^\t]*)\t[0-9]+\n([0-9a-f]{40})\t[0-9]+$/\1\t\2/p' |
        tee "${outdir}/git-tag-commits.txt.tmp"

    git tag --list '[1-9].[0-9]*' |
        sort -V |
        while read n; do
            if [ "$(git rev-list --count "main..${n}^2" 2>/dev/null)" == 0 ] && ! grep -q -v -E '^(CHANGELOG.md|tools/VERSION)$' < <(git diff --name-only "${n}^2..${n}"); then
                printf "%s\t%s\n" "${n}" "$(git rev-parse "${n}^2")"
            fi
        done |
        tee -a "${outdir}/git-tag-commits.txt.tmp"

    while read n c <&3; do
        git tag -f "v${n}-edge" "${c}"
    done 3<"${outdir}/git-tag-commits.txt.tmp"

    mv "${outdir}/git-tag-commits.txt.tmp" "${outdir}/git-tag-commits.txt"
fi

if [ ! -f "${outdir}/commits.lst" ]; then
    cd "${dartdir}/runtime/vm"

    git rev-list '--tags=[1-3].[0-9]*' \
        ^3dfb90f59f7a6846b00259770bc08104f7bcc594^1 \
        --topo-order -- \
        "${snapfiles[@]}" |
        tee "${outdir}/commits.lst.tmp"

    cd "${dartdir}"

    git tag |
        grep -E '^(1\.[7-9]|1\.[1-9][0-9]|[2-9]\.[0-9]+)\.[0-9]+$' |
        sort -V -r |
        while read -r t; do
            git rev-parse "${t}"
        done |
        tee -a "${outdir}/commits.lst.tmp"

    mv "${outdir}/commits.lst.tmp" "${outdir}/commits.lst"
fi

while read c <&3; do
    if [ -f "${outdir}"/cid-????????????????????????????????_????????????????????????????????_*"+${c:0:7}.h" ]; then
        continue
    fi
    git checkout -f -q "$c"
    if grep -q 'VM_SNAPSHOT_FILES' "${dartdir}/tools/make_version.py"; then
        cn="$(
            git describe --tags --match='v[0-9].[0-9]*-edge' --match='[0-9].[0-9]*' "${c}" |
                sed 's@[~^][0-9~^]*@@;s@-g[0-9a-f]*$@@;s@[^A-Za-z0-9.^~-]@_@'
        )"
        cnc="$(
            git describe --tags --contains --match='v[0-9].[0-9]*-edge' --match='[0-9].[0-9]*' "${c}" |
                sed 's@[^A-Za-z0-9.^~-]@_@'
        )"
        hash="$(python3 "${scriptdir}/make_version.py" "${dartdir}/tools/make_version.py")"
        echo "${c:0:11} ${hash} ${cn} ${cnc}"
        name="${outdir}/cid-${hash}_${cnc}_${cn}+${c:0:7}.h"
        g++ -E "${dartdir}/runtime/vm/raw_object.h" -I"${dartdir}/runtime" -I"${dartdir}/runtime/include" -D NDEBUG |
            sed -n -E '/^enum ClassId( : intptr_t)? [{]/,/[}];/p' |
            sed -E 's@^[[:space:]]+@@;s@, @,\n@g;/^$/d;s@^k@    k@mg'\
            >"${name}"
        cidhash="$(
            md5sum "${name}" |
                cut '-d ' -f1
        )"
        newname="${outdir}/cid-${cidhash}_${name##*/cid-}"
        mv "${name}" "${newname}"
        ls -l "${newname}"
    fi
done 3<"${outdir}/commits.lst"

rm -rf "${outdir}/classid"
mkdir -p "${outdir}/classid"

cd "${outdir}"

(
    cat "${scriptdir}/VersionTable_InconsistentClassIdHashes.cs.head"

    ls -1 "${outdir}"/cid-*.h |
        sed -E \
            -e 's@^.*/cid-([0-9a-f]{32})_([0-9a-f]{32})_.*@\1\t\2@' |
        uniq |
        sort -k2 |
        uniq -D -f1 |
        cut -f2 |
        uniq |
        while read hash; do
            printf '        "%s",\n' "$hash"
        done

    cat "${scriptdir}/VersionTable_InconsistentClassIdHashes.cs.tail"
) >"${outdir}/VersionTable_InconsistentClassIdHashes.cs.tmp"

mv "${outdir}/VersionTable_InconsistentClassIdHashes.cs.tmp" "${outdir}/VersionTable_InconsistentClassIdHashes.cs"

ls -1 "${outdir}"/cid-*.h |
    sed -E \
        -e 's@^.*/cid-@@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((1\.7\.0)-(dev\.[01]\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t1.6.1-0-\5-\8\t1.6.1\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((1\.7\.0)-(dev\.[23]\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t1.6.1-0-\5-\8\t1.6.3\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((1\.9\.0)-(dev\.0\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t1.8.4-0-\5-\8\t1.8.4\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((1\.9\.0)-(dev\.[1-3]\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t1.8.5-0-\5-\8\t1.8.5\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((1\.10\.0)-(dev\.0\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t1.9.2-0-\5-\8\t1.9.2\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((1\.11\.0)-(dev\.0\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t1.10.1-0-\5-\8\t1.10.1\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((2\.2\.0)-(dev\.[01]\.[^_^~]*)[^_]*)_v?(([^_+-]*)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t2.1.1-0-\5-\8\t2.1.1\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?(([1-9]\.[0-9]+\.[0-9]+)-[^_^~]*([~^][0-9]+)*)_v?(\4-[^_+]*)[+]([0-9a-f]{7})\.h$@\1\t\2\t\6\t\4\t\7@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_([1-9]\.[0-9]+\.[0-9]+)\^0_\3[+]([0-9a-f]{7})\.h$@\1\t\2\t\3-zzzz\t\3\t\4@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_(([1-9]\.[^~^_]*[0-9])([~^][0-9]+)*)_([1-9]\.[^-]*[0-9])-([1-9]+)[+]([0-9a-f]{7})\.h$@\1\t\2\t\4-pre.\7\t\4\t\8@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((2\.1\.1)-(dev\.0\.0)\^2~1)_v?((2\.2\.0)-dev\.1\.1-(1))[+]([0-9a-f]{7})\.h$@\1\t\2\t2.1.1-\5\t2.1.1\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?((2\.2\.0)-(dev\.2\.0-edge)[~^][^_]*)_v?(([1-9]\.[0-9]+\.[0-9]+)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t\4-0-\5-pre.\8\t\4\t\9@' \
        -e 's@^([0-9a-f]{32})_([0-9a-f]{32})_v?(([1-9]\.[0-9]+\.[0-9]+)-([^_^~]*)[^_]*)_v?(([1-9]\.[0-9]+\.[0-9]+)-[^_+]*-([0-9]+))[+]([0-9a-f]{7})\.h$@\1\t\2\t\4-0-\5-pre.\8\t\4\t\9@' |
    sort -k2 -V |
    uniq -f1 -w32 |
    sort -k3 -V |
    sed -E 's@-zzzz\t@\t@' \
    > "${outdir}/hashversions.txt"

(
    cat "${scriptdir}/VersionTable_HashVersions.cs.head"

    cut -f2,3,5 <"${outdir}/hashversions.txt" |
        while read hash ver commit; do
            printf '        ["%s"] = SemVersion.Parse("%s+%s"),\n' "${hash}" "${ver}" "${commit}"
        done

    cat "${scriptdir}/VersionTable_HashVersions.cs.tail"
) >"${outdir}/VersionTable_HashVersions.cs.tmp"

mv "${outdir}/VersionTable_HashVersions.cs.tmp" "${outdir}/VersionTable_HashVersions.cs"

uniq -w32 <"${outdir}/hashversions.txt" |
    sed -E 's@^([0-9a-f]{32})\t([0-9a-f]{32})\t([^\t]*)\t([^\t]*)\t([^\t]*)$@\1\t\4\t\2\t\3\t\5@' |
    sort -V |
    uniq -w37 |
    sort -k4 -V -r |
    uniq -w32 |
    cut -f1,3,4,5 |
    while read -r cid hash ver commit; do
        cp -v "${outdir}/cid-${cid}_${hash}_"*"+${commit}.h" "${outdir}/classid/cid-${ver}+${commit}_${cid#cid-}.h"
    done

ls -1 "${outdir}/classid/" |
    sort -V |
    (
        prev=
        while read n; do
            if [ -n "${prev}" ]; then
                diff -u "${outdir}/classid/${prev}" "${outdir}/classid/${n}" || true
            fi
            prev="${n}"
        done
    ) |
    sed -n -E 's@ = [0-9]+,$@,@;s@^[+][+][+] .*/classid/cid-([^_]*).*@VERSION\t\1@p;s@^  *([^,]*),$@=\t\1@p;s@^[-] *([^-,]*),$@-\t\1@p;s@^[+] *([^+,]*),$@+\t\1@p' \
    >"${outdir}/classid-changes.txt"

(
    cat "${scriptdir}/VersionTable_ClassIdChanges.cs.head"

    version=
    prevadd=
    prevdel=
    adds=()
    dels=()
    while read -r t v <&3; do
        case "$t" in
            "VERSION")
                if [ -n "${version}" ]; then
                    printf "        [SemVersion.Parse(\"%s\")] = new(\n            Adds: [\n" "$version"
                    for add in "${adds[@]}"; do
                        printf "                %s,\n" "$add"
                    done
                    printf "            ],\n            Dels: [\n"
                    for del in "${dels[@]}"; do
                        printf "                %s,\n" "$del"
                    done
                    printf "            ]\n        ),\n"
                fi
                version="${v}"
                prevadd=
                prevdel=
                adds=()
                dels=()
                ;;
            "=")
                prevadd="${v}"
                prevdel="${v}"
                ;;
            "-")
                dels+=(
                    "new(Value: ClassId.${v}, After: ClassId.${prevdel})"
                )
                prevdel="${v}"
                ;;
            "+")
                adds+=(
                    "new(Value: ClassId.${v}, After: ClassId.${prevadd})"
                )
                prevadd="${v}"
                ;;
        esac
    done 3<"${outdir}/classid-changes.txt"

    if [ -n "${version}" ]; then
        printf "        [SemVersion.Parse(\"%s\")] = new(\n            Adds: [\n" "$version"
        for add in "${adds[@]}"; do
            printf "                %s,\n" "$add"
        done
        printf "            ],\n            Dels: [\n"
        for del in "${dels[@]}"; do
            printf "                %s,\n" "$del"
        done
        printf "            ]\n        ),\n"
    fi

    cat "${scriptdir}/VersionTable_ClassIdChanges.cs.tail"
) >"${outdir}/VersionTable_ClassIdChanges.cs.tmp"

mv "${outdir}/VersionTable_ClassIdChanges.cs.tmp" "${outdir}/VersionTable_ClassIdChanges.cs"

