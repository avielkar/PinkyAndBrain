namespace PinkyAndBrain
{
    partial class GuiInterface
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
            this._protocolsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._protocolsFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this._protocolBrowserBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _protocolsComboBox
            // 
            this._protocolsComboBox.FormattingEnabled = true;
            this._protocolsComboBox.Location = new System.Drawing.Point(12, 54);
            this._protocolsComboBox.Name = "_protocolsComboBox";
            this._protocolsComboBox.Size = new System.Drawing.Size(193, 21);
            this._protocolsComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Protocols In Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Protocols Directory Path...";
            // 
            // _protocolBrowserBtn
            // 
            this._protocolBrowserBtn.Location = new System.Drawing.Point(211, 51);
            this._protocolBrowserBtn.Name = "_protocolBrowserBtn";
            this._protocolBrowserBtn.Size = new System.Drawing.Size(75, 23);
            this._protocolBrowserBtn.TabIndex = 3;
            this._protocolBrowserBtn.Text = "Browse Folder...";
            this._protocolBrowserBtn.UseVisualStyleBackColor = true;
            this._protocolBrowserBtn.Click += new System.EventHandler(this.protocolBrowserBtn_Click);
            // 
            // GuiInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 604);
            this.Controls.Add(this._protocolBrowserBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._protocolsComboBox);
            this.Name = "GuiInterface";
            this.Text = "GuiInterface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _protocolsComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog _protocolsFolderBrowser;
        private System.Windows.Forms.Button _protocolBrowserBtn;
    }
}