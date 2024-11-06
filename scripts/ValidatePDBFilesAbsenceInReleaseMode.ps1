# Define the path to the VSIX file - after building the project in Release mode
$vsixPath = "JfrogVSExtension\bin\Release\JfrogVSExtension.vsix"
$pdbExists = $false
$fileName = ""
        
# Check if the .vsix file contains the PDB file
if (Test-Path $vsixPath) {
    $zipContent = [System.IO.Compression.ZipFile]::OpenRead($vsixPath)
    foreach ($entry in $zipContent.Entries) {
        if ($entry.FullName -like "*JfrogVSExtension.pdb") {
            $pdbExists = $true
            $fileName = $entry.FullName
            break
        }
    }
    $zipContent.Dispose()
} else {
    Write-Error "VSIX file does not exist in the following path: $vsixPath."
    exit 1 # Fail the workflow if the .vsix file is not found
}

if ($pdbExists) {
    Write-Error  "PDB file exists in VSIX. file name = $fileName Please check your project settings."
    exit 1  # Fail the workflow if the PDB file is found
} else {
    Write-Host "PDB file not found in VSIX. Build is clean."
}