﻿namespace JFrogVSExtension
{
    using JFrogVSExtension.Logger;
    using System.Windows;
    using System.Windows.Controls;
    using System.Threading.Tasks;

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
        private async void RefreshTree(object sender, RoutedEventArgs e)
        {
            await ((MainViewModel)this.DataContext).RefreshAsync();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private async void ColapseTree(object sender, RoutedEventArgs e)
        {
            await ((MainViewModel)this.DataContext).RefreshAsync();
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

        private async void HandleClick(object sender, RoutedEventArgs e)
        {
            if ((bool)((CheckBox)e.Source).IsChecked)
            {
                await HandleCheckAsync(((CheckBox)e.Source).Content.ToString());
            }
            else
            {
                await HandleUncheckedAsync(((CheckBox)e.Source).Content.ToString());
            }
        }

        private async Task HandleCheckAsync(string filtredObject)
        {
            if (filtredObject.Equals("All"))
            {
                isAllFilterChecked = true;
                cbHigh.IsChecked = true;
                cbMedium.IsChecked = true;
                cbLow.IsChecked  = true;
                cbUnknown.IsChecked = true;
                cbNormal.IsChecked = true;
            }
            await ((MainViewModel)this.DataContext).AddSeverityToFilterAsync(filtredObject);
        }

        private async Task HandleUncheckedAsync(string filtredObject)
        {
            if (filtredObject.Equals("All"))
            {
                isAllFilterChecked = false;
                cbHigh.IsChecked = false;
                cbMedium.IsChecked = false;
                cbLow.IsChecked = false;
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
            await ((MainViewModel)this.DataContext).RemoveSeverityFromFilterAsync(filtredObject);
        }
        private void OpenFilter(object sender, RoutedEventArgs e)
        {
            FilterPopup.IsOpen = true;
        }

        private void Details_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private async void Tree_Loaded(object sender, RoutedEventArgs e)
        {
            if (isAllFilterChecked)
            {
                InitCheckbox();
            }
            await ((MainViewModel)this.DataContext).LoadAsync();
        }

        private void InitCheckbox()
        {
            cbAll.IsChecked = true;
            cbHigh.IsChecked = true;
            cbMedium.IsChecked = true;
            cbLow.IsChecked = true;
            cbUnknown.IsChecked = true;
            cbNormal.IsChecked = true;
        }

        public async Task LoadAsync()
        {
            if (isAllFilterChecked)
            {
                InitCheckbox();
            }
            await ((MainViewModel)this.DataContext).LoadAsync();
        }

        public async Task CloseAsync()
        {
            await ((MainViewModel)this.DataContext).CloseAsync();
        }
    }
}