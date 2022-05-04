[Diagnostics.CodeAnalysis.SuppressMessageAttribute("PSUseDeclaredVarsMoreThanAssignments", "", Justification = "Vars in properties block are actually used")]
param()
Set-StrictMode -Version latest
Framework "4.6"

properties {
    $projectName = "tx_r17"
    $base_dir = resolve-path .\
    $source_dir = "$base_dir\src"
    $project_dir = "$base_dir\src\$projectName"
    $unitTestAssembly = "UnitTests.dll"
    $integrationTestAssembly = "IntegrationTests.dll"
    $acceptanceTestAssembly = "escworks.AcceptanceTests.dll"
	$projectConfig = $env:Configuration
    $version = $env:BuildVersion
    $nunitPath = "$base_dir\tools\packages\NUnit.3.12.0\tools"
    $nugetExe = Join-Path -Path $source_dir -ChildPath ".nuget\nuget.exe"

    $build_dir = "$base_dir\build"
    $ci_build_dir = "$base_dir\ci_build"
    $test_dir = "$build_dir\test"
    $testCopyIgnorePath = "_ReSharper"
    $package_dir = "$build_dir\package"	
    $package_file = "$build_dir\latestVersion\" + $projectName + "_Package.zip"
    $runOctoPack = $env:RunOctoPack

    $output_dir="$base_dir\$projectName"
    $compile_dir="$build_dir"+ "_PublishedWebsites"
    $ci_compile_dir="$ci_build_dir"+ "_PublishedWebsites"

    $databaseName = $env:DatabaseName
    if ([string]::IsNullOrEmpty($databaseName)) { $databaseName = "escworks"}
    $databaseServer = $env:DatabaseServer
    if ([string]::IsNullOrEmpty($databaseServer)) { $databaseServer = "localhost"}
    $databaseUser = $env:DatabaseUser
    $databasePassword = $env:DatabasePassword
    $databaseScripts = "$source_dir\Database\scripts"
    $efConfig = "$source_dir\ConnectionStrings.config"
    $schemaDatabaseName = $databaseName + "_schema"
    $integratedSecurity = "Integrated Security=true"
    $connection_string = "server=$databaseserver;database=$databasename;$databaseUser;"
    $AliaSql = "$source_dir\Database\scripts\AliaSql.exe"
    $databaseAction = $env:DatabaseAction
    if ([string]::IsNullOrEmpty($databaseAction)) { $databaseAction = "Rebuild"}
    $webapp_dir = "$source_dir\UI"
    $vs2017_dir = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional";
    $vstest_dir = "$vs2017_dir\Common7\IDE\CommonExtensions\Microsoft\TestWindow"
    $testresults_dir = "$base_dir\TestResults"

    $vssetupMaximumVersion = "2.2.16"

    if ([string]::IsNullOrEmpty($version)) { $version = "1.0.0"}
    if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
    if ([string]::IsNullOrEmpty($runOctoPack)) {$runOctoPack = "true"}
    Write-Host("db password is $databasePassword")
}

task default -depends Init, ConnectionString, Compile, Test
task ci -depends Init, CommonAssemblyInfo, ConnectionString, Compile, LocalDB, Test, CodeCoverage

task Init {

    Write-Host("ci_build")
    Write-Host("##[section]Starting: Build task 'Init'")
    delete_file $package_file

    Write-Host "deleting $build_dir"
    rd $build_dir -recurse -force  -ErrorAction Ignore

    Write-Host "deleting $testresults_dir"
    rd $testresults_dir -recurse -force -ErrorAction Ignore

    Write-Host "deleting $compile_dir"
    rd $compile_dir -recurse -force -ErrorAction Ignore

    Write-Host "deleting $ci_compile_dir"
    rd $ci_compile_dir -recurse -force -ErrorAction Ignore

    Write-Host "creating $test_dir"
    create_directory $test_dir

    Write-Host "creating $build_dir"
    create_directory $build_dir


    Write-Host $project_dir
    Write-Host $compile_dir

    Write-Host $package_file

    Write-Host $projectConfig
    Write-Host $version
    Write-Host $runOctoPack

    Write-Host $databaseServer
    Write-Host $databaseName

    Write-Host("##[section]Finishing: Build task 'Init'")
}

