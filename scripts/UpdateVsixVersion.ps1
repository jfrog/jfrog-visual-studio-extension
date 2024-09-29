$newVersion = $env:NEW_VERSION
$manifestFileLocation = "JfrogVSExtension/source.extension.vsixmanifest"

# Validate the NEW_VERSION format: "vs22-x.x.x"
if ($newVersion -match "^vs22-(\d+\.\d+\.\d+)$") {
    # Extract version from the tag (e.g., 'vs22-1.0.0' -> '1.0.0')
    $newVersion = $matches[1] 
} else {
     Write-Error "Error: NEW_VERSION must be in the format 'vs22-x.x.x' where x is an integer."
    exit 1
}

# Load the manifest file and update the version
[xml]$manifest = Get-Content -Path $manifestFileLocation
$manifest.PackageManifest.Metadata.Identity.Version = $newVersion
$manifest.Save($manifestFileLocation)