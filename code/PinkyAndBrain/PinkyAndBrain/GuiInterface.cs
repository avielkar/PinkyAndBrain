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
using MLApp;
using MotocomdotNetWrapper;
using LED.Strip.Adressable;
using log4net;
using System.Threading;

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
        private TrajectoryCreatorHandler _trajectoryCreator;

        /// <summary>
        /// Holds the AcrossVectorValuesGenerator generator.
        /// </summary>
        private AcrossVectorValuesGenerator _acrossVectorValuesGenerator;

        /// <summary>
        /// Holds the StaticValuesGenerator generator.
        /// </summary>
        private StaticValuesGenerator _staticValuesGenerator;

        /// <summary>
        /// ControlLoop interface for doing the commands inserted in the gui.
        /// </summary>
        private ControlLoop _cntrlLoop;

        /// <summary>
        /// The matlab app object for handling the matlab application.
        /// </summary>
        private MLApp.MLApp _matlabApp;

        /// <summary>
        /// The selected protocol file name.
        /// </summary>
        private string _selectedProtocolName;

        /// <summary>
        /// The selected directions to give them hand REWARD (xxxxxyyy).
        /// The y-y-y is the indicators for the directions as followed by left-center-right.
        /// </summary>
        private byte _selectedHandRewardDirections;

        /// <summary>
        /// Locker for starting and stopping button to be enabled not both.
        /// </summary>
        private object _lockerStopStartButton;

        /// <summary>
        /// Locker for the pause nad resume button to be enabled not both.
        /// </summary>
        private object _lockerPauseResumeButton;

        /// <summary>
        /// Indicates if can press the start/stop button (that only after makeTrial presses on the init of the program or on changing parameters.
        /// When false - it means that casnt start the trials because no makeTrial presses.
        /// When true - it means that the make trials presses for the most updates properties in the gui and can press atsrt the trials.
        /// </summary>
        private bool _makeTrialsButtonPress;

        /// <summary>
        /// The controller api for the YASAKAWA motoman robot.
        /// </summary>
        private CYasnac _motocomController;

        /// <summary>
        /// Led controller for controlling the led strip.
        /// </summary>
        private LEDController _ledController;

        /// <summary>
        /// Infra red controller for turnnig the InfraRed on/off.
        /// </summary>
        private InfraRedController _infraredController;

        /// <summary>
        /// Logger for writing log information.
        /// </summary>
        private ILog _logger;
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
            _acrossVectorValuesGenerator = new AcrossVectorValuesGenerator();
            _staticValuesGenerator = new StaticValuesGenerator();
            InitializeTitleLabels();
            ShowVaryingControlsOptions(false);
            _matlabApp = new MLApp.MLApp();

            //connect to the robot and turn on it's servos.
            //avi-insert//
            _motocomController = new CYasnac("10.0.0.2", Application.StartupPath);
            //avi-insert//
            //_motocomController.SetServoOn();

            //creating the logger to writting log file information.
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Application.StartupPath+@"\Log4Net.config"));
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Info("Starting program...");

            //create the ledstrip controller and initialize it (also turn off leds).
            _ledController = new LEDController("COM4", 2000000, 250 , _logger);
            _ledController.OpenConnection();

            if (!_ledController.Connected)
                _ardionoPrtWarningLabel.Visible = true;
            _ledController.ResetLeds();

            //set the InfraRed controller object.
            _infraredController = new InfraRedController("Dev1", "AO1", "InfraRedChannel");
            //turn the infrared on.
            _infraredController.WriteEvent(true, InfraRedStatus.TurnedOn);

            Globals._systemState = SystemState.INITIALIZED;

            //make the delegate with it's control object and their nickname as pairs of dictionaries.
            Tuple<Dictionary<string, Control>, Dictionary<string, Delegate>> delegatsControlsTuple = MakeCtrlDelegateAndFunctionDictionary();
            _cntrlLoop = new ControlLoop(_matlabApp, _motocomController, _ledController, _infraredController, delegatsControlsTuple.Item2, delegatsControlsTuple.Item1, _logger);

            //reset the selected direction to be empty.
            _selectedHandRewardDirections = 0;

            //set the maximum (100%) of the  water filling to be as the cycle for the bottle to be empty (for 60ml) in 10xsec.
            _waterRewardMeasure.Maximum = Properties.Settings.Default.WaterBottleEmptyTime;

            //allocate the start/stop buttom locker.
            _lockerStopStartButton = new object();
            //disable initialy the start and stop buttom untill makeTrials buttom is pressed.
            _startButton.Enabled = false;
            _stopButtom.Enabled = false;

            //allocate the pause/resume nuttom locker.
            _lockerPauseResumeButton = new object();
            //disable initially both pause and resume buttoms untill makeTrials buttom is pressed.
            _btnPause.Enabled = false;
            _btnResume.Enabled = false;

            //add the rat names (as the setting have) to the rat names combo box.
            AddRatNamesToRatNamesComboBox();

            //set the default file browser protocol path directory.
            SetDefaultProtocolFileBrowserDirectory();

            //move the robot to it's home position when startup.
            //avi-insert//
            //_cntrlLoop.WriteHomePosFile();
            //_cntrlLoop.MoveRobotHomePosition();

            //create the result directory in the application path if needed.
            if(!Directory.Exists(Application.StartupPath + "\results"))
                Directory.CreateDirectory(Application.StartupPath + @"\results\");

            //adding background image to the window.
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Pinky_and_the_Brain_darker.jpg");
            this._varyingControlGroupBox.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Pinky_and_the_Brain_darker.jpg");
        }
        #endregion CONSTRUCTORS

        #region OUTSIDER_EVENTS_HANDLE_FUNCTION

        /// <summary>
        /// Delegate clearing the psycho online graph.
        /// </summary>
        public delegate void OnlinePsychoGraphClearDelegate();

        /// <summary>
        /// Clears the psycho online graph.
        /// </summary>
        public void OnlinePsychoGraphClear()
        {
            _onlinePsychGraphControl.Series.Clear();

            _onlinePsychGraphControl.ChartAreas.First(area => true).RecalculateAxesScale();
            
            _onlinePsychGraphControl.ChartAreas.First(area => true).AxisY.Maximum = 1.0;

            _onlinePsychGraphControl.Show();
        }

        /// <summary>
        /// Delegate setting the given point in the given series.
        /// </summary>
        /// <param name="seriesName">The series name to set it's point.</param>
        /// <param name="x">The x value of the point.</param>
        /// <param name="y">The y balue of the point.</param>
        /// <param name="newPoint">Indicated if it is a new point to add or existing point.</param>
        /// <param name="visible">Indicates if the point is visibbled on the graph.</param>
        public delegate void OnlinePsychoGraphSetPointDelegate(string seriesName, double x, double y, bool newPoint = false , bool visible = true);

        /// <summary>
        /// Setting the given point in the given series.
        /// </summary>
        /// <param name="seriesName">The series name to set the point to it.</param>
        /// <param name="x">The x value of the point.</param>
        /// <param name="y">The y value of the point.</param>
        /// <param name="newPoint">Indicates if the point is new to the chart or is an existing one.</param>
        /// <param name="visible"> Indicates if the point is visibled on th graph.</param>
        public void OnlinePsychoGraphSetPoint(string seriesName , double x , double y , bool newPoint = false , bool visible = true)
        {
            if (!(_onlinePsychGraphControl.Series.Count(series => series.Name == seriesName) > 0))
            {
                _onlinePsychGraphControl.Series.Add(seriesName);
                _onlinePsychGraphControl.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            }

            /*if (newPoint)
            {
                if (visible)
                {
                    _onlinePsychGraphControl.Series[seriesName].Points.AddXY(x, 0);
                    _onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x).IsValueShownAsLabel = false;
                }
            }
            else*/
            {
                if (_onlinePsychGraphControl.Series[seriesName].Points.Count(point => point.XValue == x) > 0)
                    _onlinePsychGraphControl.Series[seriesName].Points.Remove(_onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x));
                if (visible)
                {
                    _onlinePsychGraphControl.Series[seriesName].Points.AddXY(x, y);
                    _onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x).IsValueShownAsLabel = true;
                    string label = _onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x).LabelFormat = "{0:0.00}";
                }
            }

            _onlinePsychGraphControl.ChartAreas.First(area => true).RecalculateAxesScale();
        }

        /// <summary>
        /// Delegate for setting serieses names to the chart.
        /// </summary>
        /// <param name="seriesNames">The series list to set to the graph.</param>
        public delegate void OnlinePsychoGraphSetSeriesDelegate(List<string> seriesNames);
     
        /// <summary>
        /// Setting the online psycho graph series by the given series names list.
        /// </summary>
        /// <param name="seriesNames">The series names list to set to the chart.</param>
        public void OnlinePsychoGraphSetSeries(List<string> seriesNames)
        {
            foreach (string seriesName in seriesNames)
            {
                _onlinePsychGraphControl.Series.Add(seriesName);
                _onlinePsychGraphControl.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            }
        }

        /// <summary>
        /// Event handler when clicking on the graph in order to open it in a new big window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _onlinePsychGraphControl_Click(object sender, EventArgs e)
        {
            //The new window form to show the bigger graph on.
            Form visualizationForm = new Form();

            //adding the psych graph on it.
            visualizationForm.Controls.Add(_onlinePsychGraphControl);

            //maximize the graph.
            visualizationForm.Size = new Size(1600, 1000);
            _onlinePsychGraphControl.Size = new Size(1500, 900);

            //deleting the event fromk the click event list in order to not make a recurssion when clicking again and again. 
            _onlinePsychGraphControl.Click -= _onlinePsychGraphControl_Click;

            //showing the new big form window.
            visualizationForm.ShowDialog();
            visualizationForm.Show();
        }

        /// <summary>
        /// Delegate for the trial details ListView text changing.
        /// </summary>
        /// <param name="text">The name of the variable to be inserted.</param>
        /// <param name="value">The value of the parameter to  be inserted.</param>
        public delegate void ChangeCurrentTrialDetailsListViewText(string text , string value);

        /// <summary>
        /// Updates the trial details ListView with the given text.
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="value">The value of the parameter to add.</param>
        private void ChangeCurrentTrialDetailsListView(string name, string value)
        {
            ListViewItem lvi = new ListViewItem(name);
            lvi.SubItems.Add(value);
            _trialDetailsListView.Items.Add(lvi);
        }

        /// <summary>
        /// Delegate for the trial details ListView text clearing.
        /// </summary>
        public delegate void ClearCurrentTrialDetailsListViewText();

        /// <summary>
        /// Clear the trial details ListView text.
        /// </summary>
        private void ClearCurrentTrialDetailsListView() 
        { 
            _trialDetailsListView.Items.Clear();
            _trialDetailsListView.Columns.Clear();
            _trialDetailsListView.Columns.Add("Name", "Name", 350);
            _trialDetailsListView.Columns.Add("Description", "Description", 100);
            _trialDetailsListView.View = View.Details;
        }

        /// <summary>
        /// Delegate for the trial details ListView text changing.
        /// </summary>
        /// <param name="name">The name of the variable to be inserted.</param>
        /// <param name="value">The value of the variable to be inserted.</param>
        public delegate void ChangeGlobalDetailsListViewText(string name , string value);

        /// <summary>
        /// Update the global experiment details ListView with that parameter.
        /// </summary>
        /// <param name="name">The parameter name to show.</param>
        /// <param name="value">The value of the parameter to show.</param>
        private void ChangeGlobalExperimentDetailsListView(string name , string value)
        {
            ListViewItem lvi = new ListViewItem(name);
            lvi.SubItems.Add(value);
            _globaExperimentlInfoListView.Items.Add(lvi);
        }

        /// <summary>
        /// A delegate for clearing the global experiment details listview.
        /// </summary>
        public delegate void ClearGlobalDetailsListViewText();

        /// <summary>
        /// Clear the current global details listview.
        /// </summary>
        private void ClearGlobalExperimentDetailsListView()
        {
            _globaExperimentlInfoListView.Items.Clear();
            _globaExperimentlInfoListView.Columns.Clear();
            _globaExperimentlInfoListView.Columns.Add("Name", "Name", 190);
            _globaExperimentlInfoListView.Columns.Add("Description", "Description", 150);
            _globaExperimentlInfoListView.View = View.Details;
        }

        /// <summary>
        /// Delegate for event of finishing the experiment trials rounds.
        /// </summary>
        public delegate void FinishedAllTrialsInRoundDelegate();

        /// <summary>
        /// Handler for event of finishing the experiment trials rounds.
        /// </summary>
        public void FinishedAllTrialsRound()
        {
            _stopButtom.Enabled = false;
            _startButton.Enabled = false;
            _makeTrials.Enabled = true;
        }

        /// <summary>
        /// Handler for rat direction response panel.
        /// </summary>
        /// <param name="data"></param>
        public delegate void SetNoldusRatResponseInteractivePanelCheckboxes(byte data);

        /// <summary>
        /// Handler for updating the interactive rat response direction panel.
        /// </summary>
        /// <param name="data"></param>
        private void SetNoldusRatResponseInteractivePanel(byte data)
        {
            _leftNoldusCommunicationRadioButton.Checked = (data & 1) > 0;

            _centerNoldusCommunicationRadioButton.Checked = (data & 2) > 0;

            _rightNoldusCommunicationRadioButton.Checked =  (data & 4) > 0;

            _leftHandRewardCheckBox.Show();

            _centerHandRewardCheckBox.Show();

            _rightHandRewardCheckBox.Show();
        }

        /// <summary>
        /// Handler for water reward measurement interactive panel.
        /// </summary>
        public delegate void SetWaterRewardsMeasureDelegate(bool reset = false);

        /// <summary>
        /// Handler for setting the interactive water reward measure panel.
        /// <param name="reset">Indicated if to set the water measurement to zero (reset it).</param>
        /// </summary>
        private void SetWaterRewardsMeasure(bool reset = false)
        {
            if (!reset)
            {
                _logger.Info("Setting reward measure interactive panel....");

                //set the water anoumt in animation.
                _waterRewardMeasure.Value += 1;

                //set the water amount in the animation text.
                _waterRewardMeasure.Text = (((double)(_waterRewardMeasure.Value * 60) / _waterRewardMeasure.Maximum)).ToString("00.000") + "ml";
            }
            else
            {
                _logger.Info("Reseting reward measure interactive panel....");

                //set the water anoumt in animation.
                _waterRewardMeasure.Value = 0;

                //set the water amount in the animation text.
                _waterRewardMeasure.Text = (((double)0.000).ToString("00.000")) + "ml";
            }
        }

        /// <summary>
        /// Handler for clearing the selected rat name in the combobox.
        /// </summary>
        public delegate void ResetSelectedRatNameComboboxDelegate();

        /// <summary>
        /// Clearing the selected rat name in the combobox.
        /// </summary>
        private void ResetSelectedRatNameCombobox()
        {
            _selectedRatNameComboBox.ResetText();
            _selectedRatNameComboBox.SelectedItem = null;
        }

        /// <summary>
        /// Collect all needed controls and their delegates for the ControlLoop.
        /// </summary>
        /// <returns>
        /// The both dictionaries of the delegate and it's control.
        /// The first dictionary is for control object with it's name as key.
        /// The second dictionary is for delegate object with the same name as it's key.
        /// </returns>
        private Tuple<Dictionary<string, Control>, Dictionary<string, Delegate>> MakeCtrlDelegateAndFunctionDictionary()
        {
            //create the delegate dictionary include all delegates with their nick name to find as a key in the dictionary.
            Dictionary<string, Delegate> ctrlDelegatesDic = new Dictionary<string, Delegate>();
            Dictionary<string, Control> ctrlDictionary = new Dictionary<string, Control>();

            //add the delegate for updating the text for the current trial details ListView , also add the control of the ListView with the same key name.
            ChangeCurrentTrialDetailsListViewText changeCurrentTrialTextDelegate = new ChangeCurrentTrialDetailsListViewText(ChangeCurrentTrialDetailsListView);
            ctrlDelegatesDic.Add("UpdateCurrentTrialDetailsViewList", changeCurrentTrialTextDelegate);
            ctrlDictionary.Add("UpdateCurrentTrialDetailsViewList", _trialDetailsListView);

            //add the delegate for clearing the current trial details listview.
            ClearCurrentTrialDetailsListViewText clearCurrentTrialTextDelegate = new ClearCurrentTrialDetailsListViewText(ClearCurrentTrialDetailsListView);
            ctrlDelegatesDic.Add("ClearCurrentTrialDetailsViewList", clearCurrentTrialTextDelegate);
            ctrlDictionary.Add("ClearCurrentTrialDetailsViewList", _trialDetailsListView);

            //add the delegate for clearing the global details listview.
            ClearGlobalDetailsListViewText clearGlobalTextDelegate = new ClearGlobalDetailsListViewText(ClearGlobalExperimentDetailsListView);
            ctrlDelegatesDic.Add("ClearGlobaExperimentlDetailsViewList", clearGlobalTextDelegate);
            ctrlDictionary.Add("ClearGlobaExperimentlDetailsViewList", _globaExperimentlInfoListView);

            //add the delegate for updating the text for the current global experiment details ListView , also add the control of the ListView with the same key name.
            ChangeGlobalDetailsListViewText changeGlobalExperimentTextDelegate = new ChangeGlobalDetailsListViewText(ChangeGlobalExperimentDetailsListView);
            ctrlDelegatesDic.Add("UpdateGlobalExperimentDetailsListView", changeGlobalExperimentTextDelegate);
            ctrlDictionary.Add("UpdateGlobalExperimentDetailsListView", _globaExperimentlInfoListView);

            //add the delegates for the Noldus rat response interactive panel checkboxes.
            SetNoldusRatResponseInteractivePanelCheckboxes setNoldusRatResponseInteractivePanelCheckboxesDelegate = new SetNoldusRatResponseInteractivePanelCheckboxes(SetNoldusRatResponseInteractivePanel);
            ctrlDelegatesDic.Add("SetNoldusRatResponseInteractivePanel", setNoldusRatResponseInteractivePanelCheckboxesDelegate);
            ctrlDictionary.Add("SetNoldusRatResponseInteractivePanel", _centerHandRewardCheckBox);

            //add the delegate for the interactive water reward estimation panel.
            SetWaterRewardsMeasureDelegate setWaterRewardsMeasureDelegate = new SetWaterRewardsMeasureDelegate(SetWaterRewardsMeasure);
            ctrlDelegatesDic.Add("SetWaterRewardsMeasure", setWaterRewardsMeasureDelegate);
            ctrlDictionary.Add("SetWaterRewardsMeasure", _waterRewardMeasure);

            //add the delegate for event indicates finshing all rounds in trial experiment.
            FinishedAllTrialsInRoundDelegate finishedAlltrialRoundDelegate = new FinishedAllTrialsInRoundDelegate(FinishedAllTrialsRound);
            ctrlDelegatesDic.Add("FinishedAllTrialsRound", finishedAlltrialRoundDelegate);
            ctrlDictionary.Add("FinishedAllTrialsRound", _stopButtom);

            //add the delegate for events changing the online psycho graph for the experiment results.
            ctrlDictionary.Add("OnlinePsychoGraph", _onlinePsychGraphControl);
            
            OnlinePsychoGraphClearDelegate onlinePsychoGraphClearDelegate = new OnlinePsychoGraphClearDelegate(OnlinePsychoGraphClear);
            ctrlDelegatesDic.Add("OnlinePsychoGraphClear", onlinePsychoGraphClearDelegate);

            OnlinePsychoGraphSetPointDelegate onlinePsychoGraphSetPointDelegate = new OnlinePsychoGraphSetPointDelegate(OnlinePsychoGraphSetPoint);
            ctrlDelegatesDic.Add("OnlinePsychoGraphSetPoint", onlinePsychoGraphSetPointDelegate);

            OnlinePsychoGraphSetSeriesDelegate onlinePsychoGraphSetSeriesDelegate = new OnlinePsychoGraphSetSeriesDelegate(OnlinePsychoGraphSetSeries);
            ctrlDelegatesDic.Add("OnlinePsychoGraphSetSeries", onlinePsychoGraphSetSeriesDelegate);

            //add the delegate for clearing the selected rat name.
            ResetSelectedRatNameComboboxDelegate resetSelectedRatNameComboboxDelegate = new ResetSelectedRatNameComboboxDelegate(ResetSelectedRatNameCombobox);
            ctrlDelegatesDic.Add("ResetSelectedRatNameCombobox" , resetSelectedRatNameComboboxDelegate);
            ctrlDictionary.Add("ResetSelectedRatNameCombobox", _selectedRatNameComboBox);

            //return both dictionaries.
            return new Tuple<Dictionary<string, Control>, Dictionary<string, Delegate>>(ctrlDictionary, ctrlDelegatesDic);
        }
        #endregion OUTSIDER_EVENTS_HANDLE_FUNCTION

        #region GLOBAL_EVENTS_HANDLE_FUNCTIONS
        /// <summary>
        /// Closing the guiInterface window event handler.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void GuiInterface_Close(object sender , EventArgs e)
        {
            _excelLoader.CloseExcelProtocoConfigFilelLoader();

            //turn off the robot servos.
            //avi-insert//
            _motocomController.SetServoOff();

            //close the connection with the led strip.
            _ledController.CloseConnection();
            
            //turn off the InfraRed.
            _infraredController.WriteEvent(true, InfraRedStatus.TurnedOff);

            //stop the control loop.
            if (_cntrlLoop != null)
            {
                _cntrlLoop.Stop();
                _cntrlLoop.Dispose();
            }
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
                _protocolsComboBox.Items.Clear();
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
            //update the name of the selected protocol.
            _selectedProtocolName = _protocolsComboBox.SelectedItem.ToString();

            //_protocolsComboBox.SelectedItem = _protocolsComboBox.Items[0];
            SetVariables(_protoclsDirPath + "\\" + _selectedProtocolName);
            ShowVariablesToGui();
        }

        /// <summary>
        /// Sets the default file protocol directory.
        /// </summary>
        public void SetDefaultProtocolFileBrowserDirectory()
        {
            _protoclsDirPath = this._protocolsFolderBrowser.SelectedPath;
            AddFilesToComboBox(_protocolsComboBox, _protoclsDirPath);
        }

        /// <summary>
        /// Adding the rat names to the rat names combo box by the configuration settings.
        /// </summary>
        public void AddRatNamesToRatNamesComboBox()
        {
            //add the rat names in the config file to thw combo box.
            foreach (string ratName in Properties.Settings.Default.RatNames)
            {
                _selectedRatNameComboBox.Items.Add(ratName);
            }
        }

        /// <summary>
        /// Function handler for changing the rat name.
        /// </summary>
        /// <param name="sender">The combobox that has changed.</param>
        /// <param name="e">The args.</param>
        private void _selectedRatNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //change the selected rat name as followed by the combobox.
            if ((sender as ComboBox).SelectedItem != null)
                _cntrlLoop.RatName = (sender as ComboBox).SelectedItem.ToString();
            else
                _cntrlLoop.RatName = "";
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
            //if everything is o.k start the control loop.
            if(StartLoopStartCheck())
            {
                lock (_lockerStopStartButton)
                {
                    _startButton.Enabled = false;
                    _makeTrials.Enabled = false;
                    _stopButtom.Enabled = true;
                    _btnPause.Enabled = true;
                    _btnResume.Enabled = false;

                    //if already running - ignore.
                    if (!Globals._systemState.Equals(SystemState.RUNNING))
                    {
                        //update the system state.
                        Globals._systemState = SystemState.RUNNING;

                        //add the static variable list of double type values.
                        _staticValuesGenerator.SetVariables(_variablesList);

                        //start the control loop.
                        //need to be changed according to parameters added to which trajectoryname to be called from the excel file.
                        //string trajectoryCreatorName = _variablesList._variablesDictionary["TRAJECTORY_CREATOR"]._description["parameters"]._ratHouseParameter[0];
                        int trajectoryCreatorNum = int.Parse(_variablesList._variablesDictionary["TRAJECTORY_CREATOR"]._description["parameters"]._ratHouseParameter[0]);
                        string trajectoryCreatorName = (trajectoryCreatorNum == 0) ? "Training" : "ThreeStepAdaptation";

                        //determine the TrajectoryCreator to call with.
                        switch (trajectoryCreatorNum)
                        {
                            case 0:
                                trajectoryCreatorName = "Training";
                                break;
                            case 1:
                                trajectoryCreatorName = "ThreeStepAdaptation";
                                break;
                            case 2:
                                trajectoryCreatorName = "Azimuth1D";
                                break;
                            default:
                                break;
                        }

                        _cntrlLoop.NumOfRepetitions = int.Parse(_numOfRepetitionsTextBox.Text.ToString());
                        _cntrlLoop.NumOfStickOn = int.Parse(_textboxStickOnNumber.Text.ToString());
                        _cntrlLoop.PercentageOfTurnedOnLeds = double.Parse(_textboxPercentageOfTurnOnLeds.Text.ToString());
                        _cntrlLoop.LEDBrightness = int.Parse(_textboxLEDBrightness.Text.ToString());
                        _cntrlLoop.Start(_variablesList, _acrossVectorValuesGenerator._crossVaryingValsBoth, _staticValuesGenerator._staticVariableList, Properties.Settings.Default.Frequency, trajectoryCreatorName);
                    }
                }
            }
        }

        /// <summary>
        /// Check all parameters needed before the control loop execution.
        /// </summary>
        /// <returns>True or false if can execute the control loop.</returns>
        private bool StartLoopStartCheck()
        {
            //if selected rat name is o.k
            if (_selectedRatNameComboBox.SelectedItem!=null)
            {
            }
            else
            {
                MessageBox.Show("Error - Should select a rat name before starting!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(int.Parse(_numOfRepetitionsTextBox.Text.ToString()) % int.Parse(_textboxStickOnNumber.Text.ToString()) != 0)
            {
                MessageBox.Show("Error - StickOn number should devide Num Of Repetitions!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Make trails for the selected varying variables.
        /// </summary>
        /// <param name="sender">The sender oButtom object.</param>
        /// <param name="e">Args.</param>
        private void _makeTrials_Click(object sender, EventArgs e)
        {
            //Check if both he num of staicases and withinstairs is 1 or 0.
            #region STATUS_NUM_OF_OCCURENCES
            int withinStairStstusOccurences = NumOfSpecificStatus("WithinStair");
            int acrossStairStatusOccurences = NumOfSpecificStatus("AcrossStair");
            if (withinStairStstusOccurences != acrossStairStatusOccurences || acrossStairStatusOccurences > 1)
            {
                MessageBox.Show("Cannot start the experiment.\n The number of Withinstairs should be the same as AccrossStairs and both not occurs more than 1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //do nothing else.
                return;
            }
            #endregion STATUS_NUM_OF_OCCURENCES

            //if there ar no errors middwile. Generate crrossvals and run the control loop.
            _acrossVectorValuesGenerator.SetVariablesValues(_variablesList);
            _acrossVectorValuesGenerator.MakeTrialsVaringVectors();

            ClearVaryingListBox();

            //make the varyingCrossVals matrix.
            _acrossVectorValuesGenerator.MakeVaryingMatrix();

            //add the crossVaryingVals to the listbox.
            AddVaryingMatrixToVaryingListBox(_acrossVectorValuesGenerator._crossVaryingValsBoth, _acrossVectorValuesGenerator._varyingVectorDictionaryParalelledForLandscapeHouseParameters);

            //show the list box controls(add , remove , etc...)
            ShowVaryingControlsOptions(true);

            //show the start button
            _startButton.Enabled = true;
        }

        /// <summary>
        /// Handler for stop experiment buttom clicked.
        /// </summary>
        /// <param name="sender">The stop buttom object.</param>
        /// <param name="e">The args.</param>
        private void _stopButtom_Click(object sender, EventArgs e)
        {
            //update the system state.
            //Globals._systemState = SystemState.STOPPED;
            lock (_lockerStopStartButton)
            {
                _stopButtom.Enabled = false;
                _startButton.Enabled = true;
                _btnPause.Enabled = false;
                _btnResume.Enabled = false;

                //stop the control loop.
                _cntrlLoop.Stop();
            }
        }

        /// <summary>
        /// Handler for pause experiment button clicked.
        /// </summary>
        /// <param name="sender">The pause button object.</param>
        /// <param name="e">The args.</param>
        private void _btnPause_Click(object sender, EventArgs e)
        {
            lock (_lockerPauseResumeButton)
            {
                _btnPause.Enabled = false;
                _btnResume.Enabled = true;

                Globals._systemState = SystemState.PAUSED;
                _cntrlLoop.Pause();
            }
        }
        
        /// <summary>
        /// Handler for resume experiment button clicked.
        /// </summary>
        /// <param name="sender">The pause button object.</param>
        /// <param name="e">The args.</param>
        private void _btnResume_Click(object sender, EventArgs e)
        {
            lock (_lockerPauseResumeButton)
            {
                _btnPause.Enabled = true;
                _btnResume.Enabled = false;

                Globals._systemState = SystemState.RUNNING;
                _cntrlLoop.Resume();
            }
        }
        #endregion GLOBAL_EVENTS_HANDLE_FUNCTIONS

        #region VARYING_LISTBOX_FUNCTIONS
        /// <summary>
        /// Adding the generated cross varying values to the varying listbox.
        /// </summary>
        /// <param name="varyingCrossValsBoth">The cross genereated varying values to add to the listbox.</param>
        private void AddVaryingMatrixToVaryingListBox(List<Dictionary<string, List<double>>> varyingCrossValsBoth, Dictionary<string, Dictionary<double, double>> varyingVectorDictionaryParalelledForLandscapeHouseParameters)
        {
            //collect the titles for the listbox columns to a list.
            string listBoxTitleLineText = "";
            List<string> niceNameList = new List<string>();
            foreach (string varName in varyingCrossValsBoth.ElementAt(0).Keys)
            {
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter.ElementAt(0);
                niceNameList.Add(varNiceName);
            }

            //add the titles for the listbox columns
            listBoxTitleLineText = string.Join("\t", niceNameList);
            _varyingListBox.Items.Add(listBoxTitleLineText);

            //enable horizonal scrolling.
            _varyingListBox.HorizontalScrollbar = true;

            //set the display member and value member for each item in the ListBox thta represents a Dictionary values in the varyingCrossVals list.
            //_varyingListBox.DisplayMember = "_text";
            //_varyingListBox.ValueMember = "_listIndex";


            //make the list describes all varying lines to describes both parameters (if should) for the ratHouseParameters and the landscapeHouseParameters.
            List<Dictionary<string, string>> varyingCrossValsBothStringVersion = CrossVaryingValuesToBothParameters(varyingCrossValsBoth);

            //add all varying cross value in new line in the textbox.
            int index = 0;

            foreach (Dictionary<string, string> varRowDictionaryItem in varyingCrossValsBothStringVersion)
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
        /// Creates a string to string inner dictionary inside of string to list of doubles.
        /// </summary>
        /// <param name="ratHouseVaryingCrossVals">The crossVaryingValues of the both ratHouseParameter and landscapeHouseParameter.</param>
        /// <returns>
        /// A list of dictionaries.
        /// Each item in the dictionary describes a raw of trial for the varying.
        /// Each item is a dictionary of string to string.
        /// The first string (key) is for the variable name.
        /// The second string (value) is for the value of the both ratHouseValue and the landscapeHouseValue if enabled or only the first for the key variable string.
        /// </returns>
        private List<Dictionary<string, string>> CrossVaryingValuesToBothParameters(List<Dictionary<string, List<double>>> ratHouseVaryingCrossVals)
        {
            //The list to be returned.
            List<Dictionary<string, string>> crossVaryingBothParmeters = new List<Dictionary<string, string>>();

            //The string builder to build string with.
            StringBuilder sBuilder = new StringBuilder();

            //run over all lines in the crossVals for the ratHouseValues.
            foreach (Dictionary<string, List<double>> varRatHouseRowItem in ratHouseVaryingCrossVals)
            {
                //make a row to add to the returned list.
                Dictionary<string, string> varyingBothRowItem = new Dictionary<string, string>();

                //The string for a variable in the current row.
                string itemBothParameterString = "";

                //run over all the variables in the row.
                foreach (string varName in varRatHouseRowItem.Keys)
                {
                    //clear the string builder for the new variable in the current row.
                    sBuilder.Clear();

                    //check if the value for the variable in the current line is set to tbot the ratHouseValue and the lanscapeHouseValue.
                    if (varRatHouseRowItem[varName].Count() > 1)
                    {
                        itemBothParameterString = BracketsAppender(sBuilder, varRatHouseRowItem[varName].ElementAt(0).ToString(),
                            varRatHouseRowItem[varName].ElementAt(1).ToString());
                    }

                    else
                    {
                        itemBothParameterString = varRatHouseRowItem[varName].ElementAt(0).ToString();
                    }

                    //add the variable string description for the current line (of the both) for the dictionary describes that line.
                    varyingBothRowItem.Add(varName, itemBothParameterString);
                }

                //add the line (the description of varying trial) to the list of lines (trials).
                crossVaryingBothParmeters.Add(varyingBothRowItem);
            }

            return crossVaryingBothParmeters;
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
        private void _addVaryingCombination_Click(object sender, EventArgs e)
        {
            //get the cross varying values list.
            List<Dictionary<string, List<double>>> crossVaryingVals = _acrossVectorValuesGenerator._crossVaryingValsBoth;

            //make a dictionary with the variable name as key and the belong textbox as value.
            Dictionary<string, TextBox> varNameToTextboxDictionary = new Dictionary<string, TextBox>();

            //Showing the new little form to get the inputs for the new combination.
            ShowControlAddingVaryingForm(crossVaryingVals, varNameToTextboxDictionary);
        }

        /// <summary>
        /// Showing the new tittle form of the textboxes for the varyin variables to get the input from.
        /// </summary>
        /// <param name="crossVaryingVals">The crossVaryingVals list for all the trials.</param>
        /// <param name="varNameToTextboxDictionary">The dictionary map for the variable string (key) to the variable representing textbox(value).</param>
        /// <returns></returns>
        private Form ShowControlAddingVaryingForm(List<Dictionary<string, List<double>>> crossVaryingVals, Dictionary<string, TextBox> varNameToTextboxDictionary)
        {
            //show thw new little form for the desired variables.
            Form littleTempForm = new Form();

            int leftOffset = 0;
            int topOffset = 0;
            int width = 140;
            int height = 14;
            //add an input textbox with label show the name for each varying parameter.
            foreach (string varName in crossVaryingVals.ElementAt(0).Keys)
            {
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter.ElementAt(0);

                Label varyingAttributeLabel = new Label();
                varyingAttributeLabel.Text = varName;
                varyingAttributeLabel.Top = topOffset += 35;
                varyingAttributeLabel.Left = leftOffset;
                varyingAttributeLabel.Height = height;
                varyingAttributeLabel.Width = width;
                littleTempForm.Controls.Add(varyingAttributeLabel);

                TextBox varyingAttributeTextBox = new TextBox();
                varyingAttributeTextBox.Top = topOffset;
                varyingAttributeTextBox.Left = leftOffset + width + 20;
                littleTempForm.Controls.Add(varyingAttributeTextBox);

                //add the variable name with the belonged textbox.
                varNameToTextboxDictionary.Add(varName, varyingAttributeTextBox);
            }

            //handle the confirm adding new combination.
            Button confirmCombinationAdding = new Button();
            littleTempForm.Controls.Add(confirmCombinationAdding);
            confirmCombinationAdding.Top = topOffset;
            confirmCombinationAdding.Left = leftOffset + width;
            confirmCombinationAdding.Click += new EventHandler((sender2, e2) => confirmVaryingCombinationAdding_Click(sender2, e2, varNameToTextboxDictionary));

            //show the dialog (need to be after adding controls) with the parent as the main windows frozed behind.
            littleTempForm.ShowDialog(this);

            //retutn the little form;
            return littleTempForm;
        }

        /// <summary>
        /// Handler for clicking on the confirm buttom for adding the combination added in the little window.
        /// </summary>
        /// <param name="sender">The buttom object.</param>
        /// <param name="e">args.</param>
        /// <param name="varNameToTextboxDictionary">The dictionary map for the variable string (key) to the variable representing textbox(value).</param>
        private void confirmVaryingCombinationAdding_Click(object sender, EventArgs e, Dictionary<string, TextBox> varNameToTextboxDictionary)
        {
            //creating dictionary for variable name as key and it's value as a value.
            Dictionary<string, string> varNameToValueDictionary = new Dictionary<string, string>();

            //converting the textboxes to strings value according to their text.
            foreach (string varName in varNameToTextboxDictionary.Keys)
            {
                varNameToValueDictionary.Add(varName, varNameToTextboxDictionary[varName].Text);
            }

            //confirm if the input is spelled well.
            if (!CheckVaryingListBoxProperInput(varNameToValueDictionary))
                return;

            //add the new combination after checking the spelling for the input.
            AddNewVaryngCombination(varNameToValueDictionary);

            Button clickedButtom = sender as Button;
            Form littleWindow = clickedButtom.Parent as Form;

            //close the adding combination little window.
            littleWindow.Close();
        }

        /// <summary>
        /// Adding new line (trial) of varying combination to the varyingCrossVals and to the listbox.
        /// </summary>
        /// <param name="varNameToValueDictionary">
        /// The item that describes the trial by a dictionary.
        /// The key os for the variable name.
        /// The value is for the value of that variable include for both ratHouseParameter and landscapeHouseParameter,
        ///if needed by [x][y] string.
        /// </param>
        private void AddNewVaryngCombination(Dictionary<string, string> varNameToValueDictionary)
        {
            Dictionary<string, List<double>> varNameToValueDictionaryDoubleListVersion = new Dictionary<string, List<double>>();
            foreach (string varName in varNameToValueDictionary.Keys)
            {
                varNameToValueDictionaryDoubleListVersion.Add(varName,
                    ConvertStringListToDoubleList(BracketsSplitter(varNameToValueDictionary[varName])));
            }

            _acrossVectorValuesGenerator._crossVaryingValsBoth.Add(varNameToValueDictionaryDoubleListVersion);

            string listBoxLineText = string.Join("\t", varNameToValueDictionary.Values);
            _varyingListBox.Items.Add(listBoxLineText);
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

                _acrossVectorValuesGenerator._crossVaryingValsBoth.RemoveAt(_varyingListBox.SelectedIndex - 1);

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
        private void ReduceIndexesFromNumberedIndex(int beginIndex, ListBox varyingListBox)
        {
            for (int index = beginIndex; index < varyingListBox.Items.Count; index++)
            {
                VaryingItem varyItem = varyingListBox.Items[index] as VaryingItem;
                varyItem._listIndex--;
            }
        }

        /// <summary>
        /// Detemines ig the varying control items are visible according to the input. show.
        /// </summary>
        /// <param name="show">If to show the varying control items.</param>
        private void ShowVaryingControlsOptions(bool show)
        {
            this._varyingListBox.Visible = show;
            this._addVaryingCobination.Visible = show;
            this._removeVaryingCombination.Visible = show;
        }
        #endregion VARYING_LISTBOX_FUNCTIONS

        #region HAND_REWARD_CONTROLL_FUNCTION
        /// <summary>
        /// Handler for digital hand reward button clicked.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">Args.</param>
        private void _digitalHandRewardButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _cntrlLoop.GiveRewardHandReward(_selectedHandRewardDirections, false);
            });
        }

        /// <summary>
        /// Handler for the continious hand reward releasing (when release the button - should stop the selected reward due to the clicked).
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">Args.</param>
        private void _continiousHandRewardKeyReleaed(object sender, MouseEventArgs e)
        {
            _cntrlLoop.GiveRewardHandReward(0, true);
        }

        /// <summary>
        /// Handler for the continious hand reward clicking (when release the button - should stop the given reward due to the clicked that opened the selected REWARD).
        /// </summary>
        /// <param name="sender">The buutton.</param>
        /// <param name="e">Args.</param>
        private void _countiniousHandRewardKeyDown(object sender, MouseEventArgs e)
        {
            _cntrlLoop.GiveRewardHandReward(_selectedHandRewardDirections, true);
        }

        /// <summary>
        /// Handler for changing the state of the left reward direction.
        /// </summary>
        /// <param name="sender">The checbox.</param>
        /// <param name="e">Args.</param>
        private void _leftHandRewardCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox leftCheckBox = sender as CheckBox;

            if (leftCheckBox.Checked)
            {
                //| operator with 0000_0001
                _selectedHandRewardDirections |= 1;
            }

            else
            {
                //& operator with 0000_0100
                _selectedHandRewardDirections &= 4;
            }
        }

        /// <summary>
        /// Handler for changing the state of the center reward direction.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">Args.</param>
        private void _centerHandRewardCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox centerheckBox = sender as CheckBox;

            if (centerheckBox.Checked)
            {
                //| operator with 0000_0010
                _selectedHandRewardDirections |= 2;
            }

            else
            {
                //& operator with 0000_0101
                _selectedHandRewardDirections &= 5;
            }
        }

        /// <summary>
        /// Handler for changing the state of the right reward direction.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">Args.</param>
        private void _rightHandRewardCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rightCheckBox = sender as CheckBox;

            if (rightCheckBox.Checked)
            {
                //| operator with 0000_0100
                _selectedHandRewardDirections |= 4;
            }

            else
            {
                //& operator with 0000_0011
                _selectedHandRewardDirections &= 3;
            }

        }
        #endregion HAND_REWARD_CONTROLL_FUNCTION

        #region AUTOS
        /// <summary>
        /// Event for changing the AutoReward status.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _autoRewardsTextBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_autoRewardsCheckBox.Checked)
                _cntrlLoop.AutoReward = true;
            else
                _cntrlLoop.AutoReward = false;
        }

        /// <summary>
        /// Event for changing the AutoFixation status.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _autoFixation_CheckedChanged(object sender, EventArgs e)
        {
            if (_autoFixationCheckBox.Checked)
                _cntrlLoop.AutoFixation = true;
            else
                _cntrlLoop.AutoFixation = false;
        }

        /// <summary>
        /// Event for changing the AutoStart status.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _autoStartcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_autoStartCheckBox.Checked)
                _cntrlLoop.AutoStart = true;
            else
                _cntrlLoop.AutoStart = false;
        }

        /// <summary>
        /// Event for changing the AutoRewardSound status.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _autoRewardSound_CheckedChanged(object sender, EventArgs e)
        {
            if (_autoRewardSound.Checked)
                _cntrlLoop.AutoRewardSound = true;
            else
                _cntrlLoop.AutoRewardSound = false;
        }
        #endregion AUTOS

        #region MODES
        /// <summary>
        /// Event for changing the mode of the experiment.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _fixationOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_fixationOnlyCheckBox.Checked)
                _cntrlLoop.FixationOnlyMode = true;
            else
                _cntrlLoop.FixationOnlyMode = false;
        }

        /// <summary>
        /// Event for turnning on/off the sound of the fixation break ahhhhhhh.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _breakFixationSoundEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_breakFixationSoundEnableCheckBox.Checked)
                _cntrlLoop.EnableFixationBreakSound = true;
            else
                _cntrlLoop.EnableFixationBreakSound = false;
        }
        #endregion MODES

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
                /*Label newLabel = new Label();
                this.Controls.Add(newLabel);
                newLabel.Name = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Text = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0];
                newLabel.Width = width - 35;
                newLabel.Height = height;
                newLabel.Top = top;
                newLabel.Left = left;*/
                ShowVariableLabel(_variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter[0], top, left, width + 40, height, eachDistance, _variablesList._variablesDictionary[varName]._description["tool_tip"]._ratHouseParameter[0]);

                ShowVariableAttributes(varName, top, left, width, height, eachDistance, 750);

                top += 35;
            }
        }

        /// <summary>
        /// Show the label of the variable name in the gui variable list.
        /// </summary>
        /// <param name="varName">The variable name to show.</param>
        /// <param name="top">The top place offset.</param>
        /// <param name="left">The left place offset.</param>
        /// <param name="width">The width of the label.</param>
        /// <param name="height">The height of the label.</param>
        /// <param name="eachDistance">The distance between each label.</param>
        /// <param name="toolTipString">The tooltipper string to add to the label.</param>
        public void ShowVariableLabel(string varName , int top , int left , int width  , int height , int eachDistance , string toolTipString = "")
        {
            //create the new label to show on the gui.
            Label newLabel = new Label();
            
            //add the label on thr gui.
            _dynamicParametersPanel.Controls.Add(newLabel);
            newLabel.Name = varName;
            newLabel.Text = varName;
            newLabel.Width = width - 35;
            newLabel.Height = height;
            newLabel.Top = top;
            newLabel.Left = left;

            //add the tooltip help for the label.
            _guiInterfaceToolTip.SetToolTip(newLabel, toolTipString);

            //also , add the label to the dynamic control list.
            _dynamicAllocatedTextBoxes.Add(newLabel.Text.ToString() + "Label", newLabel);
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
            statusCombo.Items.Add("Random");

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

                case "5":
                    statusCombo.SelectedText = "Random";
                    break;
            }

            //add the status ComboBox to the gui.
            _dynamicParametersPanel.Controls.Add(statusCombo);
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

            _dynamicParametersPanel.Controls.Add(incrementTextBox);
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

            _dynamicParametersPanel.Controls.Add(highBoundTextBox);
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

            _dynamicParametersPanel.Controls.Add(lowBoundTextBox);
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
                case "5":
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

            _dynamicParametersPanel.Controls.Add(parametersTextBox);
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
                _dynamicParametersPanel.Controls.Remove(ctrl);
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

                //add the label to the gui.
                _dynamicParametersPanel.Controls.Add(lbl);
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
        /// Split [x][y]... to list of {x, y, ...}
        /// </summary>
        /// <param name="value">The string to be splitted.</param>
        /// <returns>The list of splitted strings.</returns>
        private List<string> BracketsSplitter(string value)
        {
            //split for each element as the brackets should.
            List<string> splittedList = value.Split('[').ToList();

            //The first splitted value is "" , so drop it.
            splittedList.RemoveAt(0);

            //drop the ']' brackets which appears in the end.
            for(int i=0;i<splittedList.Count;i++)
            {
                splittedList[i] = splittedList.ElementAt(i).Substring(0 , splittedList.ElementAt(i).Length-1);
            }

            //return the splitted list.
            return splittedList;
        }

        /// <summary>
        /// Converts a list of strings to a list of doubles.
        /// </summary>
        /// <param name="strList">The string list.</param>
        /// <returns>The converted double list.</returns>
        private List<double> ConvertStringListToDoubleList(List<string> strList)
        {
            //The double list to be returned.
            List<double> doubleList = new List<double>();

            //converts each element in the string list to double and insert to the double list.
            foreach (string stringVal in strList)
            {
                doubleList.Add(int.Parse(stringVal));
            }

            //return the converted list.
            return doubleList;
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
        /// Checking ig the input for the new adding varying vector is proper.
        /// </summary>
        /// <param name="toCheckVector">The list of variables with values to be checked.</param>
        /// <returns>True or false if the input is propper.</returns>
        private bool CheckVaryingListBoxProperInput(Dictionary<string , string> toCheckVector)
        {
            //check if each attribute is according to both parameters if needed and the brackets also.
            foreach (string varName in toCheckVector.Keys)
            {
                int firstLeftBracketIndex = toCheckVector[varName].IndexOf('[');
                int firstRightBracketIndex = toCheckVector[varName].IndexOf(']');
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter.ElementAt(0);

                //if both parameters are enabled ,  should check for 2 of [].
                if(_variablesList._variablesDictionary[varName]._description["parameters"]._bothParam)
                {
                    //if no brackets at all there is error because we expect for [x][y]  and not for a scalar.
                    if (firstRightBracketIndex == -1 || firstLeftBracketIndex == -1)
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " need 2 attributes!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (toCheckVector[varName].Count(x => x.Equals('[')) != 2 || toCheckVector[varName].Count(x => x.Equals(']')) != 2)
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " need 2 attributes!", "Error" ,  MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //check that the first '[' is before the first ']'.
                    if (firstLeftBracketIndex > firstRightBracketIndex)
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " has [x][y] syntax error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //check that the digits between the brackets define a real number.
                    if (!DigitsNumberChecker(toCheckVector[varName].Substring(firstLeftBracketIndex + 1, firstRightBracketIndex - firstLeftBracketIndex - 1)))
                    {;
                        MessageBox.Show("Error : variable " + varNiceName + " has some attributs that are no numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    int secondLeftBracketIndex = toCheckVector[varName].IndexOf('[', firstLeftBracketIndex + 1);
                    int secondRightBracketIndex = toCheckVector[varName].IndexOf(']', firstRightBracketIndex + 1);

                    //check that the second '[' is before the second ']'.
                    if (secondLeftBracketIndex > secondRightBracketIndex)
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " has [x][y] syntax error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //check that the digits between the brackets define a real number.
                    if (!DigitsNumberChecker(toCheckVector[varName].Substring(firstLeftBracketIndex + 1, firstRightBracketIndex - firstLeftBracketIndex - 1)))
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " has some attributs that are no numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                
                //if only the ratHouseParameter is enabled for this variable.
                else
                {
                    //if there are brackets for a scalar.
                    if (firstRightBracketIndex > -1 || firstLeftBracketIndex > -1)
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " has brackets but is a scalar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //if not represent a number.
                    if(!DigitsNumberChecker(toCheckVector[varName]))
                    {
                        MessageBox.Show("Error : variable " + varNiceName + " has [x][y] syntax error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            //if everything is o.k return true.
            return true;
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
        /// Check if a given string represent a integer.
        /// </summary>
        /// <param name="number">The given string.</param>
        /// <returns>True or False if an integer representation for the string.</returns>
        private bool IntegerChecker(string number)
        {
            if (number.Count(c => (c < '0' || c > '9')) > 0)
                return false;
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
                case "Random":
                    return "5";
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
                case "5":   //ramdom
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

        /// <summary>
        /// Handler for changing the number of repetition input text.
        /// </summary>
        /// <param name="sender">The checkbox control.</param>
        /// <param name="e">Args.</param>
        private void _numOfRepetitionsTextBox_Leave(object sender, EventArgs e)
        {
            //check if represents an integer number.
            if (!IntegerChecker(_numOfRepetitionsTextBox.Text.ToString()))
            {
                MessageBox.Show("No an integer number entered - returnnig to 1 as default", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //put the default number.
                _numOfRepetitionsTextBox.Text = "1";
            }
        }
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

    /// <summary>
    /// Enum describes the systen states.
    /// </summary>
    public enum SystemState
    {
        /// <summary>
        /// The system is running now.
        /// </summary>
        RUNNING = 0,

        /// <summary>
        /// The system has been stopped by the user.
        /// </summary>
        STOPPED = 1,
        
        /// <summary>
        /// The system has been paused by the user.
        /// </summary>
        PAUSED = 2,

        /// <summary>
        /// The system is now warmed up.
        /// </summary>
        INITIALIZED = 4,

        /// <summary>
        /// The current experiment (all trials) over , waiting for the next command.
        /// </summary>
        FINISHED = 5
    }
}