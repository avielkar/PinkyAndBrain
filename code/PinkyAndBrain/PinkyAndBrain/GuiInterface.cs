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
        public GuiInterface(ExcelProtocolConfigFieLoader excelLoader)
        {
            InitializeComponent();
            _excelLoader = excelLoader;
            _variablesList = new Variables();
            _variablesList._variablesDictionary = new Dictionary<string, Variable>();

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
            int x = 100;
            int y = 10;
            int z = 0;
            foreach (string varName in _variablesList._variablesDictionary.Keys)
            {
                Label newLabel = new Label();
                this.Controls.Add(newLabel);
                newLabel.Name = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Text = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Width = 200;
                newLabel.Height = 14;
                newLabel.Top = x;
                newLabel.Left = y;
                x += 35;
            }
        }

        #endregion


    }


}
