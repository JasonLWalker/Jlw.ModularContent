param([string]$packageName, [string]$buildType = "Release")

# Set the Current directory path into the $workingDir variable
$workingDir=(Get-Item -Path ".\").FullName

if (-Not ($packageName)){
	# Set the Current directory name into the $packageName variable
	$packageName=(Get-Item -Path ".\").Name + ".Tests"
}

# Install dependencies
dotnet restore

# test package
dotnet test --no-build --configuration $buildType --verbosity normal "${packageName}"
