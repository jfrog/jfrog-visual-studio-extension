using JFrogVSExtension.Data.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace JFrogVSExtension.Tree
{
    /// <summary>
    /// Interaction logic for Tree.xaml.
    /// </summary>
    [ProvideToolboxControl("JFrogVSExtension.Tree.Tree", true)]
    public partial class Tree : UserControl
    {

        public Tree()
        {
            InitializeComponent();
        }

        private void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e != null && e.NewValue != null)
            {
                ((TreeViewModel)this.DataContext).SelectedKey = ((ArtifactViewModel)e.NewValue).Key;
            }
        }
    }
}
