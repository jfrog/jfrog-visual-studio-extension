using JFrogVSExtension.Data;
using JFrogVSExtension.Logger;
using JFrogVSExtension.Xray;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace JFrogVSExtension.Utils
{
    class Util
    {
        public readonly static string PREFIX = "nuget://";
        // This method will load the json to a List of objects. 
        // The Json retrieved from the output itself
        public static Projects LosdNugetProjects(String output)
        {
            // Reading the file as stream and changing to list of items.
            // The items are configured in an other class
            // output = System.IO.File.ReadAllText( @"C:\alex\jvse-new\jvse\JFrogVSExtension\output.txt");
            Projects projects = JsonConvert.DeserializeObject<Projects>(output);
            return projects;
        }
        public static String GetCLIOutput(string solutionDir)
        {
            String strAppPath = GetAssemblyLocalPathFrom(typeof(MainPanelCommand));
            String strFilePath = Path.Combine(strAppPath, "Resources");
            String pathToCli = Path.Combine(strFilePath, "jfrog.exe");
            OutputLog.ShowMessage("Path for the JFrog CLI: " + pathToCli);
            //Create process
            Process pProcess = new System.Diagnostics.Process();

            // strCommand is path and file name of command to run
            pProcess.StartInfo.FileName = pathToCli;

            // strCommandParameters are parameters to pass to program
            // Here we will run the nuget command for the cli
            pProcess.StartInfo.Arguments = "rt nuget-deps-tree";

            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.CreateNoWindow = true;
            // Set output of program to be written to process output stream
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.RedirectStandardError = true;
            pProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pProcess.StartInfo.WorkingDirectory = solutionDir;

            StringBuilder strOutput = new StringBuilder();
            StringBuilder error = new StringBuilder();

            // Saving the response from the CLI to a StringBuilder.
            using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
            using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
            {
                // Get program output
                // The json returned from the CLI
                pProcess.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        outputWaitHandle.Set();
                    }
                    else
                    {
                        strOutput.AppendLine(e.Data);
                    }
                };
                pProcess.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        errorWaitHandle.Set();
                    }
                    else
                    {
                        error.AppendLine(e.Data);
                    }
                };

                // Start the process
                pProcess.Start();
                pProcess.BeginOutputReadLine();
                pProcess.BeginErrorReadLine();
                pProcess.WaitForExit();
                // To wait for the entire output to be written
                if (outputWaitHandle.WaitOne(2) &&
                       errorWaitHandle.WaitOne(2))
                {
                    // Process completed. Check process.ExitCode here.
                    if (pProcess.ExitCode != 0)
                    {
                        string message = "Failed to get CLI output. Exit code: " + pProcess.ExitCode + " Returned error:" + error.ToString();
                        throw new IOException(message);
                    }
                    if (!string.IsNullOrEmpty(error.ToString()))
                    {
                        OutputLog.ShowMessage(error.ToString());
                    }
                    // Returning the output from the CLI that is the json itself.
                    return strOutput.ToString();
                }
                else
                {
                    // Timed out.
                    OutputLog.ShowMessage("Process timeout");
                    throw new IOException("Process timeout, please run the following command from the solution directory and send us the output:" + pathToCli + " rt ndt");
                }
            }
        }

        private static string GetAssemblyLocalPathFrom(Type type)
        {
            string codebase = type.Assembly.CodeBase;
            var uri = new Uri(codebase, UriKind.Absolute);

            return Path.GetDirectoryName(uri.LocalPath);
        }

        public static Component ParseDependencies(Dependency dep, Dictionary<string, Artifact> artifactsMap, DataService dataService)
        {
            Component comp = new Component();
            if (artifactsMap.ContainsKey(dep.id))
            {
                List<String> projectDependencies = new List<string>();

                Artifact artifact = artifactsMap[dep.id];
                setComponentDetails(comp, artifact);

                Severity topSeverity;
                if (artifact.Issues != null && artifact.Issues.Count > 0)
                {
                    topSeverity = GetTopSeverityFromIssues(artifact.Issues);
                }
                else
                {
                    topSeverity = Severity.Normal;
                }

                if (dep.dependencies != null && dep.dependencies.Length > 0)
                {
                    foreach (Dependency dependency in dep.dependencies)
                    {
                        // Let's get the component information of the dependency. 
                        Component component = ParseDependencies(dependency, artifactsMap, dataService);
                        if (!dataService.Severities.Contains(component.TopSeverity))
                        {
                            continue;
                        }

                        topSeverity = GetTopSeverity(topSeverity, component.TopSeverity);
                        if (component.Issues != null && component.Issues.Count > 0 && comp.Issues != null && comp.Issues.Count > 0)
                        {
                            // Means that the component already has some issues. 
                            // Need to check that this is a new issue that we are adding.
                            foreach (Issue issue in component.Issues)
                            {
                                if (!comp.Issues.Contains(issue))
                                {
                                    comp.Issues.Add(issue);
                                }
                            }

                        }
                        else
                        {
                            comp.Issues.AddRange(component.Issues);
                        }
                        if (!dataService.getComponents().ContainsKey(component.Key))
                        {
                            dataService.getComponents().Add(component.Key, component);
                        }
                        projectDependencies.Add(dependency.id);
                    }
                }
                comp.Dependencies = projectDependencies;
                comp.TopSeverity = topSeverity;
            }
            return comp;
        }

        private static void setComponentDetails(Component comp, Artifact artifact)
        {
            if (artifact.Issues != null && artifact.Issues.Count > 0)
            {
                foreach (Issue issue in artifact.Issues)
                {
                    issue.Component = artifact.general.ComponentId;
                }
            }
            string name = artifact.general.ComponentId;
            string[] elements = name.Split(':');
            string version = "";
            if (elements.Length == 2)
            {
                name = elements[0];
                version = elements[1];
            }
            comp.Name = name;
            comp.Key = artifact.general.ComponentId;
            comp.Version = version;
            comp.Group = name;
            comp.Issues = artifact.Issues;
            comp.Licenses = artifact.licenses;
        }

        public static HashSet<Components> GetComponents(Dependency[] dependencies, HashSet<Components> componentsCache)
        {
            HashSet<Components> ids = new HashSet<Components>();
            foreach (Dependency dependency in dependencies)
            {
                Components comp = new Components()
                {
                    component_id = PREFIX + dependency.id
                };
                if (!componentsCache.Contains(comp))
                {
                    ids.Add(comp);
                    if (dependency.dependencies != null && dependency.dependencies.Length > 0)
                    {
                        HashSet<Components> internalIdS = GetComponents(dependency.dependencies, componentsCache);
                        ids.UnionWith(internalIdS);
                    }
                }

            }
            return ids;
        }
        public static Severity GetTopSeverity(Severity topSeverityComp, Severity topSeverityDep)
        {
            int compID = JFrogMonikerSelector.GetSeverityID(topSeverityComp);
            int compIDDep = JFrogMonikerSelector.GetSeverityID(topSeverityDep);

            if (compID <= compIDDep)
            {
                return topSeverityComp;
            }
            return topSeverityDep;
        }

        internal static Severity GetTopSeverityFromIssues(List<Issue> issues)
        {
            Severity topSeverity = Severity.Unknown;
            foreach (Issue issue in issues)
            {
                topSeverity = GetTopSeverity(topSeverity, issue.Severity);
            }
            return topSeverity;
        }
    }

    public class Artifacts
    {
        internal IEnumerable<Artifact> artifact;

        public List<Artifact> artifacts { get; set; } = new List<Artifact>();
    }
    public class NugetProject
    {
        public string name;
        public Dependency[] dependencies;
    }

    public class Dependency
    {
        public string id;
        public string sha1;
        public string md5;
        public Dependency[] dependencies;
    }
    public class Projects
    {
        public NugetProject[] projects;
    }

    public class Components
    {
        public string sha1 = "";
        public string component_id = "";

        public Components()
        {
        }
        public Components(String sha1, String component_id)
        {
            this.sha1 = sha1;
            this.component_id = component_id;
        }

        public override int GetHashCode()
        {
            return sha1.GetHashCode() + component_id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Components comp = new Components();
            if (obj is Components)
            {
                comp = (Components)obj;
            }
            else
            {
                return false;
            }

            if (this.sha1.Equals(comp.sha1) && this.component_id.Equals(comp.component_id))
            {
                return true;
            }
            return false;
        }
    }
}
