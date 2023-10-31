Import-Module WebAdministration
$iisAppPoolName = "GamesStore_11883_ClientPool"
$iisAppName = "GamesStore_11883_Client"
$directoryPath = "C:\inetpub\wwwroot\GamesStore_11883_Client"

# Create the app pool if it doesn't exist
if (!(Test-Path "IIS:\AppPools\$iisAppPoolName")) {
    New-WebAppPool -Name $iisAppPoolName
}

# Set .NET Framework version
Set-ItemProperty "IIS:\AppPools\$iisAppPoolName" managedRuntimeVersion v4.0

# Create the website if it doesn't exist
if (!(Test-Path "IIS:\Sites\$iisAppName")) {
    New-Website -Name $iisAppName -Port 80 -PhysicalPath $directoryPath -ApplicationPool $iisAppPoolName
}
