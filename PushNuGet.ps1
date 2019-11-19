param([string] $version)

$packages = Get-ChildItem . *$version.nupkg -rec
foreach ($file in $packages)
{ 
    nuget push -src "Github" $file.FullName 
}