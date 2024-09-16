# Check if the JFROG_CLI_VERSION environment variable is set, if not using latest version
if (-not $env:JFROG_CLI_VERSION) {
    Write-Output "Environment variable 'JFROG_CLI_VERSION' is not set. Using latest version as default."
    $env:JFROG_CLI_VERSION = "[RELEASE]"
}

# Define the URL for the JFrog CLI executable
$jfrogCliUrl = "https://releases.jfrog.io/artifactory/jfrog-cli/v2-jf/$($env:JFROG_CLI_VERSION)/jfrog-cli-windows-amd64/jf.exe"

# Define the destination path for the downloaded file
$destinationPath = Join-Path (Get-Location).Path "JFrogVSExtension/Resources/jfrog.exe"

# Download the JFrog CLI executable 
Invoke-WebRequest -Uri $jfrogCliUrl -OutFile $destinationPath -Verbose


# Verify the file was downloaded successfully 
if (Test-Path -Path $destinationPath) {
    Write-Output "JFrog CLI v$env:JFROG_CLI_VERSION successfully downloaded to: $destinationPath"
} else {
    Write-Output "Failed to download JFrog CLI to: $destinationPath"
}
