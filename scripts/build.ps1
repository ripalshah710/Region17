. .\BuildFunctions.ps1
$startTime = 
$projectName = "Demo"
$base_dir = resolve-path .\
$source_dir = "$base_dir\src"
$unitTestProjectPath = "$source_dir\UnitTests"
$integrationTestProjectPath = "$source_dir\IntegrationTests"
$uiProjectPath = "$source_dir\escWorks_PublishedWebsites\BaseLibraryWeb"
$projectConfig = $env:BuildConfiguration
$version = $env:Version
$verbosity = "m"

$build_dir = "$base_dir\build"
$test_dir = "$build_dir\test"

 
if ([string]::IsNullOrEmpty($version)) { $version = "9.9.9"}
if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
 
Function Pack{
	Write-Output "Packaging nuget packages"
	exec{
		& .\tools\octopack\Octo.exe pack --id "$projectName.UI" --version $version --basePath $uiProjectPath --outFolder $build_dir
	}
}

Function PrivateBuild{
	$sw = [Diagnostics.Stopwatch]::StartNew()
	Init
	Compile
	$sw.Stop()
	write-host "Build time: " $sw.Elapsed.ToString()
}

Function CIBuild{
	Pack
}
