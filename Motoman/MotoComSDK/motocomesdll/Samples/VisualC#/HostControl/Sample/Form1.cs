using System;
using System.Windows.Forms;

using MotoComES_CS;

namespace Sample
{
    public partial class Form1 : Form
    {
        #region フィールド
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region イベント
        /// <summary>
        /// ESGetAlarm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetAlarm_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetAlarm();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetAlarmEx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetAlarmEx_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetAlarmEx();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetAlarmHist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetAlarmHist_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetAlarmHist();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetAlarmHistEx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetAlarmHistEx_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetAlarmHistEx();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetStatus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetStatus_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetStatus();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetJobStatus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetJobStatus_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetJobStatus();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetConfiguration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetConfiguration_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetConfiguration();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetPosition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetPosition_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetPosition();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetDeviation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetDeviation_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetDeviation();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetTorque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetTorque_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetTorque();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetMonitoringTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetMonitoringTime_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetMonitoringTime();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();

        }

        /// <summary>
        /// ESGetSystemInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetSystemInfo_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetSystemInfo();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();

        }

        /// <summary>
        /// ESReadIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESReadIO_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESReadIO1();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESWriteIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESWriteIO_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESWriteIO1();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESReadRegister
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESReadRegistor_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESReadRegister();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESWriteRegister
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESWriteRegistor_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESWriteRegister();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESReadIOM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESReadIOM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESReadIOM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESWriteIOM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESWriteIOM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESWriteIOM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESReadRegisterM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESReadRegistorM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESReadRegisterM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESWriteRegisterM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESWriteRegistorM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESWriteRegisterM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetVarData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetVarData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetVarData1();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetVarData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetVarData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetVarData1();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetStrData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetStrData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetStrData();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetStrData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetStrData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetStrData();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetPositionData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetPositionData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetPositionData();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetPositionData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetPositionData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetPositionData();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetBpexPositionData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetBpexPositionData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetBpexPositionData();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetBpexPositionData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetBpexPositionData_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetBpexPositionData();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetVarDataMB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetVarDataMB_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetVarDataMB();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetVarDataMB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetVarDataMB_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetVarDataMB();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetVarDataMI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetVarDataMI_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetVarDataMI();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetVarDataMI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetVarDataMI_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetVarDataMI();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetVarDataMD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetVarDataMD_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetVarDataMD();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetVarDataMD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetVarDataMD_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetVarDataMD();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetVarDataMR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetVarDataMR_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetVarDataMR();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetVarDataMR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetVarDataMR_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetVarDataMR();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetStrDataM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetStrDataM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetStrDataM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetStrDataM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetStrDataM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetStrDataM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetPositionDataM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetPositionDataM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetPositionDataM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetPositionDataM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetPositionDataM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetPositionDataM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESGetBpexPositionDataM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESGetBpexPositionDataM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESGetBpexPositionDataM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSetBpexPositionDataM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSetBpexPositionDataM_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSetBpexPositionDataM();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESReset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESReset_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESReset();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESCancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESCancel_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESCancel();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESHold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESHold_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESHold();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESServo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESServo_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESServo();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESHlock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESHlock_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESHlock();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESCycle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESCycle_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESCycle();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESBDSP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESBDSP_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESBDSP();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESStartJob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESStartJob_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESStartJob();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSelectJob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSelectJob_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSelectJob();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESSaveFile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESSaveFile_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESSaveFile();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESLoadFile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESLoadFile_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESLoadFile();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESDeleteJob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESDeleteJob_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESDeleteJob();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();

        }

        /// <summary>
        /// ESFileList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESFileList_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            // 引数となる構造体変数の定義
            string sFileName = string.Empty;

            // ESFileListFirst
            Program.res = Program._Command.ESFileListFirst( ref sFileName );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( sFileName );
                Program._Temp.AppendLine( "" );
            }
            
            else
            {
                // 結果を表示
                textBox1.Text += Program._Temp.ToString();
                return;
            }

            // ESFileListNext
            for( ; ; )
            {
                Program.res = Program._Command.ESFileListNext( ref sFileName );

                // 戻り値が MotoComES.OK(0):正常処理
                if( Program.res == MotoComES.OK )
                {
                    Program._Temp.AppendLine( sFileName );
                    Program._Temp.AppendLine( "" );
                }

                else
                {
                    break;
                }
            }

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESCartMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESCartMove_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESCartMove();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }

        /// <summary>
        /// ESPulseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnESPulseMove_Click( object sender, EventArgs e )
        {
            Program.res = -1;

            //ESOpen
            Program.res = Program._Command.ESOpen();

            if( Program.res != MotoComES.OK )
            {
                return;
            }

            Program._Command.ESPulseMove();

            //ESClose
            Program._Command.ESClose();

            // 結果を表示
            textBox1.Text += Program._Temp.ToString();
        }
        #endregion

        #region メソッド
        #endregion
    }
}
