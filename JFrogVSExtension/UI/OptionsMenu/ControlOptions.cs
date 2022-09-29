using System;
using System.Windows.Forms;
using JFrogVSExtension.HttpClient;
using System.IO;
using System.Net;
using JFrogVSExtension.Xray;
using JFrogVSExtension.Logger;
using System.Threading.Tasks;
using static JFrogVSExtension.OptionsMenu.JFrogXrayOptions;
using Microsoft.VisualStudio.PlatformUI;
using Newtonsoft.Json.Linq;

namespace JFrogVSExtension.OptionsMenu
{
    public partial class ControlOptions : UserControl
    {
        public ControlOptions()
        {
            InitializeComponent();
        }

        public JFrogXrayOptions OptionsPage { get; set; }
        public ScanPolicy Policy { get => policy; private set => SetScanPolicy(value); }
        private ScanPolicy policy;
        public bool UseAccessToken { get => useAccessToken.Checked; }
        public string PlatformUrlTextBoxValue
        {
            get { return textBoxPlatformUrl.Text; }
        }
        public string XrayServerTextBoxValue
        {
            get { return textBoxXrayUrl.Text; }
        }
        public string ArtifactoryServerTextBoxValue
        {
            get { return textBoxArtifactoryUrl.Text; }
        }
        public string UserTextBoxValue
        {
            get { return textBoxUser.Text; }
        }

        public string PasswordTextBoxValue
        {
            get { return textBoxPassword.Text; }
        }

        public string AccessTokenTextBoxValue
        {
            get { return textBoxAccessToken.Text; }
        }

        public string ProjectTextBoxValue
        {
            get { return textBoxProject.Text; }
        }

        public string WatchesTextBoxValue
        {
            get { return textBoxWatches.Text; }
        }
        private void CustomOptionsControl_Load(object sender, EventArgs e)
        {
            textBoxPlatformUrl.Text = OptionsPage.PlatformUrl;
            textBoxArtifactoryUrl.Text = OptionsPage.ArtifactoryUrl;
            textBoxXrayUrl.Text = OptionsPage.XrayUrl;
            if (!string.IsNullOrEmpty(OptionsPage.AccessToken))
            {
                textBoxAccessToken.Text = OptionsPage.AccessToken;
                useAccessToken.Checked = true;
            }
            else
            {
                textBoxPassword.Text = OptionsPage.Password;
                textBoxUser.Text = OptionsPage.User;
                useAccessToken.Checked = false;
            }
            InitializScanPolicy(OptionsPage.Policy);
            TextBoxPlatformUrlTextChanged(sender, e);
            SeparateUrlCheckBoxCheckedChanged(sender, e);
            UseAccessTokenCheckedChanged(sender, e);
            testConnectionField.Text = "";
        }

        private void SetScanPolicy(ScanPolicy policy)
        {
            this.policy= policy;
            switch (policy)
            {
                case ScanPolicy.AllVunerabilities:
                    textBoxProject.Enabled = false;
                    textBoxWatches.Enabled = false;
                    break;
                case ScanPolicy.Project:
                    textBoxProject.Enabled = true;
                    textBoxWatches.Enabled = false;
                    break;
                case ScanPolicy.Watches:
                    textBoxProject.Enabled = false;
                    textBoxWatches.Enabled = true;
                    break;
            }
        }

        private void InitializScanPolicy(ScanPolicy policy)
        {
            switch (policy)
            {
                case ScanPolicy.AllVunerabilities:
                    radioBtnAllVulnerabilities.Checked = true;
                    break ;
                case ScanPolicy.Project:
                    radioBtnProject.Checked = true;
                    textBoxProject.Text = OptionsPage.Project;
                    break;
                case ScanPolicy.Watches:
                    radioBtnWatches.Checked = true;
                    textBoxWatches.Text = string.Join(", ",OptionsPage.Watches);
                    break;
            }
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
                if (UseAccessToken)
                {
                    HttpUtils.InitClient(textBoxXrayUrl.Text, textBoxArtifactoryUrl.Text, "", "", textBoxAccessToken.Text);
                }
                else
                {
                    HttpUtils.InitClient(textBoxXrayUrl.Text, textBoxArtifactoryUrl.Text, textBoxUser.Text, textBoxPassword.Text);
                }
                var xrayStatus = await HttpUtils.GetXrayPingAsync();
                // Artifactory ping method is void, if 200 was not recived an exwption will be thrown
                await HttpUtils.GetArtifactoryPingAsync();
                if (xrayStatus == null)
                {
                    testConnectionField.Text = "Failed to perform ping.";
                    return;
                }
                var xrayVersion = await HttpUtils.GetXrayVersionAsync();
                var artifactoryVersion = await HttpUtils.GetArtifactoryVersionAsync();
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
                        message = $"Connected Successfully. Xray version: {xrayVersion.xray_version}\r\n" +
                            $"Artifactory version: {artifactoryVersion.version}";
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

        private void ScanPolicyCheckedChanged(object sender, EventArgs e)
        {
            ScanPolicy newScanPolicy;
            if (radioBtnAllVulnerabilities.Checked)
            {
                newScanPolicy = ScanPolicy.AllVunerabilities;
            }
            else if (radioBtnProject.Checked)
            {
                newScanPolicy = ScanPolicy.Project;
            }
            else
            {
                newScanPolicy = ScanPolicy.Watches;
            }
            Policy = newScanPolicy;
        }

        private void UseAccessTokenCheckedChanged(object sender, EventArgs e)
        {
            if (useAccessToken.Checked)
            {
                textBoxAccessToken.Enabled = true;
                textBoxPassword.Enabled = false;
                textBoxUser.Enabled = false;
                textBoxAccessToken.Focus();
            }
            else
            {
                textBoxAccessToken.Enabled = false;
                textBoxPassword.Enabled = true;
                textBoxUser.Enabled = true;
                textBoxUser.Focus();
            }

        }

        private void SeparateUrlCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (separateUrlCheckBox.Checked)
            {
                textBoxArtifactoryUrl.Enabled = true;
                textBoxXrayUrl.Enabled = true;
                textBoxPlatformUrl.Enabled = false;
            }
            else
            {
                textBoxArtifactoryUrl.Enabled = false;
                textBoxXrayUrl.Enabled = false;
                textBoxPlatformUrl.Enabled = true;
                TextBoxPlatformUrlTextChanged(sender, e);
            }
        }

        private void TextBoxPlatformUrlTextChanged(object sender, EventArgs e)
        {
            if (!separateUrlCheckBox.Checked)
            {
                var platformUrl =  textBoxPlatformUrl.Text.Trim();
                if (platformUrl.Length != 0)
                {

                    platformUrl = !platformUrl.EndsWith("/") ? platformUrl + "/" : platformUrl;
                    textBoxArtifactoryUrl.Text = platformUrl + "artifactory/";
                    textBoxXrayUrl.Text = platformUrl + "xray/";
                }
            }
        }
    }
}
