TEST FILES DOCUMENTATION
========================

This directory contains test files for the DubzLib duplicate detection system.

DIRECTORY STRUCTURE:
-------------------
TargetFakeDir/                    (root - 4 files)
├── A/                           (6 files)
│   └── A/                       (2 files)
└── B/                           (7 files)

Total: 19 test files

TEST SCENARIOS:
--------------

1. EXACT DUPLICATES (Same name + Same content):
   - duplicate.txt: 3 copies (root/, A/, B/)
   - exactcopy.txt: 2 copies (A/, B/)
   - small.txt: 2 copies (root/, A/A/)

2. SAME NAME, DIFFERENT CONTENT:
   - file1.txt: 4 copies with different content
     * root/file1.txt
     * A/file1.txt  
     * A/A/file1.txt
     * B/file1.txt

3. SAME SIZE, DIFFERENT NAMES:
   - A/samesize.txt and B/samesize.txt (both ~100 bytes, different content)

4. SAME CONTENT, DIFFERENT NAMES:
   - A/different_name.txt and B/identical_content.txt (identical content)

5. SAME NAME, DIFFERENT SIZES:
   - A/samename_diffsize.txt (shorter)
   - B/samename_diffsize.txt (longer)

6. UNIQUE FILES (No duplicates):
   - root/file2.txt
   - A/A/unique.txt

TESTING COMPARISON MODES:
------------------------

GroesseUndName (Size AND Name):
- Should find: duplicate.txt (3), exactcopy.txt (2), small.txt (2), file1.txt (4)
- Should NOT find: samesize.txt, different_name.txt/identical_content.txt

Groesse (Size only):
- Should find all files with same size regardless of name
- Will group more files together based on byte size

MD5 CONTENT VERIFICATION:
------------------------
- duplicate.txt copies: identical MD5
- exactcopy.txt copies: identical MD5  
- small.txt copies: identical MD5
- file1.txt copies: different MD5 (same name, different content)
- different_name.txt/identical_content.txt: identical MD5 (different names, same content)