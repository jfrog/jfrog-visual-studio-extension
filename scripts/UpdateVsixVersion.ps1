$newVersion = $env:NEW_VERSION
$manifestFileLocation = Join-Path (Get-Location).Path $env:MANIFEST_FILE_LOCATION

# Validate the NEW_VERSION format: "vs22-x.x.x"
if ($newVersion -match "^vs22-(\d+\.\d+\.\d+)$") {
    # Extract version from the tag (e.g., 'vs22-1.0.0' -> '1.0.0')
    $newVersion = $matches[1] 
} else {
     Write-Error "Error: NEW_VERSION must be in the format 'vs22-x.x.x' where x is an integer."
    exit 1
}

if (-not $env:MANIFEST_FILE_LOCATION){
    Write-Error "Error: MANIFEST_FILE_LOCATION environment variable is not set."
    exit 1
} elseif (-not (Test-Path $env:MANIFEST_FILE_LOCATION)){
    Write-Error "The file specified in MANIFEST_FILE_LOCATION does not exist."
    exit 1
}

# Load the manifest file and update the version
[xml]$manifest = Get-Content -Path $manifestFileLocation
$manifest.PackageManifest.Metadata.Identity.Version = $newVersion
$manifest.Save($manifestFileLocation)