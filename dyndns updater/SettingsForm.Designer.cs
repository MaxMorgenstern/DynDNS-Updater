namespace DynDNS_Updater
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.v6RadioButton = new System.Windows.Forms.RadioButton();
            this.v4RadioButton = new System.Windows.Forms.RadioButton();
            this.UserToken = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.v6RadioButton);
            this.groupBox1.Controls.Add(this.v4RadioButton);
            this.groupBox1.Controls.Add(this.UserToken);
            this.groupBox1.Controls.Add(this.UserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DynDNS Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "IP Type:";
            // 
            // v6RadioButton
            // 
            this.v6RadioButton.AutoSize = true;
            this.v6RadioButton.Location = new System.Drawing.Point(207, 77);
            this.v6RadioButton.Name = "v6RadioButton";
            this.v6RadioButton.Size = new System.Drawing.Size(47, 17);
            this.v6RadioButton.TabIndex = 15;
            this.v6RadioButton.TabStop = true;
            this.v6RadioButton.Text = "IPv6";
            this.v6RadioButton.UseVisualStyleBackColor = true;
            // 
            // v4RadioButton
            // 
            this.v4RadioButton.AutoSize = true;
            this.v4RadioButton.Location = new System.Drawing.Point(132, 77);
            this.v4RadioButton.Name = "v4RadioButton";
            this.v4RadioButton.Size = new System.Drawing.Size(47, 17);
            this.v4RadioButton.TabIndex = 14;
            this.v4RadioButton.TabStop = true;
            this.v4RadioButton.Text = "IPv4";
            this.v4RadioButton.UseVisualStyleBackColor = true;
            // 
            // UserToken
            // 
            this.UserToken.Location = new System.Drawing.Point(82, 50);
            this.UserToken.Name = "UserToken";
            this.UserToken.Size = new System.Drawing.Size(172, 20);
            this.UserToken.TabIndex = 13;
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(82, 24);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(172, 20);
            this.UserName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Token:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Username:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(116, 119);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(197, 119);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 148);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton v6RadioButton;
        private System.Windows.Forms.RadioButton v4RadioButton;
        private System.Windows.Forms.TextBox UserToken;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}