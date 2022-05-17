<!-- $(
	## Add Poweshell template variables Here ##
) -->
# Jlw.LocalizedContent

## Pipeline Status

| Test | Alpha | Staging | Release |
|-----|-----|-----|-----|
| [![Build and Test](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-test.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-test.yml) | [![Build and Deploy - Alpha](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-alpha.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-alpha.yml) | [![Build and Deploy - RC](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-rc.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-rc.yml) |[![Build and Deploy](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy.yml) | 


# Data Repository
<!-- $( 
	$projectName = "Jlw.Data.LocalizedContent"
	$projectPath = "$($buildPath)**\$($projectName).csproj"
) -->

## Information / Requirements
$(Get-ProjectInfoTable $projectName $projectPath)

## Dependencies

$(Get-ProjectDependencyTable $projectPath)

# Razor Class Library
<!-- $(
	$projectName = "jlw.Web.Rcl.LocalizedContent"
	$projectPath = "$($buildPath)**\$($projectName).csproj"
	$libmanPath = "$($buildPath)\$($projectName)"
	$purposes = @{ 
		'datatables' = 'Display tables in user-friendly way'; 
		'twitter-bootstrap' = 'Responsive UI, layout, and design framework';
		'jquery' = 'Framework library used by other libraries for HTML, DOM, Event, and AJAX manipulation';
	}
) -->
## Information / Requirements

$(Get-ProjectInfoTable $projectName $projectPath)

## Back-End Dependencies

$(Get-ProjectDependencyTable $projectPath)

## Front-end Dependencies
$(Get-LibmanDependencyTable $libmanPath -purposes $purposes)


<!-- $( 
	$dbServer = "(localdb)SqlLocalDb-SampleApp"
	$dbName = "LocalizedContent"
) -->
# SQL Schema

$(Get-SqlSchemaTable "$dbServer" "$dbName" "SqlSchema/Table" $buildPath "Table")

$(Get-SqlSchemaTable "$dbServer" "$dbName" "SqlSchema/StoredProcedure" "$($buildPath)" -heading "Stored Procedure")

$(Get-SqlSchemaTable "$dbServer" "$dbName" "SqlSchema/View" "$($buildPath)" -heading "View")

