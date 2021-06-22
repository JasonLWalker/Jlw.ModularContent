param([string]$packageName, [string]$buildType = "Release", [string]$versionSuffix = "", [string]$versionPrefix = "")

# Set the Current directory path into the $workingDir variable
$workingDir=(Get-Item -Path ".\").FullName

if (-Not ($packageName)){
	# Set the Current directory name into the $packageName variable
	$packageName=(Get-Item -Path ".\").Name
}

if (-Not ($versionPrefix)){
	$versionPrefix="1.1.$([System.TimeSpan]::FromTicks($([System.DateTime]::UtcNow.Ticks)).Subtract($([System.TimeSpan]::FromTicks(630822816000000000))).TotalDays.ToString().SubString(0,9))"
}



# Install dependencies
#dotnet restore

#dotnet pack Jlw.Data.LocalizedContent -p:VersionPrefix=$versionPrefix --version-suffix=$versionSuffix --configuration $buildType

#dotnet build Jlw.Data.LocalizedContent.Tests -p:VersionPrefix=$versionPrefix --version-suffix=$versionSuffix --configuration $buildType

# Build with dotnet
dotnet build -p:VersionPrefix=$versionPrefix --version-suffix=$versionSuffix --configuration $buildType

# Pack Nuget package
#dotnet pack Jlw.Web.Rcl.LocalizedContent -p:VersionPrefix=$versionPrefix --version-suffix=$versionSuffix --configuration $buildType

#dotnet build Jlw.Web.Core31.LocalizedContent.SampleWebApp -p:VersionPrefix=$versionPrefix --version-suffix=$versionSuffix --configuration $buildType

#dotnet build Jlw.Web.Rcl.LocalizedContent -p:VersionPrefix=$versionPrefix --version-suffix=$versionSuffix --configuration $buildType
