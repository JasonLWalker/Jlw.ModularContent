param([string]$packageName, [string]$buildType = "Release", [string]$versionSuffix = "")

# Set the Current directory path into the $workingDir variable
$workingDir=(Get-Item -Path ".\").FullName

if (-Not ($packageName)){
	# Set the Current directory name into the $packageName variable
	$packageName=(Get-Item -Path ".\").Name
}

# Install dependencies
dotnet restore

# Build with dotnet
dotnet build --version-suffix=$versionSuffix --configuration $buildType --no-restore

# Pack Nuget package
dotnet pack --version-suffix=$versionSuffix --configuration $buildType
