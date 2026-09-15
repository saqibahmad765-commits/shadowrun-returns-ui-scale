# Release evidence: UI Scale 1.1.0

Player ZIP SHA-256: bbadf1288f4e49ec1bb52bb82c6517cb03dece0e79f89c2cd3e8fdd75ca6fbe3
Plugin DLL SHA-256: f1e45cfc6959f1976ad33435e2bed9f25dddfc13390d19bcf1979a8a8fce66a1

The DLL matches the installed 1.1.0 DLL and original prepared build byte-for-byte.
Repository src files were compared with the original build source and are unchanged.
Stage 1 also rebuilt the same source successfully and passed all nine tests. That rebuild has different binary metadata/hash; it is not the distributed DLL.

Source commit: pending user commit. Release tag: proposed v1.1.0, not yet created.
Tag the reviewed source commit, not the initial empty-repository commit.

Source fingerprints below use UTF-8 without BOM and LF line endings, to allow for Git line-ending conversion:
a2ac57ac0227ca676bf528f47d1cc6685a2bd47362085d8e9aa3da042569d081  src/UIScale.cs
4e123ab21df41d76a94fa08059b04c94cc5435e9667adc6067f23d90d4952cb4  src/ScalePolicy.cs
