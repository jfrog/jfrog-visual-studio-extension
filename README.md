# JFrog Visual Studio Extension

|          Target          |                                                                           Status                                                                            |                                                                                  Installs                                                                                   |
|:------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|    Visual Studio 2022    | [![Visual Studio 2022](https://vsmarketplacebadge.apphb.com/version/JFrog.JFrogV2.svg)](https://marketplace.visualstudio.com/items?itemName=JFrog.JFrogV2)  | ![Artifactory Extension Marketplace Installs](https://img.shields.io/visual-studio-marketplace/i/JFrog.JFrogV2?label=marketplace%20installs&color=blue&style=for-the-badge) |
| Visual Studio 2017, 2019 | [![Visual Studio 2017,2019](https://vsmarketplacebadge.apphb.com/version/JFrog.JFrog.svg)](https://marketplace.visualstudio.com/items?itemName=JFrog.JFrog) |           ![Visual Studio 2017,2019](https://img.shields.io/visual-studio-marketplace/i/JFrog.JFrog?label=marketplace%20installs&color=blue&style=for-the-badge)            |


# Table of Contents

- [About this Extension](#about-this-extension)
  - [Component Tree Icons](#component-tree-icons)
- [Installing the Extension](#installing-the-extension)
- [Building the Sources](#building-the-sources)
- [Troublshooting Issues](#troublshooting-issues)
- [Release Notes](#release-notes)
- [Code Contributions](#code-contributions)

## About this Extension
JFrog Visual studio extension adds JFrog Xray scanning of NuGet project dependencies to your Visual Studio.
To learn how to use the extension, please visit the [JFrog Visual Studio Extension User Guide](https://www.jfrog.com/confluence/display/XRAY/IDE+Integration#IDEIntegration-JFrogVisualStudioExtension).

### Component Tree Icons
The icon demonstrates the top severity issue of a selected component and its transitive dependencies. The following table describes the severities from lowest to highest:
|                 Icon                | Severity |                                       Description                                      |
|:-----------------------------------:|:--------:|:---------------------------------------------------------------------------------------|
|   ![Normal](JFrogVSExtension/Resources/normal.png)   |  Normal  | Scanned - No Issues                                                                    |
|  ![Unknown](JFrogVSExtension/Resources/unknown.png)  |  Unknown | No CVEs attached to the vulnerability or the selected component not identified in Xray |
|      ![Low](JFrogVSExtension/Resources/low.png)      |    Low   | Top issue with low severity                                                            |
|   ![Medium](JFrogVSExtension/Resources/medium.png)   |  Medium  | Top issue with medium severity                                                         |
|     ![High](JFrogVSExtension/Resources/high.png)     |   High   | Top issue with high severity                                                           |
| ![Critical](JFrogVSExtension/Resources/critical.png) | Critical | Top issue with critical severity  

## Installing the Extension
1. Make sure nuget.exe exists under your PATH environment variable
2. Open Visual Studio
3. Open *Tools* --> *Extensions and Updates*

![alt](docs/images/getTools.png)

4. Search for JFrog Visual Studio Extension
5. Click on *Download*
6. Once the installation is completed, re-open Visual Studio.

## Release Notes
The release notes are available [here](https://github.com/jfrog/jfrog-visual-studio-extension/releases).

## Code Contributions
We welcome community contribution through pull requests.
