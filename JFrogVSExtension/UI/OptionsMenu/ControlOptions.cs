﻿using System;
using System.Windows.Forms;
using JFrogVSExtension.HttpClient;
using System.IO;
using JFrogVSExtension.Xray;
using JFrogVSExtension.Logger;
using JFrogVSExtension.Utils;
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

        private async void TestConnection(object sender, EventArgs e)
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
                if (!isCompatibleVersion(xrayVersion))
                {
                    testConnectionField.Text = XrayUtil.GetMinimumXrayVersionErrorMessage(xrayVersion.xray_version);
                    return;
                }

                // Check components permissions. 
                String message = await HttpUtils.PostComponentToXrayAsync(new Components("", Util.PREFIX + "testComponent"));
                if (String.IsNullOrEmpty(message))
                {
                    testConnectionField.Text = "Received Xray version: " + xrayVersion.xray_version;
                }
                else
                {
                    testConnectionField.Text = message;
                }
            }
            catch (IOException ioe)
            {
                testConnectionField.Text = ioe.Message;
                await OutputLog.ShowMessageAsync("Caught exception when performing test connection: " + ioe);
            }
        }

        private bool isCompatibleVersion(XrayVersion xrayVersion)
        {
            if (XrayUtil.IsXrayVersionCompatible(xrayVersion.xray_version))
            {
                return true;
            }
            return false;
        }
    }
}
