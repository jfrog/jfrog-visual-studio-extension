namespace JFrogVSExtension.OptionsMenu
{
    partial class ControlOptions 
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xrayGroup = new System.Windows.Forms.GroupBox();
            this.AuthMethod = new System.Windows.Forms.TextBox();
            this.AuthRadioButtons = new System.Windows.Forms.GroupBox();
            this.accessToken = new System.Windows.Forms.RadioButton();
            this.basicAuth = new System.Windows.Forms.RadioButton();
            this.textBoxAccessToken = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBoxXrayUrl = new System.Windows.Forms.TextBox();
            this.textBoxArtifactoryUrl = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.artifactoryUrl = new System.Windows.Forms.TextBox();
            this.separateUrlCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWatches = new System.Windows.Forms.TextBox();
            this.textBoxProject = new System.Windows.Forms.TextBox();
            this.radioBtnWatches = new System.Windows.Forms.RadioButton();
            this.radioBtnProject = new System.Windows.Forms.RadioButton();
            this.radioBtnAllVulnerabilities = new System.Windows.Forms.RadioButton();
            this.textBoxPlatformUrl = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.serverUrl = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.testConnectionField = new System.Windows.Forms.TextBox();
            this.xrayGroup.SuspendLayout();
            this.AuthRadioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // xrayGroup
            // 
            this.xrayGroup.Controls.Add(this.AuthMethod);
            this.xrayGroup.Controls.Add(this.AuthRadioButtons);
            this.xrayGroup.Controls.Add(this.textBoxAccessToken);
            this.xrayGroup.Controls.Add(this.textBox4);
            this.xrayGroup.Controls.Add(this.textBoxXrayUrl);
            this.xrayGroup.Controls.Add(this.textBoxArtifactoryUrl);
            this.xrayGroup.Controls.Add(this.textBox3);
            this.xrayGroup.Controls.Add(this.artifactoryUrl);
            this.xrayGroup.Controls.Add(this.separateUrlCheckBox);
            this.xrayGroup.Controls.Add(this.label1);
            this.xrayGroup.Controls.Add(this.textBoxWatches);
            this.xrayGroup.Controls.Add(this.textBoxProject);
            this.xrayGroup.Controls.Add(this.radioBtnWatches);
            this.xrayGroup.Controls.Add(this.radioBtnProject);
            this.xrayGroup.Controls.Add(this.radioBtnAllVulnerabilities);
            this.xrayGroup.Controls.Add(this.textBoxPlatformUrl);
            this.xrayGroup.Controls.Add(this.textBoxUser);
            this.xrayGroup.Controls.Add(this.textBoxPassword);
            this.xrayGroup.Controls.Add(this.btnTestConnection);
            this.xrayGroup.Controls.Add(this.serverUrl);
            this.xrayGroup.Controls.Add(this.user);
            this.xrayGroup.Controls.Add(this.password);
            this.xrayGroup.Controls.Add(this.testConnectionField);
            this.xrayGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xrayGroup.Location = new System.Drawing.Point(0, 0);
            this.xrayGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xrayGroup.Name = "xrayGroup";
            this.xrayGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xrayGroup.Size = new System.Drawing.Size(1076, 626);
            this.xrayGroup.TabIndex = 0;
            this.xrayGroup.TabStop = false;
            this.xrayGroup.Text = "JFrog Options";
            // 
            // AuthMethod
            // 
            this.AuthMethod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AuthMethod.Location = new System.Drawing.Point(4, 76);
            this.AuthMethod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AuthMethod.Multiline = true;
            this.AuthMethod.Name = "AuthMethod";
            this.AuthMethod.ReadOnly = true;
            this.AuthMethod.Size = new System.Drawing.Size(183, 28);
            this.AuthMethod.TabIndex = 60;
            this.AuthMethod.TabStop = false;
            this.AuthMethod.Text = "Authentication Method";
            // 
            // AuthRadioButtons
            // 
            this.AuthRadioButtons.Controls.Add(this.accessToken);
            this.AuthRadioButtons.Controls.Add(this.basicAuth);
            this.AuthRadioButtons.Location = new System.Drawing.Point(196, 74);
            this.AuthRadioButtons.Name = "AuthRadioButtons";
            this.AuthRadioButtons.Size = new System.Drawing.Size(435, 28);
            this.AuthRadioButtons.TabIndex = 59;
            this.AuthRadioButtons.TabStop = false;
            // 
            // accessToken
            // 
            this.accessToken.AutoSize = true;
            this.accessToken.Location = new System.Drawing.Point(138, 0);
            this.accessToken.Name = "accessToken";
            this.accessToken.Size = new System.Drawing.Size(134, 24);
            this.accessToken.TabIndex = 1;
            this.accessToken.TabStop = true;
            this.accessToken.Text = "Access Token";
            this.accessToken.UseVisualStyleBackColor = true;
            this.accessToken.CheckedChanged += new System.EventHandler(this.AccessTokenCheckedChanged);
            // 
            // basicAuth
            // 
            this.basicAuth.AutoSize = true;
            this.basicAuth.Location = new System.Drawing.Point(6, 0);
            this.basicAuth.Name = "basicAuth";
            this.basicAuth.Size = new System.Drawing.Size(111, 24);
            this.basicAuth.TabIndex = 0;
            this.basicAuth.TabStop = true;
            this.basicAuth.Text = "Basic Auth";
            this.basicAuth.UseVisualStyleBackColor = true;
            // 
            // textBoxAccessToken
            // 
            this.textBoxAccessToken.Location = new System.Drawing.Point(196, 172);
            this.textBoxAccessToken.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxAccessToken.Name = "textBoxAccessToken";
            this.textBoxAccessToken.PasswordChar = '*';
            this.textBoxAccessToken.Size = new System.Drawing.Size(435, 26);
            this.textBoxAccessToken.TabIndex = 56;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(4, 172);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(121, 26);
            this.textBox4.TabIndex = 57;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "Access Token";
            // 
            // textBoxXrayUrl
            // 
            this.textBoxXrayUrl.Location = new System.Drawing.Point(196, 238);
            this.textBoxXrayUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxXrayUrl.Name = "textBoxXrayUrl";
            this.textBoxXrayUrl.Size = new System.Drawing.Size(435, 26);
            this.textBoxXrayUrl.TabIndex = 52;
            // 
            // textBoxArtifactoryUrl
            // 
            this.textBoxArtifactoryUrl.Location = new System.Drawing.Point(196, 274);
            this.textBoxArtifactoryUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxArtifactoryUrl.Name = "textBoxArtifactoryUrl";
            this.textBoxArtifactoryUrl.Size = new System.Drawing.Size(435, 26);
            this.textBoxArtifactoryUrl.TabIndex = 53;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(4, 236);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(80, 28);
            this.textBox3.TabIndex = 54;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "Xray URL";
            // 
            // artifactoryUrl
            // 
            this.artifactoryUrl.BackColor = System.Drawing.SystemColors.Control;
            this.artifactoryUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.artifactoryUrl.Location = new System.Drawing.Point(4, 274);
            this.artifactoryUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.artifactoryUrl.Multiline = true;
            this.artifactoryUrl.Name = "artifactoryUrl";
            this.artifactoryUrl.ReadOnly = true;
            this.artifactoryUrl.Size = new System.Drawing.Size(193, 28);
            this.artifactoryUrl.TabIndex = 55;
            this.artifactoryUrl.TabStop = false;
            this.artifactoryUrl.Text = "Artifactory URL";
            // 
            // separateUrlCheckBox
            // 
            this.separateUrlCheckBox.AutoSize = true;
            this.separateUrlCheckBox.Location = new System.Drawing.Point(196, 206);
            this.separateUrlCheckBox.Name = "separateUrlCheckBox";
            this.separateUrlCheckBox.Size = new System.Drawing.Size(325, 24);
            this.separateUrlCheckBox.TabIndex = 51;
            this.separateUrlCheckBox.Text = "Set Artifactory and Xray URLs separately";
            this.separateUrlCheckBox.UseVisualStyleBackColor = true;
            this.separateUrlCheckBox.CheckedChanged += new System.EventHandler(this.SeparateUrlCheckBoxCheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 388);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 50;
            this.label1.Text = "Scanning Policy";
            // 
            // textBoxWatches
            // 
            this.textBoxWatches.Location = new System.Drawing.Point(196, 481);
            this.textBoxWatches.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxWatches.Name = "textBoxWatches";
            this.textBoxWatches.Size = new System.Drawing.Size(367, 26);
            this.textBoxWatches.TabIndex = 49;
            // 
            // textBoxProject
            // 
            this.textBoxProject.Location = new System.Drawing.Point(196, 448);
            this.textBoxProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxProject.Name = "textBoxProject";
            this.textBoxProject.Size = new System.Drawing.Size(367, 26);
            this.textBoxProject.TabIndex = 48;
            // 
            // radioBtnWatches
            // 
            this.radioBtnWatches.AutoSize = true;
            this.radioBtnWatches.Location = new System.Drawing.Point(8, 483);
            this.radioBtnWatches.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioBtnWatches.Name = "radioBtnWatches";
            this.radioBtnWatches.Size = new System.Drawing.Size(190, 24);
            this.radioBtnWatches.TabIndex = 47;
            this.radioBtnWatches.TabStop = true;
            this.radioBtnWatches.Text = "According to Watches";
            this.radioBtnWatches.UseVisualStyleBackColor = true;
            this.radioBtnWatches.CheckedChanged += new System.EventHandler(this.ScanPolicyCheckedChanged);
            // 
            // radioBtnProject
            // 
            this.radioBtnProject.AutoSize = true;
            this.radioBtnProject.Location = new System.Drawing.Point(8, 449);
            this.radioBtnProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioBtnProject.Name = "radioBtnProject";
            this.radioBtnProject.Size = new System.Drawing.Size(175, 24);
            this.radioBtnProject.TabIndex = 46;
            this.radioBtnProject.TabStop = true;
            this.radioBtnProject.Text = "According to project";
            this.radioBtnProject.UseVisualStyleBackColor = true;
            this.radioBtnProject.CheckedChanged += new System.EventHandler(this.ScanPolicyCheckedChanged);
            // 
            // radioBtnAllVulnerabilities
            // 
            this.radioBtnAllVulnerabilities.AutoSize = true;
            this.radioBtnAllVulnerabilities.Location = new System.Drawing.Point(8, 416);
            this.radioBtnAllVulnerabilities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioBtnAllVulnerabilities.Name = "radioBtnAllVulnerabilities";
            this.radioBtnAllVulnerabilities.Size = new System.Drawing.Size(149, 24);
            this.radioBtnAllVulnerabilities.TabIndex = 45;
            this.radioBtnAllVulnerabilities.TabStop = true;
            this.radioBtnAllVulnerabilities.Text = "All vulnerabilities";
            this.radioBtnAllVulnerabilities.UseVisualStyleBackColor = true;
            this.radioBtnAllVulnerabilities.CheckedChanged += new System.EventHandler(this.ScanPolicyCheckedChanged);
            // 
            // textBoxPlatformUrl
            // 
            this.textBoxPlatformUrl.Location = new System.Drawing.Point(196, 40);
            this.textBoxPlatformUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPlatformUrl.Name = "textBoxPlatformUrl";
            this.textBoxPlatformUrl.Size = new System.Drawing.Size(435, 26);
            this.textBoxPlatformUrl.TabIndex = 10;
            this.textBoxPlatformUrl.TextChanged += new System.EventHandler(this.TextBoxPlatformUrlTextChanged);
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(196, 101);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(435, 26);
            this.textBoxUser.TabIndex = 20;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(196, 139);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(435, 26);
            this.textBoxPassword.TabIndex = 30;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTestConnection.Location = new System.Drawing.Point(668, 312);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(142, 44);
            this.btnTestConnection.TabIndex = 40;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.TestConnection);
            // 
            // serverUrl
            // 
            this.serverUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serverUrl.Location = new System.Drawing.Point(4, 43);
            this.serverUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.serverUrl.Multiline = true;
            this.serverUrl.Name = "serverUrl";
            this.serverUrl.ReadOnly = true;
            this.serverUrl.Size = new System.Drawing.Size(152, 28);
            this.serverUrl.TabIndex = 41;
            this.serverUrl.TabStop = false;
            this.serverUrl.Text = "JFrog Platform URL";
            // 
            // user
            // 
            this.user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.user.Location = new System.Drawing.Point(4, 101);
            this.user.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.user.Multiline = true;
            this.user.Name = "user";
            this.user.ReadOnly = true;
            this.user.Size = new System.Drawing.Size(80, 26);
            this.user.TabIndex = 42;
            this.user.TabStop = false;
            this.user.Text = "User";
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.SystemColors.Control;
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password.Location = new System.Drawing.Point(4, 142);
            this.password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Size = new System.Drawing.Size(193, 26);
            this.password.TabIndex = 43;
            this.password.TabStop = false;
            this.password.Text = "Password";
            // 
            // testConnectionField
            // 
            this.testConnectionField.BackColor = System.Drawing.SystemColors.Control;
            this.testConnectionField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testConnectionField.Location = new System.Drawing.Point(4, 312);
            this.testConnectionField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.testConnectionField.Multiline = true;
            this.testConnectionField.Name = "testConnectionField";
            this.testConnectionField.ReadOnly = true;
            this.testConnectionField.Size = new System.Drawing.Size(606, 52);
            this.testConnectionField.TabIndex = 44;
            this.testConnectionField.TabStop = false;
            // 
            // ControlOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xrayGroup);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ControlOptions";
            this.Size = new System.Drawing.Size(1076, 626);
            this.Load += new System.EventHandler(this.CustomOptionsControl_Load);
            this.xrayGroup.ResumeLayout(false);
            this.xrayGroup.PerformLayout();
            this.AuthRadioButtons.ResumeLayout(false);
            this.AuthRadioButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox xrayGroup;
        private System.Windows.Forms.TextBox textBoxPlatformUrl;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.TextBox serverUrl;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox testConnectionField;
        private System.Windows.Forms.RadioButton radioBtnAllVulnerabilities;
        private System.Windows.Forms.TextBox textBoxWatches;
        private System.Windows.Forms.TextBox textBoxProject;
        private System.Windows.Forms.RadioButton radioBtnWatches;
        private System.Windows.Forms.RadioButton radioBtnProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxXrayUrl;
        private System.Windows.Forms.TextBox textBoxArtifactoryUrl;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox artifactoryUrl;
        private System.Windows.Forms.CheckBox separateUrlCheckBox;
        private System.Windows.Forms.TextBox textBoxAccessToken;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox AuthRadioButtons;
        private System.Windows.Forms.RadioButton accessToken;
        private System.Windows.Forms.RadioButton basicAuth;
        private System.Windows.Forms.TextBox AuthMethod;
    }
}
