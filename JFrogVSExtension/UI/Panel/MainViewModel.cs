using JFrogVSExtension.Data.ViewModels;
using JFrogVSExtension.Tree;
using System;
using System.Collections.Generic;
using JFrogVSExtension.Data;
using JFrogVSExtension.Xray;

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

        #region constractor
        public MainViewModel()
        {
            Severities = new HashSet<Severity>();
            ResetSeverities();
            SeveritiesFromFilter = Severities;      
        }

        public void Refresh()
        {
            Tree = new TreeViewModel();
            Tree.Load(RefreshType.Hard, Severities);
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

        public void AddSeverityToFilter(string severityName)
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
            Tree.Load(RefreshType.None, SeveritiesFromFilter);
        }

        public void RemoveSeverityFromFilter(string severityName)
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
            Tree.Load(RefreshType.None, SeveritiesFromFilter);
        }

        public void ResetSeverities()
        {
            foreach (Severity severity in Enum.GetValues(typeof(Severity)))
            {
                Severities.Add(severity);
            }
        }

        public void Load()
        {
            Tree = new TreeViewModel();
            Tree.Load(RefreshType.Soft, Severities);
        }

        public void Close()
        {
            Tree = new TreeViewModel();
            Tree.Close();
        }
        #endregion
    }
}
