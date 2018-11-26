using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace JFrogVSExtension.ComponentsDetails
{
    /// <summary>
    /// Interaction logic for ComponentsDetails.xaml.
    /// </summary>
    [ProvideToolboxControl("JFrogVSExtension.ComponentsDetails.ComponentsDetails", true)]
    public partial class ComponentsDetails : UserControl
    {
        public ComponentsDetails()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(CultureInfo.CurrentUICulture, "We are inside {0}.Button1_Click()", this.ToString()));
        }
    }
}
