$packages = Get-ChildItem . *1.3.0.nupkg -rec
foreach ($file in $packages)
{ 
    nuget push -ApiKey AzureDevOps -src "ShamyrCloud" $file.FullName 
}