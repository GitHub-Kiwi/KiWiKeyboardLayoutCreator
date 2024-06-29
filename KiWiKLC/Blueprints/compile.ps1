$MSRoots = @("C:\Program Files*\MSBuild", "C:\Program Files*\Microsoft Visual Studio")
$MSBuild = Get-ChildItem $MSRoots -Recurse -Include MSBuild.exe -ErrorAction Ignore | ForEach-Object { $_.FullName} | Select-Object -First 1
if ($MSBuild -eq $null) {
    Exit-Script "MSBuild not found"
}
Write-Output "MSBuild: $MSBuild"
$Proj = Get-ChildItem ".\" *.vcxproj | Select-Object -First 1
Write-Output "ProjectFile: $Proj"

& $MSBuild $Proj /nologo /property:Configuration=Debug /property:Platform="x64"