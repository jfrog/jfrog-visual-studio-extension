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
            this.txtBoxServer = new System.Windows.Forms.TextBox();
            this.txtBoxUser = new System.Windows.Forms.TextBox();
            this.txtBoxPassword = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.serverUrl = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.testConnectionField = new System.Windows.Forms.TextBox();
            this.xrayGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // xrayGroup
            // 
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
            this.xrayGroup.Margin = new System.Windows.Forms.Padding(4);
            this.xrayGroup.Name = "xrayGroup";
            this.xrayGroup.Padding = new System.Windows.Forms.Padding(4);
            this.xrayGroup.Size = new System.Drawing.Size(541, 229);
            this.xrayGroup.TabIndex = 0;
            this.xrayGroup.TabStop = false;
            this.xrayGroup.Text = "JFrog Options";
            // 
            // txtBoxServer
            // 
            this.txtBoxServer.Location = new System.Drawing.Point(101, 50);
            this.txtBoxServer.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxServer.Name = "txtBoxServer";
            this.txtBoxServer.Size = new System.Drawing.Size(411, 22);
            this.txtBoxServer.TabIndex = 10;
            // 
            // txtBoxUser
            // 
            this.txtBoxUser.Location = new System.Drawing.Point(101, 80);
            this.txtBoxUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxUser.Name = "txtBoxUser";
            this.txtBoxUser.Size = new System.Drawing.Size(411, 22);
            this.txtBoxUser.TabIndex = 20;
            // 
            // txtBoxPassword
            // 
            this.txtBoxPassword.Location = new System.Drawing.Point(101, 110);
            this.txtBoxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxPassword.Name = "txtBoxPassword";
            this.txtBoxPassword.PasswordChar = '*';
            this.txtBoxPassword.Size = new System.Drawing.Size(411, 22);
            this.txtBoxPassword.TabIndex = 30;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestConnection.Location = new System.Drawing.Point(390, 144);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(4);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(122, 28);
            this.btnTestConnection.TabIndex = 40;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.performTestConnection);
            // 
            // serverUrl
            // 
            this.serverUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serverUrl.Location = new System.Drawing.Point(8, 50);
            this.serverUrl.Margin = new System.Windows.Forms.Padding(4);
            this.serverUrl.Multiline = true;
            this.serverUrl.Name = "serverUrl";
            this.serverUrl.ReadOnly = true;
            this.serverUrl.Size = new System.Drawing.Size(71, 22);
            this.serverUrl.TabIndex = 41;
            this.serverUrl.TabStop = false;
            this.serverUrl.Text = "Xray Url";
            // 
            // user
            // 
            this.user.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.user.Location = new System.Drawing.Point(8, 80);
            this.user.Margin = new System.Windows.Forms.Padding(4);
            this.user.Multiline = true;
            this.user.Name = "user";
            this.user.ReadOnly = true;
            this.user.Size = new System.Drawing.Size(71, 22);
            this.user.TabIndex = 42;
            this.user.TabStop = false;
            this.user.Text = "User";
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.SystemColors.Control;
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.password.Location = new System.Drawing.Point(8, 110);
            this.password.Margin = new System.Windows.Forms.Padding(4);
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.ReadOnly = true;
            this.password.Size = new System.Drawing.Size(71, 22);
            this.password.TabIndex = 43;
            this.password.TabStop = false;
            this.password.Text = "Password";
            // 
            // testConnectionField
            // 
            this.testConnectionField.BackColor = System.Drawing.SystemColors.Control;
            this.testConnectionField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testConnectionField.Location = new System.Drawing.Point(24, 150);
            this.testConnectionField.Margin = new System.Windows.Forms.Padding(4);
            this.testConnectionField.Multiline = true;
            this.testConnectionField.Name = "testConnectionField";
            this.testConnectionField.ReadOnly = true;
            this.testConnectionField.Size = new System.Drawing.Size(346, 71);
            this.testConnectionField.TabIndex = 44;
            this.testConnectionField.TabStop = false;
            // 
            // ControlOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xrayGroup);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ControlOptions";
            this.Size = new System.Drawing.Size(541, 229);
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
    }
}
