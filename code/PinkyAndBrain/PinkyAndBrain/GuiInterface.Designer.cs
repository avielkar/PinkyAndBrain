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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this._autoRewardsCheckBox = new System.Windows.Forms.CheckBox();
            this._varyingControlGroupBox = new System.Windows.Forms.GroupBox();
            this._btnResume = new System.Windows.Forms.Button();
            this._btnPause = new System.Windows.Forms.Button();
            this._textboxStickOnNumber = new System.Windows.Forms.TextBox();
            this._labelStickOnNumber = new System.Windows.Forms.Label();
            this._numOfRepetitionsTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._interactiveNolduscommuncation = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this._rightNoldusCommunicationRadioButton = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this._centerNoldusCommunicationRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._leftNoldusCommunicationRadioButton = new System.Windows.Forms.RadioButton();
            this._drinkControlGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._waterRewardMeasure = new WaterMeasure.WaterMeasure();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this._selectedRatGroupBox = new System.Windows.Forms.GroupBox();
            this._selectedRatNameComboBox = new System.Windows.Forms.ComboBox();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this._guiInterfaceToolTip = new System.Windows.Forms.ToolTip(this.components);
            this._trialInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.globalExperimentInfogroupBox = new System.Windows.Forms.GroupBox();
            this._globaExperimentlInfoListView = new System.Windows.Forms.ListView();
            this._onlinePsychGrpahGroupBox = new System.Windows.Forms.GroupBox();
            this._onlinePsychGraphControl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._autosGroupBox = new System.Windows.Forms.GroupBox();
            this._autoStartCheckBox = new System.Windows.Forms.CheckBox();
            this._autoFixationCheckBox = new System.Windows.Forms.CheckBox();
            this._warningsGroupBox = new System.Windows.Forms.GroupBox();
            this._ardionoPrtWarningLabel = new System.Windows.Forms.Label();
            this._autoRewardSound = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._breakFixationSoundEnableCheckBox = new System.Windows.Forms.CheckBox();
            this._fixationOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this._groupboxLedsController = new System.Windows.Forms.GroupBox();
            this._textboxLEDBrightness = new System.Windows.Forms.TextBox();
            this._labelBrightness = new System.Windows.Forms.Label();
            this._textboxPercentageOfTurnOnLeds = new System.Windows.Forms.TextBox();
            this._labelPercentageOfturnedOnLeds = new System.Windows.Forms.Label();
            this._btnPark = new System.Windows.Forms.Button();
            this._trialParametersGroup.SuspendLayout();
            this._handRewardsgroupBox.SuspendLayout();
            this._varyingControlGroupBox.SuspendLayout();
            this._interactiveNolduscommuncation.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this._drinkControlGroupBox.SuspendLayout();
            this._selectedRatGroupBox.SuspendLayout();
            this._trialInfoGroupBox.SuspendLayout();
            this.globalExperimentInfogroupBox.SuspendLayout();
            this._onlinePsychGrpahGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._onlinePsychGraphControl)).BeginInit();
            this._autosGroupBox.SuspendLayout();
            this._warningsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this._groupboxLedsController.SuspendLayout();
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
            // _protocolsFolderBrowser
            // 
            this._protocolsFolderBrowser.SelectedPath = "C:\\Users\\User\\Desktop\\protocols";
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
            this._trialDetailsListView.Location = new System.Drawing.Point(6, 16);
            this._trialDetailsListView.Name = "_trialDetailsListView";
            this._trialDetailsListView.Size = new System.Drawing.Size(699, 156);
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
            // _autoRewardsCheckBox
            // 
            this._autoRewardsCheckBox.AutoSize = true;
            this._autoRewardsCheckBox.Location = new System.Drawing.Point(59, 60);
            this._autoRewardsCheckBox.Name = "_autoRewardsCheckBox";
            this._autoRewardsCheckBox.Size = new System.Drawing.Size(84, 17);
            this._autoRewardsCheckBox.TabIndex = 9;
            this._autoRewardsCheckBox.Text = "Auto Choice";
            this._autoRewardsCheckBox.UseVisualStyleBackColor = true;
            this._autoRewardsCheckBox.CheckedChanged += new System.EventHandler(this._autoRewardsTextBox_CheckedChanged);
            // 
            // _varyingControlGroupBox
            // 
            this._varyingControlGroupBox.Controls.Add(this._btnPark);
            this._varyingControlGroupBox.Controls.Add(this._btnResume);
            this._varyingControlGroupBox.Controls.Add(this._btnPause);
            this._varyingControlGroupBox.Controls.Add(this._textboxStickOnNumber);
            this._varyingControlGroupBox.Controls.Add(this._labelStickOnNumber);
            this._varyingControlGroupBox.Controls.Add(this._numOfRepetitionsTextBox);
            this._varyingControlGroupBox.Controls.Add(this.label5);
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
            // _btnResume
            // 
            this._btnResume.Location = new System.Drawing.Point(7, 200);
            this._btnResume.Name = "_btnResume";
            this._btnResume.Size = new System.Drawing.Size(75, 23);
            this._btnResume.TabIndex = 15;
            this._btnResume.Text = "Resume";
            this._btnResume.UseVisualStyleBackColor = true;
            this._btnResume.Click += new System.EventHandler(this._btnResume_Click);
            // 
            // _btnPause
            // 
            this._btnPause.Location = new System.Drawing.Point(6, 170);
            this._btnPause.Name = "_btnPause";
            this._btnPause.Size = new System.Drawing.Size(75, 23);
            this._btnPause.TabIndex = 14;
            this._btnPause.Text = "Pause";
            this._btnPause.UseVisualStyleBackColor = true;
            this._btnPause.Click += new System.EventHandler(this._btnPause_Click);
            // 
            // _textboxStickOnNumber
            // 
            this._textboxStickOnNumber.Location = new System.Drawing.Point(269, 442);
            this._textboxStickOnNumber.Name = "_textboxStickOnNumber";
            this._textboxStickOnNumber.Size = new System.Drawing.Size(58, 20);
            this._textboxStickOnNumber.TabIndex = 13;
            this._textboxStickOnNumber.Text = "1";
            // 
            // _labelStickOnNumber
            // 
            this._labelStickOnNumber.AutoSize = true;
            this._labelStickOnNumber.Location = new System.Drawing.Point(207, 445);
            this._labelStickOnNumber.Name = "_labelStickOnNumber";
            this._labelStickOnNumber.Size = new System.Drawing.Size(56, 13);
            this._labelStickOnNumber.TabIndex = 12;
            this._labelStickOnNumber.Text = "Stick on #";
            // 
            // _numOfRepetitionsTextBox
            // 
            this._numOfRepetitionsTextBox.Location = new System.Drawing.Point(156, 442);
            this._numOfRepetitionsTextBox.Name = "_numOfRepetitionsTextBox";
            this._numOfRepetitionsTextBox.Size = new System.Drawing.Size(45, 20);
            this._numOfRepetitionsTextBox.TabIndex = 11;
            this._numOfRepetitionsTextBox.Text = "1";
            this._numOfRepetitionsTextBox.Leave += new System.EventHandler(this._numOfRepetitionsTextBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 445);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Repetitions:";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "0ml";
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
            // _waterRewardMeasure
            // 
            this._waterRewardMeasure.BackColor = System.Drawing.Color.Transparent;
            this._waterRewardMeasure.Image = null;
            this._waterRewardMeasure.Location = new System.Drawing.Point(14, 45);
            this._waterRewardMeasure.Name = "_waterRewardMeasure";
            this._waterRewardMeasure.ProgressColor = System.Drawing.Color.Aqua;
            this._waterRewardMeasure.ProgressDirection = WaterMeasure.WaterMeasure.ProgressDir.Vertical;
            this._waterRewardMeasure.ShowText = true;
            this._waterRewardMeasure.Size = new System.Drawing.Size(44, 294);
            this._waterRewardMeasure.Text = "ml";
            // 
            // _selectedRatGroupBox
            // 
            this._selectedRatGroupBox.Controls.Add(this._selectedRatNameComboBox);
            this._selectedRatGroupBox.Location = new System.Drawing.Point(1036, 719);
            this._selectedRatGroupBox.Name = "_selectedRatGroupBox";
            this._selectedRatGroupBox.Size = new System.Drawing.Size(230, 69);
            this._selectedRatGroupBox.TabIndex = 16;
            this._selectedRatGroupBox.TabStop = false;
            this._selectedRatGroupBox.Text = "SelectedRat";
            // 
            // _selectedRatNameComboBox
            // 
            this._selectedRatNameComboBox.FormattingEnabled = true;
            this._selectedRatNameComboBox.Location = new System.Drawing.Point(19, 30);
            this._selectedRatNameComboBox.Name = "_selectedRatNameComboBox";
            this._selectedRatNameComboBox.Size = new System.Drawing.Size(121, 21);
            this._selectedRatNameComboBox.TabIndex = 0;
            this._selectedRatNameComboBox.SelectedValueChanged += new System.EventHandler(this._selectedRatNameComboBox_SelectedValueChanged);
            // 
            // _guiInterfaceToolTip
            // 
            this._guiInterfaceToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // _trialInfoGroupBox
            // 
            this._trialInfoGroupBox.Controls.Add(this._trialDetailsListView);
            this._trialInfoGroupBox.Location = new System.Drawing.Point(15, 722);
            this._trialInfoGroupBox.Name = "_trialInfoGroupBox";
            this._trialInfoGroupBox.Size = new System.Drawing.Size(711, 178);
            this._trialInfoGroupBox.TabIndex = 17;
            this._trialInfoGroupBox.TabStop = false;
            this._trialInfoGroupBox.Text = "Trial Info";
            // 
            // globalExperimentInfogroupBox
            // 
            this.globalExperimentInfogroupBox.Controls.Add(this._globaExperimentlInfoListView);
            this.globalExperimentInfogroupBox.Location = new System.Drawing.Point(1275, 81);
            this.globalExperimentInfogroupBox.Name = "globalExperimentInfogroupBox";
            this.globalExperimentInfogroupBox.Size = new System.Drawing.Size(391, 310);
            this.globalExperimentInfogroupBox.TabIndex = 18;
            this.globalExperimentInfogroupBox.TabStop = false;
            this.globalExperimentInfogroupBox.Text = "Global Experiment Info";
            // 
            // _globaExperimentlInfoListView
            // 
            this._globaExperimentlInfoListView.Location = new System.Drawing.Point(6, 19);
            this._globaExperimentlInfoListView.Name = "_globaExperimentlInfoListView";
            this._globaExperimentlInfoListView.Size = new System.Drawing.Size(379, 285);
            this._globaExperimentlInfoListView.TabIndex = 0;
            this._globaExperimentlInfoListView.UseCompatibleStateImageBehavior = false;
            // 
            // _onlinePsychGrpahGroupBox
            // 
            this._onlinePsychGrpahGroupBox.Controls.Add(this._onlinePsychGraphControl);
            this._onlinePsychGrpahGroupBox.Location = new System.Drawing.Point(1275, 397);
            this._onlinePsychGrpahGroupBox.Name = "_onlinePsychGrpahGroupBox";
            this._onlinePsychGrpahGroupBox.Size = new System.Drawing.Size(391, 304);
            this._onlinePsychGrpahGroupBox.TabIndex = 19;
            this._onlinePsychGrpahGroupBox.TabStop = false;
            this._onlinePsychGrpahGroupBox.Text = "Online Psycho Graph";
            // 
            // _onlinePsychGraphControl
            // 
            chartArea1.Name = "ChartArea1";
            this._onlinePsychGraphControl.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this._onlinePsychGraphControl.Legends.Add(legend1);
            this._onlinePsychGraphControl.Location = new System.Drawing.Point(6, 19);
            this._onlinePsychGraphControl.Name = "_onlinePsychGraphControl";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this._onlinePsychGraphControl.Series.Add(series1);
            this._onlinePsychGraphControl.Size = new System.Drawing.Size(379, 279);
            this._onlinePsychGraphControl.TabIndex = 0;
            this._onlinePsychGraphControl.Text = "chart1";
            this._onlinePsychGraphControl.Click += new System.EventHandler(this._onlinePsychGraphControl_Click);
            // 
            // _autosGroupBox
            // 
            this._autosGroupBox.Controls.Add(this._autoStartCheckBox);
            this._autosGroupBox.Controls.Add(this._autoFixationCheckBox);
            this._autosGroupBox.Controls.Add(this._autoRewardsCheckBox);
            this._autosGroupBox.Location = new System.Drawing.Point(1036, 800);
            this._autosGroupBox.Name = "_autosGroupBox";
            this._autosGroupBox.Size = new System.Drawing.Size(233, 100);
            this._autosGroupBox.TabIndex = 20;
            this._autosGroupBox.TabStop = false;
            this._autosGroupBox.Text = "Autos";
            // 
            // _autoStartCheckBox
            // 
            this._autoStartCheckBox.AutoSize = true;
            this._autoStartCheckBox.Location = new System.Drawing.Point(59, 14);
            this._autoStartCheckBox.Name = "_autoStartCheckBox";
            this._autoStartCheckBox.Size = new System.Drawing.Size(73, 17);
            this._autoStartCheckBox.TabIndex = 11;
            this._autoStartCheckBox.Text = "Auto Start";
            this._autoStartCheckBox.UseVisualStyleBackColor = true;
            this._autoStartCheckBox.CheckedChanged += new System.EventHandler(this._autoStartcheckBox_CheckedChanged);
            // 
            // _autoFixationCheckBox
            // 
            this._autoFixationCheckBox.AutoSize = true;
            this._autoFixationCheckBox.Location = new System.Drawing.Point(59, 37);
            this._autoFixationCheckBox.Name = "_autoFixationCheckBox";
            this._autoFixationCheckBox.Size = new System.Drawing.Size(87, 17);
            this._autoFixationCheckBox.TabIndex = 10;
            this._autoFixationCheckBox.Text = "Auto Fixation";
            this._autoFixationCheckBox.UseVisualStyleBackColor = true;
            this._autoFixationCheckBox.CheckedChanged += new System.EventHandler(this._autoFixation_CheckedChanged);
            // 
            // _warningsGroupBox
            // 
            this._warningsGroupBox.Controls.Add(this._ardionoPrtWarningLabel);
            this._warningsGroupBox.Location = new System.Drawing.Point(1275, 719);
            this._warningsGroupBox.Name = "_warningsGroupBox";
            this._warningsGroupBox.Size = new System.Drawing.Size(391, 69);
            this._warningsGroupBox.TabIndex = 21;
            this._warningsGroupBox.TabStop = false;
            this._warningsGroupBox.Text = "Warnings";
            // 
            // _ardionoPrtWarningLabel
            // 
            this._ardionoPrtWarningLabel.AutoSize = true;
            this._ardionoPrtWarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this._ardionoPrtWarningLabel.ForeColor = System.Drawing.Color.Red;
            this._ardionoPrtWarningLabel.Location = new System.Drawing.Point(6, 20);
            this._ardionoPrtWarningLabel.Name = "_ardionoPrtWarningLabel";
            this._ardionoPrtWarningLabel.Size = new System.Drawing.Size(385, 13);
            this._ardionoPrtWarningLabel.TabIndex = 0;
            this._ardionoPrtWarningLabel.Text = "The COM4 (Arduino) port is not connected-LEDS not work properly";
            this._ardionoPrtWarningLabel.Visible = false;
            // 
            // _autoRewardSound
            // 
            this._autoRewardSound.AutoSize = true;
            this._autoRewardSound.Location = new System.Drawing.Point(9, 60);
            this._autoRewardSound.Name = "_autoRewardSound";
            this._autoRewardSound.Size = new System.Drawing.Size(97, 17);
            this._autoRewardSound.TabIndex = 12;
            this._autoRewardSound.Text = "Reward Sound";
            this._autoRewardSound.UseVisualStyleBackColor = true;
            this._autoRewardSound.CheckedChanged += new System.EventHandler(this._autoRewardSound_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._breakFixationSoundEnableCheckBox);
            this.groupBox1.Controls.Add(this._autoRewardSound);
            this.groupBox1.Controls.Add(this._fixationOnlyCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(1275, 800);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 100);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Modes";
            // 
            // _breakFixationSoundEnableCheckBox
            // 
            this._breakFixationSoundEnableCheckBox.AutoSize = true;
            this._breakFixationSoundEnableCheckBox.Location = new System.Drawing.Point(9, 37);
            this._breakFixationSoundEnableCheckBox.Name = "_breakFixationSoundEnableCheckBox";
            this._breakFixationSoundEnableCheckBox.Size = new System.Drawing.Size(93, 17);
            this._breakFixationSoundEnableCheckBox.TabIndex = 1;
            this._breakFixationSoundEnableCheckBox.Text = "B.F Sound On";
            this._breakFixationSoundEnableCheckBox.UseVisualStyleBackColor = true;
            this._breakFixationSoundEnableCheckBox.CheckedChanged += new System.EventHandler(this._breakFixationSoundEnableCheckBox_CheckedChanged);
            // 
            // _fixationOnlyCheckBox
            // 
            this._fixationOnlyCheckBox.AutoSize = true;
            this._fixationOnlyCheckBox.Location = new System.Drawing.Point(9, 19);
            this._fixationOnlyCheckBox.Name = "_fixationOnlyCheckBox";
            this._fixationOnlyCheckBox.Size = new System.Drawing.Size(86, 17);
            this._fixationOnlyCheckBox.TabIndex = 0;
            this._fixationOnlyCheckBox.Text = "Fixation Only";
            this._fixationOnlyCheckBox.UseVisualStyleBackColor = true;
            this._fixationOnlyCheckBox.CheckedChanged += new System.EventHandler(this._fixationOnlyCheckBox_CheckedChanged);
            // 
            // _groupboxLedsController
            // 
            this._groupboxLedsController.Controls.Add(this._textboxLEDBrightness);
            this._groupboxLedsController.Controls.Add(this._labelBrightness);
            this._groupboxLedsController.Controls.Add(this._textboxPercentageOfTurnOnLeds);
            this._groupboxLedsController.Controls.Add(this._labelPercentageOfturnedOnLeds);
            this._groupboxLedsController.Location = new System.Drawing.Point(1404, 800);
            this._groupboxLedsController.Name = "_groupboxLedsController";
            this._groupboxLedsController.Size = new System.Drawing.Size(158, 100);
            this._groupboxLedsController.TabIndex = 23;
            this._groupboxLedsController.TabStop = false;
            this._groupboxLedsController.Text = "Leds Controller";
            // 
            // _textboxLEDBrightness
            // 
            this._textboxLEDBrightness.Location = new System.Drawing.Point(93, 44);
            this._textboxLEDBrightness.Name = "_textboxLEDBrightness";
            this._textboxLEDBrightness.Size = new System.Drawing.Size(50, 20);
            this._textboxLEDBrightness.TabIndex = 4;
            this._textboxLEDBrightness.Text = "1";
            // 
            // _labelBrightness
            // 
            this._labelBrightness.AutoSize = true;
            this._labelBrightness.Location = new System.Drawing.Point(6, 47);
            this._labelBrightness.Name = "_labelBrightness";
            this._labelBrightness.Size = new System.Drawing.Size(83, 13);
            this._labelBrightness.TabIndex = 3;
            this._labelBrightness.Text = "Brightness(0-10)";
            // 
            // _textboxPercentageOfTurnOnLeds
            // 
            this._textboxPercentageOfTurnOnLeds.Location = new System.Drawing.Point(93, 15);
            this._textboxPercentageOfTurnOnLeds.Name = "_textboxPercentageOfTurnOnLeds";
            this._textboxPercentageOfTurnOnLeds.Size = new System.Drawing.Size(50, 20);
            this._textboxPercentageOfTurnOnLeds.TabIndex = 1;
            this._textboxPercentageOfTurnOnLeds.Text = "0.1";
            // 
            // _labelPercentageOfturnedOnLeds
            // 
            this._labelPercentageOfturnedOnLeds.AutoSize = true;
            this._labelPercentageOfturnedOnLeds.Location = new System.Drawing.Point(6, 18);
            this._labelPercentageOfturnedOnLeds.Name = "_labelPercentageOfturnedOnLeds";
            this._labelPercentageOfturnedOnLeds.Size = new System.Drawing.Size(82, 13);
            this._labelPercentageOfturnedOnLeds.TabIndex = 0;
            this._labelPercentageOfturnedOnLeds.Text = "% turn on LEDS";
            // 
            // _btnPark
            // 
            this._btnPark.Location = new System.Drawing.Point(7, 351);
            this._btnPark.Name = "_btnPark";
            this._btnPark.Size = new System.Drawing.Size(75, 23);
            this._btnPark.TabIndex = 16;
            this._btnPark.Text = "Park";
            this._btnPark.UseVisualStyleBackColor = true;
            this._btnPark.Click += new System.EventHandler(this._btnPark_Click);
            // 
            // GuiInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1678, 912);
            this.Controls.Add(this._groupboxLedsController);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._warningsGroupBox);
            this.Controls.Add(this._autosGroupBox);
            this.Controls.Add(this._onlinePsychGrpahGroupBox);
            this.Controls.Add(this.globalExperimentInfogroupBox);
            this.Controls.Add(this._trialInfoGroupBox);
            this.Controls.Add(this._selectedRatGroupBox);
            this.Controls.Add(this._drinkControlGroupBox);
            this.Controls.Add(this._interactiveNolduscommuncation);
            this.Controls.Add(this._varyingControlGroupBox);
            this.Controls.Add(this._handRewardsgroupBox);
            this.Controls.Add(this._trialParametersGroup);
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
            this._varyingControlGroupBox.PerformLayout();
            this._interactiveNolduscommuncation.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._drinkControlGroupBox.ResumeLayout(false);
            this._drinkControlGroupBox.PerformLayout();
            this._selectedRatGroupBox.ResumeLayout(false);
            this._trialInfoGroupBox.ResumeLayout(false);
            this.globalExperimentInfogroupBox.ResumeLayout(false);
            this._onlinePsychGrpahGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._onlinePsychGraphControl)).EndInit();
            this._autosGroupBox.ResumeLayout(false);
            this._autosGroupBox.PerformLayout();
            this._warningsGroupBox.ResumeLayout(false);
            this._warningsGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._groupboxLedsController.ResumeLayout(false);
            this._groupboxLedsController.PerformLayout();
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
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox _selectedRatGroupBox;
        private System.Windows.Forms.ComboBox _selectedRatNameComboBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.ToolTip _guiInterfaceToolTip;
        private System.Windows.Forms.GroupBox _trialInfoGroupBox;
        private System.Windows.Forms.GroupBox globalExperimentInfogroupBox;
        private System.Windows.Forms.ListView _globaExperimentlInfoListView;
        private System.Windows.Forms.TextBox _numOfRepetitionsTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox _onlinePsychGrpahGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart _onlinePsychGraphControl;
        private System.Windows.Forms.CheckBox _autoRewardsCheckBox;
        private System.Windows.Forms.GroupBox _autosGroupBox;
        private System.Windows.Forms.CheckBox _autoStartCheckBox;
        private System.Windows.Forms.CheckBox _autoFixationCheckBox;
        private System.Windows.Forms.GroupBox _warningsGroupBox;
        private System.Windows.Forms.Label _ardionoPrtWarningLabel;
        private System.Windows.Forms.CheckBox _autoRewardSound;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _fixationOnlyCheckBox;
        private System.Windows.Forms.CheckBox _breakFixationSoundEnableCheckBox;
        private System.Windows.Forms.GroupBox _groupboxLedsController;
        private System.Windows.Forms.TextBox _textboxPercentageOfTurnOnLeds;
        private System.Windows.Forms.Label _labelPercentageOfturnedOnLeds;
        private System.Windows.Forms.Label _labelBrightness;
        private System.Windows.Forms.TextBox _textboxLEDBrightness;
        private System.Windows.Forms.Label _labelStickOnNumber;
        private System.Windows.Forms.TextBox _textboxStickOnNumber;
        private System.Windows.Forms.Button _btnResume;
        private System.Windows.Forms.Button _btnPause;
        private System.Windows.Forms.Button _btnPark;
    }
}