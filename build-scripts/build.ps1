param([string]$packageName, [string]$buildType = "Release", [string]$versionSuffix = "")

# Set the Current directory path into the $workingDir variable
$workingDir=(Get-Item -Path ".\").FullName

if (-Not ($packageName)){
	# Set the Current directory name into the $packageName variable
	$packageName=(Get-Item -Path ".\").Name
}

# Install dependencies
#dotnet restore

dotnet build Jlw.Data.LocalizedContent --version-suffix=$versionSuffix --configuration $buildType

#dotnet build Jlw.Data.LocalizedContent.Tests --version-suffix=$versionSuffix --configuration $buildType

# Build with dotnet
# dotnet build --version-suffix=$versionSuffix --configuration $buildType

# Pack Nuget package
dotnet build Jlw.Web.Rcl.LocalizedContent --version-suffix=$versionSuffix --configuration $buildType

#dotnet build Jlw.Web.Core31.LocalizedContent.SampleWebApp --version-suffix=$versionSuffix --configuration $buildType

dotnet build --version-suffix=$versionSuffix --configuration $buildType
