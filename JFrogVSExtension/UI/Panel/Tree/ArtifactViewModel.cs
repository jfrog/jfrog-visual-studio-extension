using Microsoft.VisualStudio.Imaging.Interop;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace JFrogVSExtension.Data.ViewModels
{
    class ArtifactViewModel : BaseViewModel
    {
        #region Public Properties

        public string Key { get; set; }
        public int Issues { get; set; }
        public ObservableCollection<String> Dependencies { get; set; }
        public ObservableCollection<ArtifactViewModel> Children { get; set; }
        public ImageMoniker SeveretyMoniker { get; }


        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count > 0 && this.Children[0] != null;
            }
            set
            {
                if (value == true)
                    Expand();
                else
                   this.ClearChildren();
            }
        }
        #endregion

        public ArtifactViewModel(string key)
        {
            this.ExpandCommand = new RelayCommand(Expand);
            DataService dataService = DataService.Instance;
            this.Key = key;
            Component component = dataService.getComponent(key);
            this.SeveretyMoniker = JFrogMonikerSelector.SeverityToMoniker(component.TopSeverity);
            if (component == null || component.Dependencies == null || component.Dependencies.Count == 0)
            {
                return;
            }
            this.Dependencies = new ObservableCollection<string>(component.Dependencies);
            this.ClearChildren();
        }

        

        #region Public Commands

        public ICommand ExpandCommand { get; set; }

        public void ExpandAll()
        {
            this.IsExpanded = true;
            if (this.Children != null)
            {
                foreach (var child in this.Children)
                {
                    child.ExpandAll();
                }
            }
        }
        #endregion


        private void Expand()
        {
            if(this.Dependencies == null || this.Dependencies.Count == 0)
            {
                return;
            }
            this.Children = new ObservableCollection<ArtifactViewModel>();
            foreach (string key in this.Dependencies)
            {
                this.Children.Add(new ArtifactViewModel(key));
            }
        }

        private void ClearChildren()
        {
            this.Children = new ObservableCollection<ArtifactViewModel>
            {
                null
            };
        }
    }
}
