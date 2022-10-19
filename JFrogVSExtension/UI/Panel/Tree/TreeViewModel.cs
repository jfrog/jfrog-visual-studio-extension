using JFrogVSExtension.Data.ViewModels;
using System.Collections.ObjectModel;
using JFrogVSExtension.Data;
using System.Collections.Generic;
using System;
using System.Net.Http;
using JFrogVSExtension.Logger;
using JFrogVSExtension.HttpClient;
using JFrogVSExtension.Utils;
using JFrogVSExtension.Xray;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using System.Linq;

namespace JFrogVSExtension.Tree
{
    class TreeViewModel : BaseViewModel
    {
        #region Private properties

        private string selectedKey;

        #endregion

        #region Public Properties

        public ObservableCollection<ArtifactViewModel> Artifacts { get; set; }
        public ObservableCollection<Issue> IssueDetails { get; set; }
        public Component SelectedComponent { get; set; }
        public Boolean EnableRefreshButton { get; set; }
        public string SelectedKey
        {
            get { return selectedKey; }
            set
            {
                selectedKey = value;
                DataService dataService = DataService.Instance;
                SelectedComponent = dataService.getComponent(value);
                if (SelectedComponent != null && SelectedComponent.Issues != null)
                {
                    IssueDetails = new ObservableCollection<Issue>(SelectedComponent.Issues);
                } else
                {
                    IssueDetails = new ObservableCollection<Issue>();
                }
            }
        }

        #endregion

        #region Constructor
        public TreeViewModel()
        {
            this.EnableRefreshButton = true;
        }

        #endregion

        public async Task LoadAsync(RefreshType refreshType, HashSet<Severity> severities)
        {
            this.EnableRefreshButton = false;
            DataService dataService = DataService.Instance;

            RaisePropertyChanged("SelectedKey");
            try
            {
                String solutionDir = await GetSolutionDirAsync();
                if (String.IsNullOrWhiteSpace(solutionDir))
                {
                    return;
                }

                XrayVersion xrayVersion = await HttpUtils.GetXrayVersionAsync();
                if (!XrayUtil.IsXrayVersionCompatible(xrayVersion.xray_version))
                {
                    String errorMessage = XrayUtil.GetMinimumXrayVersionErrorMessage(xrayVersion.xray_version);
                    await OutputLog.ShowMessageAsync(errorMessage);
                    return;
                }
                // Steps to run: 
                // 1. Trigger CLI to collect json info to a file.
                // 2. Read the info.
                // 3. Send dependencies to Xray.
                // 4. Get response and build the dependencies tree.

                // Running CLI - this is the returned output.
                String returnedText = await Task.Run(() => Util.GetCLIOutputAsync("rt nuget-deps-tree",solutionDir));
                
                // Load projects from output.
                Project[] nugetProjects = Util.LoadNugetProjects(returnedText);
                Project[] npmProjects = Util.LoadNpmProjects();

                Projects projects = new Projects
                {
                    NugetProjects = nugetProjects,
                    NpmProjects = npmProjects,
                };
                if (projects.All.FirstOrDefault() == null)
                {
                    await OutputLog.ShowMessageAsync("No projects were found.");
                    return;
                }
                Artifacts artifacts = null;
                switch (refreshType)
                {
                    case RefreshType.Hard:
                        {
                            // Get information for all dependencies. Ignore the cache.
                            artifacts = await dataService.GetSecurityIssuesAsync(true, projects, solutionDir);
                            break;
                        }

                    case RefreshType.Soft:
                        {
                            // Get information only for the delta. Means only new dependencies will be added.
                            artifacts = await dataService.GetSecurityIssuesAsync(false, projects, solutionDir);
                            break;
                        }
                }
                dataService.Severities = severities;
                dataService.PopulateRootElements(projects);
               
                this.Artifacts = new ObservableCollection<ArtifactViewModel>();
                
                foreach (string key in dataService.RootElements)
                {
                    Artifacts.Add(new ArtifactViewModel(key));
                }
            }
            catch (Exception e)
            {
                dataService.ClearAllComponents();
                await OutputLog.ShowMessageAsync(e.Message);
                await OutputLog.ShowMessageAsync(e.StackTrace);
            }
            finally
            {
                this.EnableRefreshButton = true;
            }
        }

        public async Task CloseAsync()
        {
            await OutputLog.ShowMessageAsync("Closing solution. Clearing...");
        }

        private async Task<String> GetSolutionDirAsync()
        {
            EnvDTE.DTE dte = MainPanelPackage.getDTE();
            if (dte == null)
            {
                await OutputLog.ShowMessageAsync("The plugin was not initialized yet. DTE is null");
                return "";
            }
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            string solutionFullName = dte.Solution.FullName;
            if (String.IsNullOrWhiteSpace(solutionFullName))
            {
                await OutputLog.ShowMessageAsync("There is no solution yet available. The path is: " + solutionFullName);
                return "";
            }
           return System.IO.Path.GetDirectoryName(solutionFullName);
        }
    }
}