task ConnectionString {
    Write-Host("##[section]Starting: Build task 'ConnectionString'")
    $connection_string = "server=$databaseserver;database=$databasename;$integratedSecurity;"
    write-host "Using connection string: $connection_string"
    if ( Test-Path "$efConfig" ) {
        poke-xml $efConfig "//add[@name='escworks']/@connectionString" $connection_string
    }
    Write-Host("##[section]Finishing: Build task 'ConnectionString'")
}

task InjectConnectionString {
    Write-Host("##[section]Starting: Build task 'InjectConnectionString'")
    $injectedConnectionString = "Server=tcp:$databaseServer,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$databaseUser;Password=$databasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    $connection_string = $injectedConnectionString
    write-host "Using connection string to : $databaseServer"
    if ( Test-Path "$efConfig" ) {
        poke-xml $efConfig "//add[@name='escworks']/@connectionString" $connection_string
    }
    Write-Host("##[section]Finishing: Build task 'InjectConnectionString'")
}

task Restore-Packages {

    exec {
        & $nugetExe restore $source_dir\$projectName.sln
    }
}

task Compile -depends Init, Restore-Packages {
    Write-Host("##[section]Starting: Build task 'Compile'")
    exec {
        & msbuild /t:Clean`;Rebuild /v:m /maxcpucount:1 /nologo /p:RunCodeAnalysis=false /p:ActiveRulesets=MinimumRecommendedRules.ruleset /p:Configuration=$projectConfig /p:OctoPackPackageVersion=$version /p:RunOctoPack=$runOctoPack /p:OctoPackEnforceAddingFiles=true /p:PrecompileBeforePublish=true /p:UseMerge=true /p:OutDir=$build_dir $source_dir\$projectName.sln
    }

   # Write-Host("Second build")

   # exec {
       # & msbuild /p:DeployOnBuild=true /p:Configuration=Release /p:EnableUpdateable=false /p:PrecompileBeforePublish=true /p:UseMerge=true /p:OutDir=$ci_build_dir $source_dir\$projectName.sln
   # }

    Write-Host("##[section]Starting source_dir: $source_dir'")
    Copy_and_flatten $source_dir *.nupkg $build_dir
    Write-Host("##[section]Finishing: Build task 'Compile'")
}

task Test -depends Compile {
    Write-Host("##[section]Starting: Build task 'Test'")
    
    Write-Host("copy_all_assemblies_for_test")
    copy_all_assemblies_for_test $test_dir
    Write-Host("$nunitPath\nunit3-console.exe")
    Write-Host("$test_dir\$unitTestAssembly")
    #Write-Host("$test_dir\$integrationTestAssembly")
    exec {
        
        #& $nunitPath\nunit3-console.exe $test_dir\$unitTestAssembly $test_dir\$integrationTestAssembly --workers=1 --noheader --result="$build_dir\TestResult.xml"`;format=nunit3
        & $nunitPath\nunit3-console.exe $test_dir\$unitTestAssembly --workers=1 --noheader --result="$build_dir\TestResult.xml"`;format=nunit3
    }

        exec {
        #Integraton tests removed (complicating unit testing).  Original string is:
        #& $nunitPath\nunit3-console.exe $test_dir\$unitTestAssembly $test_dir\$integrationTestAssembly --workers=1 --noheader --result="$build_dir\TestResult.xml"`;format=nunit3
        & $nunitPath\nunit3-console.exe $test_dir\$integrationTestAssembly --workers=1 --noheader --result="$build_dir\IntegrationTestAssembly.xml"`;format=nunit3
    }

    Write-Host ("Path to results: $build_dir\TestResult.xml")
    Write-Host("##[section]Finishing: Build task 'Test'")
}

task AcceptanceTest {
	Write-Host("##[section]Starting: Build task 'AcceptanceTest'")
    copy_all_assemblies_for_test $test_dir
	exec {
        & $nunitPath\nunit3-console.exe $test_dir\$acceptanceTestAssembly --workers=1 --noheader --result="$build_dir\AcceptanceTestResult.xml"`;format=nunit3 --out="$build_dir\AcceptanceTestResult.txt"
    }
    Write-Host ("Path to results: $build_dir\AcceptanceTestResult.xml")
	Write-Host("##[section]Finishing: Build task 'AcceptanceTest'")
}

