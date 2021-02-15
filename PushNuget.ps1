$packages = Get-ChildItem . *5.0.0.nupkg -rec
foreach ($file in $packages)
{ 
    nuget push -ApiKey AzureDevOps -src "ShamyrCloud" $file.FullName 
}