namespace Sample
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnESGetPosition = new System.Windows.Forms.Button();
            this.btnESGetConfiguration = new System.Windows.Forms.Button();
            this.btnESGetBpexPositionData = new System.Windows.Forms.Button();
            this.btnESGetAlarm = new System.Windows.Forms.Button();
            this.btnESGetAlarmHist = new System.Windows.Forms.Button();
            this.btnESGetStatus = new System.Windows.Forms.Button();
            this.btnESGetJobStatus = new System.Windows.Forms.Button();
            this.btnESGetDeviation = new System.Windows.Forms.Button();
            this.btnESGetTorque = new System.Windows.Forms.Button();
            this.btnESReadIO = new System.Windows.Forms.Button();
            this.btnESWriteIO = new System.Windows.Forms.Button();
            this.btnESSetVarData = new System.Windows.Forms.Button();
            this.btnESGetVarData = new System.Windows.Forms.Button();
            this.btnESWriteRegister = new System.Windows.Forms.Button();
            this.btnESReadRegister = new System.Windows.Forms.Button();
            this.btnESReset = new System.Windows.Forms.Button();
            this.btnESSetBpexPositionData = new System.Windows.Forms.Button();
            this.btnESSetPositionData = new System.Windows.Forms.Button();
            this.btnESGetPositionData = new System.Windows.Forms.Button();
            this.btnESSetStrData = new System.Windows.Forms.Button();
            this.btnESGetStrData = new System.Windows.Forms.Button();
            this.btnESCycle = new System.Windows.Forms.Button();
            this.btnESHlock = new System.Windows.Forms.Button();
            this.btnESServo = new System.Windows.Forms.Button();
            this.btnESHold = new System.Windows.Forms.Button();
            this.btnESCancel = new System.Windows.Forms.Button();
            this.btnESSelectJob = new System.Windows.Forms.Button();
            this.btnESStartJob = new System.Windows.Forms.Button();
            this.btnESBDSP = new System.Windows.Forms.Button();
            this.btnESDeleteJob = new System.Windows.Forms.Button();
            this.btnESLoadFile = new System.Windows.Forms.Button();
            this.btnESSaveFile = new System.Windows.Forms.Button();
            this.btnESFileList = new System.Windows.Forms.Button();
            this.btnESGetMonitoringTime = new System.Windows.Forms.Button();
            this.btnESGetSystemInfo = new System.Windows.Forms.Button();
            this.btnESGetVarDataMB = new System.Windows.Forms.Button();
            this.btnESGetVarDataMI = new System.Windows.Forms.Button();
            this.btnESGetVarDataMR = new System.Windows.Forms.Button();
            this.btnESGetVarDataMD = new System.Windows.Forms.Button();
            this.btnESGetStrDataM = new System.Windows.Forms.Button();
            this.btnESGetPositionDataM = new System.Windows.Forms.Button();
            this.btnESGetBpexPositionDataM = new System.Windows.Forms.Button();
            this.btnESSetStrDataM = new System.Windows.Forms.Button();
            this.btnESSetVarDataMR = new System.Windows.Forms.Button();
            this.btnESSetVarDataMD = new System.Windows.Forms.Button();
            this.btnESSetVarDataMI = new System.Windows.Forms.Button();
            this.btnESSetVarDataMB = new System.Windows.Forms.Button();
            this.btnESSetBpexPositionDataM = new System.Windows.Forms.Button();
            this.btnESSetPositionDataM = new System.Windows.Forms.Button();
            this.btnESWriteIOM = new System.Windows.Forms.Button();
            this.btnESReadIOM = new System.Windows.Forms.Button();
            this.btnESWriteRegisterM = new System.Windows.Forms.Button();
            this.btnESReadRegisterM = new System.Windows.Forms.Button();
            this.btnESGetAlarmEx = new System.Windows.Forms.Button();
            this.btnESGetAlarmHistEx = new System.Windows.Forms.Button();
            this.btnESCartMove = new System.Windows.Forms.Button();
            this.btnESPulseMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point( 12, 12 );
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size( 300, 548 );
            this.textBox1.TabIndex = 57;
            this.textBox1.WordWrap = false;
            // 
            // btnESGetPosition
            // 
            this.btnESGetPosition.Location = new System.Drawing.Point( 318, 257 );
            this.btnESGetPosition.Name = "btnESGetPosition";
            this.btnESGetPosition.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetPosition.TabIndex = 7;
            this.btnESGetPosition.Text = "ESGetPosition";
            this.btnESGetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetPosition.UseVisualStyleBackColor = true;
            this.btnESGetPosition.Click += new System.EventHandler( this.btnESGetPosition_Click );
            // 
            // btnESGetConfiguration
            // 
            this.btnESGetConfiguration.Location = new System.Drawing.Point( 318, 222 );
            this.btnESGetConfiguration.Name = "btnESGetConfiguration";
            this.btnESGetConfiguration.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetConfiguration.TabIndex = 6;
            this.btnESGetConfiguration.Text = "ESGetConfiguration";
            this.btnESGetConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetConfiguration.UseVisualStyleBackColor = true;
            this.btnESGetConfiguration.Click += new System.EventHandler( this.btnESGetConfiguration_Click );
            // 
            // btnESGetBpexPositionData
            // 
            this.btnESGetBpexPositionData.Location = new System.Drawing.Point( 474, 362 );
            this.btnESGetBpexPositionData.Name = "btnESGetBpexPositionData";
            this.btnESGetBpexPositionData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetBpexPositionData.TabIndex = 26;
            this.btnESGetBpexPositionData.Text = "ESGetBpexPositionData";
            this.btnESGetBpexPositionData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetBpexPositionData.UseVisualStyleBackColor = true;
            this.btnESGetBpexPositionData.Click += new System.EventHandler( this.btnESGetBpexPositionData_Click );
            // 
            // btnESGetAlarm
            // 
            this.btnESGetAlarm.Location = new System.Drawing.Point( 318, 12 );
            this.btnESGetAlarm.Name = "btnESGetAlarm";
            this.btnESGetAlarm.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetAlarm.TabIndex = 0;
            this.btnESGetAlarm.Text = "ESGetAlarm";
            this.btnESGetAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetAlarm.UseVisualStyleBackColor = true;
            this.btnESGetAlarm.Click += new System.EventHandler( this.btnESGetAlarm_Click );
            // 
            // btnESGetAlarmHist
            // 
            this.btnESGetAlarmHist.Location = new System.Drawing.Point( 318, 82 );
            this.btnESGetAlarmHist.Name = "btnESGetAlarmHist";
            this.btnESGetAlarmHist.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetAlarmHist.TabIndex = 2;
            this.btnESGetAlarmHist.Text = "ESGetAlarmHist";
            this.btnESGetAlarmHist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetAlarmHist.UseVisualStyleBackColor = true;
            this.btnESGetAlarmHist.Click += new System.EventHandler( this.btnESGetAlarmHist_Click );
            // 
            // btnESGetStatus
            // 
            this.btnESGetStatus.Location = new System.Drawing.Point( 318, 152 );
            this.btnESGetStatus.Name = "btnESGetStatus";
            this.btnESGetStatus.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetStatus.TabIndex = 4;
            this.btnESGetStatus.Text = "ESGetStatus";
            this.btnESGetStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetStatus.UseVisualStyleBackColor = true;
            this.btnESGetStatus.Click += new System.EventHandler( this.btnESGetStatus_Click );
            // 
            // btnESGetJobStatus
            // 
            this.btnESGetJobStatus.Location = new System.Drawing.Point( 318, 187 );
            this.btnESGetJobStatus.Name = "btnESGetJobStatus";
            this.btnESGetJobStatus.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetJobStatus.TabIndex = 5;
            this.btnESGetJobStatus.Text = "ESGetJobStatus";
            this.btnESGetJobStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetJobStatus.UseVisualStyleBackColor = true;
            this.btnESGetJobStatus.Click += new System.EventHandler( this.btnESGetJobStatus_Click );
            // 
            // btnESGetDeviation
            // 
            this.btnESGetDeviation.Location = new System.Drawing.Point( 318, 292 );
            this.btnESGetDeviation.Name = "btnESGetDeviation";
            this.btnESGetDeviation.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetDeviation.TabIndex = 8;
            this.btnESGetDeviation.Text = "ESGetDeviation";
            this.btnESGetDeviation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetDeviation.UseVisualStyleBackColor = true;
            this.btnESGetDeviation.Click += new System.EventHandler( this.btnESGetDeviation_Click );
            // 
            // btnESGetTorque
            // 
            this.btnESGetTorque.Location = new System.Drawing.Point( 318, 327 );
            this.btnESGetTorque.Name = "btnESGetTorque";
            this.btnESGetTorque.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetTorque.TabIndex = 9;
            this.btnESGetTorque.Text = "ESGetTorque";
            this.btnESGetTorque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetTorque.UseVisualStyleBackColor = true;
            this.btnESGetTorque.Click += new System.EventHandler( this.btnESGetTorque_Click );
            // 
            // btnESReadIO
            // 
            this.btnESReadIO.Location = new System.Drawing.Point( 318, 432 );
            this.btnESReadIO.Name = "btnESReadIO";
            this.btnESReadIO.Size = new System.Drawing.Size( 150, 23 );
            this.btnESReadIO.TabIndex = 12;
            this.btnESReadIO.Text = "ESReadIO";
            this.btnESReadIO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESReadIO.UseVisualStyleBackColor = true;
            this.btnESReadIO.Click += new System.EventHandler( this.btnESReadIO_Click );
            // 
            // btnESWriteIO
            // 
            this.btnESWriteIO.Location = new System.Drawing.Point( 318, 467 );
            this.btnESWriteIO.Name = "btnESWriteIO";
            this.btnESWriteIO.Size = new System.Drawing.Size( 150, 23 );
            this.btnESWriteIO.TabIndex = 13;
            this.btnESWriteIO.Text = "ESWriteIO";
            this.btnESWriteIO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESWriteIO.UseVisualStyleBackColor = true;
            this.btnESWriteIO.Click += new System.EventHandler( this.btnESWriteIO_Click );
            // 
            // btnESSetVarData
            // 
            this.btnESSetVarData.Location = new System.Drawing.Point( 474, 187 );
            this.btnESSetVarData.Name = "btnESSetVarData";
            this.btnESSetVarData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetVarData.TabIndex = 21;
            this.btnESSetVarData.Text = "ESSetVarData";
            this.btnESSetVarData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetVarData.UseVisualStyleBackColor = true;
            this.btnESSetVarData.Click += new System.EventHandler( this.btnESSetVarData_Click );
            // 
            // btnESGetVarData
            // 
            this.btnESGetVarData.Location = new System.Drawing.Point( 474, 152 );
            this.btnESGetVarData.Name = "btnESGetVarData";
            this.btnESGetVarData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetVarData.TabIndex = 20;
            this.btnESGetVarData.Text = "ESGetVarData";
            this.btnESGetVarData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetVarData.UseVisualStyleBackColor = true;
            this.btnESGetVarData.Click += new System.EventHandler( this.btnESGetVarData_Click );
            // 
            // btnESWriteRegister
            // 
            this.btnESWriteRegister.Location = new System.Drawing.Point( 318, 537 );
            this.btnESWriteRegister.Name = "btnESWriteRegister";
            this.btnESWriteRegister.Size = new System.Drawing.Size( 150, 23 );
            this.btnESWriteRegister.TabIndex = 15;
            this.btnESWriteRegister.Text = "ESWriteRegister";
            this.btnESWriteRegister.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESWriteRegister.UseVisualStyleBackColor = true;
            this.btnESWriteRegister.Click += new System.EventHandler( this.btnESWriteRegistor_Click );
            // 
            // btnESReadRegister
            // 
            this.btnESReadRegister.Location = new System.Drawing.Point( 318, 502 );
            this.btnESReadRegister.Name = "btnESReadRegister";
            this.btnESReadRegister.Size = new System.Drawing.Size( 150, 23 );
            this.btnESReadRegister.TabIndex = 14;
            this.btnESReadRegister.Text = "ESReadRegister";
            this.btnESReadRegister.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESReadRegister.UseVisualStyleBackColor = true;
            this.btnESReadRegister.Click += new System.EventHandler( this.btnESReadRegistor_Click );
            // 
            // btnESReset
            // 
            this.btnESReset.Location = new System.Drawing.Point( 630, 362 );
            this.btnESReset.Name = "btnESReset";
            this.btnESReset.Size = new System.Drawing.Size( 150, 23 );
            this.btnESReset.TabIndex = 42;
            this.btnESReset.Text = "ESReset";
            this.btnESReset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESReset.UseVisualStyleBackColor = true;
            this.btnESReset.Click += new System.EventHandler( this.btnESReset_Click );
            // 
            // btnESSetBpexPositionData
            // 
            this.btnESSetBpexPositionData.Location = new System.Drawing.Point( 474, 397 );
            this.btnESSetBpexPositionData.Name = "btnESSetBpexPositionData";
            this.btnESSetBpexPositionData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetBpexPositionData.TabIndex = 27;
            this.btnESSetBpexPositionData.Text = "ESSetBpexPositionData";
            this.btnESSetBpexPositionData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetBpexPositionData.UseVisualStyleBackColor = true;
            this.btnESSetBpexPositionData.Click += new System.EventHandler( this.btnESSetBpexPositionData_Click );
            // 
            // btnESSetPositionData
            // 
            this.btnESSetPositionData.Location = new System.Drawing.Point( 474, 327 );
            this.btnESSetPositionData.Name = "btnESSetPositionData";
            this.btnESSetPositionData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetPositionData.TabIndex = 25;
            this.btnESSetPositionData.Text = "ESSetPositionData";
            this.btnESSetPositionData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetPositionData.UseVisualStyleBackColor = true;
            this.btnESSetPositionData.Click += new System.EventHandler( this.btnESSetPositionData_Click );
            // 
            // btnESGetPositionData
            // 
            this.btnESGetPositionData.Location = new System.Drawing.Point( 474, 292 );
            this.btnESGetPositionData.Name = "btnESGetPositionData";
            this.btnESGetPositionData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetPositionData.TabIndex = 24;
            this.btnESGetPositionData.Text = "ESGetPositionData";
            this.btnESGetPositionData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetPositionData.UseVisualStyleBackColor = true;
            this.btnESGetPositionData.Click += new System.EventHandler( this.btnESGetPositionData_Click );
            // 
            // btnESSetStrData
            // 
            this.btnESSetStrData.Location = new System.Drawing.Point( 474, 257 );
            this.btnESSetStrData.Name = "btnESSetStrData";
            this.btnESSetStrData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetStrData.TabIndex = 23;
            this.btnESSetStrData.Text = "ESSetStrData";
            this.btnESSetStrData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetStrData.UseVisualStyleBackColor = true;
            this.btnESSetStrData.Click += new System.EventHandler( this.btnESSetStrData_Click );
            // 
            // btnESGetStrData
            // 
            this.btnESGetStrData.Location = new System.Drawing.Point( 474, 222 );
            this.btnESGetStrData.Name = "btnESGetStrData";
            this.btnESGetStrData.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetStrData.TabIndex = 22;
            this.btnESGetStrData.Text = "ESGetStrData";
            this.btnESGetStrData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetStrData.UseVisualStyleBackColor = true;
            this.btnESGetStrData.Click += new System.EventHandler( this.btnESGetStrData_Click );
            // 
            // btnESCycle
            // 
            this.btnESCycle.Location = new System.Drawing.Point( 630, 537 );
            this.btnESCycle.Name = "btnESCycle";
            this.btnESCycle.Size = new System.Drawing.Size( 150, 23 );
            this.btnESCycle.TabIndex = 47;
            this.btnESCycle.Text = "ESCycle";
            this.btnESCycle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESCycle.UseVisualStyleBackColor = true;
            this.btnESCycle.Click += new System.EventHandler( this.btnESCycle_Click );
            // 
            // btnESHlock
            // 
            this.btnESHlock.Location = new System.Drawing.Point( 630, 502 );
            this.btnESHlock.Name = "btnESHlock";
            this.btnESHlock.Size = new System.Drawing.Size( 150, 23 );
            this.btnESHlock.TabIndex = 46;
            this.btnESHlock.Text = "ESHlock";
            this.btnESHlock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESHlock.UseVisualStyleBackColor = true;
            this.btnESHlock.Click += new System.EventHandler( this.btnESHlock_Click );
            // 
            // btnESServo
            // 
            this.btnESServo.Location = new System.Drawing.Point( 630, 467 );
            this.btnESServo.Name = "btnESServo";
            this.btnESServo.Size = new System.Drawing.Size( 150, 23 );
            this.btnESServo.TabIndex = 45;
            this.btnESServo.Text = "ESServo";
            this.btnESServo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESServo.UseVisualStyleBackColor = true;
            this.btnESServo.Click += new System.EventHandler( this.btnESServo_Click );
            // 
            // btnESHold
            // 
            this.btnESHold.Location = new System.Drawing.Point( 630, 432 );
            this.btnESHold.Name = "btnESHold";
            this.btnESHold.Size = new System.Drawing.Size( 150, 23 );
            this.btnESHold.TabIndex = 44;
            this.btnESHold.Text = "ESHold";
            this.btnESHold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESHold.UseVisualStyleBackColor = true;
            this.btnESHold.Click += new System.EventHandler( this.btnESHold_Click );
            // 
            // btnESCancel
            // 
            this.btnESCancel.Location = new System.Drawing.Point( 630, 397 );
            this.btnESCancel.Name = "btnESCancel";
            this.btnESCancel.Size = new System.Drawing.Size( 150, 23 );
            this.btnESCancel.TabIndex = 43;
            this.btnESCancel.Text = "ESCancel";
            this.btnESCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESCancel.UseVisualStyleBackColor = true;
            this.btnESCancel.Click += new System.EventHandler( this.btnESCancel_Click );
            // 
            // btnESSelectJob
            // 
            this.btnESSelectJob.Location = new System.Drawing.Point( 786, 152 );
            this.btnESSelectJob.Name = "btnESSelectJob";
            this.btnESSelectJob.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSelectJob.TabIndex = 52;
            this.btnESSelectJob.Text = "ESSelectJob";
            this.btnESSelectJob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSelectJob.UseVisualStyleBackColor = true;
            this.btnESSelectJob.Click += new System.EventHandler( this.btnESSelectJob_Click );
            // 
            // btnESStartJob
            // 
            this.btnESStartJob.Location = new System.Drawing.Point( 786, 222 );
            this.btnESStartJob.Name = "btnESStartJob";
            this.btnESStartJob.Size = new System.Drawing.Size( 150, 23 );
            this.btnESStartJob.TabIndex = 54;
            this.btnESStartJob.Text = "ESStartJob";
            this.btnESStartJob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESStartJob.UseVisualStyleBackColor = true;
            this.btnESStartJob.Click += new System.EventHandler( this.btnESStartJob_Click );
            // 
            // btnESBDSP
            // 
            this.btnESBDSP.Location = new System.Drawing.Point( 786, 12 );
            this.btnESBDSP.Name = "btnESBDSP";
            this.btnESBDSP.Size = new System.Drawing.Size( 150, 23 );
            this.btnESBDSP.TabIndex = 48;
            this.btnESBDSP.Text = "ESBDSP";
            this.btnESBDSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESBDSP.UseVisualStyleBackColor = true;
            this.btnESBDSP.Click += new System.EventHandler( this.btnESBDSP_Click );
            // 
            // btnESDeleteJob
            // 
            this.btnESDeleteJob.Location = new System.Drawing.Point( 786, 187 );
            this.btnESDeleteJob.Name = "btnESDeleteJob";
            this.btnESDeleteJob.Size = new System.Drawing.Size( 150, 23 );
            this.btnESDeleteJob.TabIndex = 53;
            this.btnESDeleteJob.Text = "ESDeleteJob";
            this.btnESDeleteJob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESDeleteJob.UseVisualStyleBackColor = true;
            this.btnESDeleteJob.Click += new System.EventHandler( this.btnESDeleteJob_Click );
            // 
            // btnESLoadFile
            // 
            this.btnESLoadFile.Location = new System.Drawing.Point( 786, 117 );
            this.btnESLoadFile.Name = "btnESLoadFile";
            this.btnESLoadFile.Size = new System.Drawing.Size( 150, 23 );
            this.btnESLoadFile.TabIndex = 51;
            this.btnESLoadFile.Text = "ESLoadFile";
            this.btnESLoadFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESLoadFile.UseVisualStyleBackColor = true;
            this.btnESLoadFile.Click += new System.EventHandler( this.btnESLoadFile_Click );
            // 
            // btnESSaveFile
            // 
            this.btnESSaveFile.Location = new System.Drawing.Point( 786, 82 );
            this.btnESSaveFile.Name = "btnESSaveFile";
            this.btnESSaveFile.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSaveFile.TabIndex = 50;
            this.btnESSaveFile.Text = "ESSaveFile";
            this.btnESSaveFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSaveFile.UseVisualStyleBackColor = true;
            this.btnESSaveFile.Click += new System.EventHandler( this.btnESSaveFile_Click );
            // 
            // btnESFileList
            // 
            this.btnESFileList.Location = new System.Drawing.Point( 786, 47 );
            this.btnESFileList.Name = "btnESFileList";
            this.btnESFileList.Size = new System.Drawing.Size( 150, 23 );
            this.btnESFileList.TabIndex = 49;
            this.btnESFileList.Text = "ESFileList";
            this.btnESFileList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESFileList.UseVisualStyleBackColor = true;
            this.btnESFileList.Click += new System.EventHandler( this.btnESFileList_Click );
            // 
            // btnESGetMonitoringTime
            // 
            this.btnESGetMonitoringTime.Location = new System.Drawing.Point( 318, 362 );
            this.btnESGetMonitoringTime.Name = "btnESGetMonitoringTime";
            this.btnESGetMonitoringTime.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetMonitoringTime.TabIndex = 10;
            this.btnESGetMonitoringTime.Text = "ESGetMonitoringTime";
            this.btnESGetMonitoringTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetMonitoringTime.UseVisualStyleBackColor = true;
            this.btnESGetMonitoringTime.Click += new System.EventHandler( this.btnESGetMonitoringTime_Click );
            // 
            // btnESGetSystemInfo
            // 
            this.btnESGetSystemInfo.Location = new System.Drawing.Point( 318, 397 );
            this.btnESGetSystemInfo.Name = "btnESGetSystemInfo";
            this.btnESGetSystemInfo.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetSystemInfo.TabIndex = 11;
            this.btnESGetSystemInfo.Text = "ESGetSystemInfo";
            this.btnESGetSystemInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetSystemInfo.UseVisualStyleBackColor = true;
            this.btnESGetSystemInfo.Click += new System.EventHandler( this.btnESGetSystemInfo_Click );
            // 
            // btnESGetVarDataMB
            // 
            this.btnESGetVarDataMB.Location = new System.Drawing.Point( 474, 432 );
            this.btnESGetVarDataMB.Name = "btnESGetVarDataMB";
            this.btnESGetVarDataMB.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetVarDataMB.TabIndex = 28;
            this.btnESGetVarDataMB.Text = "ESGetVarDataMB";
            this.btnESGetVarDataMB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetVarDataMB.UseVisualStyleBackColor = true;
            this.btnESGetVarDataMB.Click += new System.EventHandler( this.btnESGetVarDataMB_Click );
            // 
            // btnESGetVarDataMI
            // 
            this.btnESGetVarDataMI.Location = new System.Drawing.Point( 474, 502 );
            this.btnESGetVarDataMI.Name = "btnESGetVarDataMI";
            this.btnESGetVarDataMI.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetVarDataMI.TabIndex = 30;
            this.btnESGetVarDataMI.Text = "ESGetVarDataMI";
            this.btnESGetVarDataMI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetVarDataMI.UseVisualStyleBackColor = true;
            this.btnESGetVarDataMI.Click += new System.EventHandler( this.btnESGetVarDataMI_Click );
            // 
            // btnESGetVarDataMR
            // 
            this.btnESGetVarDataMR.Location = new System.Drawing.Point( 630, 82 );
            this.btnESGetVarDataMR.Name = "btnESGetVarDataMR";
            this.btnESGetVarDataMR.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetVarDataMR.TabIndex = 34;
            this.btnESGetVarDataMR.Text = "ESGetVarDataMR";
            this.btnESGetVarDataMR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetVarDataMR.UseVisualStyleBackColor = true;
            this.btnESGetVarDataMR.Click += new System.EventHandler( this.btnESGetVarDataMR_Click );
            // 
            // btnESGetVarDataMD
            // 
            this.btnESGetVarDataMD.Location = new System.Drawing.Point( 630, 12 );
            this.btnESGetVarDataMD.Name = "btnESGetVarDataMD";
            this.btnESGetVarDataMD.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetVarDataMD.TabIndex = 32;
            this.btnESGetVarDataMD.Text = "ESGetVarDataMD";
            this.btnESGetVarDataMD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetVarDataMD.UseVisualStyleBackColor = true;
            this.btnESGetVarDataMD.Click += new System.EventHandler( this.btnESGetVarDataMD_Click );
            // 
            // btnESGetStrDataM
            // 
            this.btnESGetStrDataM.Location = new System.Drawing.Point( 630, 152 );
            this.btnESGetStrDataM.Name = "btnESGetStrDataM";
            this.btnESGetStrDataM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetStrDataM.TabIndex = 36;
            this.btnESGetStrDataM.Text = "ESGetStrDataM";
            this.btnESGetStrDataM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetStrDataM.UseVisualStyleBackColor = true;
            this.btnESGetStrDataM.Click += new System.EventHandler( this.btnESGetStrDataM_Click );
            // 
            // btnESGetPositionDataM
            // 
            this.btnESGetPositionDataM.Location = new System.Drawing.Point( 630, 222 );
            this.btnESGetPositionDataM.Name = "btnESGetPositionDataM";
            this.btnESGetPositionDataM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetPositionDataM.TabIndex = 38;
            this.btnESGetPositionDataM.Text = "ESGetPositionDataM";
            this.btnESGetPositionDataM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetPositionDataM.UseVisualStyleBackColor = true;
            this.btnESGetPositionDataM.Click += new System.EventHandler( this.btnESGetPositionDataM_Click );
            // 
            // btnESGetBpexPositionDataM
            // 
            this.btnESGetBpexPositionDataM.Location = new System.Drawing.Point( 630, 292 );
            this.btnESGetBpexPositionDataM.Name = "btnESGetBpexPositionDataM";
            this.btnESGetBpexPositionDataM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetBpexPositionDataM.TabIndex = 40;
            this.btnESGetBpexPositionDataM.Text = "ESGetBpexPositionDataM";
            this.btnESGetBpexPositionDataM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetBpexPositionDataM.UseVisualStyleBackColor = true;
            this.btnESGetBpexPositionDataM.Click += new System.EventHandler( this.btnESGetBpexPositionDataM_Click );
            // 
            // btnESSetStrDataM
            // 
            this.btnESSetStrDataM.Location = new System.Drawing.Point( 630, 187 );
            this.btnESSetStrDataM.Name = "btnESSetStrDataM";
            this.btnESSetStrDataM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetStrDataM.TabIndex = 37;
            this.btnESSetStrDataM.Text = "ESSetStrDataM";
            this.btnESSetStrDataM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetStrDataM.UseVisualStyleBackColor = true;
            this.btnESSetStrDataM.Click += new System.EventHandler( this.btnESSetStrDataM_Click );
            // 
            // btnESSetVarDataMR
            // 
            this.btnESSetVarDataMR.Location = new System.Drawing.Point( 630, 117 );
            this.btnESSetVarDataMR.Name = "btnESSetVarDataMR";
            this.btnESSetVarDataMR.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetVarDataMR.TabIndex = 35;
            this.btnESSetVarDataMR.Text = "ESSetVarDataMR";
            this.btnESSetVarDataMR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetVarDataMR.UseVisualStyleBackColor = true;
            this.btnESSetVarDataMR.Click += new System.EventHandler( this.btnESSetVarDataMR_Click );
            // 
            // btnESSetVarDataMD
            // 
            this.btnESSetVarDataMD.Location = new System.Drawing.Point( 630, 47 );
            this.btnESSetVarDataMD.Name = "btnESSetVarDataMD";
            this.btnESSetVarDataMD.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetVarDataMD.TabIndex = 33;
            this.btnESSetVarDataMD.Text = "ESSetVarDataMD";
            this.btnESSetVarDataMD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetVarDataMD.UseVisualStyleBackColor = true;
            this.btnESSetVarDataMD.Click += new System.EventHandler( this.btnESSetVarDataMD_Click );
            // 
            // btnESSetVarDataMI
            // 
            this.btnESSetVarDataMI.Location = new System.Drawing.Point( 474, 537 );
            this.btnESSetVarDataMI.Name = "btnESSetVarDataMI";
            this.btnESSetVarDataMI.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetVarDataMI.TabIndex = 31;
            this.btnESSetVarDataMI.Text = "ESSetVarDataMI";
            this.btnESSetVarDataMI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetVarDataMI.UseVisualStyleBackColor = true;
            this.btnESSetVarDataMI.Click += new System.EventHandler( this.btnESSetVarDataMI_Click );
            // 
            // btnESSetVarDataMB
            // 
            this.btnESSetVarDataMB.Location = new System.Drawing.Point( 474, 467 );
            this.btnESSetVarDataMB.Name = "btnESSetVarDataMB";
            this.btnESSetVarDataMB.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetVarDataMB.TabIndex = 29;
            this.btnESSetVarDataMB.Text = "ESSetVarDataMB";
            this.btnESSetVarDataMB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetVarDataMB.UseVisualStyleBackColor = true;
            this.btnESSetVarDataMB.Click += new System.EventHandler( this.btnESSetVarDataMB_Click );
            // 
            // btnESSetBpexPositionDataM
            // 
            this.btnESSetBpexPositionDataM.Location = new System.Drawing.Point( 630, 327 );
            this.btnESSetBpexPositionDataM.Name = "btnESSetBpexPositionDataM";
            this.btnESSetBpexPositionDataM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetBpexPositionDataM.TabIndex = 41;
            this.btnESSetBpexPositionDataM.Text = "ESSetBpexPositionDataM";
            this.btnESSetBpexPositionDataM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetBpexPositionDataM.UseVisualStyleBackColor = true;
            this.btnESSetBpexPositionDataM.Click += new System.EventHandler( this.btnESSetBpexPositionDataM_Click );
            // 
            // btnESSetPositionDataM
            // 
            this.btnESSetPositionDataM.Location = new System.Drawing.Point( 630, 257 );
            this.btnESSetPositionDataM.Name = "btnESSetPositionDataM";
            this.btnESSetPositionDataM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESSetPositionDataM.TabIndex = 39;
            this.btnESSetPositionDataM.Text = "ESSetPositionDataM";
            this.btnESSetPositionDataM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESSetPositionDataM.UseVisualStyleBackColor = true;
            this.btnESSetPositionDataM.Click += new System.EventHandler( this.btnESSetPositionDataM_Click );
            // 
            // btnESWriteIOM
            // 
            this.btnESWriteIOM.Location = new System.Drawing.Point( 474, 47 );
            this.btnESWriteIOM.Name = "btnESWriteIOM";
            this.btnESWriteIOM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESWriteIOM.TabIndex = 17;
            this.btnESWriteIOM.Text = "ESWriteIOM";
            this.btnESWriteIOM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESWriteIOM.UseVisualStyleBackColor = true;
            this.btnESWriteIOM.Click += new System.EventHandler( this.btnESWriteIOM_Click );
            // 
            // btnESReadIOM
            // 
            this.btnESReadIOM.Location = new System.Drawing.Point( 474, 12 );
            this.btnESReadIOM.Name = "btnESReadIOM";
            this.btnESReadIOM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESReadIOM.TabIndex = 16;
            this.btnESReadIOM.Text = "ESReadIOM";
            this.btnESReadIOM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESReadIOM.UseVisualStyleBackColor = true;
            this.btnESReadIOM.Click += new System.EventHandler( this.btnESReadIOM_Click );
            // 
            // btnESWriteRegisterM
            // 
            this.btnESWriteRegisterM.Location = new System.Drawing.Point( 474, 117 );
            this.btnESWriteRegisterM.Name = "btnESWriteRegisterM";
            this.btnESWriteRegisterM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESWriteRegisterM.TabIndex = 19;
            this.btnESWriteRegisterM.Text = "ESWriteRegisterM";
            this.btnESWriteRegisterM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESWriteRegisterM.UseVisualStyleBackColor = true;
            this.btnESWriteRegisterM.Click += new System.EventHandler( this.btnESWriteRegistorM_Click );
            // 
            // btnESReadRegisterM
            // 
            this.btnESReadRegisterM.Location = new System.Drawing.Point( 474, 82 );
            this.btnESReadRegisterM.Name = "btnESReadRegisterM";
            this.btnESReadRegisterM.Size = new System.Drawing.Size( 150, 23 );
            this.btnESReadRegisterM.TabIndex = 18;
            this.btnESReadRegisterM.Text = "ESReadRegisterM";
            this.btnESReadRegisterM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESReadRegisterM.UseVisualStyleBackColor = true;
            this.btnESReadRegisterM.Click += new System.EventHandler( this.btnESReadRegistorM_Click );
            // 
            // btnESGetAlarmEx
            // 
            this.btnESGetAlarmEx.Location = new System.Drawing.Point( 318, 47 );
            this.btnESGetAlarmEx.Name = "btnESGetAlarmEx";
            this.btnESGetAlarmEx.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetAlarmEx.TabIndex = 1;
            this.btnESGetAlarmEx.Text = "ESGetAlarmEx";
            this.btnESGetAlarmEx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetAlarmEx.UseVisualStyleBackColor = true;
            this.btnESGetAlarmEx.Click += new System.EventHandler( this.btnESGetAlarmEx_Click );
            // 
            // btnESGetAlarmHistEx
            // 
            this.btnESGetAlarmHistEx.Location = new System.Drawing.Point( 318, 117 );
            this.btnESGetAlarmHistEx.Name = "btnESGetAlarmHistEx";
            this.btnESGetAlarmHistEx.Size = new System.Drawing.Size( 150, 23 );
            this.btnESGetAlarmHistEx.TabIndex = 3;
            this.btnESGetAlarmHistEx.Text = "ESGetAlarmHistEx\r\n";
            this.btnESGetAlarmHistEx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESGetAlarmHistEx.UseVisualStyleBackColor = true;
            this.btnESGetAlarmHistEx.Click += new System.EventHandler( this.btnESGetAlarmHistEx_Click );
            // 
            // btnESCartMove
            // 
            this.btnESCartMove.Location = new System.Drawing.Point( 786, 257 );
            this.btnESCartMove.Name = "btnESCartMove";
            this.btnESCartMove.Size = new System.Drawing.Size( 150, 23 );
            this.btnESCartMove.TabIndex = 55;
            this.btnESCartMove.Text = "ESCartMove";
            this.btnESCartMove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESCartMove.UseVisualStyleBackColor = true;
            this.btnESCartMove.Click += new System.EventHandler( this.btnESCartMove_Click );
            // 
            // btnESPulseMove
            // 
            this.btnESPulseMove.Location = new System.Drawing.Point( 786, 292 );
            this.btnESPulseMove.Name = "btnESPulseMove";
            this.btnESPulseMove.Size = new System.Drawing.Size( 150, 23 );
            this.btnESPulseMove.TabIndex = 56;
            this.btnESPulseMove.Text = "ESPulseMove";
            this.btnESPulseMove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESPulseMove.UseVisualStyleBackColor = true;
            this.btnESPulseMove.Click += new System.EventHandler( this.btnESPulseMove_Click );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 948, 572 );
            this.Controls.Add( this.btnESPulseMove );
            this.Controls.Add( this.btnESCartMove );
            this.Controls.Add( this.btnESGetAlarmHistEx );
            this.Controls.Add( this.btnESGetAlarmEx );
            this.Controls.Add( this.btnESWriteRegisterM );
            this.Controls.Add( this.btnESReadRegisterM );
            this.Controls.Add( this.btnESWriteIOM );
            this.Controls.Add( this.btnESReadIOM );
            this.Controls.Add( this.btnESSetBpexPositionDataM );
            this.Controls.Add( this.btnESSetPositionDataM );
            this.Controls.Add( this.btnESSetStrDataM );
            this.Controls.Add( this.btnESSetVarDataMR );
            this.Controls.Add( this.btnESSetVarDataMD );
            this.Controls.Add( this.btnESSetVarDataMI );
            this.Controls.Add( this.btnESSetVarDataMB );
            this.Controls.Add( this.btnESGetBpexPositionDataM );
            this.Controls.Add( this.btnESGetPositionDataM );
            this.Controls.Add( this.btnESGetStrDataM );
            this.Controls.Add( this.btnESGetVarDataMR );
            this.Controls.Add( this.btnESGetVarDataMD );
            this.Controls.Add( this.btnESGetVarDataMI );
            this.Controls.Add( this.btnESGetVarDataMB );
            this.Controls.Add( this.btnESGetSystemInfo );
            this.Controls.Add( this.btnESGetMonitoringTime );
            this.Controls.Add( this.btnESFileList );
            this.Controls.Add( this.btnESDeleteJob );
            this.Controls.Add( this.btnESLoadFile );
            this.Controls.Add( this.btnESSaveFile );
            this.Controls.Add( this.btnESSelectJob );
            this.Controls.Add( this.btnESStartJob );
            this.Controls.Add( this.btnESBDSP );
            this.Controls.Add( this.btnESCycle );
            this.Controls.Add( this.btnESHlock );
            this.Controls.Add( this.btnESServo );
            this.Controls.Add( this.btnESHold );
            this.Controls.Add( this.btnESCancel );
            this.Controls.Add( this.btnESReset );
            this.Controls.Add( this.btnESSetBpexPositionData );
            this.Controls.Add( this.btnESSetPositionData );
            this.Controls.Add( this.btnESGetPositionData );
            this.Controls.Add( this.btnESSetStrData );
            this.Controls.Add( this.btnESGetStrData );
            this.Controls.Add( this.btnESSetVarData );
            this.Controls.Add( this.btnESGetVarData );
            this.Controls.Add( this.btnESWriteRegister );
            this.Controls.Add( this.btnESReadRegister );
            this.Controls.Add( this.btnESWriteIO );
            this.Controls.Add( this.btnESReadIO );
            this.Controls.Add( this.btnESGetTorque );
            this.Controls.Add( this.btnESGetDeviation );
            this.Controls.Add( this.btnESGetJobStatus );
            this.Controls.Add( this.btnESGetStatus );
            this.Controls.Add( this.btnESGetAlarmHist );
            this.Controls.Add( this.btnESGetAlarm );
            this.Controls.Add( this.btnESGetBpexPositionData );
            this.Controls.Add( this.btnESGetConfiguration );
            this.Controls.Add( this.btnESGetPosition );
            this.Controls.Add( this.textBox1 );
            this.Font = new System.Drawing.Font( "ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 128 ) ) );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnESGetPosition;
        private System.Windows.Forms.Button btnESGetBpexPositionData;
        private System.Windows.Forms.Button btnESGetAlarm;
        private System.Windows.Forms.Button btnESGetAlarmHist;
        private System.Windows.Forms.Button btnESGetStatus;
        private System.Windows.Forms.Button btnESGetJobStatus;
        private System.Windows.Forms.Button btnESGetDeviation;
        private System.Windows.Forms.Button btnESGetTorque;
        private System.Windows.Forms.Button btnESReadIO;
        private System.Windows.Forms.Button btnESWriteIO;
        private System.Windows.Forms.Button btnESSetVarData;
        private System.Windows.Forms.Button btnESGetVarData;
        private System.Windows.Forms.Button btnESWriteRegister;
        private System.Windows.Forms.Button btnESReadRegister;
        private System.Windows.Forms.Button btnESReset;
        private System.Windows.Forms.Button btnESSetBpexPositionData;
        private System.Windows.Forms.Button btnESSetPositionData;
        private System.Windows.Forms.Button btnESGetPositionData;
        private System.Windows.Forms.Button btnESSetStrData;
        private System.Windows.Forms.Button btnESGetStrData;
        private System.Windows.Forms.Button btnESCycle;
        private System.Windows.Forms.Button btnESHlock;
        private System.Windows.Forms.Button btnESServo;
        private System.Windows.Forms.Button btnESHold;
        private System.Windows.Forms.Button btnESCancel;
        private System.Windows.Forms.Button btnESSelectJob;
        private System.Windows.Forms.Button btnESStartJob;
        private System.Windows.Forms.Button btnESBDSP;
        private System.Windows.Forms.Button btnESDeleteJob;
        private System.Windows.Forms.Button btnESLoadFile;
        private System.Windows.Forms.Button btnESSaveFile;
        private System.Windows.Forms.Button btnESFileList;
        private System.Windows.Forms.Button btnESGetConfiguration;
        private System.Windows.Forms.Button btnESGetMonitoringTime;
        private System.Windows.Forms.Button btnESGetSystemInfo;
        private System.Windows.Forms.Button btnESGetVarDataMB;
        private System.Windows.Forms.Button btnESGetVarDataMI;
        private System.Windows.Forms.Button btnESGetVarDataMR;
        private System.Windows.Forms.Button btnESGetVarDataMD;
        private System.Windows.Forms.Button btnESGetStrDataM;
        private System.Windows.Forms.Button btnESGetPositionDataM;
        private System.Windows.Forms.Button btnESGetBpexPositionDataM;
        private System.Windows.Forms.Button btnESSetStrDataM;
        private System.Windows.Forms.Button btnESSetVarDataMR;
        private System.Windows.Forms.Button btnESSetVarDataMD;
        private System.Windows.Forms.Button btnESSetVarDataMI;
        private System.Windows.Forms.Button btnESSetVarDataMB;
        private System.Windows.Forms.Button btnESSetBpexPositionDataM;
        private System.Windows.Forms.Button btnESSetPositionDataM;
        private System.Windows.Forms.Button btnESWriteIOM;
        private System.Windows.Forms.Button btnESReadIOM;
        private System.Windows.Forms.Button btnESWriteRegisterM;
        private System.Windows.Forms.Button btnESReadRegisterM;
        private System.Windows.Forms.Button btnESGetAlarmEx;
        private System.Windows.Forms.Button btnESGetAlarmHistEx;
        private System.Windows.Forms.Button btnESCartMove;
        private System.Windows.Forms.Button btnESPulseMove;
    }
}