task RebuildDatabase -depends ConnectionString {
    Write-Host("##[section]Starting: Build task 'RebuildDatabase'")
    exec {
        & $AliaSql Rebuild $databaseServer $databaseName $databaseScripts
    }
    Write-Host("##[section]Finishing: Build task 'RebuildDatabase'")
}

task UpdateDatabaseAzure -depends InjectConnectionString {
    Write-Host("##[section]Starting: Build task 'UpdateDatabaseAzure'")
    Write-Host "the server is $databaseServer"
    exec {
        & $AliaSql Update $databaseServer $databaseName $databaseScripts $databaseUser $databasePassword
    }
    Write-Host("##[section]Finishing: Build task 'UpdateDatabaseAzure'")
}

task DropDatabaseAzure -depends InjectConnectionString {
    Write-Host("##[section]Starting: Build task 'DropDatabaseAzure'")
    Write-Host "the server is $databaseServer"
    Write-Host "$AliaSql Drop $databaseServer $databaseName $databaseScripts $databaseUser $databasePassword"
    exec {
        & $AliaSql Drop $databaseServer $databaseName $databaseScripts $databaseUser $databasePassword
    }
    Write-Host("##[section]Finishing: Build task 'DropDatabaseAzure'")
}

task LoadData -depends ConnectionString, Compile, RebuildDatabase {
    exec { 
        & $nunitPath\nunit3-console.exe $test_dir\$integrationTestAssembly --where "cat == DataLoader" --noheader --result="$build_dir\DataLoadResult.xml"`;format=nunit2
    } "Build failed - data load failure"  
}

task CreateCompareSchema -depends SchemaConnectionString {
    exec {
        & $AliaSql Rebuild $databaseServer $schemaDatabaseName $databaseScripts
    }
}

task SchemaConnectionString {
    $connection_string = "server=$databaseserver;database=$schemaDatabaseName;@integratedSecurity;"
    write-host "Using connection string: $connection_string"
    #if ( Test-Path "$efConfig" ) {
    #    poke-xml $efConfig "//add[@name='escworks']/@connectionString" $connection_string null
    #}
}



task CommonAssemblyInfo {   
    Write-Host("##[section]Starting: Build task 'CommonAssemblyInfo'")
    create-commonAssemblyInfo "$version" $projectName "$source_dir\CommonAssemblyInfo.cs"
    Write-Host("##[section]Finishing: Build task 'CommonAssemblyInfo'")
}

task CodeCoverage {
    Write-Host("##[section]Starting: Build task 'CodeCoverage'")
    copy_all_assemblies_for_test $test_dir
    exec {
        & $vstest_dir\vstest.console.exe $test_dir\$unitTestAssembly $test_dir\$integrationTestAssembly /TestAdapterPath:$test_dir /Logger:trx /Enablecodecoverage /Settings:$source_dir\CodeCoverage.runSettings
    }
    Write-Host("##[section]Finishing: Build task 'CodeCoverage'")
}
 
 task LocalDB {
	ApplyLocalDb
}

task RemoteDB {
	ApplyRemoteDb
}

Function ApplyLocalDb {
    Write-Host "Applying database: $databaseName"
    MigrateDatabaseLocal
}
Function ApplyRemoteDb {
    Write-Host "Applying database: $databaseName"
    MigrateDatabaseRemote
}

Function DropLocalDb {
    Write-Host "Dropping database: $databaseName"
    SqlCmd -S "$databaseServer" -Q "ALTER DATABASE [$databaseName] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ; DROP DATABASE [$databaseName] ;"
}

Function BuildDatabaseArtifact($databaseServer, $dbName) {
    $validatedProject = Invoke-DatabaseBuild "$base_dir\src\Database\Database.csproj" -TemporaryDatabase $databaseServer
    $buildArtifact = New-DatabaseBuildArtifact $validatedProject -PackageId "$dbname.Database" -PackageVersion $version
    Export-DatabaseBuildArtifact $buildArtifact -Path $build_dir -Force # "-Force" because msbuild creates an incomplete version of this file with the same name and without the nuspec file that causes that, msbuild crashes.
    return $buildArtifact
}

Function ApplyLocalDatabase($dbName) {
    $targetDb = New-DatabaseConnection -ServerInstance "$databaseServer" -Database $dbName
    $buildArtifact = BuildDatabaseArtifact $targetDb $dbName

    $dbRelease = New-DatabaseReleaseArtifact -Source $buildArtifact -Target $targetDb
    Use-DatabaseReleaseArtifact $dbRelease -DeployTo $targetDb
}

Function global:zip_directory($directory, $file) {
    write-host "Zipping folder: " $test_assembly
    write-host "Zipping directory: " $directory
    write-host "Zipping file: " $file
    write-host "Base: " $base_dir
	
	
    delete_file $file
    cd $directory
    & "$base_dir\tools\7zip\7z.exe" a -mx=9 -r $file
    cd $base_dir
}
Function global:copy_website_files($source, $destination) {
    $exclude = @('*.user', '*.dtd', '*.tt', '*.cs', '*.csproj', '*.orig', '*.log') 
    copy_files $source $destination $exclude
    delete_directory "$destination\obj"
}

Function global:copy_files($source, $destination, $exclude = @()) {    
    create_directory $destination
    Get-ChildItem $source -Recurse -Exclude $exclude | Copy-Item -Destination {Join-Path $destination $_.FullName.Substring($source.length)} 
}

Function global:Copy_and_flatten ($source, $filter, $dest) {
    ls $source -filter $filter  -r | Where-Object {!$_.FullName.Contains("$testCopyIgnorePath") -and !$_.FullName.Contains("packages") }| cp -dest $dest -force
}

Function global:copy_all_assemblies_for_test($destination) {
    create_directory $destination
    Copy_and_flatten $source_dir *.exe $destination
    Copy_and_flatten $source_dir *.dll $destination
    Copy_and_flatten $source_dir *.config $destination
    Copy_and_flatten $source_dir *.xml $destination
    Copy_and_flatten $source_dir *.pdb $destination
    Copy_and_flatten $source_dir *.sql $destination
    Copy_and_flatten $source_dir *.xlsx $destination
}

Function global:delete_file($file) {
    if ($file) { remove-item $file -force -ErrorAction SilentlyContinue | out-null } 
}

Function global:delete_directory($directory_name) {
    rd $directory_name -recurse -force  -ErrorAction SilentlyContinue | out-null
}

Function global:delete_files_in_dir($dir) {
    get-childitem $dir -recurse | foreach ($_) {remove-item $_.fullname}
}

Function global:create_directory($directory_name) {
    mkdir $directory_name  -ErrorAction SilentlyContinue  | out-null
}

Function global:create-commonAssemblyInfo($version, $applicationName, $filename) {
    "using System;
using System.Reflection;
using System.Runtime.InteropServices;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyVersionAttribute(""$version"")]
[assembly: AssemblyFileVersionAttribute(""$version"")]
[assembly: AssemblyCopyrightAttribute(""Copyright 2017"")]
[assembly: AssemblyProductAttribute(""$applicationName"")]
[assembly: AssemblyCompanyAttribute(""Clear Measure, Inc."")]
[assembly: AssemblyConfigurationAttribute(""$projectConfig"")]
[assembly: AssemblyInformationalVersionAttribute(""$version"")]"  | out-file $filename -encoding "ASCII"    
}

Function script:poke-xml($filePath, $xpath, $value) {
    [xml] $fileXml = Get-Content $filePath
    $node = $fileXml.SelectSingleNode($xpath)
    
    Assert ($node -ne $null) "could not find node @ $xpath"
        
    if ($node.NodeType -eq "Element") {
        $node.InnerText = $value
    }
    else {
        $node.Value = $value
    }

    $fileXml.Save($filePath) 
} 

Function global:MigrateDatabaseLocal {
	exec{
		& $aliaSql $databaseAction $databaseServer $databaseName $databaseScripts
	}
}
Function global:MigrateDatabaseRemote{
	$appConfig = "$integrationTestProjectPath\app.config"
    $injectedConnectionString = "Server=tcp:$databaseServer,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=$env:DatabaseUser;Password=$env:DatabasePassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
	write-host "Using connection string for db: $injectedConnectionString"
    if ( Test-Path "$appConfig" ) {
        poke-xml $appConfig "//add[@key='ConnectionString']/@value" $injectedConnectionString
    }
	exec {
		& $aliaSql $databaseAction $databaseServer $databaseName $databaseScripts $env:DatabaseUser $env:DatabasePassword
	}
}