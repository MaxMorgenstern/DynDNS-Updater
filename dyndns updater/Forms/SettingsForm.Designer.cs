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
            this.ProviderComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.v6RadioButton = new System.Windows.Forms.RadioButton();
            this.v4RadioButton = new System.Windows.Forms.RadioButton();
            this.UserToken = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.enableButton = new System.Windows.Forms.Button();
            this.disableButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LogfileCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProviderComboBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.v6RadioButton);
            this.groupBox1.Controls.Add(this.v4RadioButton);
            this.groupBox1.Controls.Add(this.UserToken);
            this.groupBox1.Controls.Add(this.UserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 127);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DynDNS Account Settings";
            // 
            // ProviderComboBox
            // 
            this.ProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProviderComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ProviderComboBox.FormattingEnabled = true;
            this.ProviderComboBox.Location = new System.Drawing.Point(98, 19);
            this.ProviderComboBox.Name = "ProviderComboBox";
            this.ProviderComboBox.Size = new System.Drawing.Size(172, 21);
            this.ProviderComboBox.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Provider:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "IP Type:";
            // 
            // v6RadioButton
            // 
            this.v6RadioButton.AutoSize = true;
            this.v6RadioButton.Location = new System.Drawing.Point(190, 100);
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
            this.v4RadioButton.Location = new System.Drawing.Point(98, 100);
            this.v4RadioButton.Name = "v4RadioButton";
            this.v4RadioButton.Size = new System.Drawing.Size(47, 17);
            this.v4RadioButton.TabIndex = 14;
            this.v4RadioButton.TabStop = true;
            this.v4RadioButton.Text = "IPv4";
            this.v4RadioButton.UseVisualStyleBackColor = true;
            // 
            // UserToken
            // 
            this.UserToken.Location = new System.Drawing.Point(98, 72);
            this.UserToken.Name = "UserToken";
            this.UserToken.Size = new System.Drawing.Size(172, 20);
            this.UserToken.TabIndex = 13;
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(98, 46);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(172, 20);
            this.UserName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Token:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Username:";
            // 
            // MinimizedCheckBox
            // 
            this.MinimizedCheckBox.AutoSize = true;
            this.MinimizedCheckBox.Location = new System.Drawing.Point(98, 50);
            this.MinimizedCheckBox.Name = "MinimizedCheckBox";
            this.MinimizedCheckBox.Size = new System.Drawing.Size(44, 17);
            this.MinimizedCheckBox.TabIndex = 21;
            this.MinimizedCheckBox.Text = "Yes";
            this.MinimizedCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Start Minimized:";
            // 
            // enableButton
            // 
            this.enableButton.Location = new System.Drawing.Point(98, 19);
            this.enableButton.Name = "enableButton";
            this.enableButton.Size = new System.Drawing.Size(80, 23);
            this.enableButton.TabIndex = 19;
            this.enableButton.Text = "Enable";
            this.enableButton.UseVisualStyleBackColor = true;
            this.enableButton.Click += new System.EventHandler(this.enableButton_Click);
            // 
            // disableButton
            // 
            this.disableButton.Location = new System.Drawing.Point(190, 19);
            this.disableButton.Name = "disableButton";
            this.disableButton.Size = new System.Drawing.Size(80, 23);
            this.disableButton.TabIndex = 18;
            this.disableButton.Text = "Disable";
            this.disableButton.UseVisualStyleBackColor = true;
            this.disableButton.Click += new System.EventHandler(this.disableButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Autostart:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(134, 249);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(215, 249);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LogfileCheckBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.MinimizedCheckBox);
            this.groupBox2.Controls.Add(this.enableButton);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.disableButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 98);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "App Settings";
            // 
            // LogfileCheckBox
            // 
            this.LogfileCheckBox.AutoSize = true;
            this.LogfileCheckBox.Location = new System.Drawing.Point(98, 73);
            this.LogfileCheckBox.Name = "LogfileCheckBox";
            this.LogfileCheckBox.Size = new System.Drawing.Size(44, 17);
            this.LogfileCheckBox.TabIndex = 23;
            this.LogfileCheckBox.Text = "Yes";
            this.LogfileCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Write Log:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 282);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button enableButton;
        private System.Windows.Forms.Button disableButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox MinimizedCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox LogfileCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ProviderComboBox;
        private System.Windows.Forms.Label label7;
    }
}