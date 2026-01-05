#!/bin/bash

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

cd "${0%/*}"
scriptdir="${PWD}"
outdir="${PWD}/out"
dartdir="${1:-${PWD}/sdk}"
mkdir -p "${outdir}"

if [ ! -d "${dartdir}" ]; then
    git clone https://github.com/dart-lang/sdk.git "${dartdir}"
fi

cd "${dartdir}"

git log --all |
    tr '\t' '\001' |
    sed -E 's@^commit ([0-9a-f]{40})$@\t\0@' |
    tr '\n\t' '\t\n' |
    sed -n -E 's@^commit ([0-9a-f]{40})\t.*git-svn-id: https://dart.googlecode.com/svn/branches/[^@]+[@]([0-9]+) .*@\1\t\2@p' \
    >"${outdir}/svn-commits.txt"

(
    cat "${outdir}/svn-commits.txt"
    echo
    git log --all |
        tr '\t' '\001' |
        sed -E 's@^commit ([0-9a-f]{40})@\t\0@' |
        tr '\n\t' '\t\n' |
        sed -n -E 's@^commit ([0-9a-f]{40})\t([^\t:]*: [^\t]*\t)*\t    Version ([0-9]\.[0-9]+\.[^\t ]*[0-9])[^\t]*\t.*\t    svn merge (-[rc] [0-9]+:|-c )([0-9]+) https://dart.googlecode.com/svn/branches/bleeding_edge [^\t]*\t.*\t    git-svn-id: http://dart.googlecode.com/svn/[^@]+[@][0-9]+ .*@tag=\3\t\5@p'
) |
    sort -n -k2 -r |
    sed -n -E '/^tag=/N;s/^tag=([^\t]*)\t[0-9]+\n([0-9a-f]{40})\t[0-9]+$/\1\t\2/p' |
    while read n c; do
        git tag "v${n}-edge" "${c}"
    done

git rev-list '--tags=[1-3].[0-9]*' \
    ^3dfb90f59f7a6846b00259770bc08104f7bcc594^1 \
    --topo-order -- \
    "${dartdir}/runtime/vm/${snapfiles[@]}" |
    tee "${outdir}/commits.lst"

git tag |
    grep -E '^(1\.[7-9]|1\.[1-9][0-9]|[2-9]\.[0-9]+)\.[0-9]+$' |
    sort -V -r |
    while read -r t; do
        git rev-parse "${t}"
    done |
    tee -a "${outdir}/commits.lst"

while read c <&3; do
    if [ -f "${outdir}/cid-"*"+${c:0:7}.h" ]; then
        continue
    fi
    git checkout -q "$c"
    cn="$(
        git describe --tags --match='v[0-9].[0-9]*-edge' --match='[0-9].[0-9]*' |
            sed 's@[~^][0-9~^]*@@;s@-g[0-9a-f]*$@@;s@[^A-Za-z0-9.^~-]@_@'
    )"
    cnc="$(
        git describe --tags --contains --match='v[0-9].[0-9]*-edge' --match='[0-9].[0-9]*' |
            sed 's@[^A-Za-z0-9.^~-]@_@'
    )"
    hash="$("${scriptdir}/make_version.py" "${dartdir}/tools/make_version.py")"
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
done 3<"${outdir}/commits.lst"

mkdir "${outdir}/classid"

cd "${outdir}"

cat >VersionTable_HashVersions.cs <<EOE
using Semver;

namespace DartParser;

public static partial class VersionTable
{
    private static readonly Dictionary<string, SemVersion> HashVersions = new()
    {
EOE

ls -1 cid-*.h | 
    sed -E 's@_([1-9]\.[^~^_]*[0-9])([~^][0-9]+)?_([1-9]\.[^-]*[0-9])-([1-9]+)[+]@_\1-pre.\4+@;s@_v?[1-9]\.[^_]*_v?([1-9]\.[^_]*)@_\1@;s@(_[1-9]\.[0-9]+\.[0-9]+)-([0-9]+)[+]@\1-pre.\2+@;s@\.h$@@' |
    cut -d_ -f2,3 |
    sort -V |
    uniq -w32 |
    sed -E 's@(_[1-9]\.[0-9]+\.[0-9]+)[+]@\1-zzzz+@' |
    sort -t_ -k2 -V |
    sed -E 's@-zzzz[+]@+@;s@^([0-9a-f]{32})_(.*)$@        ["\1"] = SemVersion.Parse("\2"),@' \
    >> VersionTable_HashVersions.cs

cat >>VersionTable_HashVersions.cs <<EOE
    };
}
EOE

ls -1 cid-*.h |
    sed -E 's@_([1-9]\.[^~^_]*[0-9])([~^][0-9]+)?_([1-9]\.[^-]*[0-9])-([1-9]+)[+]@_\1-pre.\4+@;s@_v?[1-9]\.[^_]*_v?([1-9]\.[^_]*)@_\1@;s@(_[1-9]\.[0-9]+\.[0-9]+)-([0-9]+)[+]@\1-pre.\2+@;s@\.h$@@' |
    sort |
    uniq -w37 |
    sort -t_ -k3 -V |
    tr '_+' '\t' |
    while read -r cid hash ver commit; do
        cp -v ${cid}_${hash}_*+${commit}.h "classid/cid-${ver}+${commit}_${cid#cid-}.h"
    done

cat >VersionTable_ClassIdChanges.cs <<EOE
using Semver;

namespace DartParser;

public static partial class VersionTable
{
    public static readonly Dictionary<SemVersion, AddsDels<ClassId>> ClassIdChanges = new()
    {
EOE

ls -1 classid/ |
    sort -V |
    (
        prev=
        while read n; do
            if [ -n "${prev}" ]; then
                diff -u "classid/${prev}" "classid/${n}"
            fi
            prev="${n}"
        done
    ) |
        sed -n -E 's@ = [0-9]+,$@,@;s@^[+][+][+] classid/cid-([^_]*).*@VERSION\t\1@p;s@^  *([^,]*),$@=\t\1@p;s@^[-] *([^-,]*),$@-\t\1@p;s@^[+] *([^+,]*),$@+\t\1@p' |
        (
            version=
            prevadd=
            prevdel=
            adds=()
            dels=()
            while read -r t v; do
                case "$t" in
                    "VERSION")
                        if [ -n "${version}" ]; then
                            printf "        [SemVersion.Parse(\"%s\")] = (\n            Adds: [\n" "$version"
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
                            "(Value: ClassId.${v}, After: ClassId.${prevdel})"
                        )
                        prevdel="${v}"
                        ;;
                    "+")
                        adds+=(
                            "(Value: ClassId.${v}, After: ClassId.${prevadd})"
                        )
                        prevadd="${v}"
                        ;;
                esac
            done
            if [ -n "${version}" ]; then
                printf "        [SemVersion.Parse(\"%s\")] = (\n            Adds: [\n" "$version"
                for add in "${adds[@]}"; do
                    printf "                %s,\n" "$add"
                done
                printf "            ],\n            Dels: [\n"
                for del in "${dels[@]}"; do
                    printf "                %s,\n" "$del"
                done
                printf "            ]\n),\n"
            fi
        ) >>VersionTable_ClassIdChanges.cs

cat >>VersionTable_ClassIdChanges.cs <<EOE
    };
}
EOE

