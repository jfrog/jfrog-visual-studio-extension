using JFrogVSExtension.Data.ViewModels;
using JFrogVSExtension.Tree;
using System;
using System.Collections.Generic;
using JFrogVSExtension.Data;
using JFrogVSExtension.Xray;
using System.Threading.Tasks;

namespace JFrogVSExtension
{
    class MainViewModel : BaseViewModel
    {
        #region Public Properties
        public TreeViewModel Tree { get; private set; }
        #endregion

        #region Private Properties
        public static HashSet<Severity> Severities { get; set; }
        public static HashSet<Severity> SeveritiesFromFilter { get; set; }
        #endregion

        #region Constructor

        public MainViewModel()
        {
            Severities = new HashSet<Severity>();
            ResetSeverities();
            SeveritiesFromFilter = Severities;
        }

        public async Task RefreshAsync()
        {
            Tree = new TreeViewModel
            {
                ScanStatus = "Xray scan in progress..."
            };
            await Tree.LoadAsync(RefreshType.Hard, SeveritiesFromFilter);
            Tree.ScanStatus = "";
        }

        public void ExpandAll()
        {
            if (Tree.Artifacts != null)
            {
                foreach (var artifact in Tree.Artifacts)
                {
                    artifact.ExpandAll();
                }
            }
        }
        public void CollapseAll()
        {
            if (Tree.Artifacts != null)
            {
                foreach (var artifact in Tree.Artifacts)
                {
                    artifact.IsExpanded = false;
                }
            }
        }

        public async Task AddSeverityToFilterAsync(string severityName)
        {
            if (severityName.Equals("All"))
            {
                ResetSeverities();
                SeveritiesFromFilter = Severities;
            }
            else
            {
                Severity severity = (Severity)Enum.Parse(typeof(Severity), severityName);
                SeveritiesFromFilter.Add(severity);
            }
            Tree = new TreeViewModel();
            await Tree.LoadAsync(RefreshType.None, SeveritiesFromFilter);
        }

        public async Task RemoveSeverityFromFilterAsync(string severityName)
        {
            if (severityName.Equals("All"))
            {
                SeveritiesFromFilter = new HashSet<Severity>();
            } else
            {
                Severity severity = (Severity)Enum.Parse(typeof(Severity), severityName);

                if (SeveritiesFromFilter.Contains(severity))
                {
                    SeveritiesFromFilter.Remove(severity);
                }
            }
            Tree = new TreeViewModel();
            await Tree.LoadAsync(RefreshType.None, SeveritiesFromFilter);
        }

        public void ResetSeverities()
        {
            foreach (Severity severity in Enum.GetValues(typeof(Severity)))
            {
                Severities.Add(severity);
            }
        }

        public async Task LoadAsync()
        {
            Tree = new TreeViewModel();
            await Tree.LoadAsync(RefreshType.Soft, Severities);
        }

        public async Task CloseAsync()
        {
            Tree = new TreeViewModel();
            await Tree.CloseAsync();
        }
        #endregion
    }
}
