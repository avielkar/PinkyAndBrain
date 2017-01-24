using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PinkyAndBrain
{
    /// <summary>
    /// This partial is used for events callbacks.
    /// </summary>
    public partial class GuiInterface : Form
    {
        /// <summary>
        /// The selected protocols path to view protocols.
        /// </summary>
        private string _protoclsDirPath;

        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// The excel loader for reading data configuration.
        /// </summary>
        private ExcelProtocolConfigFieLoader _excelLoader;

        /// <summary>
        /// The list of the dynamic allocated textboxes that allocated each time the user choose different protocol.
        /// It saves the dynamic TextBox reference.
        /// </summary>
        private List<Control> _dynamicAllocatedTextBoxes;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GuiInterface(ref ExcelProtocolConfigFieLoader excelLoader)
        {
            InitializeComponent();
            _excelLoader = excelLoader;
            _variablesList = new Variables();
            _variablesList._variablesDictionary = new Dictionary<string, Variable>();
            _dynamicAllocatedTextBoxes = new List<Control>();
        }

        /// <summary>
        /// Closing the guiInterface window event handler.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void GuiInterface_Close(object sender , EventArgs e)
        {
            _excelLoader.CloseExcelProtocoConfigFilelLoader();
        }

        /// <summary>
        /// Event handler for clicking the protocol browser buttom.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">args.</param>
        private void protocolBrowserBtn_Click(object sender, EventArgs e)
        {
            if(_protocolsFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                _protoclsDirPath = _protocolsFolderBrowser.SelectedPath;
                AddFilesToComboBox(_protocolsComboBox, _protoclsDirPath);
            }
        }

        /// <summary>
        /// Handler for selecting protocol in the protocols combo box.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">args/</param>
        private void _protocolsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_protocolsComboBox.SelectedItem = _protocolsComboBox.Items[0];
            SetVariables(_protoclsDirPath + "\\" + _protocolsComboBox.SelectedItem.ToString());
            ShowVariablesToGui();
        }


        /// <summary>
        /// Function handler for changing the variable from the Gui according to the textboxes input.
        /// </summary>
        /// <param name="sender">The textbox sender object have been changed.</param>
        /// <param name="e">args.</param>
        /// <param name="varName">The variable name in the variables dictionary to update according to the textbox.</param>
        private void VariableTextBox_TextChanged(object sender, EventArgs e , string varName)
        {
            TextBox tb = sender as TextBox;
            //CheckProperInputSpelling(tb.Text , e._name);
        }




        #region my functions
        
        /// <summary>
        /// Add the files ends with .xlsx extension to the protocol ComboBox.
        /// </summary>
        private void AddFilesToComboBox(ComboBox comboBox, string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                string[] filesEntries = Directory.GetFiles(dirPath);

                foreach (string file in filesEntries)
                {
                    if(Path.GetExtension(file).Equals(".xlsx"))
                    {
                        _protocolsComboBox.Items.Add(Path.GetFileName(file));
                    }
                }

            }

            if (_protocolsComboBox.Items.Count > 0)
            {
                _protocolsComboBox.SelectedItem = _protocolsComboBox.Items[0];
                SetVariables(_protoclsDirPath + "\\" +_protocolsComboBox.Items[0].ToString());
                ShowVariablesToGui();
            }
        }

        /// <summary>
        /// Sets the variables in the chosen xslx file and stote them in the class members.
        /// </summary>
        private void SetVariables(string dirPath)
        {
            _excelLoader.ReadProtocolFile(dirPath , ref _variablesList);
        }


        /// <summary>
        /// Showing the variables from the readen excel file to the Gui with option to change them.
        /// </summary>
        private void ShowVariablesToGui()
        {
            int top = 100;
            int left = 10;
            int z = 0;

            //clear dynamic textboxes and labels from the last uploaded protocol before creating the new dynamic controls..
            ClearDynamicControls();

            foreach (string varName in _variablesList._variablesDictionary.Keys)
            {
                Label newLabel = new Label();
                this.Controls.Add(newLabel);
                newLabel.Name = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Text = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Width = 150;
                newLabel.Height = 14;
                newLabel.Top = top;
                newLabel.Left = left;

                ShowVariableAttributes(varName, top, left, 150, 14 , 230 , 1100);

                top += 35;
            }
        }

        /// <summary>
        /// Showing a single variable with all it's attributes.
        /// </summary>
        /// <param name="varName">The variable name.</param>
        /// <param name="top">The offset from the top of the window.</param>
        /// <param name="left">The offset from the left of the window for the DropDownList of each variable.</param>
        /// <param name="width">The width for each textbox in the line of the attribute.</param>
        /// <param name="height">The height for each textbox in the line of the attribute.</param>
        /// <param name="eachDistance">The distance between each textbox of the same attribute in the same line.</param>
        /// <param name="offset">The offset for each textbox of the attribute from the left.</param>
        private void ShowVariableAttributes(string varName , int top , int left , int width  , int height , int eachDistance , int offset)
        {
            //string builder for making the text to show to the gui.
            StringBuilder sBuilder = new StringBuilder();

            #region STATUS_COMBOBOX
            //add the status ComboBox.
            ComboBox statusCombo = new ComboBox();
            statusCombo.Left = left + offset;
            statusCombo.Top = top;
            statusCombo.Items.Add("Static");
            statusCombo.Items.Add("Varying");
            statusCombo.Items.Add("AcrossStair");
            statusCombo.Items.Add("WithinStair");

            //decide which items on the ComboBox is selected according to the data in the excel sheet.
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter[0])
            {
                case "1":
                    statusCombo.SelectedText = "Static";
                    break;

                case "2":
                    statusCombo.SelectedText = "Varying";
                    break;

                case "3":
                    statusCombo.SelectedText = "AcrossStair";
                    break;

                case "4":
                    statusCombo.SelectedText = "WithinStair";
                    break;
            }

            //add the status ComboBox to the gui.
            this.Controls.Add(statusCombo);
            _dynamicAllocatedTextBoxes.Add(statusCombo);
            #endregion STATUS_COMBOBOX

            offset -= eachDistance;

            #region INCREMENT_TEXTBOX
            //add the low bound textbox.
            TextBox incrementTextBox = new TextBox();
            incrementTextBox.Left = offset;
            incrementTextBox.Top = top;
            incrementTextBox.Width = width;

            //function to change the variable list dictionary according to changes.
            incrementTextBox.TextChanged += new EventHandler((sender , e) => VariableTextBox_TextChanged(sender , e , varName));

            //check if need to show two parameters of the _landscapeParameters and _ratHouseParameter or only the _ratHouseParameter.
            //show both parameters.
            if (_variablesList._variablesDictionary[varName]._description["increament"]._bothParam)
            {
                string incrementTextVala = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);
                string incrementTextValb = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._landscapeParameters);

                incrementTextBox.Text = BracketsAppender(sBuilder, incrementTextVala, incrementTextValb);
            }

            //show only the _ratHouseParameter.
            else
            {
                string lowBoundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);
                incrementTextBox.Text = lowBoundTextVal;
            }

            this.Controls.Add(incrementTextBox);
            _dynamicAllocatedTextBoxes.Add(incrementTextBox);
            #endregion INCREMENT_TEXTBOX

            offset -= eachDistance;

            #region HIGHBOUND_TEXTBOX
            //add the low bound textbox.
            TextBox highBoundTextBox = new TextBox();
            highBoundTextBox.Left = offset;
            highBoundTextBox.Top = top;
            highBoundTextBox.Width = width;

            //check if need to show two parameters of the _landscapeParameters and _ratHouseParameter or only the _ratHouseParameter.
            //show both parameters.
            if (_variablesList._variablesDictionary[varName]._description["high_bound"]._bothParam)
            {
                string highBoundTextVala = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                string highBoundTextValb = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._landscapeParameters);

                highBoundTextBox.Text = BracketsAppender(sBuilder, highBoundTextVala, highBoundTextValb);
            }

            //show only the _ratHouseParameter.
            else
            {
                string highBoundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                highBoundTextBox.Text = highBoundTextVal;
            }

            this.Controls.Add(highBoundTextBox);
            this._dynamicAllocatedTextBoxes.Add(highBoundTextBox);
            #endregion HIGHBOUND_TEXTBOX

            offset -= eachDistance;

            #region LOWBOUND_TEXTBOX
            //add the low bound textbox.
            TextBox lowBoundTextBox = new TextBox();
            lowBoundTextBox.Left = offset;
            lowBoundTextBox.Top = top;
            lowBoundTextBox.Width = width;

            //check if need to show two parameters of the _landscapeParameters and _ratHouseParameter or only the _ratHouseParameter.
            //show both parameters.
            if(_variablesList._variablesDictionary[varName]._description["low_bound"]._bothParam)
            {
                string lowBoundTextVala = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                string lowBoundTextValb = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._landscapeParameters);

                lowBoundTextBox.Text = BracketsAppender(sBuilder, lowBoundTextVala, lowBoundTextValb);
            }

            //show only the _ratHouseParameter.
            else
            {
                string lowBoundTextVal = string.Join("," , _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                lowBoundTextBox.Text = lowBoundTextVal;
            }

            this.Controls.Add(lowBoundTextBox);
            _dynamicAllocatedTextBoxes.Add(lowBoundTextBox);
            #endregion LOWBOUND_TEXTBOX

            offset -= eachDistance;

            #region PARMETERS_VALUE_TEXTBOX
            //add the low bound textbox.
            TextBox parametersTextBox = new TextBox();
            parametersTextBox.Left = offset;
            parametersTextBox.Top = top;
            parametersTextBox.Width = width;

            //print the parameter in the gui according to the representation of each status.
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter[0])
            {
                case "1":   //static
                    //check if need to show two parameters of the _landscapeParameters and _ratHouseParameter or only the _ratHouseParameter.
                    //show both parameters.
                    if (_variablesList._variablesDictionary[varName]._description["parameters"]._bothParam)
                    {
                        string parametersTextBoxa = string.Join(",", _variablesList._variablesDictionary[varName]._description["parameters"]._ratHouseParameter);
                        string parametersTextBoxb = string.Join(",", _variablesList._variablesDictionary[varName]._description["parameters"]._landscapeParameters);

                        parametersTextBox.Text = BracketsAppender(sBuilder, parametersTextBoxa, parametersTextBoxb);
                    }

                    //show only the _ratHouseParameter.
                    else
                    {
                        string parametersTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["parameters"]._ratHouseParameter);
                        parametersTextBox.Text = parametersTextVal;
                    }
                    break;
                    
                case "2":   //varying
                case "3":   //acrossstair
                case "4":   //withinstair
                    if (_variablesList._variablesDictionary[varName]._description["parameters"]._bothParam)
                    {
                        string lowboundTextVala = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                        string highboundTextVala = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                        string increasingTextVala = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);

                        string lowboundTextValb = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._landscapeParameters);
                        string highboundTextValb = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._landscapeParameters);
                        string increasingTextValb = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._landscapeParameters);

                        string partA = ThreeStagesRepresentation(sBuilder, lowboundTextVala, increasingTextVala, highboundTextVala);
                        string partB = ThreeStagesRepresentation(sBuilder, lowboundTextValb, increasingTextValb, highboundTextValb);

                        parametersTextBox.Text = BracketsAppender(sBuilder , partA, partB);
                    }

                    //show only the _ratHouseParameter.
                    else
                    {
                        string lowboundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                        string highboundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                        string increasingTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);

                        parametersTextBox.Text = ThreeStagesRepresentation(sBuilder, lowboundTextVal, increasingTextVal, highboundTextVal);
                    }
                    break;

            }

            this.Controls.Add(parametersTextBox);
            _dynamicAllocatedTextBoxes.Add(parametersTextBox);
            #endregion PARMETERS_VALUE_TEXTBOX

            offset -= eachDistance;
        }

        /// <summary>
        /// Clears the dynamic allocated textboxes and labels due to the previous loaded protocol..
        /// </summary>
        private void ClearDynamicControls()
        {
            foreach (Control ctrl in _dynamicAllocatedTextBoxes)
            {
                this.Controls.Remove(ctrl);
            }

            _dynamicAllocatedTextBoxes.RemoveAll(x=> true);
        }

        /// <summary>
        /// Add manipulation to the partA and partB for showing them in the gui as in the excel file.
        /// </summary>
        /// <param name="sBuilder">The stream builder to buld the string with.</param>
        /// <param name="partA">The first part to put to the string.</param>
        /// <param name="partB">The second part to put to the string.</param>
        /// <returns>[partA][partB]</returns>
        private string BracketsAppender(StringBuilder sBuilder , string partA , string partB)
        {
            sBuilder.Clear();
            sBuilder.Append("[");
            sBuilder.Append(partA);
            sBuilder.Append("][");
            sBuilder.Append(partB);
            sBuilder.Append("]");

            return sBuilder.ToString();
        }

        /// <summary>
        /// Creates a string for the gui with 3 steps.
        /// </summary>
        /// <param name="sBuilder">The string writer.</param>
        /// <param name="lowPart">The low bound.</param>
        /// <param name="increasingPart">Increament.</param>
        /// <param name="highPart">The high bound.</param>
        /// <returns>The string to the gui by [low:inc:high].</returns>
        private string ThreeStagesRepresentation(StringBuilder sBuilder , string lowPart , string increasingPart , string highPart)
        {
            sBuilder.Clear();
            sBuilder.Append(lowPart);
            sBuilder.Append(":");
            sBuilder.Append(increasingPart);
            sBuilder.Append(":");
            sBuilder.Append(highPart);
            return sBuilder.ToString();
        }

        private bool CheckProperInputSpelling(string newText  , string varName)
        {
            return true;
        }


        #endregion

    }
}
