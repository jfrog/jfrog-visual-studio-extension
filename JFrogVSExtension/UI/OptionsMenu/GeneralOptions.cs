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
            url = "";
            Password = "";
            User = "";
        }

        public string Server { get => getUrl(); set => setUrl(value); }
        protected String url;

        public String getUrl()
        {
            return url;
        }

        public void setUrl(String url)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            this.url = url;
        }
        public string User { get; set; } = "";

        public string Password { get; set; } = "";

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
                Server = _optionsControl.ServerTextBoxValue;
                Password = _optionsControl.PasswordTextBoxValue;
                User = _optionsControl.UserTextBoxValue;
            }
            base.OnApply(e);
            HttpUtils.InitClient(url, User, Password);
        }
    }
}
