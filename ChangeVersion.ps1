param([string] $from, [string] $to)

$configFiles = Get-ChildItem . *.csproj -rec
foreach ($file in $configFiles)
{
    (Get-Content $file.PSPath -Encoding UTF8) |
    Foreach-Object { $_ -replace "<Version>$from</Version>", "<Version>$to</Version>" } |
    Set-Content $file.PSPath -Encoding UTF8
}