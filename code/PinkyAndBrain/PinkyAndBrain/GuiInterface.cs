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
using Params;
using Trajectories;
using MotocomdotNetWrapper;
using LED.Strip.Adressable;
using InfraRedSystem;
using log4net;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using MotomanSystem;
using VaryingValuesGenerators;

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
        private Dictionary<string, Control> _dynamicAllocatedTextBoxes;

        /// <summary>
        /// Indicates each of the dynamic allocatex textbox status before freezing it via running.
        /// </summary>
        private Dictionary<string, bool> _dynamicAllocatexTextboxesEnabledStatusBeforeFreeze;

        /// <summary>
        /// Dictionary describes all ButtonBase (checkboxes and radiobuttons) names in the gui as keys with their conrol as value.
        /// </summary>
        private Dictionary<string, ButtonBase> _buttonbasesDictionary;

        /// <summary>
        /// A list holds all the titles for the variables attribute to show in the title of the table.
        /// </summary>
        private List<Label> _titlesLabelsList;

        /// <summary>
        /// Holds the AcrossVectorValuesGenerator generator.
        /// </summary>
        private IVaryingValuesGenerator _acrossVectorValuesGenerator;

        /// <summary>
        /// Holds the StaticValuesGenerator generator.
        /// </summary>
        private StaticValuesGenerator _staticValuesGenerator;

        /// <summary>
        /// ControlLoop interface for doing the commands inserted in the gui.
        /// </summary>
        private ControlLoop _cntrlLoop;

        /// <summary>
        /// The selected protocol file full name.
        /// </summary>
        private string _selectedProtocolFullName;

        /// <summary>
        /// The selected protocol name with no .xlsx extension and ('-') additional name.
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
        /// Indicates if the robot was engaged or disengage.
        /// </summary>
        private bool _isEngaged;

        /// <summary>
        /// The controller api for the YASAKAWA motoman robot.
        /// </summary>
        private MotomanController _motocomController;

        /// <summary>
        /// Led controller for controlling the led strip.
        /// </summary>
        private LEDController _ledController;

        /// <summary>
        /// Second Led controller for controlling the led strip.
        /// </summary>
        private LEDController _ledController2;

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
            _dynamicAllocatedTextBoxes = new Dictionary<string, Control>();
            _dynamicAllocatexTextboxesEnabledStatusBeforeFreeze = new Dictionary<string, bool>();
            _acrossVectorValuesGenerator = DecideVaryinVectorsGeneratorByProtocolName();
            _staticValuesGenerator = new StaticValuesGenerator();
            InitializeTitleLabels();
            ShowVaryingControlsOptions(false);

            InitializeCheckBoxesDictionary();

            //creating the logger to writting log file information.
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Application.StartupPath + @"\Log4Net.config"));
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Info("Starting program...");

            try
            {
                //connect to the robot and turn on it's servos.
                _motocomController = new MotomanController("10.0.0.2", Properties.Settings.Default.Frequency , _logger);
            }
            catch
            {
                MessageBox.Show("Cannot connect to the robot BSC - check if robot is conncted in play mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //create the ledstrip controller and initialize it (also turn off leds).
            _ledController = new LEDController("COM4", 2000000, 150, 10, _logger);
            _ledController.OpenConnection();

            //create the second ledstrip controller and initialize it (also turn off leds).
            _ledController2 = new LEDController("COM5", 2000000, 150, 10, _logger);
            _ledController2.OpenConnection();

            if (!_ledController.Connected)
                _ardionoPrtWarningLabel.Visible = true;
            _ledController.ResetLeds();

            if (!_ledController2.Connected)
                _ardionoPrtWarningLabel.Visible = true;
            _ledController2.ResetLeds();

            //set the InfraRed controller object.
            _infraredController = new InfraRedController("Dev1", "AO1", "InfraRedChannel");
            //turn the infrared on.
            _infraredController.WriteEvent(true, InfraRedStatus.TurnedOn);

            Globals._systemState = SystemState.INITIALIZED;

            //make the delegate with it's control object and their nickname as pairs of dictionaries.
            Tuple<Dictionary<string, Control>, Dictionary<string, Delegate>> delegatsControlsTuple = MakeCtrlDelegateAndFunctionDictionary();
            _cntrlLoop = new ControlLoop(_motocomController, _ledController, _ledController2, _infraredController, delegatsControlsTuple.Item2, delegatsControlsTuple.Item1, _logger);

            //reset the selected direction to be empty.
            _selectedHandRewardDirections = 0;

            //set the maximum (100%) of the  water filling to be as the cycle for the bottle to be empty (for 60ml) in 10xsec.
            _waterRewardMeasure.Maximum = Properties.Settings.Default.WaterBottleEmptyTime;

            //allocate the start/stop buttom locker.
            _lockerStopStartButton = new object();
            //disable initialy the start and stop buttom untill makeTrials buttom is pressed.
            _btnStart.Enabled = false;
            _btnStop.Enabled = false;

            //allocate the pause/resume nuttom locker.
            _lockerPauseResumeButton = new object();
            //disable initially both pause and resume buttoms untill makeTrials buttom is pressed.
            _btnPause.Enabled = false;
            _btnResume.Enabled = false;

            //disable the make trials btn untill engaged state.
            _btnMakeTrials.Enabled = true;

            //the start point is in the disengaged state.
            _isEngaged = false;

            //add the rat names (as the setting have) to the rat names combo box.
            AddRatNamesToRatNamesComboBox();

            //add the students names (as the setting have) to the student names combo box.
            AddStudentsNamesToRatNamesComboBox();

            //set the default file browser protocol path directory.
            SetDefaultProtocolFileBrowserDirectory();

            //move the robot to it's home position when startup.
            //avi-insert//
            //_cntrlLoop.WriteHomePosFile();
            //_cntrlLoop.MoveRobotHomePosition();

            //create the result directory in the application path if needed.
            if (!Directory.Exists(Application.StartupPath + "\results"))
                Directory.CreateDirectory(Application.StartupPath + @"\results\");

            //adding background image to the window.
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Pinky_and_the_Brain_darker.jpg");
            this._varyingControlGroupBox.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Pinky_and_the_Brain_darker.jpg");
        }
        #endregion CONSTRUCTORS

        #region SELECTING_INTERFACES_FUNCTION_for_SELECTEDPROTOCOL
        /// <summary>
        /// Decide which of the VaryingVectorsGenerator to call by the protocol type.
        /// </summary>
        /// <returns>The mathed IVaryingVectorGenerator for the protocol type.</returns>
        private IVaryingValuesGenerator DecideVaryinVectorsGeneratorByProtocolName()
        {
            //take the name before the .xlsx and the generic name before the additional name (if added with '-' char).
            switch (_selectedProtocolName)
            {
                case "ThreeStepAdaptation":
                case "Azimuth1D":
                    return new VaryingValuesGenerator();
                case "Azimuth3D":
                    return new VaryingValuesGenerator3DAzimuth();
                case "AdamDelta":
                    return new VaryingValuesGeneratorAdamDelta();
                case "HeadingDiscrimination":
                    return new VaryingValuesGeneratorHeadingDiscrimination();
                default:
                    return new VaryingValuesGenerator();
            }
        }

        /// <summary>
        /// Returns the ITrajectoryCreator to create trajectories with by the protocol type name.
        /// </summary>
        /// <param name="protoclName">he protocol name.</param>
        /// <returns>The ITrajectoryCreator to create trajectories with for the trials.</returns>
        private ITrajectoryCreator DecideTrajectoryCreatorByProtocolName(string protoclName)
        {
            //determine the TrajectoryCreator to call with.
            switch (protoclName)
            {
                case "Training":
                    return new Training(_variablesList, _acrossVectorValuesGenerator._crossVaryingValsBoth, _staticValuesGenerator._staticVariableList, Properties.Settings.Default.Frequency);
                case "Azimuth3D":
                    return new Azimuth3D(_variablesList, _acrossVectorValuesGenerator._crossVaryingValsBoth, _staticValuesGenerator._staticVariableList, Properties.Settings.Default.Frequency);
                case "HeadingDiscrimination":
                    return new HeadingDiscrimination(_variablesList, _acrossVectorValuesGenerator._crossVaryingValsBoth, _staticValuesGenerator._staticVariableList, Properties.Settings.Default.Frequency);
                default:
                    {
                        MessageBox.Show("The protocol name is not accessed, running now the HeadingDiscrimination protocol", "Warning", MessageBoxButtons.OK);

                        return new HeadingDiscrimination(_variablesList, _acrossVectorValuesGenerator._crossVaryingValsBoth, _staticValuesGenerator._staticVariableList, Properties.Settings.Default.Frequency);
                    }
            }
        }
        #endregion SELECTING_INTERFACES_FUNCTION_for_SELECTEDPROTOCOL

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

            _onlinePsychGraphControl.ChartAreas.First(area => true).AxisX.Maximum = 100.0;

            _onlinePsychGraphControl.ChartAreas.First(area => true).AxisX.Minimum = -100.0;

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
        public delegate void OnlinePsychoGraphSetPointDelegate(string seriesName, double x, double y, bool newPoint = false, bool visible = true);

        /// <summary>
        /// Setting the given point in the given series.
        /// </summary>
        /// <param name="seriesName">The series name to set the point to it.</param>
        /// <param name="x">The x value of the point.</param>
        /// <param name="y">The y value of the point.</param>
        /// <param name="newPoint">Indicates if the point is new to the chart or is an existing one.</param>
        /// <param name="visible"> Indicates if the point is visibled on th graph.</param>
        public void OnlinePsychoGraphSetPoint(string seriesName, double x, double y, bool newPoint = false, bool visible = true)
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
                    _onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x).LabelFormat = "{0:0.00}";
                    _onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x).Color = ColorByStimulus(seriesName);
                    _onlinePsychGraphControl.Series[seriesName].Points.First(point => point.XValue == x).MarkerStyle = MarkerStyleByStimulus(seriesName);
                    //show the x axis value for all the points.
                    _onlinePsychGraphControl.ChartAreas[0].AxisX.IsInterlaced = true;
                    _onlinePsychGraphControl.ChartAreas[0].AxisX.IsLabelAutoFit = true;
                }
            }

            _onlinePsychGraphControl.ChartAreas.First(area => true).RecalculateAxesScale();
        }

        /// <summary>
        /// Decide which marker style to give to the point by the stim type.
        /// </summary>
        /// <param name="stimulsType">The stim type value.</param>
        /// <returns></returns>
        private MarkerStyle MarkerStyleByStimulus(string stimulusType)
        {
            switch (stimulusType)
            {
                case "1":   //vestibular only
                    return MarkerStyle.Circle;
                case "2":  //visual only
                    return MarkerStyle.Cross;
                case "10":
                    return MarkerStyle.Diamond;
                case "12":
                    return MarkerStyle.Square;
                case "3":   //combined
                    return MarkerStyle.Star10;
                case "4":
                    return MarkerStyle.Star4;
                case "5":
                    return MarkerStyle.Star5;
                case "11":
                    return MarkerStyle.Star6;
                case "13":
                    return MarkerStyle.Triangle;
                case "14":
                    return MarkerStyle.Cross;
                case "15":
                    return MarkerStyle.Diamond;
                default:
                    return MarkerStyle.Circle;
            }
        }

        /// <summary>
        /// Decide which color to give to the point by the stim type.
        /// </summary>
        /// <param name="stimulsType">The stim type value.</param>
        /// <returns></returns>
        private Color ColorByStimulus(string stimulsType)
        {
            switch (stimulsType)
            {
                case "1":   //vestibular only
                    return Color.Green;
                case "2":  //visual only
                case "10":
                case "12":
                    return Color.Red;
                case "3":   //combined
                case "4":
                case "5":
                case "11":
                case "13":
                case "14":
                case "15":
                    return Color.Blue;
                default:
                    return Color.Black;
                    break;
            }
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
        /// Delegate for the trial details ListView text changing.
        /// </summary>
        /// <param name="text">The name of the variable to be inserted.</param>
        /// <param name="value">The value of the parameter to  be inserted.</param>
        public delegate void ChangeCurrentTrialDetailsListViewText(string text, string value);

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
        public delegate void ChangeGlobalDetailsListViewText(string name, string value);

        /// <summary>
        /// Update the global experiment details ListView with that parameter.
        /// </summary>
        /// <param name="name">The parameter name to show.</param>
        /// <param name="value">The value of the parameter to show.</param>
        private void ChangeGlobalExperimentDetailsListView(string name, string value)
        {
            _logger.Info("Start updating details list view");

            ListViewItem lvi = new ListViewItem(name);
            lvi.SubItems.Add(value);
            _globaExperimentlInfoListView.Items.Add(lvi);

            _logger.Info("End updating details list view");
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
            //retun back from the textboxes freeze during the running.
            ReturnBackFromFreezeDynamicTextBoxes();

            _btnStop.Enabled = false;
            _btnStart.Enabled = false;
            _btnPause.Enabled = false;
            _btnResume.Enabled = false;
            _btnMakeTrials.Enabled = true;
            _btnEnagae.Enabled = false;
            _btnPark.Enabled = true;
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

            _rightNoldusCommunicationRadioButton.Checked = (data & 4) > 0;

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
        /// Handler for clearing the selected student name in the combobox.
        /// </summary>
        public delegate void ResetSelectedStudentNameComboboxDelegate();

        /// <summary>
        /// Clearing the selected rat name in the combobox.
        /// </summary>
        private void ResetSelectedRatNameCombobox()
        {
            _comboBoxSelectedRatName.ResetText();
            _comboBoxSelectedRatName.SelectedItem = null;
        }

        /// <summary>
        /// Clearing the selected student name in the combobox.
        /// </summary>
        private void ResetSelectedStudentNameComboBox()
        {
            _comboBoxStudentName.ResetText();
            _comboBoxStudentName.SelectedItem = null;
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
            ctrlDictionary.Add("FinishedAllTrialsRound", _btnStop);

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
            ctrlDelegatesDic.Add("ResetSelectedRatNameCombobox", resetSelectedRatNameComboboxDelegate);
            ctrlDictionary.Add("ResetSelectedRatNameCombobox", _comboBoxSelectedRatName);

            //add the delegate for clearing the selected student name.
            ResetSelectedStudentNameComboboxDelegate resetSelectedStudentNameComboboxDelegate = new ResetSelectedStudentNameComboboxDelegate(ResetSelectedStudentNameComboBox);
            ctrlDelegatesDic.Add("ResetSelectedStudentNameCombobox", resetSelectedStudentNameComboboxDelegate);
            ctrlDictionary.Add("ResetSelectedStudentNameCombobox", _comboBoxStudentName);

            //return both dictionaries.
            return new Tuple<Dictionary<string, Control>, Dictionary<string, Delegate>>(ctrlDictionary, ctrlDelegatesDic);
        }
        #endregion OUTSIDER_EVENTS_HANDLE_FUNCTION

        #region BIGGER_ONLINE_PSYCHO_GRAPH_EVENTS
        /// <summary>
        /// Event handler when double clicking on the graph in order to open it in a new big window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
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
            _onlinePsychGraphControl.DoubleClick -= _onlinePsychGraphControl_Click;

            //showing the new big form window.
            visualizationForm.Show();

            //make the foem unclosable until the main form is closed.
            visualizationForm.FormClosing += visualizationForm_FormClosing;
        }

        /// <summary>
        /// Event for closing the bigger onlinePsychoGrsph form.
        /// </summary>
        /// <param name="sender">The bigger graph form.</param>
        /// <param name="e">The args.</param>
        void visualizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
        #endregion BIGGER_ONLINE_PSYCHO_GRAPH_EVENTS

        #region GLOBAL_EVENTS_HANDLE_FUNCTIONS
        /// <summary>
        /// Closing the guiInterface window event handler.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void GuiInterface_Close(object sender, EventArgs e)
        {
            _excelLoader.CloseExcelProtocoConfigFilelLoader();

            //turn off the robot servos.
            //avi-insert//
            _motocomController.SetServoOff();

            //close the connection with the led strip.
            _ledController.CloseConnection();
            _ledController2.CloseConnection();

            //turn off the InfraRed.
            _infraredController.WriteEvent(true, InfraRedStatus.TurnedOff);

            //stop the control loop.
            if (_cntrlLoop != null)
            {
                _cntrlLoop.Stop();
                _cntrlLoop.Dispose();
            }
        }
        #endregion GLOBAL_EVENTS_HANDLE_FUNCTIONS

        #region PROTOCOL_GROUPBOX_FUNCTION
        /// <summary>
        /// Event handler for clicking the protocol browser buttom.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">args.</param>
        private void protocolBrowserBtn_Click(object sender, EventArgs e)
        {
            if (_protocolsFolderBrowser.ShowDialog() == DialogResult.OK)
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
            _selectedProtocolFullName = _protocolsComboBox.SelectedItem.ToString();
            //delete the extension and the additional name from the full name to get the short name.
            _selectedProtocolName = _selectedProtocolFullName.Split('.')[0].Split('-')[0];

            //_protocolsComboBox.SelectedItem = _protocolsComboBox.Items[0];
            SetVariables(_protoclsDirPath + "\\" + _selectedProtocolFullName);
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
        /// Add the files ends with .xlsx extension to the protocol ComboBox.
        /// </summary>
        private void AddFilesToComboBox(ComboBox comboBox, string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                string[] filesEntries = Directory.GetFiles(dirPath);

                foreach (string file in filesEntries)
                {
                    if (Path.GetExtension(file).Equals(".xlsx"))
                    {
                        _protocolsComboBox.Items.Add(Path.GetFileName(file));
                    }
                }

            }

            if (_protocolsComboBox.Items.Count > 0)
            {
                _protocolsComboBox.SelectedItem = _protocolsComboBox.Items[0];
                SetVariables(_protoclsDirPath + "\\" + _protocolsComboBox.Items[0].ToString());
                //that was deleted because it show the variables already in the two lines before.
                //ShowVariablesToGui();
            }
        }

        /// <summary>
        /// Sets the variables in the chosen xslx file and stote them in the class members.
        /// </summary>
        private void SetVariables(string dirPath)
        {
            _excelLoader.ReadProtocolFile(dirPath, ref _variablesList);
        }

        /// <summary>
        /// Save the variables in the gui to an excell sheet parameters file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        private void _btnSaveProtocol_Click(object sender, EventArgs e)
        {
            _excelLoader.WriteProtocolFile(_protoclsDirPath + @"\" + _textboxNewProtocolName.Text.ToString(), _variablesList, _buttonbasesDictionary);
        }
        #endregion PROTOCOL_GROUPBOX_FUNCTION

        #region SELECTED_RAT_GROUPBOX_FUNCTIONS
        /// <summary>
        /// Adding the rat names to the rat names combo box by the configuration settings.
        /// </summary>
        public void AddRatNamesToRatNamesComboBox()
        {
            //add the rat names in the config file to thw combo box.
            foreach (string ratName in Properties.Settings.Default.RatNames)
            {
                _comboBoxSelectedRatName.Items.Add(ratName);
            }
        }

        /// <summary>
        /// Adding the students names to the rat names combo box by the configuration settings.
        /// </summary>
        public void AddStudentsNamesToRatNamesComboBox()
        {
            //add the rat names in the config file to thw combo box.
            foreach (string studentName in Properties.Settings.Default.StudentsName)
            {
                _comboBoxStudentName.Items.Add(studentName);
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
        /// Function handler for changing the student name.
        /// </summary>
        /// <param name="sender">The combobox that has changed.</param>
        /// <param name="e">The args.</param>
        private void _comboBoxStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chenge the selected student name as followed by the combobox.
            //change the selected rat name as followed by the combobox.
            if ((sender as ComboBox).SelectedItem != null)
                _cntrlLoop.StudentName = (sender as ComboBox).SelectedItem.ToString();
            else
                _cntrlLoop.StudentName = "";
        }
        #endregion  

        #region EXPERIMENT_RUNNING_CHANGING_FUNCTION
        /// <summary>
        /// Function handler for starting the experiment tirlas. 
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">args.</param>
        private void _btnStart_Click(object sender, EventArgs e)
        {
            //if everything is o.k start the control loop.
            if (StartLoopStartCheck())
            {
                if (_isEngaged)
                {
                    //check if auto button is checked , and add warning message.
                    if (AutosOptionsSelected())
                    {
                        //if aborted , get out of the start logics function.
                        if (DialogResult.No == MessageBox.Show("An auto options is selected.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            return;
                    }
                    lock (_lockerStopStartButton)
                    {
                        #region ENABLE_DISABLE_BUTTONS
                        _btnStart.Enabled = false;
                        _btnMakeTrials.Enabled = false;
                        _btnStop.Enabled = true;
                        _btnPause.Enabled = true;
                        _btnResume.Enabled = false;
                        _btnPark.Enabled = false;
                        _btnMoveRobotSide.Enabled = false;
                        #endregion

                        //if already running - ignore.
                        if (!Globals._systemState.Equals(SystemState.RUNNING))
                        {
                            //update the system state.
                            Globals._systemState = SystemState.RUNNING;

                            //freeze all dynamic inputs via running.
                            FreezeDynamicsTextBoxes();

                            //add the static variable list of double type values.
                            _staticValuesGenerator.SetVariables(_variablesList);

                            //start the control loop.
                            //need to be changed according to parameters added to which trajectoryname to be called from the excel file.
                            //string trajectoryCreatorName = _variablesList._variablesDictionary["TRAJECTORY_CREATOR"]._description["parameters"]._ratHouseParameter[0];
                            //int trajectoryCreatorNum = int.Parse(_variablesList._variablesDictionary["TRAJECTORY_CREATOR"]._description["parameters"]._ratHouseParameter);
                            ITrajectoryCreator trajectoryCreator = DecideTrajectoryCreatorByProtocolName(_selectedProtocolName);

                            _cntrlLoop.NumOfRepetitions = int.Parse(_numOfRepetitionsTextBox.Text.ToString());
                            _cntrlLoop.NumOfStickOn = int.Parse(_textboxStickOnNumber.Text.ToString());
                            _cntrlLoop.PercentageOfTurnedOnLeds = double.Parse(_textboxPercentageOfTurnOnLeds.Text.ToString());
                            _cntrlLoop.LEDBrightness = int.Parse(_textboxLEDBrightness.Text.ToString());
                            _cntrlLoop.LEDcolorRed = int.Parse(_textBoxLedRedColor.Text.ToString());
                            _cntrlLoop.LEDcolorGreen = int.Parse(_textBoxLedGreenColor.Text.ToString());
                            _cntrlLoop.LEDcolorBlue = int.Parse(_textBoxLedBlueColor.Text.ToString());
                            _cntrlLoop.ProtocolFullName = _selectedProtocolFullName.Split('.')[0];//delete the.xlsx extension from the protocol file name.
                            _cntrlLoop.Start(_variablesList, _acrossVectorValuesGenerator._crossVaryingValsBoth, _staticValuesGenerator._staticVariableList, Properties.Settings.Default.Frequency, trajectoryCreator);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error - Robot is not engaged!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Decide whether one or more of the auto's options are selected.
        /// </summary>
        /// <returns></returns>
        private bool AutosOptionsSelected()
        {
            return (_checkBoxAutoChoice.Checked ||
                    _checkBoxAutoFixation.Checked ||
                    _checkBoxAutoStart.Checked);
        }

        /// <summary>
        /// Check all parameters needed before the control loop execution.
        /// </summary>
        /// <returns>True or false if can execute the control loop.</returns>
        private bool StartLoopStartCheck()
        {
            //if selected rat name is o.k
            if (_comboBoxSelectedRatName.SelectedItem != null && _comboBoxStudentName.SelectedItem != null)
            {
            }
            else
            {
                MessageBox.Show("Error - Should select a rat name before starting!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (int.Parse(_numOfRepetitionsTextBox.Text.ToString()) % int.Parse(_textboxStickOnNumber.Text.ToString()) != 0)
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

            //decide which creator to use depends on the protocol name.
            _acrossVectorValuesGenerator = DecideVaryinVectorsGeneratorByProtocolName();

            //if there ar no errors middwile. Generate crrossvals and run the control loop.
            _acrossVectorValuesGenerator.SetVariablesValues(_variablesList);

            ClearVaryingListBox();

            //make the varyingCrossVals matrix.
            _acrossVectorValuesGenerator.MakeVaryingMatrix();

            //add the crossVaryingVals to the listbox.
            AddVaryingMatrixToVaryingListBox(_acrossVectorValuesGenerator._crossVaryingValsBoth);

            //show the list box controls(add , remove , etc...)
            ShowVaryingControlsOptions(true);

            //show the start button
            _btnStart.Enabled = true;
        }

        /// <summary>
        /// Handler for stop experiment buttom clicked.
        /// </summary>
        /// <param name="sender">The stop buttom object.</param>
        /// <param name="e">The args.</param>
        private void _btnStop_Click(object sender, EventArgs e)
        {
            //update the system state.
            //Globals._systemState = SystemState.STOPPED;
            lock (_lockerStopStartButton)
            {
                //stop the control loop.
                _cntrlLoop.Stop();

                //retun back from the textboxes freeze during the running.
                ReturnBackFromFreezeDynamicTextBoxes();

                #region ENABLE_DISABLE_BUTTONS
                _btnStop.Enabled = false;
                _btnStart.Enabled = true;
                _btnPause.Enabled = false;
                _btnResume.Enabled = false;
                _btnPark.Enabled = true;
                _btnMoveRobotSide.Enabled = true;
                #endregion
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
                #region ENABLE_DISABLE_BUTTONS
                _btnPause.Enabled = false;
                _btnResume.Enabled = true;
                _btnPark.Enabled = true;
                _btnEnagae.Enabled = false;
                #endregion

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
                #region ENABLE_DISABLE_BUTTONS
                _btnPause.Enabled = true;
                _btnResume.Enabled = false;
                _btnPark.Enabled = false;
                _btnEnagae.Enabled = false;
                _btnStop.Enabled = true;
                _btnMoveRobotSide.Enabled = false;
                #endregion

                Globals._systemState = SystemState.RUNNING;
                _cntrlLoop.Resume();
            }
        }

        /// <summary>
        /// Function handler for parking the robot.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Args.</param>
        private void _btnPark_Click(object sender, EventArgs e)
        {
            lock (_lockerPauseResumeButton)
            {
                lock (_lockerPauseResumeButton)
                {
                    #region DISABLE_BUTTONS
                    bool isBtnStartEnabled = _btnStart.Enabled;
                    bool isBtnStopEnabled = _btnStop.Enabled;
                    bool isBtnPauseEnabled = _btnPause.Enabled;

                    _btnStart.Enabled = false;
                    _btnStop.Enabled = false;
                    _btnResume.Enabled = false;
                    _btnPause.Enabled = false;
                    _btnEnagae.Enabled = false;
                    _btnPark.Enabled = false;
                    _btnMoveRobotSide.Enabled = false;
                    #endregion

                    //set robot servo on and go homeposition.
                    _motocomController.SetServoOn();

                    string checkerParkPosition = CheckBothRobotsAtParkPosition(MotocomSettings.Default.DeltaParkToPark);
                    string checkerEngagePosition = CheckBothRobotAroundEngagePosition(MotocomSettings.Default.DeltaEngageToPark);
                    string checkerAsidePosition = CheckBothRobotAroundASidePosition(MotocomSettings.Default.DeltaASideToPark);

                    if (checkerParkPosition.Equals(string.Empty) || checkerEngagePosition.Equals(string.Empty) || checkerAsidePosition.Equals(string.Empty))
                    {

                        _motocomController.WriteParkPositionFile();
                        _motocomController.MoveRobotParkPosition();

                        _motocomController.WaitJobFinished();

                        _isEngaged = false;
                    }
                    else
                    {
                        MessageBox.Show(checkerParkPosition + "\n" + checkerEngagePosition, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    _motocomController.SetServoOff();

                    #region ENABLE_BUTTONS_BACK
                    _btnStart.Enabled = isBtnStartEnabled;
                    _btnStop.Enabled = isBtnStopEnabled;
                    _btnResume.Enabled = false;
                    _btnPause.Enabled = isBtnPauseEnabled;
                    _btnEnagae.Enabled = true;
                    _btnPark.Enabled = true;
                    _btnMoveRobotSide.Enabled = true;
                    #endregion
                }
            }

        }

        /// <summary>
        /// Handler for Engaging the rovot to it's start point.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Args.</param>
        private void _btnEnagae_Click(object sender, EventArgs e)
        {
            lock (_lockerStopStartButton)
            {
                lock (_lockerPauseResumeButton)
                {
                    #region DISABLE_BUTTONS
                    bool isBtnStartEnabled = _btnStart.Enabled;
                    bool isBtnStopEnabled = _btnStop.Enabled;
                    bool isBtnPauseEnabled = _btnPause.Enabled;

                    _btnStart.Enabled = false;
                    _btnStop.Enabled = false;
                    _btnResume.Enabled = false;
                    _btnPause.Enabled = false;
                    _btnEnagae.Enabled = false;
                    _btnPark.Enabled = false;
                    _btnMoveRobotSide.Enabled = false;
                    #endregion

                    try
                    {
                        //set robot servo on and go homeposition.
                        _motocomController.SetServoOn();
                    }
                    catch
                    {
                        MessageBox.Show("Cannot set the servos on - check if robot is conncted in play mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string checkerParkPosition = CheckBothRobotsAtParkPosition(MotocomSettings.Default.DeltaParkToEngage);
                    string checkerEngagePosition = CheckBothRobotAroundEngagePosition(MotocomSettings.Default.DeltaEngageToEngage);

                    if (checkerParkPosition.Equals(string.Empty) || checkerEngagePosition.Equals(string.Empty))
                    {
                        _motocomController.WriteHomePosFile();
                        _motocomController.MoveRobotHomePosition();

                        _motocomController.WaitJobFinished();

                        _isEngaged = true;
                    }
                    else
                    {
                        MessageBox.Show(checkerParkPosition + "\n" + checkerEngagePosition, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    #region ENABLE_BUTTONS_BACK
                    _btnStart.Enabled = isBtnStartEnabled;
                    _btnStop.Enabled = isBtnStopEnabled;
                    //if paused and then parked and engaged in the middle of the experiment.
                    if(_isEngaged && !_btnStop.Enabled && !_btnStart.Enabled) _btnResume.Enabled = true;
                    _btnPause.Enabled = isBtnPauseEnabled;
                    _btnEnagae.Enabled = true;
                    _btnPark.Enabled = true;
                    #endregion
                }
            }
        }

        /// <summary>
        /// Moves the R1 robot aside.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnMoveRobotSide_Click(object sender, EventArgs e)
        {
            #region DISABLE_ENABLE_BUTTONS
            _btnStart.Enabled = false;
            _btnStop.Enabled = false;
            _btnResume.Enabled = false;
            _btnPause.Enabled = false;
            _btnEnagae.Enabled = false;
            _btnPark.Enabled = false;
            _btnMoveRobotSide.Enabled = false;
            #endregion DISABLE_ENABLE_BUTTONS

            try
            {
                //set robot servo on and go homeposition.
                _motocomController.SetServoOn();
            }
            catch
            {
                MessageBox.Show("Cannot set the servos on - check if robot is conncted in play mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _motocomController.WriteASidePositionFile();;
            _motocomController.MoveRobotASidePosition();

            _motocomController.WaitJobFinished();

            try
            {
                //set robot servo on and go homeposition.
                _motocomController.SetServoOff();
            }
            catch
            {
                MessageBox.Show("Cannot set the servos off - check if robot is conncted in play mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #region DISABLE_ENABLE_BUTTONS
            _btnPark.Enabled = true;
            #endregion DISABLE_ENABLE_BUTTONS
        }
        #endregion

        #region CHECK_ROBOTS_POSITION_FUNCTIONS
        /// <summary>
        /// Check the both robots exactly at threre park position.
        /// </summary>
        /// <returns></returns>
        private string CheckBothRobotsAtParkPosition(double delta)
        {
            return CheckBothRobotsAroundDeltaParkPosition(delta);
        }

        /// <summary>
        /// Check the both robots arounf the park poistion.
        /// </summary>
        /// <returns></returns>
        private string CheckBothRobotsAroundParkPosition(double delta)
        {
            return CheckBothRobotsAroundDeltaParkPosition(delta);
        }

        /// <summary>
        /// Check the robots position around delta from there park position.
        /// </summary>
        /// <param name="delta">The delta can be accurated to the park position.</param>
        /// <returns>Empty string if close enough to the position in delta mm , , otherwise the distance string.</returns>
        private string CheckBothRobotsAroundDeltaParkPosition(double delta)
        {
            _motocomController.SetRobotControlGroup(1);

            double[] robot1Pos = _motocomController.GetRobotPlace();

            //when checking that robot position is close to engage for park or park for engage, if delta is small, allow x to be large (it is along the engage-park line).
            bool robot1PosInPark =
                (Math.Abs(robot1Pos[0] - (MotocomSettings.Default.R1OriginalX - MotocomSettings.Default.ParkingBackwordDistance)) < delta || delta <= 10) &&
                Math.Abs(robot1Pos[1] - MotocomSettings.Default.R1OriginalY) < delta &&
                Math.Abs(robot1Pos[2] - MotocomSettings.Default.R1OriginalZ) < delta;

            _motocomController.SetRobotControlGroup(2);

            double[] robot2Pos = _motocomController.GetRobotPlace();

            bool robot2PosInPark =
                (Math.Abs(robot2Pos[0] - MotocomSettings.Default.R2OriginalX) < delta || delta <= 10) &&
                Math.Abs(robot2Pos[1] - MotocomSettings.Default.R2OriginalY) < delta &&
                Math.Abs(robot2Pos[2] - MotocomSettings.Default.R2OriginalZ) < delta;

            string message = "Robot is out of Range.\nMove manually to < " + delta.ToString() + "mm of the Park position. Current location from Park:\n" +
                "R1XDelta = " + (robot1Pos[0] - (MotocomSettings.Default.R1OriginalX - MotocomSettings.Default.ParkingBackwordDistance)).ToString("0.00") + "mm\n" +
                "R1YDelta = " + (robot1Pos[1] - MotocomSettings.Default.R1OriginalY).ToString("0.00") + "mm\n" +
                "R1ZDelta = " + (robot1Pos[2] - MotocomSettings.Default.R1OriginalZ).ToString("0.00") + "mm\n" +
                "R2XDelta = " + (robot2Pos[0] - MotocomSettings.Default.R2OriginalX).ToString("0.00") + "mm\n" +
                "R2YDelta = " + (robot2Pos[1] - MotocomSettings.Default.R2OriginalY).ToString("0.00") + "mm\n" +
                "R2ZDelta = " + (robot2Pos[2] - MotocomSettings.Default.R2OriginalZ).ToString("0.00") + "mm";

            return (robot1PosInPark && robot2PosInPark) ? (string.Empty) : (message);
        }

        /// <summary>
        /// Check if robot1 is in x backward position from it's parking position.
        /// </summary>
        /// <returns></returns>
        private bool Checkrobot1BackwardParkingPosition()
        {
            _motocomController.SetRobotControlGroup(1);

            double[] robot1Pos = _motocomController.GetRobotPlace();

            return robot1Pos[0] - (MotocomSettings.Default.R1OriginalX - 500) < 0;
        }

        /// <summary>
        /// Check both robots exactly at there enagae position.
        /// </summary>
        /// <returns></returns>
        private string CheckBothRobotsAtEngagePosition(double delta)
        {
            return CheckBothRobotAroundDeltaEngagePosition(delta);
        }

        /// <summary>
        /// Check the both robots around the engage position.
        /// </summary>
        /// <returns></returns>
        private string CheckBothRobotAroundEngagePosition(double delta)
        {
            return CheckBothRobotAroundDeltaEngagePosition(delta);
        }

        /// <summary>
        /// Check the both robots around the Aside position.
        /// </summary>
        /// <returns></returns>
        private string CheckBothRobotAroundASidePosition(double delta)
        {
            return CheckBothRobotAroundDeltaASidePosition(delta);
        }

        /// <summary>
        /// Check the both robots arounf delta from the engage position.
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        private string CheckBothRobotAroundDeltaEngagePosition(double delta)
        {
            _motocomController.SetRobotControlGroup(1);

            double[] robot1Pos = _motocomController.GetRobotPlace();

            //when checking that robot position is close to engage for park or park for engage, if delta is small, allow x to be large (it is along the engage-park line).
            bool robot1PosInEngage = (Math.Abs(robot1Pos[0] - MotocomSettings.Default.R1OriginalX) < delta || delta <= 10) &&
                Math.Abs(robot1Pos[1] - MotocomSettings.Default.R1OriginalY) < delta &&
                Math.Abs(robot1Pos[2] - MotocomSettings.Default.R1OriginalZ) < delta;

            _motocomController.SetRobotControlGroup(2);

            double[] robot2Pos = _motocomController.GetRobotPlace();

            bool robot2PosInEngage = (Math.Abs(robot2Pos[0] - MotocomSettings.Default.R2OriginalX) < delta || delta <= 10) &&
                Math.Abs(robot2Pos[1] - MotocomSettings.Default.R2OriginalY) < delta &&
                Math.Abs(robot2Pos[2] - MotocomSettings.Default.R2OriginalZ) < delta;

            string message = "\nOR\nMove manually to < " + delta + "mm of the Engage position. Current location from Engage:\n" +
                "R1XDelta = " + (robot1Pos[0] - MotocomSettings.Default.R1OriginalX).ToString("0.00") + "mm\n" +
                "R1YDelta = " + (robot1Pos[1] - MotocomSettings.Default.R1OriginalY).ToString("0.00") + "mm\n" +
                "R1ZDelta = " + (robot1Pos[2] - MotocomSettings.Default.R1OriginalZ).ToString("0.00") + "mm\n" +
                "R2XDelta = " + (robot2Pos[0] - MotocomSettings.Default.R2OriginalX).ToString("0.00") + "mm\n" +
                "R2YDelta = " + (robot2Pos[1] - MotocomSettings.Default.R2OriginalY).ToString("0.00") + "mm\n" +
                "R2ZDelta = " + (robot2Pos[2] - MotocomSettings.Default.R2OriginalZ).ToString("0.00") + "mm";

            return (robot1PosInEngage && robot2PosInEngage) ? (string.Empty) : (message);
        }

        /// <summary>
        /// Check the both robots around delta from the ASide position.
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        private string CheckBothRobotAroundDeltaASidePosition(double delta)
        {
            _motocomController.SetRobotControlGroup(1);

            double[] robot1Pos = _motocomController.GetRobotPlace();

            //when checking that robot position is close to engage for park or park for engage, if delta is small, allow x to be large (it is along the engage-park line).
            bool robot1PosInEngage = (Math.Abs(robot1Pos[0] - (MotocomSettings.Default.R1ASideX)) < delta || delta <= 10) &&
                                     Math.Abs(robot1Pos[1] - MotocomSettings.Default.R1ASideY) < delta &&
                                     Math.Abs(robot1Pos[2] - MotocomSettings.Default.R1ASideZ) < delta;

            _motocomController.SetRobotControlGroup(2);

            double[] robot2Pos = _motocomController.GetRobotPlace();

            bool robot2PosInEngage = (Math.Abs(robot2Pos[0] - MotocomSettings.Default.R2OriginalX) < delta || delta <= 10) &&
                                     Math.Abs(robot2Pos[1] - MotocomSettings.Default.R2OriginalY) < delta &&
                                     Math.Abs(robot2Pos[2] - MotocomSettings.Default.R2OriginalZ) < delta;

            string message = "Move manually to < " + delta + "mm of the Aside position. Current location from Aside:\n" +
                             "R1XDelta = " + (robot1Pos[0] - MotocomSettings.Default.R1ASideX).ToString("0.00") + "mm\n" +
                             "R1YDelta = " + (robot1Pos[1] - MotocomSettings.Default.R1ASideY).ToString("0.00") + "mm\n" +
                             "R1ZDelta = " + (robot1Pos[2] - MotocomSettings.Default.R1ASideZ).ToString("0.00") + "mm\n" +
                             "R2XDelta = " + (robot2Pos[0] - MotocomSettings.Default.R1ASideRX).ToString("0.00") + "mm\n" +
                             "R2YDelta = " + (robot2Pos[1] - MotocomSettings.Default.R1ASideRX).ToString("0.00") + "mm\n" +
                             "R2ZDelta = " + (robot2Pos[2] - MotocomSettings.Default.R1ASideRZ).ToString("0.00") + "mm";

            return (robot1PosInEngage && robot2PosInEngage) ? (string.Empty) : (message);
        }
        #endregion CHECK_ROBOTS_POSITION_FUNCTIONS

        #region VARYING_LISTBOX_FUNCTIONS
        /// <summary>
        /// Adding the generated cross varying values to the varying listbox.
        /// </summary>
        /// <param name="varyingCrossValsBoth">The cross genereated varying values to add to the listbox.</param>
        private void AddVaryingMatrixToVaryingListBox(List<Dictionary<string, double>> varyingCrossValsBoth)
        {
            //collect the titles for the listbox columns to a list.
            string listBoxTitleLineText = "";
            List<string> niceNameList = new List<string>();
            foreach (string varName in varyingCrossValsBoth.ElementAt(0).Keys)
            {
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter;
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



            //add all varying cross value in new line in the textbox.
            int index = 0;

            foreach (Dictionary<string, double> varRowDictionaryItem in varyingCrossValsBoth)
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
            List<Dictionary<string, double>> crossVaryingVals = _acrossVectorValuesGenerator._crossVaryingValsBoth;

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
        private Form ShowControlAddingVaryingForm(List<Dictionary<string, double>> crossVaryingVals, Dictionary<string, TextBox> varNameToTextboxDictionary)
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
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter;

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
        /// The value is for the value of that variable include ratHouseParameter
        /// </param>
        private void AddNewVaryngCombination(Dictionary<string, string> varNameToValueDictionary)
        {
            Dictionary<string, double> varNameToValueDictionaryDoubleListVersion = new Dictionary<string, double>();
            foreach (string varName in varNameToValueDictionary.Keys)
            {
                varNameToValueDictionaryDoubleListVersion.Add(varName, double.Parse(varNameToValueDictionary[varName]));
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

        /// <summary>
        /// Function handler for changing the variable from the Gui according to the textboxes input when leaving the textbox.
        /// </summary>
        /// <param name="sender">The textbox sender object have been changed.</param>
        /// <param name="e">args.</param>
        /// <param name="varName">The variable name in the variables dictionary to update according to the textbox.</param>
        private void VariableTextBox_TextBoxLeaved(object sender, EventArgs e, string varName, string varAttibuteName)
        {
            TextBox tb = sender as TextBox;

            CheckProperInputSpelling(tb.Text, varName, varAttibuteName);

            //if a left textbox updated and need to equalize the right checkbox also.
            UpdateRightTextBoxesAvailability(_checkBoxRightAndLeftSame.Checked);
        }

        /// <summary>
        /// Function handler for status variable combobox changed.
        /// </summary>
        /// <param name="sender">The combobox object that was changed.</param>
        /// <param name="e">The args.</param>
        /// <param name="varName">The var name it's combobox changed.</param>
        private void statusCombo_SelectedIndexChanged(object sender, EventArgs e, string varName)
        {
            ComboBox cb = sender as ComboBox;
            string selectedIndex = "";

            //decide which index in the status list was selected.
            selectedIndex = StatusIndexByNameDecoder(cb.SelectedItem.ToString());

            //update the status in the variables dictionary.
            _variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter = selectedIndex;

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
                if (!attribute.Equals("status"))
                {
                    if (_dynamicAllocatedTextBoxes.ContainsKey(varName + attribute))
                        FreezeTextBoxAccordingToStatus((TextBox)_dynamicAllocatedTextBoxes[varName + attribute], varName, attribute.Equals("parameters"));
                }
            }
            #endregion TEXTBOXES_FREEZING_NEW_STATUS

            //change the parametes attribute textbox for the changed status variable.
            #region PRAMETERS_TEXTBOX_CHANGE_TEXT_SHOW
            SetParametersTextBox(varName, new StringBuilder());
            #endregion PRAMETERS_TEXTBOX_CHANGE_TEXT_SHOW
        }

        /// <summary>
        /// Handle event if the number of repetitions for the experiments changed dynamically.
        /// </summary>
        /// <param name="sender">The textbox sendr.</param>
        /// <param name="e">The args</param>
        private void _numOfRepetitionsTextBox_TextChanged(object sender, EventArgs e)
        {
            _cntrlLoop.NumOfRepetitions = int.Parse((sender as TextBox).Text.ToString());
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
            if (_checkBoxAutoChoice.Checked)
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
            if (_checkBoxAutoFixation.Checked)
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
            if (_checkBoxAutoStart.Checked)
                _cntrlLoop.AutoStart = true;
            else
                _cntrlLoop.AutoStart = false;
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
            if (_checkBoxFixationOnly.Checked)
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
            if (_checkBoxBreakFixationSoundEnable.Checked)
                _cntrlLoop.EnableFixationBreakSound = true;
            else
                _cntrlLoop.EnableFixationBreakSound = false;
        }

        /// <summary>
        /// Event for changing the AutoRewardSound status.
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The param.</param>
        private void _checkboxCenterRewardSound_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBoxCenterRewardSound.Checked)
                _cntrlLoop.CenterRewardSound = true;
            else
                _cntrlLoop.CenterRewardSound = false;
        }

        private void _checkboxSideRewardSound_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkboxSideRewardSound.Checked)
                _cntrlLoop.SideRewardSound = true;
            else
                _cntrlLoop.SideRewardSound = false;
        }

        /// <summary>
        /// Handler for event turnning on/off the SecondResponseChance checkbox.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Args.</param>
        private void _checkboxSecondResponseChance_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.SecondResponseChance = (sender as CheckBox).Checked;
        }

        /// <summary>
        /// Handler for event turnning on/off the SoundOn for error choice.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Args.</param>
        private void _checkboxErrorSoundOn_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.EnableErrorSound = (sender as CheckBox).Checked;
        }

        /// <summary>
        /// Handler for event turnning on/off the clue the rat get afet first reward.
        /// </summary>
        /// <param name="sender">The sender checkbox.</param>
        /// <param name="e">The args.</param>
        private void _tbEnableGoClueSound_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.EnableGoCueSound = (sender as CheckBox).Checked;

            if ((sender as CheckBox).Checked)
            {
                _radiobuttonGoCueBothSide.Enabled = true;
                _radiobuttonGoCueCorrectSide.Enabled = true;
            }
            else
            {
                _radiobuttonGoCueBothSide.Enabled = false;
                _radiobuttonGoCueCorrectSide.Enabled = false;
            }
        }

        /// <summary>
        /// Handler for event turnning on/off the clue both side option the rat get afet first reward.
        /// </summary>
        /// <param name="sender">The sender checkbox.</param>
        /// <param name="e">The args.</param>
        private void _radiobuttonGoCueBothSide_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.EnableCueSoundInBothSide = (sender as RadioButton).Checked;
        }

        /// <summary>
        /// Handler for event turnning on/off the clue correct side option the rat get afet first reward.
        /// </summary>
        /// <param name="sender">The sender checkbox.</param>
        /// <param name="e">The args.</param>
        private void _radiobuttonGoCueCorrectSide_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.EnableCueSoundCorrectSide = (sender as RadioButton).Checked;
        }

        /// <summary>
        /// Handler for event turnning on/off the right + left parameters must be the same.
        /// </summary>
        /// <param name="sender">The sender CheckBox.</param>
        /// <param name="e">The args.</param>
        private void _checkBoxRightAndLeftSame_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.EnableRightLeftMustEquals = (sender as CheckBox).Checked;

            //make the textboxes for all pairs of right and left to be the same (disable the right textboxes and make thier values the same as the left textboxes).
            UpdateRightTextBoxesAvailability((sender as CheckBox).Checked);
        }

        /// <summary>
        /// Handles a changed in the state of RRDelta,
        /// </summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">Args.</param>
        private void _checkbixRRDelta_CheckedChanged(object sender, EventArgs e)
        {
            _cntrlLoop.EnableRRDelta = (sender as CheckBox).Checked;
        }

        /// <summary>
        /// Update and disabled all Right textboxes according to the equalization of the right textboxes and the left checkboxes.
        /// </summary>
        /// <param name="equals">If right checkboxes should equal the left checkboxes.</param>
        public void UpdateRightTextBoxesAvailability(bool equals)
        {
            UpdateRightCheckBoxAvailability("REWARD_RIGHT_DELAY", "REWARD_LEFT_DELAY", equals);
            UpdateRightCheckBoxAvailability("REWARD_RIGHT_DURATION", "REWARD_LEFT_DURATION", equals);
            UpdateRightCheckBoxAvailability("REWARD_RIGHT_DELAY_SC", "REWARD_LEFT_DELAY_SC", equals);
            UpdateRightCheckBoxAvailability("REWARD_RIGHT_DURATION_SC", "REWARD_LEFT_DURATION_SC", equals);
            UpdateRightCheckBoxAvailability("COHERENCE_RIGHT_STRIP", "COHERENCE_LEFT_STRIP", equals);
            UpdateRightCheckBoxAvailability("FLICKER_RIGHT", "FLICKER_LEFT", equals);
        }

        /// <summary>
        /// Updates the cue radio buttons disable/enable status according to the cue checkbox status.
        /// </summary>
        public void UpdateCueGroupRadioButtons()
        {
            if ((_buttonbasesDictionary["GO_CUE_SOUND"] as CheckBox).Checked)
            {
                (_buttonbasesDictionary["CORRECT_CUE_SOUND"] as RadioButton).Enabled = true;
                (_buttonbasesDictionary["BOTH_SIDE_CUE_SOUND"] as RadioButton).Enabled = true;
            }
            else
            {
                (_buttonbasesDictionary["CORRECT_CUE_SOUND"] as RadioButton).Enabled = false;
                (_buttonbasesDictionary["BOTH_SIDE_CUE_SOUND"] as RadioButton).Enabled = false;
            }
        }

        /// <summary>
        /// Update a right textboxe with the given name to be equals and disabled according to equals parameter.
        /// </summary>
        /// <param name="checkboxRightName">The right textbox to be disabled and equaled to the left textbox.</param>
        /// <param name="cehckBoxLeftName">The left textbox to be equals to.</param>
        /// <param name="equals">Inducate if equals and disable or to enable.</param>
        public void UpdateRightCheckBoxAvailability(string checkboxRightName, string cehckBoxLeftName, bool equals)
        {
            if (_dynamicAllocatedTextBoxes.Keys.Contains(checkboxRightName + "parameters"))
            {
                if (equals)
                {
                    _dynamicAllocatedTextBoxes[checkboxRightName + "parameters"].Enabled = false;
                    _dynamicAllocatedTextBoxes[checkboxRightName + "parameters"].Text = _dynamicAllocatedTextBoxes[cehckBoxLeftName + "parameters"].Text;
                    _variablesList._variablesDictionary[checkboxRightName]._description["parameters"]._ratHouseParameter = _variablesList._variablesDictionary[cehckBoxLeftName]._description["parameters"]._ratHouseParameter;
                }
                else
                {
                    _dynamicAllocatedTextBoxes[checkboxRightName + "parameters"].Enabled = true;
                }
            }
        }

        #endregion MODES

        #region PARAMETERS_GROUPBOXFUNCTIONS
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

            //add the labels title for each column.
            AddVariablesLabelsTitles(ref top, left, width, height, eachDistance);

            //filter only the variables where the status is not -1 (not for the checkboxes for the gui).
            foreach (string varName in _variablesList._variablesDictionary.Keys.Where(name => int.Parse(_variablesList._variablesDictionary[name]._description["status"]._ratHouseParameter) != -1))
            {
                ShowVariableLabel(_variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter,
                    top,
                    left,
                    width + 40,
                    height,
                    eachDistance,
                    _variablesList._variablesDictionary[varName]._description["tool_tip"]._ratHouseParameter);

                ShowVariableAttributes(varName,
                    top,
                    left,
                    width,
                    height,
                    eachDistance,
                    750,
                    _variablesList._variablesDictionary[varName]._description["color"]._ratHouseParameter);

                top += 35;
            }

            //reset checkboxes statuses before matching them to the protocol file.
            foreach (ButtonBase item in _buttonbasesDictionary.Values)
            {
                if (item is CheckBox)
                {
                    (item as CheckBox).Checked = false;
                }
                else if (item is RadioButton)
                {
                    (item as RadioButton).Checked = false;
                    (item as RadioButton).Enabled = false;
                }

                //todo: add exception if not of these types
            }

            //filter only the variables where the status is  -1 (for the checkboxes for the gui).
            foreach (string varName in _variablesList._variablesDictionary.Keys.Where(name => int.Parse(_variablesList._variablesDictionary[name]._description["status"]._ratHouseParameter) == -1))
            {
                if (_buttonbasesDictionary[varName] is RadioButton)
                {
                    (_buttonbasesDictionary[varName] as RadioButton).Checked = false;

                    if (int.Parse(_variablesList._variablesDictionary[varName]._description["parameters"]
                            ._ratHouseParameter) == 1)
                    {
                        (_buttonbasesDictionary[varName] as RadioButton).Checked = true;
                    }
                }
                else if (_buttonbasesDictionary[varName] is CheckBox)
                {
                    (_buttonbasesDictionary[varName] as CheckBox).Checked = false;

                    if (int.Parse(_variablesList._variablesDictionary[varName]._description["parameters"]
                            ._ratHouseParameter) == 1)
                    {
                        (_buttonbasesDictionary[varName] as CheckBox).Checked = true;
                    }
                }
            }

            //update if right equals to left according to the checkbox status.
            UpdateRightTextBoxesAvailability(_checkBoxRightAndLeftSame.Checked);
            UpdateCueGroupRadioButtons();
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
        public void ShowVariableLabel(string varName, int top, int left, int width, int height, int eachDistance, string toolTipString = "")
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
        /// <param name="color">The color to be in the background of wach textbox.</param>
        private void ShowVariableAttributes(string varName, int top, int left, int width, int height, int eachDistance, int offset, string color)
        {
            //string builder for making the text to show to the gui.
            StringBuilder sBuilder = new StringBuilder();

            #region STATUS_COMBOBOX
            //add the status ComboBox.
            ComboBox statusCombo = new ComboBox();
            statusCombo.Left = left + offset;
            statusCombo.Top = top;
            statusCombo.Items.Add("Const");
            statusCombo.Items.Add("Static");
            statusCombo.Items.Add("Varying");
            statusCombo.Items.Add("AcrossStair");
            statusCombo.Items.Add("WithinStair");
            statusCombo.Items.Add("Random");
            statusCombo.Items.Add("Vector");

            //Handle event when a status of a variable is changed.
            statusCombo.SelectedIndexChanged += new EventHandler((sender, args) => statusCombo_SelectedIndexChanged(sender, args, varName));

            //decide which items on the ComboBox is selected according to the data in the excel sheet.
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter)
            {
                case "0":
                    statusCombo.SelectedText = "Const";
                    break;

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
                case "6":
                    statusCombo.SelectedText = "Vector";
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
            //add the color to the textbox
            incrementTextBox.BackColor = Color.FromName(color);

            //function to change the variable list dictionary according to changes when leave the textbox.
            incrementTextBox.LostFocus += new EventHandler((sender, e) => VariableTextBox_TextBoxLeaved(sender, e, varName, "increament"));

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(incrementTextBox, varName, false);

            //show the _ratHouseParameter.
            string lowBoundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);
            incrementTextBox.Text = lowBoundTextVal;

            _dynamicParametersPanel.Controls.Add(incrementTextBox);
            _dynamicAllocatedTextBoxes.Add(varName + "increament", incrementTextBox);
            #endregion INCREMENT_TEXTBOX

            offset -= eachDistance;

            #region HIGHBOUND_TEXTBOX
            //add the low bound textbox.
            TextBox highBoundTextBox = new TextBox();
            highBoundTextBox.Left = offset;
            highBoundTextBox.Top = top;
            highBoundTextBox.Width = width;
            //add the colr to the textbox
            highBoundTextBox.BackColor = Color.FromName(color);

            //function to change the variable list dictionary according to changes when leave the textbox.
            highBoundTextBox.LostFocus += new EventHandler((sender, e) => VariableTextBox_TextBoxLeaved(sender, e, varName, "high_bound"));

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(highBoundTextBox, varName, false);

            //show the _ratHouseParameter.
            string highBoundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
            highBoundTextBox.Text = highBoundTextVal;

            _dynamicParametersPanel.Controls.Add(highBoundTextBox);
            this._dynamicAllocatedTextBoxes.Add(varName + "high_bound", highBoundTextBox);
            #endregion HIGHBOUND_TEXTBOX

            offset -= eachDistance;

            #region LOWBOUND_TEXTBOX
            //add the low bound textbox.
            TextBox lowBoundTextBox = new TextBox();
            lowBoundTextBox.Left = offset;
            lowBoundTextBox.Top = top;
            lowBoundTextBox.Width = width;
            //add the color to the textbox
            lowBoundTextBox.BackColor = Color.FromName(color);

            //function to change the variable list dictionary according to changes when leave the textbox.
            lowBoundTextBox.LostFocus += new EventHandler((sender, e) => VariableTextBox_TextBoxLeaved(sender, e, varName, "low_bound"));

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(lowBoundTextBox, varName, false);

            //show the _ratHouseParameter.
            lowBoundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
            lowBoundTextBox.Text = lowBoundTextVal;

            _dynamicParametersPanel.Controls.Add(lowBoundTextBox);
            _dynamicAllocatedTextBoxes.Add(varName + "low_bound", lowBoundTextBox);
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
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter)
            {
                case "0":   //const
                case "1":   //static
                case "6":   //vector
                    //show the _ratHouseParameter.
                    parametersTextBox.Text = string.Join(",", _variablesList._variablesDictionary[varName]._description["parameters"]._ratHouseParameter); ;
                    break;

                case "2":   //varying
                case "3":   //acrossstair
                case "4":   //withinstair
                case "5":
                    //show the _ratHouseParameter.
                    string lowboundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                    string highboundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                    string increasingTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);

                    parametersTextBox.Text = ThreeStagesRepresentation(sBuilder, lowboundTextVal, increasingTextVal, highboundTextVal);
                    break;

            }

            //add the color to the texctbox
            parametersTextBox.BackColor = Color.FromName(color);

            _dynamicParametersPanel.Controls.Add(parametersTextBox);
            _dynamicAllocatedTextBoxes.Add(varName + "parameters", parametersTextBox);

            //freezing the textbox according to the status
            FreezeTextBoxAccordingToStatus(parametersTextBox, varName, true);

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
        /// Initializes the checkboxes dictionary with names as key with the control as value.
        /// </summary>
        private void InitializeCheckBoxesDictionary()
        {
            _buttonbasesDictionary = new Dictionary<string, ButtonBase>();

            _buttonbasesDictionary.Add("AUTO_FIXATION", _checkBoxAutoFixation);
            _buttonbasesDictionary.Add("CENTER_REWARD_SOUND", _checkBoxCenterRewardSound);
            _buttonbasesDictionary.Add("SIDE_REWARD_SOUND", _checkboxSideRewardSound);
            _buttonbasesDictionary.Add("AUTO_START", _checkBoxAutoStart);
            _buttonbasesDictionary.Add("AUTO_CHOICE", _checkBoxAutoChoice);
            _buttonbasesDictionary.Add("B.F_SOUND_ON", _checkBoxBreakFixationSoundEnable);
            _buttonbasesDictionary.Add("SEC_RESP_CHANCE", _checkboxSecondResponseChance);
            _buttonbasesDictionary.Add("GO_CUE_SOUND", _checkBoxEnableGoCue);
            _buttonbasesDictionary.Add("CORRECT_CUE_SOUND", _radiobuttonGoCueCorrectSide);
            _buttonbasesDictionary.Add("BOTH_SIDE_CUE_SOUND", _radiobuttonGoCueBothSide);
            _buttonbasesDictionary.Add("ERROR_SOUND", _checkboxErrorSoundOn);
            _buttonbasesDictionary.Add("FIXATION_ONLY", _checkBoxFixationOnly);
            _buttonbasesDictionary.Add("RIGHT_LEFT_PARAMETERS_EQUALS", _checkBoxRightAndLeftSame);
            _buttonbasesDictionary.Add("RR_DELTA", _checkboxRRDelta);
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
        private void AddVariablesLabelsTitles(ref int top, int left, int width, int height, int eachDistance)
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
        private string BracketsAppender(StringBuilder sBuilder, string partA, string partB)
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
            for (int i = 0; i < splittedList.Count; i++)
            {
                splittedList[i] = splittedList.ElementAt(i).Substring(0, splittedList.ElementAt(i).Length - 1);
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
        private string ThreeStagesRepresentation(StringBuilder sBuilder, string lowPart, string increasingPart, string highPart)
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
        /// <param name="parametersTextbox">Is the textbox describe a parameters attribute textbox or vector attribute or const attribute values. </param>
        private void FreezeTextBoxAccordingToStatus(TextBox textBox, string varName, bool parametersTextbox)
        {
            //if const disabled textbox and break no matter what.
            if (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter == "0")
            {
                textBox.Enabled = false;
                return;
            }

            //decide which items on the ComboBox is selected according to the data in the excel sheet.
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter)
            {
                case "1":
                case "6":
                    textBox.Enabled = false;
                    break;

                case "2":
                case "3":
                case "4":
                    textBox.Enabled = true;
                    break;
            }

            //reverse the result.
            if (parametersTextbox)
            {
                textBox.Enabled = !textBox.Enabled;
            }
        }

        /// <summary>
        /// Freezes all dyynamics allocated textboxes.
        /// </summary>
        public void FreezeDynamicsTextBoxes()
        {
            foreach (KeyValuePair<string ,Control> dynamicControlPair in _dynamicAllocatedTextBoxes)
            {
                if (dynamicControlPair.Value is TextBox)
                {
                    if (_dynamicAllocatexTextboxesEnabledStatusBeforeFreeze.Keys.Contains(dynamicControlPair.Key))
                    {
                        _dynamicAllocatexTextboxesEnabledStatusBeforeFreeze[dynamicControlPair.Key] = dynamicControlPair.Value.Enabled;
                    }
                    else
                    {
                        _dynamicAllocatexTextboxesEnabledStatusBeforeFreeze.Add(dynamicControlPair.Key , dynamicControlPair.Value.Enabled);
                    }

                    dynamicControlPair.Value.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Retuens back vefore the freeze of the dynamic controls textboxes.
        /// </summary>
        public void ReturnBackFromFreezeDynamicTextBoxes()
        {
            foreach (KeyValuePair<string, Control> dynamicControlPair in _dynamicAllocatedTextBoxes)
            {
                if (dynamicControlPair.Value is TextBox)
                {
                    if (_dynamicAllocatexTextboxesEnabledStatusBeforeFreeze[dynamicControlPair.Key])
                    {
                        dynamicControlPair.Value.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the input spelling to be proper and update the dictionary according to that.
        /// </summary>
        /// <param name="attributeValue">The attribute value to check.</param>
        /// <param name="varName">The var name to attributed updated according to the new value if the input is proper.</param>
        /// <param name="attributeName">The attribute of the variable to be pdated if the input was proper.</param>
        /// <returns></returns>
        private Param CheckProperInputSpelling(string attributeValue, string varName, string attributeName)
        {
            Param par = new Param();

            //if one attribute only (can be a scalar either a vector).

            //split each vector of data for robot to a list of components.
            par._ratHouseParameter = attributeValue;

            //if the input can be only a scalar
            if (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter != "6")
            {
                //if the input for one value contains more than one dot for precison dot or chars that are not digits.
                //if true , update the values in the variables dictionary.
                if (DigitsNumberChecker(par._ratHouseParameter))
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
            //if the input can be a scalar either a vector.
            else
            {
                if (VectorNumberChecker(par._ratHouseParameter))
                {
                    _variablesList._variablesDictionary[varName]._description[attributeName] = par;

                    SetParametersTextBox(varName, new StringBuilder());
                }
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
        private bool CheckVaryingListBoxProperInput(Dictionary<string, string> toCheckVector)
        {
            //check if each attribute is according to both parameters if needed and the brackets also.
            foreach (string varName in toCheckVector.Keys)
            {
                int firstLeftBracketIndex = toCheckVector[varName].IndexOf('[');
                int firstRightBracketIndex = toCheckVector[varName].IndexOf(']');
                string varNiceName = _variablesList._variablesDictionary[varName]._description["nice_name"]._ratHouseParameter;

                //if there are brackets for a scalar.
                if (firstRightBracketIndex > -1 || firstLeftBracketIndex > -1)
                {
                    MessageBox.Show("Error : variable " + varNiceName + " has brackets but is a scalar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //if not represent a number.
                if (!DigitsNumberChecker(toCheckVector[varName]))
                {
                    MessageBox.Show("Error : variable " + varNiceName + " has [x][y] syntax error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //if everything is o.k return true.
            return true;
        }

        /// <summary>
        /// Check if vector input is spelled propperly.
        /// </summary>
        /// <param name="str">The string vector to be checked.</param>
        /// <returns>If the string vector is properlly spelled.</returns>
        private bool VectorNumberChecker(string str)
        {
            int x1 = str.Count(x => x == ' ') + 1;
            int y1 = str.Split(' ').Count();
            if (str.Where(x => (x < '0' || x > '9') && x != ' ' && x != '-' && x != '.').Count() > 0)
            {
                MessageBox.Show("Warnning : Vector can include onlt scalar and spaces.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            else if (!DigitsNumberChecker(str.Split(' ').ToList()))
            {
                MessageBox.Show("Warnning : Vector include to much spaces chars.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

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
            if (str == "")
                return false;

            //can starts with negative sign.
            if (str.StartsWith("-"))
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
                _variablesList._variablesDictionary[variable.Key]._description["status"]._ratHouseParameter == statusVal);
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
                case "Const":
                    return "0";
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
                case "Vector":
                    return "6";
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
            switch (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter)
            {
                case "0":   //const
                case "1":   //static
                case "6":
                    //show the _ratHouseParameter.
                    string parametersTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["parameters"]._ratHouseParameter);
                    parametersTextBox.Text = parametersTextVal;
                    break;

                case "2":   //varying
                case "3":   //acrossstair
                case "4":   //withinstair
                case "5":   //ramdom
                    //show the _ratHouseParameter.
                    string lowboundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                    string highboundTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                    string increasingTextVal = string.Join(",", _variablesList._variablesDictionary[varName]._description["increament"]._ratHouseParameter);

                    parametersTextBox.Text = ThreeStagesRepresentation(sBuilder, lowboundTextVal, increasingTextVal, highboundTextVal);
                    break;

            }
        }
        #endregion

        #region HandSounds
        /// <summary>special
        /// Handler for clicking to play the CenterRewardSound.
        /// </summary>
        /// <param name="sender">The bottun.</param>
        /// <param name="e">The args.</param>
        private void _btnRewardSound_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _cntrlLoop.PlayRewardSound();
            });
        }

        /// <summary>
        /// Handler for clicking to play the BreakFixation.
        /// </summary>
        /// <param name="sender">The bottun.</param>
        /// <param name="e">The args.</param>
        private void _btnBreakFixationSound_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _cntrlLoop.PlayBreakFixationSound();
            });
        }
        #endregion HandSounds
    }

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