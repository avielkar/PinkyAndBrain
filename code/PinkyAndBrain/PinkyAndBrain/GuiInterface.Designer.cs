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
            this._startButton = new System.Windows.Forms.Button();
            this._varyingListBox = new System.Windows.Forms.ListBox();
            this._addVaryingCobination = new System.Windows.Forms.Button();
            this._removeVaryingCombination = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // _protocolsComboBox
            // 
            this._protocolsComboBox.FormattingEnabled = true;
            this._protocolsComboBox.Location = new System.Drawing.Point(12, 54);
            this._protocolsComboBox.Name = "_protocolsComboBox";
            this._protocolsComboBox.Size = new System.Drawing.Size(193, 21);
            this._protocolsComboBox.TabIndex = 0;
            this._protocolsComboBox.SelectedIndexChanged += new System.EventHandler(this._protocolsComboBox_SelectedIndexChanged);
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
            // _startButton
            // 
            this._startButton.Location = new System.Drawing.Point(829, 415);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(75, 23);
            this._startButton.TabIndex = 4;
            this._startButton.Text = "Start";
            this._startButton.UseVisualStyleBackColor = true;
            this._startButton.Click += new System.EventHandler(this._startButton_Click);
            // 
            // _varyingListBox
            // 
            this._varyingListBox.FormattingEnabled = true;
            this._varyingListBox.IntegralHeight = false;
            this._varyingListBox.Location = new System.Drawing.Point(1011, 54);
            this._varyingListBox.Name = "_varyingListBox";
            this._varyingListBox.Size = new System.Drawing.Size(240, 384);
            this._varyingListBox.TabIndex = 5;
            // 
            // _addVaryingCobination
            // 
            this._addVaryingCobination.Location = new System.Drawing.Point(1011, 444);
            this._addVaryingCobination.Name = "_addVaryingCobination";
            this._addVaryingCobination.Size = new System.Drawing.Size(114, 23);
            this._addVaryingCobination.TabIndex = 6;
            this._addVaryingCobination.Text = "Add Combination";
            this._addVaryingCobination.UseVisualStyleBackColor = true;
            this._addVaryingCobination.Click += new System.EventHandler(this._addVaryingCombination_Click);
            // 
            // _removeVaryingCombination
            // 
            this._removeVaryingCombination.Location = new System.Drawing.Point(1128, 444);
            this._removeVaryingCombination.Name = "_removeVaryingCombination";
            this._removeVaryingCombination.Size = new System.Drawing.Size(123, 23);
            this._removeVaryingCombination.TabIndex = 7;
            this._removeVaryingCombination.Text = "Remove Combination";
            this._removeVaryingCombination.UseVisualStyleBackColor = true;
            this._removeVaryingCombination.Click += new System.EventHandler(this._removeVaryingCombination_Click);
            // 
            // GuiInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 604);
            this.Controls.Add(this._removeVaryingCombination);
            this.Controls.Add(this._addVaryingCobination);
            this.Controls.Add(this._varyingListBox);
            this.Controls.Add(this._startButton);
            this.Controls.Add(this._protocolBrowserBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._protocolsComboBox);
            this.Name = "GuiInterface";
            this.Text = "GuiInterface";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GuiInterface_Close);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _protocolsComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog _protocolsFolderBrowser;
        private System.Windows.Forms.Button _protocolBrowserBtn;
        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.ListBox _varyingListBox;
        private System.Windows.Forms.Button _addVaryingCobination;
        private System.Windows.Forms.Button _removeVaryingCombination;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}