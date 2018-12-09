namespace JFrogVSExtension
{
    using JFrogVSExtension.Logger;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainPanelControl.
    /// </summary>
    public partial class MainPanelControl : UserControl
    {
        private static bool isAllFilterChecked = true;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPanelControl"/> class.
        /// </summary>
        public MainPanelControl()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        /// <summary>
        /// Handles Refresh button.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void RefreshTree(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)this.DataContext).Refresh();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void ColapseTree(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)this.DataContext).Refresh();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void ExpandTree(object sender, RoutedEventArgs e)
        {

            ((MainViewModel)this.DataContext).ExpandAll();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void CollapseTree(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)this.DataContext).CollapseAll();
        }

        private void HandleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)e.Source).IsChecked)
            {
                HandleCheck(((CheckBox)e.Source).Content.ToString());
            }
            else
            {
                HandleUnchecked(((CheckBox)e.Source).Content.ToString());
            }
        }
        private void HandleCheck(string filtredObject)
        {

            if (filtredObject.Equals("All"))
            {
                isAllFilterChecked = true;
                cbHigh.IsChecked = true;
                cbMajor.IsChecked = true;
                cbMinor.IsChecked  = true;
                cbUnknown.IsChecked = true;
                cbNormal.IsChecked = true;
            }
            ((MainViewModel)this.DataContext).AddSeverityToFilter(filtredObject);
        }

        private void HandleUnchecked(string filtredObject)
        {
            if (filtredObject.Equals("All"))
            {
                isAllFilterChecked = false;
                cbHigh.IsChecked = false;
                cbMajor.IsChecked = false;
                cbMinor.IsChecked = false;
                cbUnknown.IsChecked = false;
                cbNormal.IsChecked = false;
            }
            else
            {
                if (isAllFilterChecked)
                {
                    isAllFilterChecked = false;
                    cbAll.IsChecked = false;
                }
            }
            ((MainViewModel)this.DataContext).RemoveSeverityFromFilter(filtredObject);
        }
        private void OpenFilter(object sender, RoutedEventArgs e)
        {
            FilterPopup.IsOpen = true;
        }

        private void Details_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Tree_Loaded(object sender, RoutedEventArgs e)
        {
            if (isAllFilterChecked)
            {
                InitCheckbox();
            }
            ((MainViewModel)this.DataContext).Load();
        }

        private void InitCheckbox()
        {
            cbAll.IsChecked = true;
            cbHigh.IsChecked = true;
            cbMajor.IsChecked = true;
            cbMinor.IsChecked = true;
            cbUnknown.IsChecked = true;
            cbNormal.IsChecked = true;
        }

        public void Load()
        {
            if (isAllFilterChecked)
            {
                InitCheckbox();
            }
            ((MainViewModel)this.DataContext).Load();
        }

        public void Close()
        {
            ((MainViewModel)this.DataContext).Close();
        }
    }
}