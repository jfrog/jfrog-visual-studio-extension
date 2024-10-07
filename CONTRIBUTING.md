# Contribution Guide

## Guidelines
-   If the existing tests do not already cover your changes, please add tests.

## Building the Sources

To build the plugin sources, please follow these steps:
1. Clone the code from git.
2. Download the [JFrog CLI executable](https://jfrog.com/getcli/) for Windows and place it under **$PROJECT_LOCATION\JFrogVSExtension\Resources**.
3. Open Visual Studio.
4. Open *Tools* --> *Get Tools and Features*

![alt](docs/images/getTools.png)

5. Select the *workloads* tab and scroll to the bottom for the *Other Toolsets* section. Install *Visual Studio extension development*. Read more about Visual Studio SDK [here](https://docs.microsoft.com/en-us/visualstudio/extensibility/installing-the-visual-studio-sdk?view=vs-2017).

![alt](docs/images/extension.png)

6. Once the installation is completed, re-open Visual Studio.
7. Click on *File* --> *Open* --> *Project/Solution* and navigate to the project root dir and select the sln file.
8. To build the project, click on *Build* tab --> *Build Solution*. The VSIX file will be created in the following location: **$PROJECT_LOCATION\bin\Release\JFrog.VSExtension.vsix**
9. If the build fails, please refer to the *Troublshooting Issues* section.

![alt](docs/images/build.png)

10. If you'd like to help us develop and enhance the extension, this step is for you.
   To build and run the plugin following your code changes, click on *Debug* --> *Start Debugging*.
                           
![alt](docs/images/debug.png)

## Run the tests
After build has finished successfuly, you can run the tests using:
1. Visual Studio Test Explorer - click on *Test* -> *Run All Tests*.
2. Command line interface - navigate to the tests folder /UnitTestJfrogVSExtension/bin/{Release/Debug} and then run the following command:
```bash
dotnet test .\UnitTestJfrogVSExtension.dll
```

## Troublshooting Issues
When openning the project in Visual Studio for the first time, the following error may appear : *"Fody.WeavingTask" task was not given a value for the required parameter "SolutionDir"*.

To fix this,close the solution and open it again. More information can be found [here](https://stackoverflow.com/questions/50225374/xamarinissues-with-fody-weavingtask-and-solutiondir)
