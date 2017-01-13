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

    public struct Param
    {
        List<int> ratHouseParameter;
        List<int> landscapeParameters;
    }

    public struct Variable
    {
        string name;
        string niceName;
        bool vectGen;
        bool editable;
        int category;
        string callBack;
        string toolTip;
        Param Parameters;
        Param lowBound;
        Param highBound;
        Param increment;
    }

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
        private List<Variable> _variablesList;

        private ExcelProtocolConfigFieLoader _excelLoader;



        /// <summary>
        /// Constructor.
        /// </summary>
        public GuiInterface(ExcelProtocolConfigFieLoader excelLoader)
        {
            InitializeComponent();
            _excelLoader = excelLoader;

        }

        private void protocolBrowserBtn_Click(object sender, EventArgs e)
        {
            if(_protocolsFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                _protoclsDirPath = _protocolsFolderBrowser.SelectedPath;
                AddFilesToComboBox(_protocolsComboBox, _protoclsDirPath);

            }
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
                SetVariables(dirPath);
            }
        }

        /// <summary>
        /// Sets the variables in the chosen xlsx file and stote them in the class members.
        /// </summary>
        private void SetVariables(string dirPath)
        {
            
        }

        #endregion
    }


}
