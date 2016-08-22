param($installPath, $toolsPath, $package, $project)

$Config = $project

$WebConfig = $Config.ProjectItems | where-object {$_.Name -eq "Web.config"}
$item = $Config.ProjectItems | where-object {$_.Name -eq "Web.Main.config"}
$WebConfig.ProjectItems.AddFromFile($item.Properties.Item("FullPath").Value)
$item.Properties.Item("BuildAction").Value = [int]0
$item.Properties.Item("CopyToOutputDirectory").Value = [int]0
$item = $Config.ProjectItems | where-object {$_.Name -eq "Web.Hotfix.config"}
$WebConfig.ProjectItems.AddFromFile($item.Properties.Item("FullPath").Value)
$item.Properties.Item("BuildAction").Value = [int]0
$item.Properties.Item("CopyToOutputDirectory").Value = [int]0
$item = $Config.ProjectItems | where-object {$_.Name -eq "Web.Release.config"}
$WebConfig.ProjectItems.AddFromFile($item.Properties.Item("FullPath").Value)
$item.Properties.Item("BuildAction").Value = [int]0
$item.Properties.Item("CopyToOutputDirectory").Value = [int]0

$log4netConfig = $Config.ProjectItems | where-object {$_.Name -eq "log4net.config"}
$log4netConfig.Properties.Item("BuildAction").Value = [int]0
$log4netConfig.Properties.Item("CopyToOutputDirectory").Value = [int]0
$item = $Config.ProjectItems | where-object {$_.Name -eq "log4net.Main.config"}
$item.Properties.Item("BuildAction").Value = [int]0
$item.Properties.Item("CopyToOutputDirectory").Value = [int]0
$log4netConfig.ProjectItems.AddFromFile($item.Properties.Item("FullPath").Value)
$item = $Config.ProjectItems | where-object {$_.Name -eq "log4net.Hotfix.config"}
$item.Properties.Item("BuildAction").Value = [int]0
$item.Properties.Item("CopyToOutputDirectory").Value = [int]0
$log4netConfig.ProjectItems.AddFromFile($item.Properties.Item("FullPath").Value)
$item = $Config.ProjectItems | where-object {$_.Name -eq "log4net.Release.config"}
$item.Properties.Item("BuildAction").Value = [int]0
$item.Properties.Item("CopyToOutputDirectory").Value = [int]0
$log4netConfig.ProjectItems.AddFromFile($item.Properties.Item("FullPath").Value)

$myProject = $project.ProjectItems.Item("My Project")
$Properties = $project.ProjectItems.Item("Properties")

if ($project.CodeModel.Language -eq "{B5E9BD34-6D3E-4B5D-925E-8A43B79820B4}") 
{
	$myProject.Delete()
}
if ($project.CodeModel.Language -eq "{B5E9BD33-6D3E-4B5D-925E-8A43B79820B4}") 
{
	$Properties.Delete()

	$item = $project.ProjectItems | where-object {$_.Name -eq "Site.Mobile.Master"}
	$item.Delete()
	$item = $project.ProjectItems | where-object {$_.Name -eq "Site.Mobile.Master.cs"}
	$item.Delete()
	$item = $project.ProjectItems | where-object {$_.Name -eq "Site.Mobile.Master.designer.cs"}
	$item.Delete()

	$item = $project.ProjectItems | where-object {$_.Name -eq "ViewSwitcher.ascx"}
	$item.Delete()
	$item = $project.ProjectItems | where-object {$_.Name -eq "ViewSwitcher.ascx.cs"}
	$item.Delete()
	$item = $project.ProjectItems | where-object {$_.Name -eq "ViewSwitcher.ascx.designer.cs"}
	$item.Delete()

	$App_Start = $project.ProjectItems.Item("App_Start")
	$item = $App_Start.ProjectItems | where-object {$_.Name -eq "RouteConfig.cs"}
	$item.Delete()
}

$project.Save()
