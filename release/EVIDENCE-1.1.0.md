# Release evidence: UI Scale 1.1.0

Player ZIP SHA-256: bbadf1288f4e49ec1bb52bb82c6517cb03dece0e79f89c2cd3e8fdd75ca6fbe3
Plugin DLL SHA-256: f1e45cfc6959f1976ad33435e2bed9f25dddfc13390d19bcf1979a8a8fce66a1

The DLL matches the installed 1.1.0 DLL and original prepared build byte-for-byte.
Repository src files were compared with the original build source and are unchanged.
Stage 1 also rebuilt the same source successfully and passed all nine tests. That rebuild has different binary metadata/hash; it is not the distributed DLL.

Reviewed source commit: https://github.com/saqibahmad765-commits/shadowrun-returns-ui-scale/commit/9c756d065ad9578a3e63ef80c5e9696fae4fe81b . Release tag: v1.1.0.
Release-preparation commits change documentation only; plugin source remains unchanged.

Source fingerprints below use UTF-8 without BOM and LF line endings, to allow for Git line-ending conversion:
a2ac57ac0227ca676bf528f47d1cc6685a2bd47362085d8e9aa3da042569d081  src/UIScale.cs
4e123ab21df41d76a94fa08059b04c94cc5435e9667adc6067f23d90d4952cb4  src/ScalePolicy.cs
