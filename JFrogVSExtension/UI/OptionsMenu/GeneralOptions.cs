using JFrogVSExtension.HttpClient;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace JFrogVSExtension.OptionsMenu
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    [Guid("8bb519a5-4864-43b0-8684-e2f2f723101c")]
    public class JFrogXrayOptions : DialogPage
    {
        private ControlOptions _optionsControl;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PathForVisualStudio { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected override System.Windows.Forms.IWin32Window Window
        {
            get
            {
                _optionsControl = new ControlOptions();
                _optionsControl.OptionsPage = this;
                return _optionsControl;
            }
        }

        public JFrogXrayOptions()
        {
            platformUrl = "";
            xrayUrl = "";
            artifctoryUrl = "";
            Password = "";
            User = "";
            AccessToken = "";
        }

        public string PlatformUrl { get => platformUrl; set => platformUrl = AddSlashIfNeeded(value); }
        protected string platformUrl;
        public string XrayUrl { get => xrayUrl; set => xrayUrl = AddSlashIfNeeded(value); }
        protected string xrayUrl;
        public string ArtifactoryUrl { get => artifctoryUrl; set => artifctoryUrl = AddSlashIfNeeded(value); }
        protected string artifctoryUrl;
        public string Project { get; set; }
        public string[] Watches { get; set; }
        public ScanPolicy Policy { get; set; }
        public string User { get; set; } 
        public string Password { get; set; }
        public string AccessToken { get; set; }
        /// <summary>
        /// Handles "apply" messages from the Visual Studio environment.
        /// </summary>
        /// <devdoc>
        /// This method is called when VS wants to save the user's 
        /// changes (for example, when the user clicks OK in the dialog).
        /// </devdoc>
        protected override void OnApply(PageApplyEventArgs e)
        {
            if (e.ApplyBehavior == ApplyKind.Apply)
            {
                PlatformUrl = _optionsControl.PlatformUrlTextBoxValue;
                XrayUrl = _optionsControl.XrayServerTextBoxValue;
                artifctoryUrl = _optionsControl.ArtifactoryServerTextBoxValue;
                if (_optionsControl.UseAccessToken)
                {
                    AccessToken = _optionsControl.AccessTokenTextBoxValue;
                    Password = "";
                    User = "";
                }
                else
                {
                    Password = _optionsControl.PasswordTextBoxValue;
                    User = _optionsControl.UserTextBoxValue;
                    AccessToken = "";
                }
            }
            base.OnApply(e);
            HttpUtils.InitClient(XrayUrl,ArtifactoryUrl, User, Password, AccessToken);
        }

        private string AddSlashIfNeeded(string url)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            return url;
        }

        public enum ScanPolicy
        {
            AllVunerabilities,
            Project,
            Watches
        }
    }
}
