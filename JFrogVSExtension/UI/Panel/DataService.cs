using JFrogVSExtension.HttpClient;
using JFrogVSExtension.Logger;
using JFrogVSExtension.Utils;
using JFrogVSExtension.Xray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFrogVSExtension.Data
{
    public class DataService
    {
        private static DataService instance;
        private Dictionary<string, Component> components;
        private HashSet<Components> componentsCache;
        private Artifacts artifacts = new Artifacts();
        public List<string> RootElements { get; private set; }
        public HashSet<Severity> Severities { get; set; }
        private DataService()
        {
            InitializeComponent();
        }
        public static DataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataService();
                }
                return instance;
            }
        }

        public HashSet<Components> GetComponentsCache()
        {
            return componentsCache;
        }

        public Artifacts GetArtifacts()
        {
            return artifacts;
        }

        public void populateRootElements(Projects projects)
        {
            List<String> names = new List<String>();
            foreach (NugetProject project in projects.projects)
            {
                List<String> projectDependencies = new List<string>();
                Component comp = new Component()
                {
                    Key = project.name,
                    Group = project.name,
                    Issues = new List<Issue>()
                };
                Severity topSeverity = Severity.Unknown;
                if (project.dependencies != null && project.dependencies.Length > 0)
                {
                    foreach (Dependency dep in project.dependencies)
                    {
                        Component depComponent = getComponent(dep);
                        if (Severities.Contains(depComponent.TopSeverity))
                        {
                            projectDependencies.Add(depComponent.Key);
                        } 
                        topSeverity = getTopComponentSeverity(topSeverity, depComponent);
                        foreach (Issue issue in depComponent.Issues)
                        {
                            if (!comp.Issues.Contains(issue))
                            {
                                comp.Issues.Add(issue);
                            }
                        }
                    }
                }

                comp.TopSeverity = topSeverity;
                comp.Dependencies = projectDependencies;
                names.Add(project.name);
                // Adding to the data service components the project itself
                if (!getComponents().ContainsKey(comp.Key))
                {
                    getComponents().Add(comp.Key, comp);                
                }
                else
                {
                    if (!getComponents().ContainsValue(comp))
                    {
                        //New value for the same key
                        getComponents().Remove(comp.Key);
                        getComponents().Add(comp.Key, comp);
                    }
                }
            }

            RootElements.Clear();
            foreach (String name in names)
            {
                RootElements.Add(name);
            }
        }

        private Component getComponent(Dependency dep)
        {
            var artifactsMap = artifacts.artifacts.ToDictionary(x => x.general.ComponentId, x => x);
            return Util.ParseDependencies(dep, artifactsMap, this);
        }

        // Parsing the dependencies and returning the top severity from all the dependency. 
        // This top severity that is returned, is the project serverity
        private Severity getTopComponentSeverity(Severity topSeverity, Component depComponent)
        {
            if (depComponent != null)
            {
                topSeverity = Util.GetTopSeverity(topSeverity, depComponent.TopSeverity);
                // Adding to the data service components the dependencies itself.
                if (!getComponents().ContainsKey(depComponent.Key))
                {
                    getComponents().Add(depComponent.Key, depComponent);
                }
            }
            return topSeverity;
        }

        public async Task<Artifacts> RefreshArtifactsAsync(bool hard, Projects projects)
        {
            List<Components> components = new List<Components>();

            if (hard)
            {
                // Removes all components so Xray will later on scan for ALL the dependencies. 
                ClearAllComponents();
            }

            HashSet<Components> componentsSet = new HashSet<Components>();
            foreach (NugetProject nugetProject in projects.projects)
            {
                if (nugetProject.dependencies != null && nugetProject.dependencies.Length > 0)
                {
                    // Get project's components which are not included in the cache.
                    componentsSet.UnionWith(Util.GetComponents(nugetProject.dependencies, GetComponentsCache()));
                    // Update cache with new components.
                    GetComponentsCache().UnionWith(componentsSet);
                }
            }
            components = componentsSet.ToList();

            int BULK = 100;
            int i = 0;
            Artifacts artifacts = GetArtifacts();
            while (i + BULK < components.Count)
            {
                Artifacts buldArtifacts = await HttpUtils.GetCopmonentsFromXrayAsync(components.GetRange(i, BULK));
                artifacts.artifacts.AddRange(buldArtifacts.artifacts);
                i += BULK;
            }
            if (components.Count - i > 0)
            {
                Artifacts artifactsToAdd = await HttpUtils.GetCopmonentsFromXrayAsync(components.GetRange(i, components.Count - i));
                artifacts.artifacts.AddRange(artifactsToAdd.artifacts);
            }
            return artifacts;
        }

        public void ClearAllComponents()
        {
            GetComponentsCache().Clear();
            GetArtifacts().artifacts.Clear();
        }

        public Component getComponent(string key)
        {
            if (components.ContainsKey(key))
            {
                return components[key];
            }
            return new Component();
        }

        public Dictionary<string, Component> getComponents()
        {
            return components;
        }

        private void InitializeComponent()
        {
            this.RootElements = new List<string> {};
            this.components = new Dictionary<string, Component>();
            this.componentsCache = new HashSet<Components>();
        }
    }

    public enum RefreshType
    {
        Hard, Soft, None
    }
}

