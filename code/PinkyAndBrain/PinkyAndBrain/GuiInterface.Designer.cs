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
            this._makeTrials = new System.Windows.Forms.Button();
            this._stopButtom = new System.Windows.Forms.Button();
            this._trialDetailsListView = new System.Windows.Forms.ListView();
            this._trialParametersGroup = new System.Windows.Forms.GroupBox();
            this._dynamicParametersPanel = new System.Windows.Forms.Panel();
            this._handRewardsgroupBox = new System.Windows.Forms.GroupBox();
            this._rightHandRewardCheckBox = new System.Windows.Forms.CheckBox();
            this._centerHandRewardCheckBox = new System.Windows.Forms.CheckBox();
            this._leftHandRewardCheckBox = new System.Windows.Forms.CheckBox();
            this._digitalHandRewardButton = new System.Windows.Forms.Button();
            this._continiousHandRewardButton = new System.Windows.Forms.Button();
            this._varyingControlGroupBox = new System.Windows.Forms.GroupBox();
            this._interactiveNolduscommuncation = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this._rightNoldusCommunicationRadioButton = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this._centerNoldusCommunicationRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._leftNoldusCommunicationRadioButton = new System.Windows.Forms.RadioButton();
            this._drinkControlGroupBox = new System.Windows.Forms.GroupBox();
            this._waterRewardMeasure = new WaterMeasure.WaterMeasure();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._trialParametersGroup.SuspendLayout();
            this._handRewardsgroupBox.SuspendLayout();
            this._varyingControlGroupBox.SuspendLayout();
            this._interactiveNolduscommuncation.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this._drinkControlGroupBox.SuspendLayout();
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
            this._startButton.Location = new System.Drawing.Point(6, 409);
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
            this._varyingListBox.Location = new System.Drawing.Point(87, 19);
            this._varyingListBox.Name = "_varyingListBox";
            this._varyingListBox.Size = new System.Drawing.Size(240, 384);
            this._varyingListBox.TabIndex = 5;
            // 
            // _addVaryingCobination
            // 
            this._addVaryingCobination.Location = new System.Drawing.Point(87, 409);
            this._addVaryingCobination.Name = "_addVaryingCobination";
            this._addVaryingCobination.Size = new System.Drawing.Size(114, 23);
            this._addVaryingCobination.TabIndex = 6;
            this._addVaryingCobination.Text = "Add Combination";
            this._addVaryingCobination.UseVisualStyleBackColor = true;
            this._addVaryingCobination.Click += new System.EventHandler(this._addVaryingCombination_Click);
            // 
            // _removeVaryingCombination
            // 
            this._removeVaryingCombination.Location = new System.Drawing.Point(204, 409);
            this._removeVaryingCombination.Name = "_removeVaryingCombination";
            this._removeVaryingCombination.Size = new System.Drawing.Size(123, 23);
            this._removeVaryingCombination.TabIndex = 7;
            this._removeVaryingCombination.Text = "Remove Combination";
            this._removeVaryingCombination.UseVisualStyleBackColor = true;
            this._removeVaryingCombination.Click += new System.EventHandler(this._removeVaryingCombination_Click);
            // 
            // _makeTrials
            // 
            this._makeTrials.Location = new System.Drawing.Point(6, 380);
            this._makeTrials.Name = "_makeTrials";
            this._makeTrials.Size = new System.Drawing.Size(75, 23);
            this._makeTrials.TabIndex = 8;
            this._makeTrials.Text = "MakeTrials";
            this._makeTrials.UseVisualStyleBackColor = true;
            this._makeTrials.Click += new System.EventHandler(this._makeTrials_Click);
            // 
            // _stopButtom
            // 
            this._stopButtom.Location = new System.Drawing.Point(6, 438);
            this._stopButtom.Name = "_stopButtom";
            this._stopButtom.Size = new System.Drawing.Size(75, 23);
            this._stopButtom.TabIndex = 9;
            this._stopButtom.Text = "Stop";
            this._stopButtom.UseVisualStyleBackColor = true;
            this._stopButtom.Click += new System.EventHandler(this._stopButtom_Click);
            // 
            // _trialDetailsListView
            // 
            this._trialDetailsListView.HoverSelection = true;
            this._trialDetailsListView.Location = new System.Drawing.Point(15, 719);
            this._trialDetailsListView.Name = "_trialDetailsListView";
            this._trialDetailsListView.Size = new System.Drawing.Size(711, 181);
            this._trialDetailsListView.TabIndex = 10;
            this._trialDetailsListView.UseCompatibleStateImageBehavior = false;
            // 
            // _trialParametersGroup
            // 
            this._trialParametersGroup.Controls.Add(this._dynamicParametersPanel);
            this._trialParametersGroup.Location = new System.Drawing.Point(15, 81);
            this._trialParametersGroup.Name = "_trialParametersGroup";
            this._trialParametersGroup.Size = new System.Drawing.Size(909, 620);
            this._trialParametersGroup.TabIndex = 11;
            this._trialParametersGroup.TabStop = false;
            this._trialParametersGroup.Text = "Parameters";
            // 
            // _dynamicParametersPanel
            // 
            this._dynamicParametersPanel.AutoScroll = true;
            this._dynamicParametersPanel.Location = new System.Drawing.Point(6, 19);
            this._dynamicParametersPanel.Name = "_dynamicParametersPanel";
            this._dynamicParametersPanel.Size = new System.Drawing.Size(897, 595);
            this._dynamicParametersPanel.TabIndex = 12;
            // 
            // _handRewardsgroupBox
            // 
            this._handRewardsgroupBox.Controls.Add(this._rightHandRewardCheckBox);
            this._handRewardsgroupBox.Controls.Add(this._centerHandRewardCheckBox);
            this._handRewardsgroupBox.Controls.Add(this._leftHandRewardCheckBox);
            this._handRewardsgroupBox.Controls.Add(this._digitalHandRewardButton);
            this._handRewardsgroupBox.Controls.Add(this._continiousHandRewardButton);
            this._handRewardsgroupBox.Location = new System.Drawing.Point(732, 719);
            this._handRewardsgroupBox.Name = "_handRewardsgroupBox";
            this._handRewardsgroupBox.Size = new System.Drawing.Size(192, 181);
            this._handRewardsgroupBox.TabIndex = 12;
            this._handRewardsgroupBox.TabStop = false;
            this._handRewardsgroupBox.Text = "Hand Rewards";
            // 
            // _rightHandRewardCheckBox
            // 
            this._rightHandRewardCheckBox.AutoSize = true;
            this._rightHandRewardCheckBox.Location = new System.Drawing.Point(78, 86);
            this._rightHandRewardCheckBox.Name = "_rightHandRewardCheckBox";
            this._rightHandRewardCheckBox.Size = new System.Drawing.Size(46, 17);
            this._rightHandRewardCheckBox.TabIndex = 8;
            this._rightHandRewardCheckBox.Text = "right";
            this._rightHandRewardCheckBox.UseVisualStyleBackColor = true;
            this._rightHandRewardCheckBox.CheckedChanged += new System.EventHandler(this._rightHandRewardCheckBox_CheckedChanged);
            // 
            // _centerHandRewardCheckBox
            // 
            this._centerHandRewardCheckBox.AutoSize = true;
            this._centerHandRewardCheckBox.Location = new System.Drawing.Point(78, 52);
            this._centerHandRewardCheckBox.Name = "_centerHandRewardCheckBox";
            this._centerHandRewardCheckBox.Size = new System.Drawing.Size(56, 17);
            this._centerHandRewardCheckBox.TabIndex = 7;
            this._centerHandRewardCheckBox.Text = "center";
            this._centerHandRewardCheckBox.UseVisualStyleBackColor = true;
            this._centerHandRewardCheckBox.CheckedChanged += new System.EventHandler(this._centerHandRewardCheckBox_CheckedChanged);
            // 
            // _leftHandRewardCheckBox
            // 
            this._leftHandRewardCheckBox.AutoSize = true;
            this._leftHandRewardCheckBox.Location = new System.Drawing.Point(78, 19);
            this._leftHandRewardCheckBox.Name = "_leftHandRewardCheckBox";
            this._leftHandRewardCheckBox.Size = new System.Drawing.Size(40, 17);
            this._leftHandRewardCheckBox.TabIndex = 6;
            this._leftHandRewardCheckBox.Text = "left";
            this._leftHandRewardCheckBox.UseVisualStyleBackColor = true;
            this._leftHandRewardCheckBox.CheckedChanged += new System.EventHandler(this._leftHandRewardCheckBox_CheckedChanged);
            // 
            // _digitalHandRewardButton
            // 
            this._digitalHandRewardButton.Location = new System.Drawing.Point(6, 123);
            this._digitalHandRewardButton.Name = "_digitalHandRewardButton";
            this._digitalHandRewardButton.Size = new System.Drawing.Size(75, 23);
            this._digitalHandRewardButton.TabIndex = 5;
            this._digitalHandRewardButton.Text = "Digital";
            this._digitalHandRewardButton.UseVisualStyleBackColor = true;
            this._digitalHandRewardButton.Click += new System.EventHandler(this._digitalHandRewardButton_Click);
            // 
            // _continiousHandRewardButton
            // 
            this._continiousHandRewardButton.Location = new System.Drawing.Point(111, 123);
            this._continiousHandRewardButton.Name = "_continiousHandRewardButton";
            this._continiousHandRewardButton.Size = new System.Drawing.Size(75, 23);
            this._continiousHandRewardButton.TabIndex = 4;
            this._continiousHandRewardButton.Text = "Continious";
            this._continiousHandRewardButton.UseVisualStyleBackColor = true;
            this._continiousHandRewardButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this._countiniousHandRewardKeyDown);
            this._continiousHandRewardButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this._continiousHandRewardKeyReleaed);
            // 
            // _varyingControlGroupBox
            // 
            this._varyingControlGroupBox.Controls.Add(this._varyingListBox);
            this._varyingControlGroupBox.Controls.Add(this._startButton);
            this._varyingControlGroupBox.Controls.Add(this._makeTrials);
            this._varyingControlGroupBox.Controls.Add(this._addVaryingCobination);
            this._varyingControlGroupBox.Controls.Add(this._stopButtom);
            this._varyingControlGroupBox.Controls.Add(this._removeVaryingCombination);
            this._varyingControlGroupBox.Location = new System.Drawing.Point(930, 81);
            this._varyingControlGroupBox.Name = "_varyingControlGroupBox";
            this._varyingControlGroupBox.Size = new System.Drawing.Size(339, 468);
            this._varyingControlGroupBox.TabIndex = 13;
            this._varyingControlGroupBox.TabStop = false;
            this._varyingControlGroupBox.Text = "Varying Control";
            // 
            // _interactiveNolduscommuncation
            // 
            this._interactiveNolduscommuncation.Controls.Add(this.panel3);
            this._interactiveNolduscommuncation.Controls.Add(this.panel2);
            this._interactiveNolduscommuncation.Controls.Add(this.panel1);
            this._interactiveNolduscommuncation.Location = new System.Drawing.Point(1036, 555);
            this._interactiveNolduscommuncation.Name = "_interactiveNolduscommuncation";
            this._interactiveNolduscommuncation.Size = new System.Drawing.Size(233, 146);
            this._interactiveNolduscommuncation.TabIndex = 14;
            this._interactiveNolduscommuncation.TabStop = false;
            this._interactiveNolduscommuncation.Text = "Interacive Noldus Communication";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._rightNoldusCommunicationRadioButton);
            this.panel3.Location = new System.Drawing.Point(165, 61);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(47, 33);
            this.panel3.TabIndex = 5;
            // 
            // _rightNoldusCommunicationRadioButton
            // 
            this._rightNoldusCommunicationRadioButton.AutoSize = true;
            this._rightNoldusCommunicationRadioButton.Location = new System.Drawing.Point(19, 17);
            this._rightNoldusCommunicationRadioButton.Name = "_rightNoldusCommunicationRadioButton";
            this._rightNoldusCommunicationRadioButton.Size = new System.Drawing.Size(14, 13);
            this._rightNoldusCommunicationRadioButton.TabIndex = 2;
            this._rightNoldusCommunicationRadioButton.TabStop = true;
            this._rightNoldusCommunicationRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._centerNoldusCommunicationRadioButton);
            this.panel2.Location = new System.Drawing.Point(91, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(43, 34);
            this.panel2.TabIndex = 4;
            // 
            // _centerNoldusCommunicationRadioButton
            // 
            this._centerNoldusCommunicationRadioButton.AutoSize = true;
            this._centerNoldusCommunicationRadioButton.Location = new System.Drawing.Point(13, 18);
            this._centerNoldusCommunicationRadioButton.Name = "_centerNoldusCommunicationRadioButton";
            this._centerNoldusCommunicationRadioButton.Size = new System.Drawing.Size(14, 13);
            this._centerNoldusCommunicationRadioButton.TabIndex = 1;
            this._centerNoldusCommunicationRadioButton.TabStop = true;
            this._centerNoldusCommunicationRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._leftNoldusCommunicationRadioButton);
            this.panel1.Location = new System.Drawing.Point(19, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(42, 34);
            this.panel1.TabIndex = 3;
            // 
            // _leftNoldusCommunicationRadioButton
            // 
            this._leftNoldusCommunicationRadioButton.AutoSize = true;
            this._leftNoldusCommunicationRadioButton.Location = new System.Drawing.Point(14, 18);
            this._leftNoldusCommunicationRadioButton.Name = "_leftNoldusCommunicationRadioButton";
            this._leftNoldusCommunicationRadioButton.Size = new System.Drawing.Size(14, 13);
            this._leftNoldusCommunicationRadioButton.TabIndex = 0;
            this._leftNoldusCommunicationRadioButton.TabStop = true;
            this._leftNoldusCommunicationRadioButton.UseVisualStyleBackColor = true;
            // 
            // _drinkControlGroupBox
            // 
            this._drinkControlGroupBox.Controls.Add(this.label4);
            this._drinkControlGroupBox.Controls.Add(this.label3);
            this._drinkControlGroupBox.Controls.Add(this._waterRewardMeasure);
            this._drinkControlGroupBox.Location = new System.Drawing.Point(931, 555);
            this._drinkControlGroupBox.Name = "_drinkControlGroupBox";
            this._drinkControlGroupBox.Size = new System.Drawing.Size(99, 345);
            this._drinkControlGroupBox.TabIndex = 15;
            this._drinkControlGroupBox.TabStop = false;
            this._drinkControlGroupBox.Text = "Drink Control";
            // 
            // _waterRewardMeasure
            // 
            this._waterRewardMeasure.BackColor = System.Drawing.Color.Transparent;
            this._waterRewardMeasure.Image = null;
            this._waterRewardMeasure.Location = new System.Drawing.Point(14, 45);
            this._waterRewardMeasure.Name = "_waterRewardMeasure";
            this._waterRewardMeasure.ProgressColor = System.Drawing.Color.Aqua;
            this._waterRewardMeasure.ProgressDirection = WaterMeasure.WaterMeasure.ProgressDir.Vertical;
            this._waterRewardMeasure.ShowPercentage = true;
            this._waterRewardMeasure.Size = new System.Drawing.Size(44, 294);
            this._waterRewardMeasure.Text = "ml";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "60ml";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "0ml";
            // 
            // GuiInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 912);
            this.Controls.Add(this._drinkControlGroupBox);
            this.Controls.Add(this._interactiveNolduscommuncation);
            this.Controls.Add(this._varyingControlGroupBox);
            this.Controls.Add(this._handRewardsgroupBox);
            this.Controls.Add(this._trialParametersGroup);
            this.Controls.Add(this._trialDetailsListView);
            this.Controls.Add(this._protocolBrowserBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._protocolsComboBox);
            this.Name = "GuiInterface";
            this.Text = "GuiInterface";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GuiInterface_Close);
            this._trialParametersGroup.ResumeLayout(false);
            this._handRewardsgroupBox.ResumeLayout(false);
            this._handRewardsgroupBox.PerformLayout();
            this._varyingControlGroupBox.ResumeLayout(false);
            this._interactiveNolduscommuncation.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._drinkControlGroupBox.ResumeLayout(false);
            this._drinkControlGroupBox.PerformLayout();
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
        private System.Windows.Forms.Button _makeTrials;
        private System.Windows.Forms.Button _stopButtom;
        private System.Windows.Forms.ListView _trialDetailsListView;
        private System.Windows.Forms.GroupBox _trialParametersGroup;
        private System.Windows.Forms.Panel _dynamicParametersPanel;
        private System.Windows.Forms.GroupBox _handRewardsgroupBox;
        private System.Windows.Forms.Button _digitalHandRewardButton;
        private System.Windows.Forms.Button _continiousHandRewardButton;
        private System.Windows.Forms.CheckBox _rightHandRewardCheckBox;
        private System.Windows.Forms.CheckBox _centerHandRewardCheckBox;
        private System.Windows.Forms.CheckBox _leftHandRewardCheckBox;
        private System.Windows.Forms.GroupBox _varyingControlGroupBox;
        private System.Windows.Forms.GroupBox _interactiveNolduscommuncation;
        private System.Windows.Forms.RadioButton _rightNoldusCommunicationRadioButton;
        private System.Windows.Forms.RadioButton _centerNoldusCommunicationRadioButton;
        private System.Windows.Forms.RadioButton _leftNoldusCommunicationRadioButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox _drinkControlGroupBox;
        private WaterMeasure.WaterMeasure _waterRewardMeasure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}