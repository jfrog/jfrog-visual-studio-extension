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
            this.watchesText = new System.Windows.Forms.TextBox();
            this.projectText = new System.Windows.Forms.TextBox();
            this.watches = new System.Windows.Forms.RadioButton();
            this.project = new System.Windows.Forms.RadioButton();
            this.allVulnerabilities = new System.Windows.Forms.RadioButton();
            this.txtBoxServer = new System.Windows.Forms.TextBox();
            this.txtBoxUser = new System.Windows.Forms.TextBox();
            this.txtBoxPassword = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.serverUrl = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.testConnectionField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xrayGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // xrayGroup
            // 
            this.xrayGroup.Controls.Add(this.label1);
            this.xrayGroup.Controls.Add(this.watchesText);
            this.xrayGroup.Controls.Add(this.projectText);
            this.xrayGroup.Controls.Add(this.watches);
            this.xrayGroup.Controls.Add(this.project);
            this.xrayGroup.Controls.Add(this.allVulnerabilities);
            this.xrayGroup.Controls.Add(this.txtBoxServer);
            this.xrayGroup.Controls.Add(this.txtBoxUser);
            this.xrayGroup.Controls.Add(this.txtBoxPassword);
            this.xrayGroup.Controls.Add(this.btnTestConnection);
            this.xrayGroup.Controls.Add(this.serverUrl);
            this.xrayGroup.Controls.Add(this.user);
            this.xrayGroup.Controls.Add(this.password);
            this.xrayGroup.Controls.Add(this.testConnectionField);
            this.xrayGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xrayGroup.Location = new System.Drawing.Point(0, 0);
            this.xrayGroup.Name = "xrayGroup";
            this.xrayGroup.Size = new System.Drawing.Size(408, 227);
            this.xrayGroup.TabIndex = 0;
            this.xrayGroup.TabStop = false;
            this.xrayGroup.Text = "JFrog Options";
            // 
            // watchesText
            // 
            this.watchesText.Location = new System.Drawing.Point(139, 196);
            this.watchesText.Name = "watchesText";
            this.watchesText.Size = new System.Drawing.Size(246, 20);
            this.watchesText.TabIndex = 49;
            // 
            // projectText
            // 
            this.projectText.Location = new System.Drawing.Point(139, 172);
            this.projectText.Name = "projectText";
            this.projectText.Size = new System.Drawing.Size(246, 20);
            this.projectText.TabIndex = 48;
            // 
            // watches
            // 
            this.watches.AutoSize = true;
            this.watches.Location = new System.Drawing.Point(4, 196);
            this.watches.Name = "watches";
            this.watches.Size = new System.Drawing.Size(131, 17);
            this.watches.TabIndex = 47;
            this.watches.TabStop = true;
            this.watches.Text = "According to Watches";
            this.watches.UseVisualStyleBackColor = true;
            this.watches.CheckedChanged += new System.EventHandler(this.watches_CheckedChanged);
            // 
            // project
            // 
            this.project.AutoSize = true;
            this.project.Location = new System.Drawing.Point(4, 172);
            this.project.Name = "project";
            this.project.Size = new System.Drawing.Size(120, 17);
            this.project.TabIndex = 46;
            this.project.TabStop = true;
            this.project.Text = "According to project";
            this.project.UseVisualStyleBackColor = true;
            this.project.CheckedChanged += new System.EventHandler(this.project_CheckedChanged);
            // 
            // allVulnerabilities
            // 
            this.allVulnerabilities.AutoSize = true;
            this.allVulnerabilities.Location = new System.Drawing.Point(4, 148);
            this.allVulnerabilities.Name = "allVulnerabilities";
            this.allVulnerabilities.Size = new System.Drawing.Size(102, 17);
            this.allVulnerabilities.TabIndex = 45;
            this.allVulnerabilities.TabStop = true;
            this.allVulnerabilities.Text = "All vulnerabilities";
            this.allVulnerabilities.UseVisualStyleBackColor = true;
            this.allVulnerabilities.CheckedChanged += new System.EventHandler(this.allVulnerabilities_CheckedChanged);
            // 
            // txtBoxServer
            // 
            this.txtBoxServer.Location = new System.Drawing.Point(76, 28);
            this.txtBoxServer.Name = "txtBoxServer";
            this.txtBoxServer.Size = new System.Drawing.Size(309, 20);
            this.txtBoxServer.TabIndex = 10;
            // 
            // txtBoxUser
            // 
            this.txtBoxUser.Location = new System.Drawing.Point(76, 54);
            this.txtBoxUser.Name = "txtBoxUser";
            this.txtBoxUser.Size = new System.Drawing.Size(309, 20);
            this.txtBoxUser.TabIndex = 20;
            // 
            // txtBoxPassword
            // 
            this.txtBoxPassword.Location = new System.Drawing.Point(76, 80);
            this.txtBoxPassword.Name = "txtBoxPassword";
            this.txtBoxPassword.PasswordChar = '*';
            this.txtBoxPassword.Size = new System.Drawing.Size(309, 20);
            this.txtBoxPassword.TabIndex = 30;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestConnection.Location = new System.Drawing.Point(293, 106);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(92, 23);
            this.btnTestConnection.TabIndex = 40;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.TestConnection);
            // 
            // serverUrl
            // 
            this.serverUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serverUrl.Location = new System.Drawing.Point(6, 28);
            this.serverUrl.Multiline = true;
            this.serverUrl.Name = "serverUrl";
            this.serverUrl.ReadOnly = true;
            this.serverUrl.Size = new System.Drawing.Size(53, 18);
            this.serverUrl.TabIndex = 41;
            this.serverUrl.TabStop = false;
            this.serverUrl.Text = "Xray Url";
            // 
            // user
            // 
            this.user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.user.Location = new System.Drawing.Point(6, 52);
            this.user.Multiline = true;
            this.user.Name = "user";
            this.user.ReadOnly = true;
            this.user.Size = new System.Drawing.Size(53, 18);
            this.user.TabIndex = 42;
            this.user.TabStop = false;
            this.user.Text = "User";
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.SystemColors.Control;
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password.Location = new System.Drawing.Point(6, 76);
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Size = new System.Drawing.Size(53, 18);
            this.password.TabIndex = 43;
            this.password.TabStop = false;
            this.password.Text = "Password";
            // 
            // testConnectionField
            // 
            this.testConnectionField.BackColor = System.Drawing.SystemColors.Control;
            this.testConnectionField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testConnectionField.Location = new System.Drawing.Point(6, 106);
            this.testConnectionField.Multiline = true;
            this.testConnectionField.Name = "testConnectionField";
            this.testConnectionField.ReadOnly = true;
            this.testConnectionField.Size = new System.Drawing.Size(260, 23);
            this.testConnectionField.TabIndex = 44;
            this.testConnectionField.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Scannig Policy";
            // 
            // ControlOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xrayGroup);
            this.Name = "ControlOptions";
            this.Size = new System.Drawing.Size(408, 227);
            this.Load += new System.EventHandler(this.CustomOptionsControl_Load);
            this.xrayGroup.ResumeLayout(false);
            this.xrayGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox xrayGroup;
        private System.Windows.Forms.TextBox txtBoxServer;
        private System.Windows.Forms.TextBox txtBoxUser;
        private System.Windows.Forms.TextBox txtBoxPassword;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.TextBox serverUrl;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox testConnectionField;
        private System.Windows.Forms.RadioButton allVulnerabilities;
        private System.Windows.Forms.TextBox watchesText;
        private System.Windows.Forms.TextBox projectText;
        private System.Windows.Forms.RadioButton watches;
        private System.Windows.Forms.RadioButton project;
        private System.Windows.Forms.Label label1;
    }
}
