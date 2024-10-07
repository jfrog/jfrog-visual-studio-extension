using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System;

namespace UnitTestJfrogVSExtension
{
    [TestClass]
    public class PowerShellScriptTests
    {
        public TestContext TestContext { get; set; }

        // use relative path (from base dir) in order to be able to run in other environments
        public static string rootDir = GetProjectRoot(AppDomain.CurrentDomain.BaseDirectory);
        public static string updateVersionScriptPath = Path.Combine(rootDir, @"scripts\UpdateVsixVersion.ps1");
        public static string downloadCliScriptPath = Path.Combine(rootDir, @"scripts\DownloadJfrogCli.ps1");
        public static string vsixManifestMockPath = Path.Combine(rootDir, @"scripts\vsixmanifestMock");

        [TestMethod]
        public void Test_UpdateVsixVersion_ValidVersion()
        {
            var envVars = new Dictionary<string, string>
            {
                { "NEW_VERSION", "vs22-1.2.3" }, // Valid version
                { "MANIFEST_FILE_LOCATION", vsixManifestMockPath},
            };

            // script should succeed and return exit code 0
            int exitCode = RunPowerShellScript(updateVersionScriptPath, envVars);
            Assert.AreEqual(0, exitCode);
        }

        [TestMethod]
        public void Test_UpdateVsixVersion_InvalidVersion()
        {
            var envVars = new Dictionary<string, string>
            {
                { "NEW_VERSION", "vs-1.5.56" }, // Invalid version
                { "MANIFEST_FILE_LOCATION", vsixManifestMockPath},
            };

            // script should fail and return exit code 1
            int exitCode = RunPowerShellScript(updateVersionScriptPath, envVars);
            Assert.AreEqual(1, exitCode);
        }


        [TestMethod]
        public void Test_DownloadJfrogCli()
        {
            var envVars = new Dictionary<string, string>
            {
                { "JFROG_CLI_VERSION", "2.67.0" },
                {"PROJECT_ROOT", rootDir }
            };

            //  script should succeed and return exit code 0
            int exitCode = RunPowerShellScript(downloadCliScriptPath, envVars);
            Assert.AreEqual(0, exitCode);
        }

        private static string GetProjectRoot(string currentDir)
        {
            while (Directory.GetFiles(currentDir, "*.sln").Length == 0)
            {
                if (Directory.GetParent(currentDir) == null)
                {
                    Console.WriteLine("ERROR: Root directory not found.");
                } else {
                    currentDir = Directory.GetParent(currentDir).FullName;
                }
            }
            return currentDir;
        }

        private int RunPowerShellScript(string scriptPath, Dictionary<string, string> envVars)
        {
            // Create a new process to run the PowerShell script
            ProcessStartInfo processInfo = new ProcessStartInfo("powershell.exe", $"-ExecutionPolicy Bypass -File \"{scriptPath}\"")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Set environment variables
            foreach (var envVar in envVars)
            {
                processInfo.EnvironmentVariables[envVar.Key] = envVar.Value; 
            }

            // Run the script in a process
            using (Process process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
                process.WaitForExit();

                // read output and error
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // log output and error for debug
                Debug.WriteLine($"Output: {output}");
                Debug.WriteLine($"Error: {error}");

                return process.ExitCode;
            }

        }

    }
}
