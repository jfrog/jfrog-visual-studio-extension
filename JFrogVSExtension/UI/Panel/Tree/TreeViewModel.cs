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
        }

        public void Load(RefreshType refreshType, HashSet<Severity> severities)
        {
            DataService dataService = DataService.Instance;

            RaisePropertyChanged("SelectedKey");
            try
            {
                String solutionDir = GetSolutionDir();
                if (String.IsNullOrWhiteSpace(solutionDir))
                {
                    return;
                }

                XrayVersion xrayVersion = HttpUtils.GetVersion();
                if (!XrayUtil.IsXrayVersionCompatible(xrayVersion.xray_version))
                {
                    String errorMessage = XrayUtil.GetErrorMessage(xrayVersion.xray_version);
                    OutputLog.ShowMessage(errorMessage);
                    return;
                }
                // Steps to run: 
                // Trigger CLI to collect json info to a file
                // Read the info
                // Send dependencies to Xray  
                // Get response and build the dependencies tree

                // Running CLI - this is the returned output.
                String returnedText = Util.GetCLIOutput(solutionDir);
                // Load projects from output
                Projects projects = Util.LosdNugetProjects(returnedText);

                if (projects.projects == null || projects.projects.Length == 0)
                {
                    OutputLog.ShowMessage("No projects were found.");
                    return;
                }
                List<Components> components = new List<Components>();
                Artifacts artifacts = null;
                switch (refreshType)
                {
                    case RefreshType.Hard:
                        {
                            // Get information for all dependencies. Ignore the cache
                            artifacts = dataService.RefreshArtifacts(true, projects);
                            break;
                        }

                    case RefreshType.Soft:
                        {
                            // Get information only for the delta. Means only new dependencies will be added.
                            artifacts = dataService.RefreshArtifacts(false, projects);
                            break;
                        }
                }
                dataService.Severities = severities;
                dataService.populateRootElements(projects);
               
                this.Artifacts = new ObservableCollection<ArtifactViewModel>();
                
                foreach (string key in dataService.RootElements)
                {
                    Artifacts.Add(new ArtifactViewModel(key));
                }
            }
            catch (IOException ioe)
            {
                dataService.ClearAllComponents();
                OutputLog.ShowMessage(ioe.Message);
                OutputLog.ShowMessage(ioe.StackTrace);
            }
            catch (HttpRequestException he)
            {
                dataService.ClearAllComponents();
                OutputLog.ShowMessage(he.Message);
                OutputLog.ShowMessage(he.StackTrace);
            }            
        }

        public void Close()
        {
            OutputLog.ShowMessage("Closing solution. Clearing...");
        }

        private String GetSolutionDir()
        {
            EnvDTE.DTE dte = MainPanelPackage.getDTE();
            if (dte == null)
            {
                OutputLog.ShowMessage("The plugin was not initialized yet. DTE is null");
                return "";
            }
            string solutionFullName = dte.Solution.FullName;
            if (String.IsNullOrWhiteSpace(solutionFullName))
            {
                OutputLog.ShowMessage("There is no solution yet available. The path is: " + solutionFullName);
                return "";
            }
           return System.IO.Path.GetDirectoryName(solutionFullName);
        }
        #endregion
    }
}
