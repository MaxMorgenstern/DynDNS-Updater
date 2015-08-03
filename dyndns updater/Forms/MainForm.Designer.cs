namespace DynDNS_Updater
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutDynDNSUpdaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IPTempBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LogBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.UserToken = new System.Windows.Forms.TextBox();
            this.MainFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Placeholder_StripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.MainFormStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.startToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.aboutDynDNSUpdaterToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // aboutDynDNSUpdaterToolStripMenuItem
            // 
            this.aboutDynDNSUpdaterToolStripMenuItem.Name = "aboutDynDNSUpdaterToolStripMenuItem";
            this.aboutDynDNSUpdaterToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.aboutDynDNSUpdaterToolStripMenuItem.Text = "About DynDNS Updater";
            this.aboutDynDNSUpdaterToolStripMenuItem.Click += new System.EventHandler(this.aboutDynDNSUpdaterToolStripMenuItem_Click);
            // 
            // IPTempBox
            // 
            this.IPTempBox.Enabled = false;
            this.IPTempBox.Location = new System.Drawing.Point(81, 83);
            this.IPTempBox.Name = "IPTempBox";
            this.IPTempBox.Size = new System.Drawing.Size(241, 20);
            this.IPTempBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current IP:";
            // 
            // LogBox
            // 
            this.LogBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LogBox.FormattingEnabled = true;
            this.LogBox.HorizontalScrollbar = true;
            this.LogBox.Location = new System.Drawing.Point(9, 116);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(313, 238);
            this.LogBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Token:";
            // 
            // UserName
            // 
            this.UserName.Enabled = false;
            this.UserName.Location = new System.Drawing.Point(81, 31);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(241, 20);
            this.UserName.TabIndex = 6;
            // 
            // UserToken
            // 
            this.UserToken.Enabled = false;
            this.UserToken.Location = new System.Drawing.Point(81, 57);
            this.UserToken.Name = "UserToken";
            this.UserToken.Size = new System.Drawing.Size(241, 20);
            this.UserToken.TabIndex = 7;
            // 
            // MainFormStatusStrip
            // 
            this.MainFormStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripStatusLabel,
            this.Placeholder_StripStatusLabel,
            this.UpdateStripStatusLabel});
            this.MainFormStatusStrip.Location = new System.Drawing.Point(0, 368);
            this.MainFormStatusStrip.Name = "MainFormStatusStrip";
            this.MainFormStatusStrip.Size = new System.Drawing.Size(334, 22);
            this.MainFormStatusStrip.SizingGrip = false;
            this.MainFormStatusStrip.TabIndex = 8;
            this.MainFormStatusStrip.Text = "statusStrip1";
            // 
            // StatusStripStatusLabel
            // 
            this.StatusStripStatusLabel.Name = "StatusStripStatusLabel";
            this.StatusStripStatusLabel.Size = new System.Drawing.Size(40, 17);
            this.StatusStripStatusLabel.Text = "Active";
            // 
            // UpdateStripStatusLabel
            // 
            this.UpdateStripStatusLabel.Name = "UpdateStripStatusLabel";
            this.UpdateStripStatusLabel.Size = new System.Drawing.Size(96, 17);
            this.UpdateStripStatusLabel.Text = "Update Available";
            this.UpdateStripStatusLabel.Click += new System.EventHandler(this.Update_StripStatusLabel_Click);
            // 
            // Placeholder_StripStatusLabel
            // 
            this.Placeholder_StripStatusLabel.Name = "Placeholder_StripStatusLabel";
            this.Placeholder_StripStatusLabel.Size = new System.Drawing.Size(152, 17);
            this.Placeholder_StripStatusLabel.Spring = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 390);
            this.Controls.Add(this.MainFormStatusStrip);
            this.Controls.Add(this.UserToken);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPTempBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "DynDNS Updater";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainFormStatusStrip.ResumeLayout(false);
            this.MainFormStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutDynDNSUpdaterToolStripMenuItem;
        private System.Windows.Forms.TextBox IPTempBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox LogBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox UserToken;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip MainFormStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel Placeholder_StripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel UpdateStripStatusLabel;
    }
}

