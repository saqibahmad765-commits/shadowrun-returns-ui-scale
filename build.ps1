param(
    [Parameter(Mandatory=$true)]
    [string]$GamePath,
    [string]$CompilerPath = (Join-Path $env:WINDIR 'Microsoft.NET\Framework\v3.5\csc.exe')
)
$ErrorActionPreference = 'Stop'
$game = (Resolve-Path -LiteralPath $GamePath).Path
$refs = @(
    (Join-Path $game 'BepInEx\core\BepInEx.dll'),
    (Join-Path $game 'BepInEx\core\0Harmony.dll'),
    (Join-Path $game 'Shadowrun_Data\Managed\Assembly-CSharp.dll'),
    (Join-Path $game 'Shadowrun_Data\Managed\UnityEngine.dll')
)
foreach ($file in (@($CompilerPath) + $refs)) {
    if (!(Test-Path -LiteralPath $file -PathType Leaf)) { throw "Required dependency missing: $file. See BUILDING.md." }
}
$out = Join-Path $PSScriptRoot 'artifacts'
New-Item -ItemType Directory -Path $out -Force | Out-Null
$compileArgs = @('/nologo', '/target:library', '/optimize+', "/out:$out\UIScale.dll")
$compileArgs += @($refs | ForEach-Object { '/reference:' + $_ })
$compileArgs += @("$PSScriptRoot\src\UIScale.cs", "$PSScriptRoot\src\ScalePolicy.cs")
& $CompilerPath @compileArgs
if ($LASTEXITCODE -ne 0) { throw 'Plugin compilation failed.' }
& $CompilerPath /nologo /target:exe "/out:$out\ScaleTests.exe" "$PSScriptRoot\src\ScalePolicy.cs" "$PSScriptRoot\tests\Tests.cs"
if ($LASTEXITCODE -ne 0) { throw 'Test compilation failed.' }
& "$out\ScaleTests.exe"
if ($LASTEXITCODE -ne 0) { throw 'Scale tests failed.' }
Write-Output "Built $out\UIScale.dll. No game files were changed."
