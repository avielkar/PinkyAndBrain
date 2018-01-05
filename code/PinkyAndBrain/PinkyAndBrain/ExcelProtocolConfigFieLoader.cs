using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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

                    param._ratHouseParameter = excelStringValuesArray[k,i];

                    if(excelStringValuesArray[k, i] != null)
                        param = DisassamblyDataAttributeValue(excelStringValuesArray[k, i]);

                    var._description.Add(attributes[i], param);
                }

                //adding the variable (line in the excel data file into the dictionary of variables with the variable name as the key).
                variables._variablesDictionary.Add(var._description["name"]._ratHouseParameter, var);
            }

            //clost the file.
            xlWorkbook.Close();
        }

        /// <summary>
        /// Writes a protocol file to the protocols folder.
        /// </summary>
        /// <param name="protocolFilePath">The protocol folder path with protocol name.</param>
        /// <param name="variables">The variables to write to the protocol.</param>
        /// <param name="checkboxesDictionary">A dictionary consists all gui checkboxes and their states.</param>
        public void WriteProtocolFile(string protocolFilePath , Variables variables , Dictionary<string, CheckBox> checkboxesDictionary)
        {
            Excel.Workbook newProtocolFile = _xlApp.Workbooks.Add();

            Excel.Worksheet workSheet = newProtocolFile.Worksheets.Add();

            //change the sheet name to variables.
            workSheet.Name = "variables";

            //add the first line of heades titles to each column
            int columnIndex = 1;
            foreach (string title in variables._variablesDictionary.ElementAt(0).Value._description.Keys)
            {
                //add the title for each column of the excel file.
                workSheet.Cells[1, columnIndex] = title;
                //move next column title.
                columnIndex++;
            }

            //save each line according to the variable parameters.
            int rowIndex = 2;
            foreach (KeyValuePair<string, Variable> item in variables._variablesDictionary)
            {
                //reset the column index for the new line.
                columnIndex = 1;

                foreach (string titleName in item.Value._description.Keys)
                {
                    //write the column to the variable
                    workSheet.Cells[rowIndex, columnIndex] = item.Value._description[titleName]._ratHouseParameter;
                    //go next column for the same variable.
                    columnIndex++;
                }

                //go next line (for next variable)
                rowIndex++;
            }

            //add to each checkbox in the gui it's state.
            foreach (KeyValuePair<string , CheckBox> item in checkboxesDictionary)
            {
                //reset the column index for the new line.
                columnIndex = 1;

                foreach (string titleName in  variables._variablesDictionary.ElementAt(0).Value._description.Keys)
                {
                    //add the name of the variable checkbox.
                    if (titleName == "name")
                    {
                        workSheet.Cells[rowIndex, columnIndex] = item.Key;
                    }
                    else if (titleName == "parameters")
                    {
                        //write the column to the variable
                        workSheet.Cells[rowIndex, columnIndex] = item.Value.Checked ? "1" : "0";
                    }
                    else if(titleName == "status")
                    {
                        //write the column to the variable
                        workSheet.Cells[rowIndex, columnIndex] = "-1";
                    }
                    else
                    {
                        //write the column to the variable
                        workSheet.Cells[rowIndex, columnIndex] = "0";
                    }

                    //go next column for the same variable.
                    columnIndex++;
                }

                //move to next line.
                rowIndex++;
            }

            try//it is for the event when the file with the same name exists and the user cnceked the saving.
            {
                //save the file and close it.
                newProtocolFile.SaveAs(protocolFilePath + ".xlsx");
                newProtocolFile.Close();
            }
            catch { }
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

            //split each vector of data for each robot to a list of components.
            par._ratHouseParameter =attributeValue;

            return par;
        }
    }
}
