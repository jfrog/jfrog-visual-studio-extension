# Check if the JFROG_CLI_VERSION environment variable is set, if not using latest version
if ($env:JFROG_CLI_VERSION) {
    Write-Output "Downloading Jfrog CLI version: $env:JFROG_CLI_VERSION"
} else {
    Write-Error "Error: JFROG_CLI_VERSION environment variable is not set."
    exit 1
}

# if root was not defined - use current location
if (-not $env:PROJECT_ROOT) {
    $env:PROJECT_ROOT="$(pwd)"
}

# Define the URL for the JFrog CLI executable
$jfrogCliUrl = "https://releases.jfrog.io/artifactory/jfrog-cli/v2-jf/$($env:JFROG_CLI_VERSION)/jfrog-cli-windows-amd64/jf.exe"

# Define the destination path for the downloaded file
$destinationPath = Join-Path $env:PROJECT_ROOT "JFrogVSExtension/Resources/jfrog.exe"

# Download the JFrog CLI executable
Invoke-WebRequest -Uri $jfrogCliUrl -OutFile $destinationPath -Verbose


# Verify the file was downloaded successfully 
if (Test-Path -Path $destinationPath) {
    Write-Output "JFrog CLI v$env:JFROG_CLI_VERSION successfully downloaded to: $destinationPath"
    $downloadedVersion = & $destinationPath --version

    if ($downloadedVersion -eq "jf version $env:JFROG_CLI_VERSION"){
        Write-Output "Successfully downloaded Jfrog CLI version $env:JFROG_CLI_VERSION."
    } else {
        Write-Error "Version does not match the environment variable. Expected: $env:JFROG_CLI_VERSION, Got: $downloadedVersion"
        exit 1
    }

} else {
    Write-Error "Failed to download JFrog CLI to: $destinationPath"
}
