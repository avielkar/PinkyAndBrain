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
        #region MEMBERS

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
        /// The dictionary of the dynamic allocated textboxes that allocated each time the user choose different protocol.
        /// It saves the dynamic TextBox reference.
        /// The string represent the name of the varName concatinating with the attributename for each textbox.
        /// </summary>
        private Dictionary<string , Control> _dynamicAllocatedTextBoxes;

        /// <summary>
        /// A list holds all the titles for the variables attribute to show in the title of the table.
        /// </summary>
        private List<Label> _titlesLabelsList;

        /// <summary>
        /// Holds the trajectory creator object.
        /// </summary>
        private TrajectoryCreator _trajectoryCreator;

        /// <summary>
        /// Holds the AcrossVectorValuesGenerator generator.
        /// </summary>
        private AcrossVectorValuesGenerator _acrossVectorValuesGenerator;

        #endregion MEMBERS

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor.
        /// </summary>
        public GuiInterface(ref ExcelProtocolConfigFieLoader excelLoader)
        {
            InitializeComponent();
            _excelLoader = excelLoader;
            _variablesList = new Variables();
            _variablesList._variablesDictionary = new Dictionary<string, Variable>();
            _dynamicAllocatedTextBoxes = new Dictionary<string,Control>();
            _trajectoryCreator = new TrajectoryCreator();
            _acrossVectorValuesGenerator = new AcrossVectorValuesGenerator();
            InitializeTitleLabels();
            _varyingListBox.Visible = false;
        }
        #endregion CONSTRUCTORS

        #region EVENTS_HANDLE_FUNCTIONS
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
        /// Function handler for changing the variable from the Gui according to the textboxes input when leaving the textbox.
        /// </summary>
        /// <param name="sender">The textbox sender object have been changed.</param>
        /// <param name="e">args.</param>
        /// <param name="varName">The variable name in the variables dictionary to update according to the textbox.</param>
        private void VariableTextBox_TextBoxLeaved(object sender, EventArgs e , string varName , string varAttibuteName)
        {
            TextBox tb = sender as TextBox;

            CheckProperInputSpelling(tb.Text , varName , varAttibuteName);
        }

        /// <summary>
        /// Function handler for status variable combobox changed.
        /// </summary>
        /// <param name="sender">The combobox object that was changed.</param>
        /// <param name="e">The args.</param>
        /// <param name="varName">The var name it's combobox changed.</param>
        private void statusCombo_SelectedIndexChanged(object sender, EventArgs e , string varName)
        {
            ComboBox cb = sender as ComboBox;
            string selectedIndex="";

            //decide which index in the status list was selected.
            selectedIndex = StatusIndexByNameDecoder(cb.SelectedItem.ToString());

            //update the status in the variables dictionary.
            _variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter[0] = selectedIndex;

            //Check if both he num of staicases and withinstairs is 1 or 0.
            #region STATUS_NUM_OF_OCCURENCES
            int withinStairStstusOccurences = NumOfSpecificStatus("WithinStair");
            int acrossStairStatusOccurences = NumOfSpecificStatus("AcrossStair");
            if (withinStairStstusOccurences != acrossStairStatusOccurences || acrossStairStatusOccurences > 1)
            {
                MessageBox.Show("The number of Withinstairs is the same as AccrossStairs and both not occurs more than 1!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion STATUS_NUM_OF_OCCURENCES

            #region TEXTBOXES_FREEZING_NEW_STATUS
            //update the gui textboxes freezing according to the new status.
            foreach (string attribute in _variablesList._variablesDictionary[varName]._description.Keys)
            {
                if(!attribute.Equals("status"))
                {
                    if (_dynamicAllocatedTextBoxes.ContainsKey(varName + attribute))
                        FreezeTextBoxAccordingToStatus((TextBox)_dynamicAllocatedTextBoxes[varName + attribute] , varName , attribute.Equals("parameters"));
                }
            }
            #endregion TEXTBOXES_FREEZING_NEW_STATUS

            //change the parametes attribute textbox for the changed status variable.
            #region PRAMETERS_TEXTBOX_CHANGE_TEXT_SHOW
            SetParametersTextBox(varName , new StringBuilder());
            #endregion PRAMETERS_TEXTBOX_CHANGE_TEXT_SHOW
        }

        /// <summary>
        /// Function handler for starting the experiment tirlas. 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">args.</param>
        private void _startButton_Click(object sender, EventArgs e)
        {
            //Check if both he num of staicases and withinstairs is 1 or 0.
            #region STATUS_NUM_OF_OCCURENCES
            int withinStairStstusOccurences = NumOfSpecificStatus("WithinStair");
            int acrossStairStatusOccurences = NumOfSpecificStatus("AcrossStair");
            if (withinStairStstusOccurences != acrossStairStatusOccurences || acrossStairStatusOccurences > 1)
            {
                MessageBox.Show("Cannor start the experiment.\n The number of Withinstairs should be the same as AccrossStairs and both not occurs more than 1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                //do nothing else.
                return;
            }
            #endregion STATUS_NUM_OF_OCCURENCES

            //if there ar no errors middwile. Generate crrossvals and run the control loop.
            _acrossVectorValuesGenerator.SetVariablesValues(_variablesList);
            _acrossVectorValuesGenerator.MakeTrialsVaringVectors();

            ClearVaryingListBox();

            AddVaryingMatrixToVaryingListBox(_acrossVectorValuesGenerator.MakeVaryingMatrix());
            _varyingListBox.Visible = true;
        }
        #endregion EVENTS_HANDLE_FUNCTIONS

        #region my_functions

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
                //that was deleted because it show the variables already in the two lines before.
                //ShowVariablesToGui();
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
            int width = 130;
            int height = 14;
            int eachDistance = 150;

            //clear dynamic textboxes and labels from the last uploaded protocol before creating the new dynamic controls..
            ClearDynamicControls();

            AddVariablesLabelsTitles(ref top, left, width, height, eachDistance);

            foreach (string varName in _variablesList._variablesDictionary.Keys)
            {
                Label newLabel = new Label();
                this.Controls.Add(newLabel);
                newLabel.Name = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Text = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Width = width - 35;
                newLabel.Height = height;
                newLabel.Top = top;
                newLabel.Left = left;

                ShowVariableAttributes(varName, top, left, width, height, eachDistance, 750);

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

            //Handle event when a status of a variable is changed.
            statusCombo.SelectedIndexChanged += new EventHandler((sender , args) => statusCombo_SelectedIndexChanged(sender , args , varName));

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
            _dynamicAllocatedTextBoxes.Add(varName + "status", statusCombo);
            #endregion STATUS_COMBOBOX

            offset -= eachDistance;

            #region INCREMENT_TEXTBOX
            //add the low bound textbox.
            TextBox incrementTextBox = new TextBox();
            incrementTextBox.Left = offset;
            incrementTextBox.Top = top;
            incrementTextBox.Width = width;

            //function to change the variable list dictionary according to changes when leave the textbox.
            incrementTextBox.LostFocus += new EventHandler((sender , e) => VariableTextBox_TextBoxLeaved(sender , e , varName , "increament"));

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(incrementTextBox, varName , false);

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
            _dynamicAllocatedTextBoxes.Add(varName + "increament" , incrementTextBox);
            #endregion INCREMENT_TEXTBOX

            offset -= eachDistance;

            #region HIGHBOUND_TEXTBOX
            //add the low bound textbox.
            TextBox highBoundTextBox = new TextBox();
            highBoundTextBox.Left = offset;
            highBoundTextBox.Top = top;
            highBoundTextBox.Width = width;

            //function to change the variable list dictionary according to changes when leave the textbox.
            highBoundTextBox.LostFocus += new EventHandler((sender, e) => VariableTextBox_TextBoxLeaved(sender, e, varName, "high_bound"));

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(highBoundTextBox, varName , false);

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
            this._dynamicAllocatedTextBoxes.Add(varName + "high_bound" , highBoundTextBox);
            #endregion HIGHBOUND_TEXTBOX

            offset -= eachDistance;

            #region LOWBOUND_TEXTBOX
            //add the low bound textbox.
            TextBox lowBoundTextBox = new TextBox();
            lowBoundTextBox.Left = offset;
            lowBoundTextBox.Top = top;
            lowBoundTextBox.Width = width;

            //function to change the variable list dictionary according to changes when leave the textbox.
            lowBoundTextBox.LostFocus += new EventHandler((sender, e) => VariableTextBox_TextBoxLeaved(sender, e, varName, "low_bound"));

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(lowBoundTextBox, varName , false);

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
            _dynamicAllocatedTextBoxes.Add(varName + "low_bound" , lowBoundTextBox);
            #endregion LOWBOUND_TEXTBOX

            offset -= eachDistance;

            #region PARMETERS_VALUE_TEXTBOX
            //add the low bound textbox.
            TextBox parametersTextBox = new TextBox();
            parametersTextBox.Left = offset;
            parametersTextBox.Top = top;
            parametersTextBox.Width = width;
            //add name to the control in order to get it from the list if needed.
            parametersTextBox.Name = "parameters";

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
            _dynamicAllocatedTextBoxes.Add(varName + "parameters" , parametersTextBox);

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(parametersTextBox, varName , true);

            //function to change the variable list dictionary according to changes when leave the textbox.
            parametersTextBox.LostFocus += new EventHandler((sender, e) => VariableTextBox_TextBoxLeaved(sender, e, varName, "parameters"));

            #endregion PARMETERS_VALUE_TEXTBOX

            offset -= eachDistance;
        }

        /// <summary>
        /// Clears the dynamic allocated textboxes and labels due to the previous loaded protocol..
        /// </summary>
        private void ClearDynamicControls()
        {
            foreach (Control ctrl in _dynamicAllocatedTextBoxes.Values)
            {
                this.Controls.Remove(ctrl);
            }

            _dynamicAllocatedTextBoxes.Clear();
        }

        /// <summary>
        /// Initalize the title labels with their text and add them to the controls.
        /// </summary>
        private void InitializeTitleLabels()
        {
            //Make all the titles labels.
            _titlesLabelsList = new List<Label>();
            Label parametersLabel = new Label();
            Label lowBoundLabel = new Label();
            Label highBoundLabel = new Label();
            Label incrementLabel = new Label();
            Label statusLabel = new Label();

            //Add their text attribute.
            parametersLabel.Text = "Parameter";
            lowBoundLabel.Text = "Low Bound";
            highBoundLabel.Text = "High Bound";
            incrementLabel.Text = "Increment";
            statusLabel.Text = "Status";

            //Add them to the list of titles labels.
            _titlesLabelsList.Add(parametersLabel);
            _titlesLabelsList.Add(lowBoundLabel);
            _titlesLabelsList.Add(highBoundLabel);
            _titlesLabelsList.Add(incrementLabel);
            _titlesLabelsList.Add(statusLabel);
        }

        /// <summary>
        /// Adding titles lables for the attributes of the parameters.
        /// </summary>
        /// <param name="top">The offset from the top of the window.</param>
        /// <param name="left">The offset from the left of the window for the DropDownList of each variable.</param>
        /// <param name="width">The width for each label in the line of the titles.</param>
        /// <param name="height">The height for each label in the line of the titles.</param>
        /// <param name="eachDistance">The distance between each label in the titles line.</param>
        private void AddVariablesLabelsTitles(ref int top , int left , int width , int height , int eachDistance)
        {
            //add offset for the first.
            left += eachDistance;

            //Place each title label control.
            foreach (Label lbl in _titlesLabelsList)
            {
                lbl.Top = top;
                lbl.Left = left;
                lbl.Width = width;
                lbl.Height = height;
                left += eachDistance;

                this.Controls.Add(lbl);
            }

            //increase the top for the reference.
            top += 35;
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

        /// <summary>
        /// Freezing a textbox according to it's status.
        /// </summary>
        /// <param name="textBox">The textbox to freeze or not.</param>
        /// <param name="varName">The variable name for chaecing the status for the textbox. </param>
        /// <param name="parametersTextbox">Is the textbox describe a parameters attribute textbox. </param>
        private void FreezeTextBoxAccordingToStatus(TextBox textBox , string varName , bool parametersTextbox)
        {
            //decide which items on the ComboBox is selected according to the data in the excel sheet.
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter[0])
            {
                case "1":
                    textBox.Enabled = false;
                    break;

                case "2":
                case "3":
                case "4":
                    textBox.Enabled = true;
                    break;
            }

            //reverse the result.
            if(parametersTextbox)
            {
                textBox.Enabled = !textBox.Enabled;
            }
        }

        /// <summary>
        /// Checks the input spelling to be proper and update the dictionary according to that.
        /// </summary>
        /// <param name="attributeValue">The attribute value to check.</param>
        /// <param name="varName">The var name to attributed updated according to the new value if the input is proper.</param>
        /// <param name="attributeName">The attribute of the variable to be pdated if the input was proper.</param>
        /// <returns></returns>
        private Param CheckProperInputSpelling(string attributeValue , string varName , string attributeName)
        {
            Param par = new Param();
            par._ratHouseParameter = new List<string>();
            par._landscapeParameters = new List<string>();

            //if there are a two attributes in the attribute data. [x][y] == 2 attributes. x,y,z,w == vector for one attribute only.
            if (attributeValue.Count(x => x == '[') == 2)
            {
                string ratHouseParameteString;
                string landscapeHouseParameteString;

                ratHouseParameteString = string.Join("", attributeValue.Skip(1).TakeWhile(x => x != ']').ToArray());
                landscapeHouseParameteString = string.Join("", attributeValue.Skip(1).SkipWhile(x => x != '[').Skip(1).TakeWhile(x => x != ']').ToArray());

                //split each vector of data for each robot to a list of components.
                par._ratHouseParameter = ratHouseParameteString.Split(',').ToList();
                par._landscapeParameters = landscapeHouseParameteString.Split(',').ToList();
                par._bothParam = true;

                //if the input for one value contains more than one dot for precison dot or chars that are not digits.
                //if true , update the values in the variables dictionary.
                if (DigitsNumberChecker(par._ratHouseParameter))
                {
                    if(DigitsNumberChecker(par._landscapeParameters))
                    {
                        _variablesList._variablesDictionary[varName]._description[attributeName] = par;

                        SetParametersTextBox(varName, new StringBuilder());
                    }
                    
                    //show the previous text to the changed textbox (taken from the variable list dictionary).
                    else
                    {
                        //refresh according to the last.
                        ShowVariablesToGui();
                    }
                }


                //show the previous text to the changed textbox (taken from the variable list dictionary).
                else
                {
                    //refresh according to the last.
                    ShowVariablesToGui();
                }
            }

            //if one attribute only (can be a scalar either a vector).
            else
            {
                //split each vector of data for each robot to a list of components.
                par._ratHouseParameter = string.Join("", attributeValue.SkipWhile(x => x == '[').TakeWhile(x => x != ']').ToArray()).Split(',').ToList();
                par._bothParam = false;

                //if the input for one value contains more than one dot for precison dot or chars that are not digits.
                //if true , update the values in the variables dictionary.
                if(DigitsNumberChecker(par._ratHouseParameter))
                {
                    _variablesList._variablesDictionary[varName]._description[attributeName] = par;

                    SetParametersTextBox(varName, new StringBuilder());
                }

                //show the previous text to the changed textbox (taken from the variable list dictionary).
                else
                {
                    //refresh according to the last.
                    ShowVariablesToGui();
                }
            }

            return par;
        }

        /// <summary>
        /// Check if all items in the list can represent numbers.
        /// </summary>
        /// <param name="lst">The input list.</param>
        /// <returns>True if all list items can be considered as numbers , False otherwise.</returns>
        private bool DigitsNumberChecker(List<string> lst)
        {
            bool returnVal = true;

            foreach (string value in lst)
            {
                //if the input for one value contains more than one dot for precison dot or chars that are not digits.
                returnVal = returnVal & DigitsNumberChecker(value);
            }

            return returnVal;
        }

        /// <summary>
        /// Check if a string can represent a number or not.
        /// </summary>
        /// <param name="str">The input string to be a number.</param>
        /// <returns>True if the string can be a number , False otherwise.</returns>
        private bool DigitsNumberChecker(string str)
        {
            //can starts with negative sign.
            if(str.StartsWith("-"))
            {
                str = str.Substring(1, str.Length - 1);
            }

            if (str.Where(x => (x < '0' || x > '9') && x != '.').Count() > 0 || str.Count(x => x == '.') > 1)
            {
                MessageBox.Show("Warnning : cannot include more than one dot for precision or characters that are not digits.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the num of statusVal statuses in the variable list.
        /// </summary>
        /// <param name="statusVal">The status to check it's occurence number.</param>
        /// <returns>The num of statusVal in the variable list.</returns>
        private int NumOfSpecificStatus(string statusVal)
        {
            statusVal = StatusIndexByNameDecoder(statusVal);
            return _variablesList._variablesDictionary.Count(variable =>
                _variablesList._variablesDictionary[variable.Key]._description["status"]._ratHouseParameter[0] == statusVal);
        }

        /// <summary>
        /// Get the index (number) of a status by the status name.
        /// </summary>
        /// <param name="statusValueByName">The status value by name.</param>
        /// <returns>The status value by index(number).</returns>
        private string StatusIndexByNameDecoder(string statusValueByName)
        {
            switch (statusValueByName)
            {
                case "Static":
                    return "1";
                case "Varying":
                    return "2";
                case "AcrossStair":
                    return "3";
                case "WithinStair":
                    return "4";
            }

            return "4";
        }

        /// <summary>
        /// Changing the text for the parameters textbox for the varName variable.
        /// </summary>
        /// <param name="varName">The variable to change it's parameters textbox.</param>
        private void SetParametersTextBox(string varName, StringBuilder sBuilder)
        {
            //find the relevant parameters attribute textbox according to the variable name.
            TextBox parametersTextBox = _dynamicAllocatedTextBoxes[varName + "parameters"] as TextBox;

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

                        parametersTextBox.Text = BracketsAppender(sBuilder, partA, partB);
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
        }

        #region VARYING_LISTBOX_FUNCTIONS
        /// <summary>
        /// Adding the generated cross varying values to the varying listbox.
        /// </summary>
        /// <param name="varyingCrossVals">The cross genereated varying values to add to the listbox.</param>
        private void AddVaryingMatrixToVaryingListBox(List<Dictionary<string , double>> varyingCrossVals)
        {
            //collect the titles for the listbox columns to a list.
            string listBoxTitleLineText="";
            List<string> niceNameList = new List<string>();
            foreach (string varName in varyingCrossVals.ElementAt(0).Keys)
            {
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter.ElementAt(0);
                niceNameList.Add(varNiceName);
            }  

            //add the itles for the listbox columns/
            listBoxTitleLineText = string.Join("\t" ,  niceNameList);
            _varyingListBox.Items.Add(listBoxTitleLineText);

            //enable horizonal scrolling.
            _varyingListBox.HorizontalScrollbar = true;

            //set the display member and value member for each item in the ListBox thta represents a Dictionary values in the varyingCrossVals list.
            //_varyingListBox.DisplayMember = "_text";
            //_varyingListBox.ValueMember = "_listIndex";

            //add all varying cross value in new line in the textbox.
            int index = 0;
            foreach (Dictionary<string , double> varRowDictionaryItem in varyingCrossVals)
            {
                string listBoxLineText = string.Join("\t", varRowDictionaryItem.Values);

                /*VaryingItem varyItem = new VaryingItem();
                varyItem._text = listBoxLineText;
                varyItem._listIndex = index;*/

                index++;
                _varyingListBox.Items.Add(listBoxLineText);
            }
        }

        /// <summary>
        /// Clears the varying list box.
        /// </summary>
        private void ClearVaryingListBox()
        {
            _varyingListBox.Items.Clear();
        }

        /// <summary>
        /// Handler for adding combination to the varying cross values.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">args.</param>
        private void _addVaryingCobination_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handler for removing selected varting combination from varying cross values.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">args.</param>
        private void _removeVaryingCombination_Click(object sender, EventArgs e)
        {
            int selectedIndex = _varyingListBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                //take the selected item.
                //VaryingItem selectedCombination = _varyingListBox.SelectedItem as VaryingItem;

                //set the _acrossVectorValuesGenerator cross varying values
                //_acrossVectorValuesGenerator._crossVaryingVals.RemoveAt(selectedCombination._listIndex);

                _acrossVectorValuesGenerator._crossVaryingVals.RemoveAt(_varyingListBox.SelectedIndex - 1);

                //update also the gui varying listbox.
                //_varyingListBox.Items.Remove(selectedCombination);
                _varyingListBox.Items.RemoveAt(_varyingListBox.SelectedIndex);

                //ReduceIndexesFromNumberedIndex(selectedCombination._listIndex, _varyingListBox);

                //if (selectedCombination._listIndex > 0)
                //_varyingListBox.SelectedItem = (_varyingListBox.Items[selectedCombination._listIndex - 1] as VaryingItem);

                if (selectedIndex > 1)
                {
                    _varyingListBox.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        /// <summary>
        /// Reduces the indexes of the value of the VaryingItem in the varyingListBox to be matched with the _crossVaryingVals list.
        /// </summary>
        /// <param name="beginIndex">The index to begin reducing it's value index.</param>
        /// <param name="varyingListBox">Yhe varying listbox to reduce it's indexes.</param>
        private void ReduceIndexesFromNumberedIndex(int beginIndex , ListBox varyingListBox)
        {
            for (int index = beginIndex; index < varyingListBox.Items.Count;index++)
            {
                VaryingItem varyItem = varyingListBox.Items[index] as VaryingItem;
                varyItem._listIndex--;
            }
        }
        #endregion VARYING_LISTBOX_FUNCTIONS
    }

        #endregion my_functions

    public class VaryingItem
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public VaryingItem()
        {

        }

        /// <summary>
        /// The text the item have to show.
        /// </summary>
        public string _text 
        {
            get; 
            set;
        }

        /// <summary>
        /// The index in the varying cross vals list to be referenced to.
        /// </summary>
        public int _listIndex 
        {
            get;
            set;
        }
    }
}
