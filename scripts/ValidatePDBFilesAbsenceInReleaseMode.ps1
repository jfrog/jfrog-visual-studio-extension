# if vsix file path was not defined - use the default location in release folder
if (-not $env:VSIX_PATH) {
    $env:VSIX_PATH=Join-Path "$(pwd)" "JfrogVSExtension\bin\Release\JfrogVSExtension.vsix"
}

$pdbExists = $false
$fileName = ""
 
# Validate that the necessary assembly for unzipping the .vsix file exists
Add-Type -AssemblyName System.IO.Compression.FileSystem

# Check if the .vsix file contains the PDB file
if (Test-Path $env:VSIX_PATH) {
    $zipContent = [System.IO.Compression.ZipFile]::OpenRead($env:VSIX_PATH)
    foreach ($entry in $zipContent.Entries) {
        if ($entry.FullName -like "*JfrogVSExtension.pdb") {
            $pdbExists = $true
            $fileName = $entry.FullName
            break
        }
    }
    $zipContent.Dispose()
} else {
    Write-Error "VSIX file does not exist in the following path: $env:VSIX_PATH."
    exit 1 # Fail the workflow if the .vsix file is not found
}

if ($pdbExists) {
    Write-Error  "PDB file exists in VSIX. file name = $fileName Please check your project settings."
    exit 1  # Fail the workflow if the PDB file is found
} else {
    Write-Host "PDB file not found in VSIX. Release mode build is clean."
}