$packages = Get-ChildItem . *1.1.2.nupkg -rec
foreach ($file in $packages)
{ 
    nuget push -ApiKey AzureDevOps -src "ShamyrCloud" $file.FullName 
}