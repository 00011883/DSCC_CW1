Import-Module WebAdministration
$iisAppPoolName = "GamesStore_11883_ClientPool"
$iisAppName = "GamesStore_11883_Client"

#navigate to the sites root
Set-Location IIS:\Sites\

#check if the site exists
if (Test-Path $iisAppName -pathType container)
{
    Stop-WebSite $iisAppName
	Remove-Website $iisAppName
}

#navigate to the app pools root
Set-Location IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
	Remove-WebAppPool $iisAppPoolName
}