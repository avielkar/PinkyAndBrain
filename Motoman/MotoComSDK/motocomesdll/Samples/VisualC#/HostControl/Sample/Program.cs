using System;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );
                Application.Run( new Form1() );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString(), ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        /// <summary>
        /// エンコード
        /// </summary>
        public static Encoding _ECode = Encoding.Default;

        /// <summary>
        /// ハンドル
        /// </summary>
        public static IntPtr _Handle = new IntPtr();

        /// <summary>
        /// 結果表示用
        /// </summary>
        public static StringBuilder _Temp = new StringBuilder();

        /// <summary>
        /// 関数実行結果
        /// </summary>      
        public static int res = -1;

        /// <summary>
        /// コマンド実行
        /// </summary>
        public static clsCommand _Command = new clsCommand();
    }
}
