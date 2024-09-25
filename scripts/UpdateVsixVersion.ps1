$newVersion = $env:NEW_VERSION
$manifestFileLocation = "JfrogVSExtension/source.extension.vsixmanifest"

$newVersion = $newVersion.Substring(1) # Extract version from the tag (e.g., 'v1.0.0' -> '1.0.0')
[xml]$manifest = Get-Content -Path $manifestFileLocation
$manifest.PackageManifest.Metadata.Identity.Version = $newVersion
$manifest.Save($manifestFileLocation)