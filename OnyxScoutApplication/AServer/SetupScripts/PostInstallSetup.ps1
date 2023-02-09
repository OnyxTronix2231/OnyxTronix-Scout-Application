$sharepath = "C:\inetpub\AspNetCoreWebApps\app" 
$Acl = Get-ACL
$SharePath 
$AccessRule= New-Object System.Security.AccessControl.FileSystemAccessRule("DefaultAppPool","full","ContainerInherit,Objectinherit","none","Allow") 
$Acl.AddAccessRule($AccessRule)
Set-Acl $SharePath $Acl
