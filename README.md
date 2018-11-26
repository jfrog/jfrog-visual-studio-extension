# Overview
JFrog Visual studio extension adds JFrog Xray scanning of NuGet project dependencies to your Visual Studio.

# Installing the Extension
1. Open Visual Studio
2. Open *Tools* --> *Extensions and Updates*

![alt](docs/images/getTools.png)

3. Search for JFrog Visual Studio Extension
4. Click on *Download*
5. Once the installation is completed, re-open Visual Studio.

# Building the Sources

To build the plugin sources, please follow these steps:
1. Clone the code from git.
2. Open Visual Studio.
3. Open *Tools* --> *Get Tools and Features*

![alt](docs/images/getTools.png)

4. Select the *workloads* tab and scroll to the bottem for the *Other Toolsets* section. Install *Visual Studio extension development*. Read more about Visual Studio SDK [here](https://docs.microsoft.com/en-us/visualstudio/extensibility/installing-the-visual-studio-sdk?view=vs-2017).

![alt](docs/images/extension.png)

5. Once the installation is completed, re-open Visual Studio.
6. Click on *File* --> *Open* --> *Project/Solution* and navigate to the project root dir and select the sln file.
7. To build the project, click on *Build* tab --> *Build Solution*. The VSIX file will be created in the following location: **$PROJECT_LOCATION\bin\Release\JFrog.VSExtension.vsix**

![alt](docs/images/build.png)

8. If you'd like to help us develop and enhance the extension, this step is for you.
   To build and run the plugin following your code changes, click on *Debug* --> *Start Debugging*.
                           
![alt](docs/images/debug.png)

# Troublshooting Issues
When openning the project in Visual Studio for the first time, the following error may appear : *"Fody.WeavingTask" task was not given a value for the required parameter "SolutionDir"*.

To fix this,close the solution and open it again. More information can be found [here](https://stackoverflow.com/questions/50225374/xamarinissues-with-fody-weavingtask-and-solutiondir)

# Code Contributions
We welcome community contribution through pull requests.
