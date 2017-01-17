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

        private ExcelProtocolConfigFieLoader _excelLoader;



        /// <summary>
        /// Constructor.
        /// </summary>
        public GuiInterface(ref ExcelProtocolConfigFieLoader excelLoader)
        {
            InitializeComponent();
            _excelLoader = excelLoader;
            _variablesList = new Variables();
            _variablesList._variablesDictionary = new Dictionary<string, Variable>();

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

        private void protocolBrowserBtn_Click(object sender, EventArgs e)
        {
            if(_protocolsFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                _protoclsDirPath = _protocolsFolderBrowser.SelectedPath;
                AddFilesToComboBox(_protocolsComboBox, _protoclsDirPath);
            }
        }

        private void _protocolsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_protocolsComboBox.SelectedItem = _protocolsComboBox.Items[0];
            SetVariables(_protoclsDirPath + "\\" + _protocolsComboBox.SelectedItem.ToString());
            ShowVariablesToGui();
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


        private void ShowVariablesToGui()
        {
            int top = 100;
            int left = 10;
            int z = 0;
            foreach (string varName in _variablesList._variablesDictionary.Keys)
            {
                Label newLabel = new Label();
                this.Controls.Add(newLabel);
                newLabel.Name = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Text = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Width = 200;
                newLabel.Height = 14;
                newLabel.Top = top;
                newLabel.Left = left;

                ShowVariableAttributes(varName, top, left, 200, 14);

                top += 35;
            }
        }

        private void ShowVariableAttributes(string varName , int top , int left , int width  , int height)
        {
            #region status ComboBox
            //add the status ComboBox.
            ComboBox statusCombo = new ComboBox();
            statusCombo.Left = left + 1000;
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
            #endregion

            //add the low bound textbox.
            TextBox lowBoundTextBox = new TextBox();
            lowBoundTextBox.Left = left + 800;
            lowBoundTextBox.Top = top;

            //check if need to show two parameters of the _landscapeParameters and _ratHouseParameter or only the _ratHouseParameter.
            //show both parameters.
            if(_variablesList._variablesDictionary[varName]._description["low_bound"]._bothParam)
            {
            }

            //show only the _ratHouseParameter.
            else
            {
                string lowBoundTextVal = string.Join(" " , _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                lowBoundTextBox.Text = lowBoundTextVal;
            }

            this.Controls.Add(lowBoundTextBox);

        }

        #endregion

    }


}
