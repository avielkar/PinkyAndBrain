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

using Params;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is used to read the data from excel protocl file and load it into the GuiInterface.
    /// The excel file may contains two sheets of parameters (for the trial) and of functions (for the calling functions to analze response , trajectory creation etc).
    /// </summary>
    public class ExcelProtocolConfigFieLoader
    {
        /// <summary>
        /// Excel application handler.
        /// </summary>
        private Excel.Application _xlApp;

        /// <summary>
        /// Start the excel app to run.
        /// </summary>
        public ExcelProtocolConfigFieLoader()
        {
            _xlApp = new Excel.Application();
        }

        /// <summary>
        /// Closes the excel application and destroy it's running app.
        /// </summary>
        public void CloseExcelProtocoConfigFilelLoader()
        {
            _xlApp.Quit();
        }

        /// <summary>
        /// Reads an excel protocol files and insert all the variables in the protocol with each variable attributes.
        /// </summary>
        /// <param name="protocolFilePath">The protocol file path to be read.</param>
        /// <param name="variables">The object where all the variables with their attributes will be saved.</param>
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
            //Variables vars = new Variables();
            variables._variablesDictionary = new Dictionary<string, Variable>();

            //collecting all the attributes for each variable (all variables have them).
            string[] attributes = new string[excelStringValuesArray.GetLength(1)];
            for(int i=0;i<excelStringValuesArray.GetLength(1);i++)
            {
                attributes[i] = excelStringValuesArray[0, i];
            }

            //run along all the data lines.
            for (int k = 1; k < excelStringValuesArray.GetLength(0); k++)
            {
                //making the new variable to be inserted into the protocol variables dictionary.
                Variable var = new Variable();
                var._description = new Dictionary<string, Param>();

                //run along the number of columns along the lines to collect the attributes of the specific variable..
                for (int i = 0; i < excelStringValuesArray.GetLength(1); i++)
                {   
                    Param param = new Param();

                    param._ratHouseParameter = new List<string>();
                    param._ratHouseParameter.Add(excelStringValuesArray[k,i]);

                    if(excelStringValuesArray[k, i] != null)
                        param = DisassamblyDataAttributeValue(excelStringValuesArray[k, i]);

                    var._description.Add(attributes[i], param);
                }

                //adding the variable (line in the excel data file into the dictionary of variables with the variable name as the key).
                variables._variablesDictionary.Add(var._description["name"]._ratHouseParameter[0], var);

            }

            //clost the file.
            xlWorkbook.Close();

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

        /// <summary>
        /// Dissasembly data attribute to it's components(if it's a vector attribute for both the _ratHouseParameter and _landscapeParameters) for a Param class.
        /// </summary>
        /// <param name="attributeValue">The attribute value of the excel cell to be dissasembly.</param>
        /// <returns>The param disassemblied object acordding to the value.</returns>
        private Param DisassamblyDataAttributeValue(string attributeValue)
        {
            Param par = new Param();
            par._ratHouseParameter = new List<string>();

            //if there are a two attributes in the attribute data. [x] == 1 attributes. [x,y,z,w] == vector for one attribute only.

            //split each vector of data for each robot to a list of components.
            par._ratHouseParameter = string.Join("", attributeValue.SkipWhile(x => x == '[').TakeWhile(x => x != ']').ToArray()).Split(',').ToList();

            return par;
        }
    }
}
