using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is used to read the data from excel protocl file and load it into the GuiInterface.
    /// The excel file may contains two sheets of parameters (for the trial) and of functions (for the calling functions to analze response , trajectory creation etc).
    /// </summary>
    public class ExcelProtocolConfigFieLoader
    {
        /// <summary>
        /// The file path to read from.
        /// </summary>
        private string _filePath;

        /// <summary>
        /// The variables in the readen protocol file.
        /// </summary>
        private Variable _variables;

        public ExcelProtocolConfigFieLoader()
        {

        }

        private void ReadProtocolFile(string protocolFilePath)
        {

        }

        private void LoadProtocolFileToGui()
        {

        }
    }
}
