namespace DefaultTerminal
{
    partial class FormMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.checkRunOnStartup = new System.Windows.Forms.CheckBox();
            this.groupAdvanced = new System.Windows.Forms.GroupBox();
            this.textProcessNames = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.labelAdvancedDescription = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupAbout = new System.Windows.Forms.GroupBox();
            this.linkGitHub = new System.Windows.Forms.LinkLabel();
            this.labelVersion = new System.Windows.Forms.Label();
            this.groupBoxGeneral.SuspendLayout();
            this.groupAdvanced.SuspendLayout();
            this.groupAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGeneral.Controls.Add(this.checkRunOnStartup);
            this.groupBoxGeneral.Location = new System.Drawing.Point(12, 12);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(460, 59);
            this.groupBoxGeneral.TabIndex = 1;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "General";
            // 
            // checkRunOnStartup
            // 
            this.checkRunOnStartup.AutoSize = true;
            this.checkRunOnStartup.Location = new System.Drawing.Point(18, 27);
            this.checkRunOnStartup.Name = "checkRunOnStartup";
            this.checkRunOnStartup.Size = new System.Drawing.Size(227, 16);
            this.checkRunOnStartup.TabIndex = 1;
            this.checkRunOnStartup.Text = "Run automatically at system startup";
            this.checkRunOnStartup.UseVisualStyleBackColor = true;
            // 
            // groupAdvanced
            // 
            this.groupAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAdvanced.Controls.Add(this.labelAdvancedDescription);
            this.groupAdvanced.Controls.Add(this.textProcessNames);
            this.groupAdvanced.Location = new System.Drawing.Point(12, 84);
            this.groupAdvanced.Name = "groupAdvanced";
            this.groupAdvanced.Size = new System.Drawing.Size(460, 109);
            this.groupAdvanced.TabIndex = 2;
            this.groupAdvanced.TabStop = false;
            this.groupAdvanced.Text = "Advanced";
            // 
            // textProcessNames
            // 
            this.textProcessNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textProcessNames.Location = new System.Drawing.Point(18, 67);
            this.textProcessNames.Name = "textProcessNames";
            this.textProcessNames.Size = new System.Drawing.Size(424, 21);
            this.textProcessNames.TabIndex = 0;
            this.textProcessNames.Text = "explorer.exe|devenv.exe";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(351, 326);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // labelAdvancedDescription
            // 
            this.labelAdvancedDescription.AutoSize = true;
            this.labelAdvancedDescription.Location = new System.Drawing.Point(16, 27);
            this.labelAdvancedDescription.Name = "labelAdvancedDescription";
            this.labelAdvancedDescription.Size = new System.Drawing.Size(395, 24);
            this.labelAdvancedDescription.TabIndex = 1;
            this.labelAdvancedDescription.Text = "Enter the target process to change the default console.\r\nPlease note that changin" +
    "g this setting can lead to unexpected errors.";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(258, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupAbout
            // 
            this.groupAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAbout.Controls.Add(this.labelVersion);
            this.groupAbout.Controls.Add(this.linkGitHub);
            this.groupAbout.Location = new System.Drawing.Point(12, 207);
            this.groupAbout.Name = "groupAbout";
            this.groupAbout.Size = new System.Drawing.Size(460, 87);
            this.groupAbout.TabIndex = 2;
            this.groupAbout.TabStop = false;
            this.groupAbout.Text = "About";
            // 
            // linkGitHub
            // 
            this.linkGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkGitHub.Location = new System.Drawing.Point(18, 23);
            this.linkGitHub.Name = "linkGitHub";
            this.linkGitHub.Size = new System.Drawing.Size(424, 23);
            this.linkGitHub.TabIndex = 1;
            this.linkGitHub.TabStop = true;
            this.linkGitHub.Text = "https://github.com/iodes/DefaultTerminal";
            this.linkGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.Location = new System.Drawing.Point(18, 48);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(416, 22);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Version 1.0.0.0 / © 2020 Kevin So.";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.groupAbout);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupAdvanced);
            this.Controls.Add(this.groupBoxGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DefaultTerminal";
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.groupAdvanced.ResumeLayout(false);
            this.groupAdvanced.PerformLayout();
            this.groupAbout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.CheckBox checkRunOnStartup;
        private System.Windows.Forms.GroupBox groupAdvanced;
        private System.Windows.Forms.TextBox textProcessNames;
        private System.Windows.Forms.Label labelAdvancedDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupAbout;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel linkGitHub;
    }
}

