#!/usr/bin/env python3
# Modified from the original Dart make_version.py
# Copyright (c) 2026, Ben Peddell.
#
# Original Copyright (c) 2011, the Dart project authors.
# Please see the Dart AUTHORS file for details.
# All rights reserved.
# Use of this source code is governed by a BSD-style license
# that can be found in the LICENSE file.
#
# This python script creates a version string in a C++ file.

import argparse
import hashlib
import os
import sys


def MakeSnapshotHashString(snapfiles: list[str], dartdir: str) -> str:
    vmhash = hashlib.md5()
    for vmfilename in snapfiles:
        vmfilepath = os.path.join(dartdir, 'runtime', 'vm', vmfilename)
        with open(vmfilepath, 'rb') as vmfile:
            vmhash.update(vmfile.read())
    return vmhash.hexdigest()


def LoadSnapshotFilesFromCurrent(filename: str = '../tools/make_version.py') -> list[str]:
    with open(filename, 'rt') as f:
        first_line = False
        last_line = False
        snapfiles = []

        for line in f:
            line = line.strip()
            if line == 'VM_SNAPSHOT_FILES = [' or line == 'VM_SNAPSHOT_FILES=[':
                first_line = True
            elif first_line and line == ']':
                last_line = True
            elif first_line and not last_line and line[0] == '\'':
                line = line.strip('\',')
                snapfiles.append(line)

    return snapfiles


def main():
    if len(sys.argv) == 2:
        snapfile = sys.argv[1]
    else:
        snapfile = '../tools/make_version.py'

    snapdir = os.path.abspath(os.path.dirname(snapfile), '..')

    snapfiles = LoadSnapshotFilesFromCurrent(snapfile)

    if not snapfiles:
        sys.stderr.write('No VM_SNAPSHOT_FILES in make_version.py\n')
        return 1

    print(MakeSnapshotHashString(snapfiles, snapdir))

    return 0

if __name__ == '__main__':
    sys.exit(main())
