[CmdletBinding()]
param(
    [Parameter(Position = 0)][ValidateSet("AcceptanceTest", "ci", "CodeCoverage", "CommonAssemblyInfo", "Compile", "ConnectionString", "CreateCompareSchema", "default", "DropDatabaseAzure", "Init", "InjectConnectionString", "LoadData", "LocalDB", "RebuildDatabase", "RemoteDB", "Restore-Packages", "SchemaConnectionString", "Test", "UpdateDatabaseAzure")][string[]] $taskList = @('Default'),
    [string]$Script = "ci_build.ps1",
    [ValidateSet("Quiet", "Minimal", "Normal", "Verbose", "Diagnostic")][string]$Verbosity = "Normal",
    [string]$ToolsDirectory = "tools",
    [string]$Configuration = "Release",
    [ValidatePattern("\d+\.\d+\.\d+\.\d+")][string] $Version = "0.0.0.1",
    [ValidateSet("zip", "MsDeploy", "Nuget", "Octopack")][string] $Package,
    [string]$CCTXConnectionString,
    [string]$ITCConnectionString,
    [string]$NSBConnectionString,
    [string]$SourceDirectoryName = "src",
    [string]$ArtifactsDirectory,
    [string]$DatabaseAction = "rebuild",
    [string]$DatabaseScripts
)

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

[string[]] $tasks = Get-Content $Script | Sort-Object | Select-String -Pattern 'task (.*?) ' | ForEach-Object Matches | ForEach-Object { $_.Groups[1].Value } | Sort-Object
(Get-Content $PSCommandPath) -replace '(?<=ValidateSet\()(.*)(?=\)\]\[string\[\]\] \$taskList)', """$($tasks -join '", "')""" | Out-File $PSCommandPath -Encoding ascii
Write-Verbose "Task validation set (tab completion) has been updated in $($PSCommandPath)"

Set-StrictMode -Version Latest

$ErrorActionPreference = "Stop"

$psakeMaximumVersion = "4.9.0"
$vssetupMaximumVersion = "2.2.16"

if ($PSVersionTable.PSVersion.Major -lt 6) {
    Write-Host "Updating package provider"
    Install-PackageProvider NuGet -Scope CurrentUser -Force | Out-Null
}

#nuget provisioning
$sourceDirectory = Join-Path -Path $PSScriptRoot -ChildPath $SourceDirectoryName
$nugetDirectory = Join-Path -Path $sourceDirectory -ChildPath ".nuget"
$nugetPath = Join-Path $nugetDirectory "nuget.exe"
if ((Test-Path -Path $nugetPath) -eq $false) {
    Write-Host "Downloading nuget to $nugetDirectory"
    New-Item -ItemType Directory -Path $nugetDirectory -Force | Out-Null
    Invoke-WebRequest -UseBasicParsing -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nugetPath | Out-Null
}
else {
    if (((Get-ChildItem $nugetPath).LastWriteTime - [DateTime]::Today).Days -lt 0) {
        Write-Host "Nuget daily check" -foreground Yellow
        & $nugetPath update -self
    }
}

#vssetup provisioning (it doesn't like to be loaded directly from disk)
if (-not (Get-Module -ListAvailable -Name VSSetup)) {
    Write-Host "Installing module VSSetup"
    Find-Module -Name vssetup -MaximumVersion $vssetupMaximumVersion | Install-Module -Scope CurrentUser -Force
}
else {
    Write-Host "VSSetup already installed"
}

#psake provisioning
$psakePath = ".\$ToolsDirectory\psake\$psakeMaximumVersion\psake.psm1"
if ((Test-Path -Path $psakePath) -eq $false) {
    Write-Host "Psake module missing"
    Write-Host "Seaching for psake package and saving local copy"
    $module = Find-Module -Name psake -MaximumVersion $psakeMaximumVersion
    Write-Host "Psake module found. Saving local copy"
    $module | Save-Module -Path .\$ToolsDirectory\ -Force
}

# '[p]sake' is the same as 'psake' but is not polluted
Remove-Module [p]sake
Import-Module $psakePath -Scope Local

$parameters = @{
    projectConfig = $Configuration
    ArtifactsDirectory = $ArtifactsDirectory
    Version = $Version
    cctxConnectionString = $CCTXConnectionString
    itcConnectionString = $ITCConnectionString
    nsbConnectionString = $NSBConnectionString
}

$parameters.GetEnumerator() | ForEach-Object {
    Write-Verbose "$($_.Key):$($_.Value)"
}

Invoke-Psake -buildFile ".\ci_build.ps1" -taskList $taskList -parameters $parameters -Verbose:$VerbosePreference

if ($psake.build_success) { } else {pause;}
