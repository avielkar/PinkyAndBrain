using System;
using System.Text;

using MotoComES_CS;

namespace Sample
{
    /// <summary>
    /// 関数実行クラス
    /// </summary>
    public class clsCommand
    {
        #region コマンド

        /// <summary>
        /// ESOpen
        /// </summary>
        /// <returns></returns>
        public int ESOpen()
        {
            Program._Temp = new StringBuilder();
            Program.res = -1;

            // IPアドレス文字列をバイト配列へ
            int iByteCount = MotoComES._ECode.GetByteCount( "192.168.255.1" ) + 1;
            byte[] bIPAdd = MotoComES.StringToByteArray( "192.168.255.1", iByteCount );

            Program._Temp.AppendLine( "ESOpen" );

            // 関数を実行
            Program.res = MotoComES.ESOpen( 1, ref bIPAdd[ 0 ], ref Program._Handle );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "handle:" + Program._Handle.ToString() );
            }

            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESClose
        /// </summary>
        public int ESClose()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESClose" );

            // 関数を実行
            Program.res = MotoComES.ESClose( Program._Handle );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Handle = IntPtr.Zero;
            }

            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetAlarm
        /// </summary>
        /// <returns></returns>
        public int ESGetAlarm()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESAlarmList alarmList = new MotoComES.ESAlarmList();

            Program._Temp.AppendLine( "ESGetAlarm" );

            // 関数を実行
            Program.res = MotoComES.ESGetAlarm( Program._Handle, ref alarmList );

            Program._Temp.AppendLine( "res\t" + Program.res.ToString() );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                // 格納された要素数で繰り返す
                for( int i = 0; i < alarmList.data.Length; i++ )
                {
                    Program._Temp.AppendLine( "alarmCode\t" + alarmList.data[ i ].alarmCode.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmData\t" + alarmList.data[ i ].alarmData.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmType\t" + alarmList.data[ i ].alarmType.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmTime\t" + MotoComES.ByteArrayToString( alarmList.data[ i ].alarmTime ) );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmName\t" + MotoComES.ByteArrayToString( alarmList.data[ i ].alarmName ) );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetAlarmEx
        /// </summary>
        /// <returns></returns>
        public int ESGetAlarmEx()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESAlarmListEx alarmListEx = new MotoComES.ESAlarmListEx();

            Program._Temp.AppendLine( "ESGetAlarm" );

            // 関数を実行
            Program.res = MotoComES.ESGetAlarmEx( Program._Handle, ref alarmListEx );

            Program._Temp.AppendLine( "res\t" + Program.res.ToString() );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                // 格納された要素数で繰り返す
                for( int i = 0; i < alarmListEx.data.Length; i++ )
                {
                    Program._Temp.AppendLine( "alarmCode\t" + alarmListEx.data[ i ].alarmData.alarmCode.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmData\t" + alarmListEx.data[ i ].alarmData.alarmData.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmType\t" + alarmListEx.data[ i ].alarmData.alarmType.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmTime\t" + MotoComES.ByteArrayToString( alarmListEx.data[ i ].alarmData.alarmTime ) );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmName\t" + MotoComES.ByteArrayToString( alarmListEx.data[ i ].alarmData.alarmName ) );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmAddInfo" + MotoComES.ByteArrayToString( alarmListEx.data[ i ].subcodeData.alarmAddInfo ) );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmStrData" + MotoComES.ByteArrayToString( alarmListEx.data[ i ].subcodeData.alarmStrData ) );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "alarmHighlightData" + MotoComES.ByteArrayToString( alarmListEx.data[ i ].subcodeData.alarmHighlightData ) );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetAlarmHist
        /// </summary>
        /// <returns></returns>
        public int ESGetAlarmHist()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESAlarmData alarmData = new MotoComES.ESAlarmData();

            Program._Temp.AppendLine( "ESGetAlarmHist" );

            // 関数を実行
            Program.res = MotoComES.ESGetAlarmHist( Program._Handle, 1001, ref alarmData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "alarmCode\t" + alarmData.alarmCode.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmData\t" + alarmData.alarmData.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmType\t" + alarmData.alarmType.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmTime\t" + MotoComES.ByteArrayToString( alarmData.alarmTime ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmName\t" + MotoComES.ByteArrayToString( alarmData.alarmName ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }
            return Program.res;
        }

        /// <summary>
        /// ESGetAlarmHistEx
        /// </summary>
        /// <returns></returns>
        public int ESGetAlarmHistEx()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESAlarmDataEx alarmDataEx = new MotoComES.ESAlarmDataEx();

            Program._Temp.AppendLine( "ESGetAlarmHist" );

            // 関数を実行
            Program.res = MotoComES.ESGetAlarmHistEx( Program._Handle, 1001, ref alarmDataEx );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "alarmCode\t" + alarmDataEx.alarmData.alarmCode.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmData\t" + alarmDataEx.alarmData.alarmData.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmType\t" + alarmDataEx.alarmData.alarmType.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmTime\t" + MotoComES.ByteArrayToString( alarmDataEx.alarmData.alarmTime ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmName\t" + MotoComES.ByteArrayToString( alarmDataEx.alarmData.alarmName ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmAddInfo" + MotoComES.ByteArrayToString( alarmDataEx.subcodeData.alarmAddInfo ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmStrData" + MotoComES.ByteArrayToString( alarmDataEx.subcodeData.alarmStrData ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "alarmHighlightData" + MotoComES.ByteArrayToString( alarmDataEx.subcodeData.alarmHighlightData ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }
            return Program.res;
        }

        /// <summary>
        /// ESGetStatus
        /// </summary>
        /// <returns></returns>
        public int ESGetStatus()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESStatusData statusData = new MotoComES.ESStatusData();

            Program._Temp.AppendLine( "ESGetStatus" );

            //関数を実行
            Program.res = MotoComES.ESGetStatus( Program._Handle, ref statusData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "status1\t" + Convert.ToString( statusData.status1, 2 ).PadLeft( 8, '0' ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "status2\t" + Convert.ToString( statusData.status2, 2 ).PadLeft( 8, '0' ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }
            return Program.res;
        }

        /// <summary>
        /// ESGetJobStatus
        /// </summary>
        /// <returns></returns>
        public int ESGetJobStatus()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESJobStatusData jobStatusData = new MotoComES.ESJobStatusData();

            Program._Temp.AppendLine( "ESGetJobStatus" );

            //関数を実行
            Program.res = MotoComES.ESGetJobStatus( Program._Handle, 1, ref jobStatusData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "jobName\t" + MotoComES.ByteArrayToString( jobStatusData.jobName ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "lineNo\t" + jobStatusData.lineNo.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "stepNo\t" + jobStatusData.stepNo.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "sppedOverride\t" + jobStatusData.speedOverride.ToString() );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            //ESClose
            ESClose();
            return Program.res;
        }

        /// <summary>
        /// ESGetConfiguration
        /// </summary>
        /// <returns></returns>
        public int ESGetConfiguration()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESConfigurationData configData = new MotoComES.ESConfigurationData();

            Program._Temp.AppendLine( "ESGetConfiguration" );

            // 関数を実行
            Program.res = MotoComES.ESGetConfiguration( Program._Handle, 1, ref configData );

            Program._Temp.AppendLine( "res\t" + Program.res.ToString() );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                // 配列の要素数
                const int iLength1 = MotoComES.ESConfigurationData.configurations_Length1;
                const int iLength2 = MotoComES.ESConfigurationData.configurations_Length2;

                // 変換用配列
                byte[][] bTemp = new byte[ iLength1 ][];

                for( int i = 0; i < bTemp.Length; i++ )
                {
                    bTemp[ i ] = new byte[ iLength2 ];
                }

                // 配列の変換
                MotoComES.ArrayEdit( configData.configurations, iLength1, iLength2, ref bTemp );

                for( int i = 0; i < MotoComES.ESConfigurationData.configurations_Length1; i++ )
                {
                    Program._Temp.AppendLine( "axis" + ( i + 1 ).ToString() + "\t" + MotoComES.ByteArrayToString( bTemp[ i ] ) );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetPosition
        /// </summary>
        /// <returns></returns>
        public int ESGetPosition()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESPositionData positionData = new MotoComES.ESPositionData();

            Program._Temp.AppendLine( "ESGetPosition" );

            // 関数を実行
            Program.res = MotoComES.ESGetPosition( Program._Handle, 101, ref positionData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "dataType\t" + positionData.dataType.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "fig\t" + positionData.fig.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "toolNo\t" + positionData.toolNo.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "userFrameNo\t" + positionData.userFrameNo.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "exFig\t" + positionData.exFig.ToString() );
                Program._Temp.AppendLine( "" );
                for( int i = 0; i < MotoComES.ESAxisData.axis_Length; i++ )
                {
                    Program._Temp.AppendLine( "axis" + ( i + 1 ).ToString() + "\t" + positionData.axesData.axis[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetDeviation
        /// </summary>
        /// <returns></returns>
        public int ESGetDeviation()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESAxisData axisData = new MotoComES.ESAxisData();

            Program._Temp.AppendLine( "ESGetDeviation" );

            // 関数を実行
            Program.res = MotoComES.ESGetDeviation( Program._Handle, 1, ref axisData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < MotoComES.ESAxisData.axis_Length; i++ )
                {
                    Program._Temp.AppendLine( "axis" + ( i + 1 ).ToString() + "\t" + axisData.axis[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetTorque
        /// </summary>
        /// <returns></returns>
        public int ESGetTorque()
        {
            // 引数となる構造体変数の定義
            MotoComES.ESAxisData axisData = new MotoComES.ESAxisData();

            Program._Temp.AppendLine( "ESGetTorque" );

            // 関数を実行
            Program.res = MotoComES.ESGetTorque( Program._Handle, 1, ref axisData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < MotoComES.ESAxisData.axis_Length; i++ )
                {
                    Program._Temp.AppendLine( "axis" + ( i + 1 ).ToString() + "\t" + axisData.axis[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetMonitoringTime
        /// </summary>
        /// <returns></returns>
        public int ESGetMonitoringTime()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMonitoringTimeData timeData = new MotoComES.ESMonitoringTimeData();

            Program._Temp.AppendLine( "ESGetMonitoringTime" );

            // 関数を実行
            Program.res = MotoComES.ESGetMonitoringTime( Program._Handle, 1, ref timeData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "startTime\t" + MotoComES.ByteArrayToString( timeData.startTime ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "elapseTime\t" + MotoComES.ByteArrayToString( timeData.elapseTime ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESGetSystemInfo
        /// </summary>
        /// <returns></returns>
        public int ESGetSystemInfo()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESSystemInfoData infoData = new MotoComES.ESSystemInfoData();

            Program._Temp.AppendLine( "ESGetMonitoringTime" );

            // 関数を実行
            Program.res = MotoComES.ESGetSystemInfo( Program._Handle, 11, ref infoData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "systemVersion\t" + MotoComES.ByteArrayToString( infoData.systemVersion ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "name\t" + MotoComES.ByteArrayToString( infoData.name ) );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "parameterNo\t" + MotoComES.ByteArrayToString( infoData.parameterNo ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESReadIO1
        /// </summary>
        /// <returns></returns>
        public int ESReadIO1()
        {
            Program.res = -1;

            // 引数となる変数を定義
            short ioData = new short();

            Program._Temp.AppendLine( "ESReadIO" );

            // 関数を実行
            Program.res = MotoComES.ESReadIO1( Program._Handle, 2501, ref ioData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "ioData\t" + Convert.ToString( ioData, 2 ).ToString().PadLeft( 8, '0' ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESWriteIO1
        /// </summary>
        /// <returns></returns>
        public int ESWriteIO1()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESWriteIO" );

            // 関数を実行
            Program.res = MotoComES.ESWriteIO1( Program._Handle, 2501, 1 );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );


            return Program.res;
        }

        /// <summary>
        /// ESReadIO2
        /// </summary>
        /// <returns></returns>
        public int ESReadIO2()
        {
            Program.res = -1;

            // 引数となる変数を定義
            short ioData = new short();

            Program._Temp.AppendLine( "ESReadIO" );

            // 関数を実行
            Program.res = MotoComES.ESReadIO2( Program._Handle, 2501, ref ioData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "ioData\t" + Convert.ToString( ioData, 2 ).ToString().PadLeft( 8, '0' ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESWriteIO2
        /// </summary>
        /// <returns></returns>
        public int ESWriteIO2()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESWriteIO" );

            // 関数を実行
            Program.res = MotoComES.ESWriteIO2( Program._Handle, 2501, 1 );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESReadRegister
        /// </summary>
        /// <returns></returns>
        public int ESReadRegister()
        {
            Program.res = -1;

            // 引数となる変数を定義
            ushort regData = new ushort();

            Program._Temp.AppendLine( "ESReadRegister" );

            // 関数を実行
            Program.res = MotoComES.ESReadRegister( Program._Handle, 0 + 1, ref regData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "regData\t" + Convert.ToString( regData, 2 ).ToString().PadLeft( 16, '0' ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESWriteRegister
        /// </summary>
        /// <returns></returns>
        public int ESWriteRegister()
        {
            Program.res = -1;

            // 引数となる変数を定義
            ushort regData = new ushort();

            Program._Temp.AppendLine( "ESWriteRegister" );

            unchecked
            {
                regData = 65535;
            }

            // 関数を実行
            Program.res = MotoComES.ESWriteRegister( Program._Handle, 0 + 1, regData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESReadIOM
        /// </summary>
        /// <returns></returns>
        public int ESReadIOM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiByteData multiData = new MotoComES.ESMultiByteData();

            Program._Temp.AppendLine( "ESReadIOM" );

            // 関数を実行
            Program.res = MotoComES.ESReadIOM( Program._Handle, 2501, 2, ref multiData );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "#250" + i + "X\t" + Convert.ToString( multiData.data[ i ], 2 ).ToString().PadLeft( 8, '0' ) );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESWriteIOM
        /// </summary>
        /// <returns></returns>
        public int ESWriteIOM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiByteData multiData = new MotoComES.ESMultiByteData();
            multiData.data = new byte[ MotoComES.ESMultiByteData.multiData_Length ];

            //multiDataに値をセット
            unchecked
            {
                for( int i = 0; i < 2; i++ )
                {
                    multiData.data[ i ] = ( byte )( i + 1 );
                }
            }

            Program._Temp.AppendLine( "ESWriteIOM" );

            // 関数を実行
            Program.res = MotoComES.ESWriteIOM( Program._Handle, 2501, 2, multiData );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESReadRegisterM
        /// </summary>
        /// <returns></returns>
        public int ESReadRegisterM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiUShortData multiData = new MotoComES.ESMultiUShortData();

            Program._Temp.AppendLine( "ESReadRegisterM" );

            // 関数を実行
            Program.res = MotoComES.ESReadRegisterM( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "Registor(" + i + ")\t" + Convert.ToString( multiData.data[ i ], 2 ).ToString().PadLeft( 16, '0' ) );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESWriteRegisterM
        /// </summary>
        /// <returns></returns>
        public int ESWriteRegisterM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiUShortData multiData = new MotoComES.ESMultiUShortData();
            multiData.data = new ushort[ MotoComES.ESMultiUShortData.multiData_Length ];

            //multiDataに値をセット
            unchecked
            {
                for( int i = 0; i < 2; i++ )
                {
                    multiData.data[ i ] = ( ushort )( i + 1 );
                }
            }

            Program._Temp.AppendLine( "ESWriteRegisterM" );

            // 関数を実行
            Program.res = MotoComES.ESWriteRegisterM( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );
            return Program.res;
        }

        /// <summary>
        /// ESGetVarData1
        /// </summary>
        /// <returns></returns>
        public int ESGetVarData1()
        {
            Program.res = -1;

            // 引数となる変数を定義
            double data = new double();

            Program._Temp.AppendLine( "ESGetVarData" );

            // 関数を実行
            Program.res = MotoComES.ESGetVarData1( Program._Handle, 1, 0 + 1, ref data ); //RS022=0, B変数取得

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "data\t" + data.ToString() );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetVarData1
        /// </summary>
        /// <returns></returns>
        public int ESSetVarData1()
        {
            Program.res = -1;

            // 引数となる変数を定義
            double data = new double();

            Program._Temp.AppendLine( "ESSetVarData" );

            data = ( double )1;

            // 関数を実行
            Program.res = MotoComES.ESSetVarData1( Program._Handle, 1, 0 + 1, data ); //RS022=0, R変数

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetVarData2
        /// </summary>
        /// <returns></returns>
        public int ESGetVarData2()
        {
            Program.res = -1;

            // 引数となる変数を定義
            double data = new double();

            Program._Temp.AppendLine( "ESGetVarData" );

            // 関数を実行
            Program.res = MotoComES.ESGetVarData2( Program._Handle, 1, 0 + 1, ref data ); //RS022=0, B変数取得

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "data\t" + data.ToString() );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetVarData2
        /// </summary>
        /// <returns></returns>
        public int ESSetVarData2()
        {
            Program.res = -1;

            // 引数となる変数を定義
            double data = new double();

            Program._Temp.AppendLine( "ESSetVarData" );

            data = ( double )1;

            // 関数を実行
            Program.res = MotoComES.ESSetVarData2( Program._Handle, 1, 0 + 1, data ); //RS022=0, R変数

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetStrData
        /// </summary>
        /// <returns></returns>
        public int ESGetStrData()
        {
            Program.res = -1;

            // 引数となる変数を定義
            byte[] data = new byte[ MotoComES.String_Length ];

            Program._Temp.AppendLine( "ESGetStrData" );

            // 関数を実行
            Program.res = MotoComES.ESGetStrData( Program._Handle, 0 + 1, ref data[ 0 ] ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "data\t" + MotoComES.ByteArrayToString( data ) );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetStrData
        /// </summary>
        /// <returns></returns>
        public int ESSetStrData()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESSetStrData" );

            // 引数となる変数を定義
            byte[] cp = MotoComES.StringToByteArray( "test", MotoComES._ECode.GetByteCount( "test" ) + 1 );

            // 関数を実行         
            Program.res = MotoComES.ESSetStrData( Program._Handle, 0 + 1, ref cp[ 0 ] ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetPositionData
        /// </summary>
        /// <returns></returns>
        public int ESGetPositionData()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESPositionData positionData = new MotoComES.ESPositionData();

            Program._Temp.AppendLine( "ESGetPositionData" );

            // 関数を実行
            Program.res = MotoComES.ESGetPositionData( Program._Handle, 0 + 1, ref positionData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "dataType\t" + positionData.dataType.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "fig\t" + positionData.fig.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "toolNo\t" + positionData.toolNo.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "userFrameNo\t" + positionData.userFrameNo.ToString() );
                Program._Temp.AppendLine( "" );
                Program._Temp.AppendLine( "exFig\t" + positionData.exFig.ToString() );
                Program._Temp.AppendLine( "" );
                for( int i = 0; i < MotoComES.ESAxisData.axis_Length; i++ )
                {
                    Program._Temp.AppendLine( "axis" + ( i + 1 ).ToString() + "\t" + positionData.axesData.axis[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetPositionData
        /// </summary>
        /// <returns></returns>
        public int ESSetPositionData()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESPositionData positionData = new MotoComES.ESPositionData();
            positionData.axesData = new MotoComES.ESAxisData();
            positionData.axesData.axis = new double[ MotoComES.ESAxisData.axis_Length ];

            //positionDataに値をセット
            positionData.dataType = 16; //ベース座標
            positionData.fig = 0;
            positionData.toolNo = 0;
            positionData.userFrameNo = 0;
            positionData.exFig = 0;
            for( int i = 0; i < MotoComES.ESAxisData.axis_Length - 2; i++ )
            {
                positionData.axesData.axis[ i ] = i + 1;
            }
            positionData.axesData.axis[ 6 ] = 0;
            positionData.axesData.axis[ 7 ] = 0;

            Program._Temp.AppendLine( "ESSetPositionData" );

            // 関数を実行
            Program.res = MotoComES.ESSetPositionData( Program._Handle, 0 + 1, positionData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetBpexPositionData
        /// </summary>
        /// <returns></returns>
        public int ESGetBpexPositionData()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESBpexPositionData positionData = new MotoComES.ESBpexPositionData();

            Program._Temp.AppendLine( "ESGetBpexPositionData" );

            // 関数を実行
            Program.res = MotoComES.ESGetBpexPositionData( Program._Handle, 1, 0 + 1, ref positionData ); //RS022=0,BP変数

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "dataType\t" + positionData.dataType.ToString() );
                Program._Temp.AppendLine( "" );
                for( int i = 0; i < MotoComES.ESAxisData.axis_Length; i++ )
                {
                    Program._Temp.AppendLine( "axis" + ( i + 1 ).ToString() + "\t" + positionData.axesData.axis[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetBpexPositionData
        /// </summary>
        /// <returns></returns>
        public int ESSetBpexPositionData()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESBpexPositionData positionData = new MotoComES.ESBpexPositionData();
            positionData.axesData = new MotoComES.ESAxisData();
            positionData.axesData.axis = new double[ MotoComES.ESAxisData.axis_Length ];

            //positionDataに値をセット
            positionData.dataType = 16; //ベース座標
            for( int i = 0; i < 3; i++ )
            {
                positionData.axesData.axis[ i ] = i + 1;
            }
            positionData.axesData.axis[ 3 ] = 0;
            positionData.axesData.axis[ 4 ] = 0;
            positionData.axesData.axis[ 5 ] = 0;
            positionData.axesData.axis[ 6 ] = 0;
            positionData.axesData.axis[ 7 ] = 0;

            Program._Temp.AppendLine( "ESSetBpexPositionData" );

            // 関数を実行
            Program.res = MotoComES.ESSetBpexPositionData( Program._Handle, 1, 0 + 1, positionData ); //RS022=0,BP変数

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetVarDataMB
        /// </summary>
        /// <returns></returns>
        public int ESGetVarDataMB()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiByteData multiData = new MotoComES.ESMultiByteData();

            Program._Temp.AppendLine( "ESGetVarDataMB" );

            // 関数を実行
            Program.res = MotoComES.ESGetVarDataMB( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "B(" + i + ")\t" + multiData.data[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetVarDataMB
        /// </summary>
        /// <returns></returns>
        public int ESSetVarDataMB()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiByteData multiData = new MotoComES.ESMultiByteData();
            multiData.data = new byte[ MotoComES.ESMultiByteData.multiData_Length ];

            //multiDataに値をセット
            unchecked
            {
                for( int i = 0; i < 2; i++ )
                {
                    multiData.data[ i ] = ( byte )( i + 1 );
                }
            }

            Program._Temp.AppendLine( "ESSetVarDataMB" );

            // 関数を実行
            Program.res = MotoComES.ESSetVarDataMB( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetVarDataMI
        /// </summary>
        /// <returns></returns>
        public int ESGetVarDataMI()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiShortData multiData = new MotoComES.ESMultiShortData();

            Program._Temp.AppendLine( "ESGetVarDataMI" );

            // 関数を実行
            Program.res = MotoComES.ESGetVarDataMI( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "I(" + i + ")\t" + multiData.data[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetVarDataMI
        /// </summary>
        /// <returns></returns>
        public int ESSetVarDataMI()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiShortData multiData = new MotoComES.ESMultiShortData();
            multiData.data = new short[ MotoComES.ESMultiShortData.multiData_Length ];

            //multiDataに値をセット
            unchecked
            {
                for( int i = 0; i < 2; i++ )
                {
                    multiData.data[ i ] = ( short )( i + 1 );
                }
            }

            Program._Temp.AppendLine( "ESSetVarDataMI" );

            // 関数を実行
            Program.res = MotoComES.ESSetVarDataMI( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetVarDataMD
        /// </summary>
        /// <returns></returns>
        public int ESGetVarDataMD()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiLongData multiData = new MotoComES.ESMultiLongData();

            Program._Temp.AppendLine( "ESGetVarDataMD" );

            // 関数を実行
            Program.res = MotoComES.ESGetVarDataMD( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "D(" + i + ")\t" + multiData.data[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetVarDataMD
        /// </summary>
        /// <returns></returns>
        public int ESSetVarDataMD()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiLongData multiData = new MotoComES.ESMultiLongData();
            multiData.data = new int[ MotoComES.ESMultiLongData.multiData_Length ];

            //multiDataに値をセット
            for( int i = 0; i < 2; i++ )
            {
                multiData.data[ i ] = ( int )( i + 1 );
            }

            Program._Temp.AppendLine( "ESSetVarDataMD" );

            // 関数を実行
            Program.res = MotoComES.ESSetVarDataMD( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetVarDataMR
        /// </summary>
        /// <returns></returns>
        public int ESGetVarDataMR()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiRealData multiData = new MotoComES.ESMultiRealData();

            Program._Temp.AppendLine( "ESGetVarDataMR" );

            // 関数を実行
            Program.res = MotoComES.ESGetVarDataMR( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "R(" + i + ")\t" + multiData.data[ i ].ToString() );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetVarDataMR
        /// </summary>
        /// <returns></returns>
        public int ESSetVarDataMR()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiRealData multiData = new MotoComES.ESMultiRealData();
            multiData.data = new double[ MotoComES.ESMultiRealData.multiData_Length ];

            //multiDataに値をセット
            for( int i = 0; i < 2; i++ )
            {
                multiData.data[ i ] = ( double )( i + 1 );
            }

            Program._Temp.AppendLine( "ESSetVarDataMR" );

            // 関数を実行
            Program.res = MotoComES.ESSetVarDataMR( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetStrDataM
        /// </summary>
        /// <returns></returns>
        public int ESGetStrDataM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiStrData multiData = new MotoComES.ESMultiStrData();

            Program._Temp.AppendLine( "ESGetStrDataM" );

            // 関数を実行
            Program.res = MotoComES.ESGetStrDataM( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                // 配列の要素数
                const int iLength1 = MotoComES.ESMultiStrData.multiData_Length1;
                const int iLength2 = MotoComES.ESMultiStrData.multiData_Length2;

                // 変換用配列
                byte[][] bTemp = new byte[ iLength1 ][];

                for( int i = 0; i < bTemp.Length; i++ )
                {
                    bTemp[ i ] = new byte[ iLength2 ];
                }

                // 配列の変換
                MotoComES.ArrayEdit( multiData.data, iLength1, iLength2, ref bTemp );

                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "S(" + i + ")\t" + MotoComES.ByteArrayToString( bTemp[ i ] ) );
                    Program._Temp.AppendLine( "" );
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetStrDataM
        /// </summary>
        /// <returns></returns>
        public int ESSetStrDataM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiStrData multiData = new MotoComES.ESMultiStrData();
            multiData.data = new byte[ MotoComES.ESMultiStrData.multiData_Length ];

            //multiDataに値をセット
            for( int i = 0; i < 2; i++ )
            {
                byte[] b = MotoComES.StringToByteArray( "test" + i.ToString(), MotoComES.ESMultiStrData.multiData_Length2 );

                for( int j = 0; j < MotoComES.ESMultiStrData.multiData_Length2 - 1; j++ )
                {
                    multiData.data[ i * MotoComES.ESMultiStrData.multiData_Length2 + j ] = b[ j ];
                }
            }

            Program._Temp.AppendLine( "ESSetStrDataM" );

            // 関数を実行
            Program.res = MotoComES.ESSetStrDataM( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESGetPositionDataM
        /// </summary>
        /// <returns></returns>
        public int ESGetPositionDataM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiPositionData multiData = new MotoComES.ESMultiPositionData();

            Program._Temp.AppendLine( "ESGetPositionDataM" );

            // 関数を実行
            Program.res = MotoComES.ESGetPositionDataM( Program._Handle, 0 + 1, 2, ref multiData ); //RS022=0

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "P(" + i + ")" );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "dataType\t" + multiData.data[ i ].dataType.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "fig\t" + multiData.data[ i ].fig.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "toolNo\t" + multiData.data[ i ].toolNo.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "userFrameNo\t" + multiData.data[ i ].userFrameNo.ToString() );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "exFig\t" + multiData.data[ i ].exFig.ToString() );
                    Program._Temp.AppendLine( "" );
                    for( int j = 0; j < MotoComES.ESAxisData.axis_Length; j++ )
                    {
                        Program._Temp.AppendLine( "axis" + ( j + 1 ).ToString() + "\t" + multiData.data[ i ].axesData.axis[ j ].ToString() );
                        Program._Temp.AppendLine( "" );
                    }
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetPositionDataM
        /// </summary>
        /// <returns></returns>
        public int ESSetPositionDataM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiPositionData multiData = new MotoComES.ESMultiPositionData();
            multiData.data = new MotoComES.ESPositionData[ MotoComES.ESMultiPositionData.multiData_Length ];
            for( int i = 0; i < MotoComES.ESMultiPositionData.multiData_Length; i++ )
            {
                multiData.data[ i ].dataType = new int();
                multiData.data[ i ].fig = new int();
                multiData.data[ i ].toolNo = new int();
                multiData.data[ i ].userFrameNo = new int();
                multiData.data[ i ].exFig = new int();
                multiData.data[ i ].axesData = new MotoComES.ESAxisData();
                multiData.data[ i ].axesData.axis = new double[ MotoComES.ESAxisData.axis_Length ];
            }

            //multiDataに値をセット
            for( int i = 0; i < 2; i++ )
            {
                //positionDataに値をセット
                multiData.data[ i ].dataType = 16; //ベース座標
                multiData.data[ i ].fig = 0;
                multiData.data[ i ].toolNo = 0;
                multiData.data[ i ].userFrameNo = 0;
                multiData.data[ i ].exFig = 0;
                for( int j = 0; j < MotoComES.ESAxisData.axis_Length - 2; j++ )
                {
                    multiData.data[ i ].axesData.axis[ j ] = ( i + 1 ) * 10 + j + 1;
                }
                multiData.data[ i ].axesData.axis[ 6 ] = 0;
                multiData.data[ i ].axesData.axis[ 7 ] = 0;
            }

            Program._Temp.AppendLine( "ESSetPositionDataM" );

            // 関数を実行
            Program.res = MotoComES.ESSetPositionDataM( Program._Handle, 0 + 1, 2, multiData ); //RS022=0

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            //ESClose
            ESClose();

            return Program.res;
        }

        /// <summary>
        /// ESGetBpexPositionDataM
        /// </summary>
        /// <returns></returns>
        public int ESGetBpexPositionDataM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiBpexPositionData multiData = new MotoComES.ESMultiBpexPositionData();

            Program._Temp.AppendLine( "ESGetBpexPositionDataM" );

            // 関数を実行
            Program.res = MotoComES.ESGetBpexPositionDataM( Program._Handle, 1, 0 + 1, 2, ref multiData ); //RS022=0, BP変数

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                for( int i = 0; i < 2; i++ )
                {
                    Program._Temp.AppendLine( "BP(" + i + ")" );
                    Program._Temp.AppendLine( "" );
                    Program._Temp.AppendLine( "dataType\t" + multiData.data[ i ].dataType.ToString() );
                    Program._Temp.AppendLine( "" );
                    for( int j = 0; j < MotoComES.ESAxisData.axis_Length; j++ )
                    {
                        Program._Temp.AppendLine( "axis" + ( j + 1 ).ToString() + "\t" + multiData.data[ i ].axesData.axis[ j ].ToString() );
                        Program._Temp.AppendLine( "" );
                    }
                }
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            }

            return Program.res;
        }

        /// <summary>
        /// ESSetBpexPositionDataM
        /// </summary>
        /// <returns></returns>
        public int ESSetBpexPositionDataM()
        {
            Program.res = -1;

            // 引数となる構造体変数の定義
            MotoComES.ESMultiBpexPositionData multiData = new MotoComES.ESMultiBpexPositionData();
            multiData.data = new MotoComES.ESBpexPositionData[ MotoComES.ESMultiBpexPositionData.multiData_Length ];
            for( int i = 0; i < MotoComES.ESMultiBpexPositionData.multiData_Length; i++ )
            {
                multiData.data[ i ].dataType = new int();
                multiData.data[ i ].axesData = new MotoComES.ESAxisData();
                multiData.data[ i ].axesData.axis = new double[ MotoComES.ESAxisData.axis_Length ];
            }

            //multiDataに値をセット
            for( int i = 0; i < 2; i++ )
            {
                //positionDataに値をセット
                multiData.data[ i ].dataType = 16; //ベース座標
                for( int j = 0; j < 3; j++ )
                {
                    multiData.data[ i ].axesData.axis[ j ] = ( i + 1 ) * 10 + j + 1;
                }
                multiData.data[ i ].axesData.axis[ 3 ] = 0;
                multiData.data[ i ].axesData.axis[ 4 ] = 0;
                multiData.data[ i ].axesData.axis[ 5 ] = 0;
                multiData.data[ i ].axesData.axis[ 6 ] = 0;
                multiData.data[ i ].axesData.axis[ 7 ] = 0;
            }

            Program._Temp.AppendLine( "ESSetBpexPositionDataM" );

            // 関数を実行
            Program.res = MotoComES.ESSetBpexPositionDataM( Program._Handle, 1, 0 + 1, 2, multiData ); //RS022=0, BP変数

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESReset
        /// </summary>
        /// <returns></returns>
        public int ESReset()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESReset" );

            // 関数を実行
            Program.res = MotoComES.ESReset( Program._Handle );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESCancel
        /// </summary>
        /// <returns></returns>
        public int ESCancel()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESCancel" );

            // 関数を実行
            Program.res = MotoComES.ESCancel( Program._Handle );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESHold
        /// </summary>
        /// <returns></returns>
        public int ESHold()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESHold" );

            // 関数を実行
            Program.res = MotoComES.ESHold( Program._Handle, 1 ); //Hold On

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESServo
        /// </summary>
        /// <returns></returns>
        public int ESServo()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESServo" );

            // 関数を実行
            Program.res = MotoComES.ESServo( Program._Handle, 1 ); //Servo On

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESHlock
        /// </summary>
        /// <returns></returns>
        public int ESHlock()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESHlock" );

            // 関数を実行
            Program.res = MotoComES.ESHlock( Program._Handle, 1 ); //Hlock On

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESCycle
        /// </summary>
        /// <returns></returns>
        public int ESCycle()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESCycle" );

            // 関数を実行
            Program.res = MotoComES.ESCycle( Program._Handle, 1 ); //Cycle Step

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESBDSP
        /// </summary>
        /// <returns></returns>
        public int ESBDSP()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESBDSP" );

            // 引数となる変数の定義
            byte[] message = MotoComES.StringToByteArray( "test", MotoComES._ECode.GetByteCount( "test" ) + 1 );

            // 関数を実行
            Program.res = MotoComES.ESBDSP( Program._Handle, ref message[ 0 ] );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESStartJob
        /// </summary>
        /// <returns></returns>
        public int ESStartJob()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESStartJob" );

            // 関数を実行
            Program.res = MotoComES.ESStartJob( Program._Handle );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESSelectJob
        /// </summary>
        /// <returns></returns>
        public int ESSelectJob()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESSelectJob" );

            // 引数となる変数の定義
            byte[] jobName = MotoComES.StringToByteArray( "TEST", MotoComES._ECode.GetByteCount( "TEST" ) + 1 );

            // 関数を実行
            Program.res = MotoComES.ESSelectJob( Program._Handle, 1, 1, ref jobName[ 0 ] );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESSaveFile
        /// </summary>
        /// <returns></returns>
        public int ESSaveFile()
        {
            Program.res = -1;

            Program._Temp.AppendLine( "ESSaveFile" );

            // 引数となる変数の定義
            string sSavePath = "C:\\Temp";
            string sFileName = "TEST.jbi";
            byte[] savePath = MotoComES.StringToByteArray( sSavePath, Program._ECode.GetByteCount( sSavePath ) + 1 );
            byte[] fileName = MotoComES.StringToByteArray( sFileName, Program._ECode.GetByteCount( sFileName ) + 1 );

            // 関数を実行
            Program.res = MotoComES.ESSaveFile( Program._Handle, ref savePath[ 0 ], ref fileName[ 0 ] );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "savePath\t" + sSavePath );
                Program._Temp.AppendLine( "fileName\t" + sFileName );
                Program._Temp.AppendLine( "" );
            }
            else
            {
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
                Program._Temp.AppendLine( "" );
            }

            return Program.res;
        }

        /// <summary>
        /// ESLoadFile
        /// </summary>
        /// <returns></returns>
        public int ESLoadFile()
        {
            Program.res = -1;

            // 引数となる変数の定義
            byte[] fPath = MotoComES.StringToByteArray( "C:\\Temp\\TEST.JBI",
                MotoComES._ECode.GetByteCount( "C:\\Temp\\TEST.JBI" ) + 1 );

            Program._Temp.AppendLine( "ESLoadFile" );

            // 関数を実行
            Program.res = MotoComES.ESLoadFile( Program._Handle, ref fPath[ 0 ] );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESDeleteJob
        /// </summary>
        /// <returns></returns>
        public int ESDeleteJob()
        {
            Program.res = -1;

            // 引数となる変数の定義
            byte[] fPath = MotoComES.StringToByteArray( "TEST.JBI",
                MotoComES._ECode.GetByteCount( "TEST.JBI" ) + 1 );

            Program._Temp.AppendLine( "ESDeleteJob" );

            // 関数を実行
            Program.res = MotoComES.ESDeleteJob( Program._Handle, ref fPath[ 0 ] );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESListFileFirst
        /// </summary>
        /// <returns></returns>
        public int ESFileListFirst( ref string psFileName )
        {
            Program.res = -1;

            // 引数となる変数の定義
            byte[] fileName = new byte[ MotoComES.FileName_Length ];

            Program._Temp.AppendLine( "ESFileListFirst" );

            // 関数を実行
            Program.res = MotoComES.ESFileListFirst( Program._Handle, 1, ref fileName[ 0 ] );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                psFileName = MotoComES.ByteArrayToString( fileName );
            }

            return Program.res;
        }

        /// <summary>
        /// ESListFileNext
        /// </summary>
        /// <returns></returns>
        public int ESFileListNext( ref string psFileName )
        {
            Program.res = -1;

            // 引数となる変数の定義
            byte[] fileName = new byte[ MotoComES.FileName_Length ];

            // 関数を実行
            Program.res = MotoComES.ESFileListNext( Program._Handle, ref fileName[ 0 ] );

            // 戻り値が MotoComES.OK(0):正常処理
            if( Program.res == MotoComES.OK )
            {
                Program._Temp.AppendLine( "ESListFileNext" );
                Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
                Program._Temp.AppendLine( "" );
                psFileName = MotoComES.ByteArrayToString( fileName );
            }

            return Program.res;
        }

        /// <summary>
        /// ESCartMove
        /// </summary>
        /// <returns></returns>
        public int ESCartMove()
        {
            Program.res = -1;

            // 引数となる変数の定義
            int iMoveType;
            MotoComES.ESCartMoveData moveData = new MotoComES.ESCartMoveData();
            moveData.moveData = new MotoComES.ESMoveData();
            moveData.robotPos = new MotoComES.ESPositionData();
            moveData.robotPos.axesData = new MotoComES.ESAxisData();
            moveData.robotPos.axesData.axis = new double[ MotoComES.ESAxisData.axis_Length ];
            moveData.basePos = new MotoComES.ESBaseData();
            moveData.basePos.axis = new double[ MotoComES.ESBaseData.axis_Length ];
            moveData.stationPos = new MotoComES.ESStationData();
            moveData.stationPos.axis = new double[ MotoComES.ESStationData.axis_Length ];

            iMoveType = 1;                      // MOVJ

            moveData.moveData.robotNo = 1;
            moveData.moveData.startionNo = 0;
            moveData.moveData.speedType = 0;    // %
            moveData.moveData.speed = 25d;

            moveData.robotPos.dataType = 16;    // ベース座標
            moveData.robotPos.fig = 0;
            moveData.robotPos.toolNo = 0;
            moveData.robotPos.userFrameNo = 0;
            moveData.robotPos.exFig = 0;
            for( int i = 0; i < moveData.robotPos.axesData.axis.Length; i++ )
            {
                moveData.robotPos.axesData.axis[ i ] = i + 1;
            }
            moveData.robotPos.axesData.axis[ 6 ] = 0;
            moveData.robotPos.axesData.axis[ 7 ] = 0;

            for( int i = 0; i < moveData.basePos.axis.Length; i++ )
            {
                moveData.basePos.axis[ i ] = i + 1;
            }

            for( int i = 0; i < moveData.stationPos.axis.Length; i++ )
            {
                moveData.stationPos.axis[ i ] = i + 1;
            }

            Program._Temp.AppendLine( "ESCartMove" );

            // 関数を実行
            Program.res = MotoComES.ESCartMove( Program._Handle, iMoveType, moveData );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }

        /// <summary>
        /// ESPulseMove
        /// </summary>
        /// <returns></returns>
        public int ESPulseMove()
        {
            Program.res = -1;

            // 引数となる変数の定義
            int iMoveType;
            MotoComES.ESPulseMoveData moveData = new MotoComES.ESPulseMoveData();
            moveData.moveData = new MotoComES.ESMoveData();
            moveData.robotPos = new MotoComES.ESPulsePosData();
            moveData.robotPos.axesData = new MotoComES.ESAxisData();
            moveData.robotPos.axesData.axis = new double[ MotoComES.ESAxisData.axis_Length ];
            moveData.basePos = new MotoComES.ESBaseData();
            moveData.basePos.axis = new double[ MotoComES.ESBaseData.axis_Length ];
            moveData.stationPos = new MotoComES.ESStationData();
            moveData.stationPos.axis = new double[ MotoComES.ESStationData.axis_Length ];

            // 引数へ
            iMoveType = 2;                      // MOVL

            moveData.moveData.robotNo = 0;
            moveData.moveData.startionNo = 1;
            moveData.moveData.speedType = 1;    // V
            moveData.moveData.speed = 4500d;

            for( int i = 0; i < moveData.robotPos.axesData.axis.Length; i++ )
            {
                moveData.robotPos.axesData.axis[ i ] = i + 1;
            }
            moveData.robotPos.axesData.axis[ 6 ] = 0;
            moveData.robotPos.axesData.axis[ 7 ] = 0;

            for( int i = 0; i < moveData.basePos.axis.Length; i++ )
            {
                moveData.basePos.axis[ i ] = i + 1;
            }

            for( int i = 0; i < moveData.stationPos.axis.Length; i++ )
            {
                moveData.stationPos.axis[ i ] = i + 1;
            }

            moveData.toolNo = 0;

            Program._Temp.AppendLine( "ESPulseMove" );

            // 関数を実行
            Program.res = MotoComES.ESPulseMove( Program._Handle, iMoveType, moveData );

            Program._Temp.AppendLine( "res\t" + Convert.ToString( Program.res, 16 ) );
            Program._Temp.AppendLine( "" );

            return Program.res;
        }
        #endregion
    }
}
