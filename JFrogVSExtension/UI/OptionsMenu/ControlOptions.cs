using System;
using System.Windows.Forms;
using JFrogVSExtension.HttpClient;
using System.IO;
using System.Net;
using JFrogVSExtension.Xray;
using JFrogVSExtension.Logger;
using System.Threading.Tasks;

namespace JFrogVSExtension.OptionsMenu
{
    public partial class ControlOptions : UserControl
    {
        public ControlOptions()
        {
            InitializeComponent();
        }

        public JFrogXrayOptions OptionsPage { get; set; }
        public string ServerTextBoxValue
        {
            get { return txtBoxServer.Text; }
            set { txtBoxServer.Text = value; }
        }
        public string UserTextBoxValue
        {
            get { return txtBoxUser.Text; }
            set { txtBoxUser.Text = value; }
        }

        public string PasswordTextBoxValue
        {
            get { return txtBoxPassword.Text; }
            set { txtBoxPassword.Text = value; }
        }

        private void CustomOptionsControl_Load(object sender, EventArgs e)
        {
            txtBoxServer.Text = OptionsPage.Server;
            txtBoxPassword.Text = OptionsPage.Password;
            txtBoxUser.Text = OptionsPage.User;
            testConnectionField.Text = "";
        }

#pragma warning disable VSTHRD100 // Avoid async void methods - Signature expected by event handler.
        private async void TestConnection(object sender, EventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            this.btnTestConnection.Enabled = false;
            testConnectionField.Text = "Awaiting response...";
            await PerformTestConnectionAsync();
            this.btnTestConnection.Enabled = true;
        }

        private async Task PerformTestConnectionAsync()
        {
            try
            {
                if (!txtBoxServer.Text.EndsWith("/"))
                {
                    txtBoxServer.Text += "/";
                }
                HttpUtils.InitClient(txtBoxServer.Text, txtBoxUser.Text, txtBoxPassword.Text);
                XrayStatus xrayStatus = await HttpUtils.GetPingAsync();
                if (xrayStatus == null)
                {
                    testConnectionField.Text = "Failed to perform ping.";
                    return;
                }
                XrayVersion xrayVersion = await HttpUtils.GetVersionAsync();
                if (!XrayUtil.IsXrayVersionCompatible(xrayVersion.xray_version))
                {
                    testConnectionField.Text = XrayUtil.GetMinimumXrayVersionErrorMessage(xrayVersion.xray_version);
                    return;
                }

                // Check components permissions. 
                var response = await HttpUtils.TestConnectionAndPermissionsAsync();
                string message;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        message = $"Received {HttpStatusCode.Unauthorized} from Xray. Please check your credentials.";
                        break;
                    case HttpStatusCode.Forbidden:
                        message =  $"Received {HttpStatusCode.Forbidden} from Xray. Please make sure that the user has 'View Components' permission in Xray.";
                        break;
                    default:
                        message = $"Successfully connected to Xray version: {xrayVersion.xray_version}";
                        break;
                }
                testConnectionField.Text = message;
            }
            catch (IOException ioe)
            {
                testConnectionField.Text = ioe.Message;
                await OutputLog.ShowMessageAsync("Caught exception when performing test connection: " + ioe);
            }
        }

        private void allVulnerabilities_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void project_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void watches_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
