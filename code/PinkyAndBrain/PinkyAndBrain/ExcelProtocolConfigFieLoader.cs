using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//For excel file reading.
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab

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
        //private Variable _variables;

        //Excel application handler.
        private Excel.Application _xlApp;

        
        public ExcelProtocolConfigFieLoader()
        {
            _xlApp = new Excel.Application();
        }

        public void ReadProtocolFile(string protocolFilePath , ref Variables variables)
        {
            Excel.Workbook xlWorkbook = _xlApp.Workbooks.Open(protocolFilePath);

            //TODO : take care when there is no such sheet.
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets["variables"];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            object[,] valueArray = (object[,])xlRange.get_Value(
                Excel.XlRangeValueDataType.xlRangeValueDefault);


            string[,] excelStringValuesArray = Convert2DObjectsTo2DStrings(valueArray);

            for (int i = 1; i < xlRange.Cells.Rows.Count; i++)
            {
                string variableName = xlRange.Cells[i, 1];

                Param param = new Param();
                param._landscapeParameters = new List<string>();
                param._ratHouseParameter = new List<string>();
                param._name = variableName;
                param._bothParam = false;

                Variable var = new Variable();
                var._name = xlRange.Cells[i, 1];
                var._niceName = xlRange.Cells[i, 2];
                var._vectGen = xlRange.Cells[i, 3];
                var._editable = xlRange.Cells[i, 4];
                var._category = xlRange.Cells[i, 5];
                var._callBack = xlRange.Cells[i, 6];
                var._toolTip = xlRange.Cells[i, 7];
                var._parameters = xlRange.Cells[i, 8];
                var._lowBound = xlRange.Cells[i, 9];
                var._highBound = xlRange.Cells[i, 10];
                var._increment = xlRange.Cells[i, 11];

                variables._variablesDictionary.Add(variableName, var);
            }

        }

        private void LoadProtocolFileToGui()
        {

        }

        /// <summary>
        /// Converts a 2D array of object type to 2D array of string type.
        /// </summary>
        /// <param name="array">The 2D object array.</param>
        /// <returns>The 2d string array.</returns>
        private string[,] Convert2DObjectsTo2DStrings(object[,] array)
        {
            string[,] returnArray = new string[array.GetLength(0),array.GetLength(1)];

            for(int i=0;i<array.GetLength(0);i++)
            {
                for(int j=0;j<array.GetLength(1);j++)
                {
                    returnArray[i,j] = (array[i+1, j+1] == null)?null:array[i+1, j+1].ToString();
                }
            }

            return returnArray;
        }
    }
}
