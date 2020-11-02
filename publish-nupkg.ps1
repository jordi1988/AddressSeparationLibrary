# Publishes the newest package to NuGet.
$dir = $PSScriptRoot + "\AddressSeparation\bin\Release\"
$filter="*.nupkg"
$latest = Get-ChildItem -Path $dir -Filter $filter | Sort-Object LastAccessTime -Descending | Select-Object -First 1

# [System.Environment]::SetEnvironmentVariable('NUGETAPIKEY', 'insert_key_here',[System.EnvironmentVariableTarget]::Machine)
$api_key = [System.Environment]::GetEnvironmentVariable('NUGETAPIKEY', 'machine')

dotnet nuget push $latest.FullName --api-key $api_key --source https://api.nuget.org/v3/index.json --skip-duplicate