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

            //vars going to collect all the variables in the excel data with thier attributes as dictionary of a vraible name and all it's attribute in inner dictionary.
            Variables vars = new Variables();
            vars._variablesDictionary = new Dictionary<string, Variable>();

            //collecting all the attributes for each variable (all variables have them).
            string[] attributes = new string[excelStringValuesArray.GetLength(1)];
            for(int i=0;i<excelStringValuesArray.GetLength(1);i++)
            {
                attributes[i] = excelStringValuesArray[0, i];
            }

            //run along all the data lines.
            for (int k = 1; k < excelStringValuesArray.GetLength(0); k++)
            {
                Variable var = new Variable();
                var._description = new Dictionary<string, Param>();

                //run along the number of columns along the lines.
                for (int i = 0; i < excelStringValuesArray.GetLength(1); i++)
                {   
                    Param param = new Param();
                    param._name = attributes[i];

                    param._ratHouseParameter = new List<string>();
                    param._ratHouseParameter.Add(excelStringValuesArray[k,i]);

                    var._description.Add(attributes[i], param);
                }

                vars._variablesDictionary.Add(var._description["name"]._ratHouseParameter[0], var);

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
