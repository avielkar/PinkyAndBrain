using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PinkyAndBrain.TrajectoryCreators;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MLApp;
using System.Diagnostics;
using System.Windows.Forms;
using MotocomdotNetWrapper;
using LED.Strip.Adressable;
using System.IO;
using System.Reflection;
using log4net;
using System.Windows.Forms.DataVisualization.Charting;
using System.Media;
using WMPLib;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is the main program controll loop.
    /// It calls all the needed other inerfaces to make what it's needed to be created.
    /// The function is called by the GuiInterface after the statButton is clicked.
    /// </summary>
    class ControlLoop:IDisposable
    {
        #region ATTRIBUTES
        /// <summary>
        /// The trajectory creator interface for making the trajectory for each trial.
        /// </summary>
        private ITrajectoryCreator _trajectoryCrator;

        /// <summary>
        /// The trajectory creation 
        /// </summary>
        private TrajectoryCreatorHandler _trajectoryCreatorHandler;

        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.
        /// </summary>
        private List<Dictionary<string, List<double>>> _crossVaryingVals;

        /// <summary>
        /// The static variables list in double value presentation.
        /// The string is for the variable name.
        /// The outer list is for the two inner list (or one , conditioned in the landscapeHouseParameter).
        /// The inners lists are for the values for each of the ratHouseParameter and landscapeHouseParameter (if there).
        /// The inners kist is with size 1 if the input is a scalar.
        /// Otherwise ,  if a vector , it would be a list with the size of the vector.
        /// </summary>
        private Dictionary<string, List<List<double>>> _staticVariablesList;

        /// <summary>
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;

        /// <summary>
        /// The Matlab computing process object for drawing graphs and etc.
        /// </summary>
        private MLApp.MLApp _matlabApp;

        /// <summary>
        /// The name of the selected protocol.
        /// </summary>
        private string _selectedProtocolName;

        /// <summary>
        /// The current varying trial combination that should be selected to make the trajectory from.
        /// </summary>
        private int _currentVaryingTrialIndex;

        /// <summary>
        /// The total number of trials for the experiment should have.
        /// </summary>
        private int _totalNumOfTrials;
        
        /// <summary>
        /// The total number of correct answers(head stability during the trial movement plus correct decision).
        /// </summary>
        private int _totalCorrectAnswers;

        /// <summary>
        /// Thr total number of enterance to the center during timeout time with stability during the duration time.
        /// </summary>
        private int _totalHeadStabilityInCenterDuringDurationTime;

        /// <summary>
        /// The total success states for a trial (correct or wrong answer but some answer).
        /// </summary>
        private int _totalChoices;

        /// <summary>
        ///The total number of head fixation breaks during the duration time.
        /// </summary>
        private int _totalHeadFixationBreaks;

        /// <summary>
        /// The total number of head fixation breaks only during the start delay time.
        /// </summary>
        private int _totalHeadFixationBreaksStartDelay;

        /// <summary>
        /// The varying index selector for choosing the current combination index.
        /// </summary>
        private VaryingIndexSelector _varyingIndexSelector;

        /// <summary>
        /// The current repetition index number.
        /// </summary>
        private int _repetitionIndex;

        /// <summary>
        /// The current stickOn index number.
        /// </summary>
        private int _stickOnNumberIndex;

        /// <summary>
        /// Includes all the currebt trial timings and delays.
        /// </summary>
        private TrialTimings _currentTrialTimings;

        /// <summary>
        /// The current trial trajectories.
        /// The first element in the tuple is the ratHouseTrajectory.
        /// The second element in the tuple is the landscapeHouseTrajectory.
        /// </summary>
        private Tuple<Trajectory, Trajectory> _currentTrialTrajectories;

        /// <summary>
        /// The current trial stimulus type.
        /// </summary>
        private int _currentTrialStimulusType;

        /// <summary>
        /// A random object for random numbers.
        /// </summary>
        private Random _timingRandomizer;

        /// <summary>
        /// The robot reward controller.
        /// </summary>
        private RewardController _rewardController;

        /// <summary>
        /// Controller for the rat Noldus responses.
        /// </summary>
        private RatResponseController _ratResponseController;

        /// <summary>
        /// Controller for writing events for the AlphaOmega.
        /// </summary>
        private AlphaOmegaEventsWriter _alphaOmegaEventsWriter;

        /// <summary>
        /// Infra red controller for turnnig the InfraRed on/off.
        /// </summary>
        private InfraRedController _infraredController;

        /// <summary>
        /// Indicated if the control loop should not make another trials.
        /// </summary>
        private bool _stopAfterTheEndOfTheCurrentTrial;

        /// <summary>
        /// Indicated if the control loop should paused until resume button is pressed.
        /// </summary>
        private bool _pauseAfterTheEndOfTheCurrentTrial;

        /// <summary>
        /// Describes the delegate for a control with it's nick name.
        /// </summary>
        private Dictionary<string, Delegate> _mainGuiControlsDelegatesDictionary;

        /// <summary>
        /// Describes the control object with it's nick name.
        /// </summary>
        private Dictionary<string , Control> _mainGuiInterfaceControlsDictionary;

        /// <summary>
        /// The current rat sampling response come from the Noldus.
        /// The sampling rate is readen from solution settings configuration.
        /// </summary>
        private byte _currentRatResponse;

        /// <summary>
        /// The rat decision about the current trial stimulus direction.
        /// </summary>
        private RatDecison _currentRatDecision;

        /// <summary>
        /// Indicates if the rat decision should be inverse to the heading due to the random heading direction region.
        /// </summary>
        private bool _inverseRRDecision;

        /// <summary>
        /// The decision the rat should choose.
        /// </summary>
        private RatDecison _correctDecision;

        /// <summary>
        /// The selected rat name (that makes the experiment).
        /// </summary>
        public string RatName { get; set; }

        /// <summary>
        /// The number of repetitions for the varying set.
        /// </summary>
        public int NumOfRepetitions { get; set; }

        /// <summary>
        /// The number of stick on number for each specific value in the optional values for rounds (must devide NumOfRepetitions).
        /// </summary>
        public int NumOfStickOn { get; set; }

        /// <summary>
        /// The percentage number of turned on LEDS.
        /// </summary>
        public double PercentageOfTurnedOnLeds { get; set; }

        /// <summary>
        /// The LEDs brightness value (0-31).
        /// </summary>
        public int LEDBrightness { get; set; }

        /// <summary>
        /// Timer for raising event to sample the Noldus reponse direction and store it in _currentRatResponse.
        /// </summary>
        private System.Timers.Timer _ratSampleResponseTimer;

        /// <summary>
        /// Timer for raising event for counting the water the rat have rewarded so far.
        /// </summary>
        private System.Timers.Timer _waterRewardFillingTimer;

        /// <summary>
        /// The JBI protocol file creator for each trial trajectory.
        /// </summary>
        private MotomanProtocolFileCreator _motomanProtocolFileCreator;

        /// <summary>
        /// The YASAKAWA motoman robot controller.
        /// </summary>
        private CYasnac _motomanController;

        /// <summary>
        /// The led controller for controlling the leds visibility in the ledstrip connected to the arduino.
        /// </summary>
        private LEDController _ledController;

        /// <summary>
        /// The leds selector dor selecting different led to turn on.
        /// </summary>
        private VaryingIndexSelector _ledSelector;

        /// <summary>
        /// The SavedDataMaker object to create new result file for each experiment.
        /// </summary>
        private SavedDataMaker _savedExperimentDataMaker;

        /// <summary>
        /// Logger for writing log information.
        /// </summary>
        private ILog _logger;
        
        /// <summary>
        /// The online psyco graph maker object to control the psycho chart.
        /// </summary>
        private OnlinePsychGraphMaker _onlinePsychGraphMaker;

        /// <summary>
        /// Indictaed wether giving a second reward automatically in the direction side of the stimulus type.
        /// </summary>
        public bool AutoReward { get; set; }

        /// <summary>
        /// Indicated if whether to wait for a rat to enter it's head to the center automatically skiping this step.
        /// </summary>
        public bool AutoStart { get; set; }

        /// <summary>
        /// Indicated if to check the fixation during the duration time or to skip that stage.k
        /// </summary>
        public bool AutoFixation { get; set; }

        /// <summary>
        /// Indicated if to sound a media during the reward with the same direction.
        /// </summary>
        public bool AutoRewardSound { get; set; }

        /// <summary>
        /// Indicates if the mode of the trial is only untill the fixation stage (include).
        /// </summary>
        public bool FixationOnlyMode { get; set; }

        /// <summary>
        /// Indicates if to enable fixation break sound.
        /// </summary>
        public bool EnableFixationBreakSound { get; set; }

        /// <summary>
        /// Indicates the autos options that are commanded in the real time (when the code use it at the conditions and not only if the user change it betweens).
        /// </summary>
        public AutosOptions _autosOptionsInRealTime { get; set; }

        /// <summary>
        /// Dictionary represent a sound name and it's file path.
        /// </summary>
        private Dictionary<string, string> _soundPlayerPathDB;

        /// <summary>
        /// Object for handles WindowsMediaPlayer controls to play mp3 files.
        /// </summary>
        private WindowsMediaPlayer _windowsMediaPlayer;
        #endregion ATTRIBUTES

        #region CONTRUCTORS
        /// <summary>
        /// Default constructor.
        /// <param name="matlabApp">The matlab app handler.</param>
        /// <param name="motomanController">The motoman controller object.</param>
        /// <param name="ledController">The led controller object.</param>
        /// <param name="infraRedController">The InfraRed controller for turning on/off the InfraRed.</param>
        /// <param name="ctrlDelegatesDic">The controls delegate names and objects.</param>
        /// <param name="mainGuiInterfaceControlsDictionary">The name of each main gui needed control and it's reference.</param>
        /// <param name="logger">The program logger for logging into log file.</param>
        /// </summary>
        public ControlLoop(MLApp.MLApp matlabApp , CYasnac motomanController , LEDController ledController , InfraRedController infraRedController, Dictionary<string, Delegate> ctrlDelegatesDic, Dictionary<string , Control> mainGuiInterfaceControlsDictionary , ILog logger)
        {
            _matlabApp = matlabApp;
            _trajectoryCreatorHandler = new TrajectoryCreatorHandler(_matlabApp);
            
            _rewardController = new RewardController("Dev1" , "Port1" ,"Line0:2", "RewardChannels");
            _ratResponseController = new RatResponseController("Dev1", "Port0", "Line0:2", "RatResponseChannels");
            _alphaOmegaEventsWriter = new AlphaOmegaEventsWriter("Dev1", "Port0", "Line3:7", "AlphaOmegaEventsChannels" , "Port1" , "Line3" , "AlphaOmegaStrobeChannel");
            _infraredController = infraRedController;
            
            _stopAfterTheEndOfTheCurrentTrial = false;
            
            //configure  rge timer for the sampling Noldus rat response direction.
            _ratSampleResponseTimer = new System.Timers.Timer(Properties.Settings.Default.NoldusRatReponseSampleRate);
            _ratSampleResponseTimer.Elapsed += SetRatReponse;
            
            //configure the water filling timer for the water reward estimation interactive window.
            _waterRewardFillingTimer = new System.Timers.Timer();
            _waterRewardFillingTimer.Interval = 100;
            _waterRewardFillingTimer.Elapsed += WaterRewardFillingTimer_Tick;

            //initialized the motoman JBI file creatotr for each trial (the command file that would be send to the motoman controller).
            _motomanProtocolFileCreator = new MotomanProtocolFileCreator(@"C:\Users\User\Desktop\GAUSSIANMOVING2.JBI");

            //take the motoman controller object.
            _motomanController = motomanController;

            //take the led controller object.
            _ledController = ledController;
            //initialize the leds index selector.
            _ledSelector = new VaryingIndexSelector(250);

            //initialize the savedDataMaker object once.
            _savedExperimentDataMaker = new SavedDataMaker();

            //copy the logger reference to writing lof information
            _logger = logger;

            //initialize the controls changed by the controlLoop and their invoking functions.
            _mainGuiControlsDelegatesDictionary = ctrlDelegatesDic;
            _mainGuiInterfaceControlsDictionary = mainGuiInterfaceControlsDictionary;

            //initialize the online psycho graph.
            _onlinePsychGraphMaker = new OnlinePsychGraphMaker
            {
                ClearDelegate = _mainGuiControlsDelegatesDictionary["OnlinePsychoGraphClear"],
                SetSeriesDelegate = _mainGuiControlsDelegatesDictionary["OnlinePsychoGraphSetSeries"],
                SetPointDelegate = _mainGuiControlsDelegatesDictionary["OnlinePsychoGraphSetPoint"],
                ChartControl = _mainGuiInterfaceControlsDictionary["OnlinePsychoGraph"] as Chart
            };

            //initialize the MrdiaPlayer controller and the DB of sound effects and their path.
            _soundPlayerPathDB = new Dictionary<string, string>();
            LoadAllSoundPlayers();
            _windowsMediaPlayer = new WindowsMediaPlayer();
        }
        #endregion CONTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Transfer the control from the main gui to the control loop until a new gui event is handled by the user.
        /// </summary>
        public void Start(Variables variablesList, List<Dictionary<string, List<double>>> crossVaryingList, Dictionary<string, List<List<double>>> staticVariablesList, int frequency, string trajectoryCreatorName)
        {
            //initialize variables.
            _variablesList = variablesList;
            _crossVaryingVals = crossVaryingList;
            _staticVariablesList = staticVariablesList;
            _frequency = frequency;
            
            //initialize counters and varying selector.
            _totalNumOfTrials = _crossVaryingVals.Count();
            _varyingIndexSelector = new VaryingIndexSelector(_totalNumOfTrials);
            _totalCorrectAnswers = 0;
            _totalHeadStabilityInCenterDuringDurationTime = 0;
            _totalChoices = 0;
            _totalHeadFixationBreaks = 0;
            _totalHeadFixationBreaksStartDelay = 0;

            //reset repetition index and stickOnNumber (would be reset in the loop immediately because the condition == NumOfStickOn).
            _repetitionIndex = 0;
            _stickOnNumberIndex = NumOfStickOn;

            _timingRandomizer = new Random();

            //set the trajectory creator name to the given one that should be called in the trajectoryCreatorHandler.
            //also , set the other properties.
            _trajectoryCreatorHandler.SetTrajectoryAttributes(trajectoryCreatorName, _variablesList, _crossVaryingVals, _staticVariablesList, _frequency);

            //reset the RewardController outputs.
            _rewardController.ResetControllerOutputs();

            //set the frequency for the JBI file creator.
            _motomanProtocolFileCreator.Frequency = _frequency;

            //create a new results file for the new experiment.
            _savedExperimentDataMaker.CreateControlNewFile(RatName);

            //clear and initialize the psyco online graph.
            _onlinePsychGraphMaker.Clear();
            _onlinePsychGraphMaker.VaryingParametrsNames = GetVaryingVariablesList();
            _onlinePsychGraphMaker.HeadingDireactionRegion = new Region
            {
                LowBound = double.Parse(_variablesList._variablesDictionary["HEADING_DIRECTION"]._description["low_bound"]._ratHouseParameter[0]),
                Increament = double.Parse(_variablesList._variablesDictionary["HEADING_DIRECTION"]._description["increament"]._landscapeParameters[0]),
                HighBound = double.Parse(_variablesList._variablesDictionary["HEADING_DIRECTION"]._description["high_bound"]._landscapeParameters[0])
            };
            _onlinePsychGraphMaker.InitSerieses();

            //reset the amount of water measurement interactive panel.
            _mainGuiInterfaceControlsDictionary["SetWaterRewardsMeasure"].BeginInvoke(
              _mainGuiControlsDelegatesDictionary["SetWaterRewardsMeasure"], true);

            //run the main control loop function in other thread from the main thread ( that handling events and etc).
            _stopAfterTheEndOfTheCurrentTrial = false;
            Globals._systemState = SystemState.RUNNING;
            _ratSampleResponseTimer.Start();
            Task.Run(() => MainControlLoop());
        }

        /// <summary>
        /// Stop the MainControlLoop function.
        /// </summary>
        public void Stop()
        {
            //Globals._systemState = SystemState.STOPPED;
            _stopAfterTheEndOfTheCurrentTrial = true;
        }

        /// <summary>
        /// Pause the MainControlLoop function.
        /// </summary>
        public void Pause()
        {
            _pauseAfterTheEndOfTheCurrentTrial = true;
        }

        /// <summary>
        /// Resume the MainControlLoop.
        /// </summary>
        public void Resume()
        {
            _pauseAfterTheEndOfTheCurrentTrial = false;

            Task.Run(()=>MainControlLoop());
        }

        /// <summary>
        /// Clear all the control loop items and timers.
        /// </summary>
        public void Dispose()
        {
            //stop the timer for sampling the rat Noldus response.
            _ratSampleResponseTimer.Stop();

            //remove the timer for rat Noldus response.
            _ratSampleResponseTimer.Elapsed -= SetRatReponse;            
        }

        public void MainControlLoop()
        {
            //set robot servo on and go homeposition.
            _motomanController.SetServoOn();
            WriteHomePosFile();
            MoveRobotHomePosition();

            for (; _repetitionIndex < NumOfRepetitions / NumOfStickOn;)
            {
                //while all trial are not executed or not come with response stage.
                while (!_varyingIndexSelector.IsFinished())
                {
                    //if system has stopped , wait for the end of the current trial ans break,
                    if (Globals._systemState.Equals(SystemState.STOPPED) || _stopAfterTheEndOfTheCurrentTrial || _pauseAfterTheEndOfTheCurrentTrial)
                    {
                        Globals._systemState = SystemState.STOPPED;

                        if (!_pauseAfterTheEndOfTheCurrentTrial)
                            EndControlLoopByStopFunction();

                        return;
                    }

                    //initialize the stickOnNumber if needed and choose a new varyingIndex.
                    if (_stickOnNumberIndex == NumOfStickOn)
                    {
                        //choose the random combination index for the current trial.
                        _currentVaryingTrialIndex = _varyingIndexSelector.ChooseRandomCombination();

                        //craetes the trajectory for both robots for the current trial if not one of the training protocols.
                        _currentTrialTrajectories = _trajectoryCreatorHandler.CreateTrajectory(_currentVaryingTrialIndex);

                        //reset the stickOnNumber.
                        _stickOnNumberIndex = 0;
                    }

                    //make that current option strickOn times one after one.
                    for (; _stickOnNumberIndex < NumOfStickOn;)
                    {
                        //if system has stopped , wait for the end of the current trial ans break,
                        if (Globals._systemState.Equals(SystemState.STOPPED) || _stopAfterTheEndOfTheCurrentTrial || _pauseAfterTheEndOfTheCurrentTrial)
                        {
                            Globals._systemState = SystemState.STOPPED;

                            if (!_pauseAfterTheEndOfTheCurrentTrial)
                                EndControlLoopByStopFunction();

                            return;
                        }

                        //show some trial details to the gui trial details panel.
                        ShowTrialDetailsToTheDetailsListView();
                        //show the global experiment details for global experiment details.
                        ShowGlobalExperimentDetailsListView();

                        //initialize the currebt time parameters and all the current trial variables.
                        InitializationStage();

                        //wait the rat to first (in the current trial - for "start buttom") move it's head to the center.
                        bool headEnteredToTheCenterDuringTheTimeoutDuration = WaitForHeadEnteranceToTheCenterStage();

                        //indicates if during duration1 time - the rat stays with the eyes in the center.
                        bool duration1HeadInTheCenterStabilityStage = false;

                        //if the rat entered it's head to the center in the before timeOut time.
                        if (headEnteredToTheCenterDuringTheTimeoutDuration)
                        {
                            //if the rat head was stable in the center for the startDelay time as required start the movement.
                            if (duration1HeadInTheCenterStabilityStage = CheckDuration1HeadInTheCenterStabilityStage())
                            {
                                //update the state of the rat decision.
                                _currentRatDecision = RatDecison.DurationTime;

                                //moving the robot with duration time , and checking for the stability of the head in the center.
                                if (MovingTheRobotDurationWithHeadCenterStabilityStage())
                                {
                                    //update the number of total head in the center with stability during the duration time.
                                    _totalHeadStabilityInCenterDuringDurationTime++;
                                    _currentRatDecision = RatDecison.PassDurationTime;

                                    //reward the rat in the center with water for duration of rewardCenterDuration for stable head in the center during the movement.
                                    RewardCenterStage(AutoReward, AutoRewardSound);

                                    //if not to skip all stages after the fixation stage.
                                    if (!FixationOnlyMode)
                                    {
                                        //wait the rat to response to the movement during the response tine.
                                        Tuple<RatDecison, bool> decision = ResponseTimeStage();

                                        //second reward stage (condition if needed in the stage)
                                        SecondRewardStage(decision, AutoReward);
                                    }
                                }

                                //if fixation break
                                else
                                {
                                    //update the number of head fixation breaks.
                                    _totalHeadFixationBreaks++;
                                }

                                //after the end of rewrad wait a time delay before backword movement to the home poistion.
                                RewardToBackwardDelayStage();
                            }

                            //sounds the beep for missing the movement head in the center.
                            else
                            {
                                Task.Run(() => { _windowsMediaPlayer.URL = _soundPlayerPathDB["MissingAnswer"]; _windowsMediaPlayer.controls.play(); });
                                _totalHeadFixationBreaksStartDelay++;
                            }
                        }

                        //sounds the beep with the missing start gead in the center.
                        else
                        {
                            //Task.Run(() => { _windowsMediaPlayer.URL = _soundPlayerPathDB["WrongAnswer"]; _windowsMediaPlayer.controls.play(); });
                        }

                        //the post trial stage for saving the trial data and for the delay between trials.
                        if (PostTrialStage(duration1HeadInTheCenterStabilityStage))
                            _stickOnNumberIndex++;
                    }
                }

                //reset all trials combination statuses for the next repetition.
                _varyingIndexSelector.ResetTrialsStatus();
                _repetitionIndex++;
            }

            //end of all trials and repetitions.
            EndControlLoopByStopFunction();
        }

        /// <summary>
        /// This function is called if the control loop asked to be ended.
        /// </summary>
        public void EndControlLoopByStopFunction()
        {
                Globals._systemState = SystemState.FINISHED;

                //set robot's servo off.
                _motomanController.SetServoOff();

                //reset the repetition index.
                _repetitionIndex = 0;

                //show the final global experiment info (show it the last time)
                ShowGlobalExperimentDetailsListView();

                //Close and release the current saved file.
                _savedExperimentDataMaker.CloseFile();

                //raise an event for the GuiInterface that the trials round is over.
                _mainGuiInterfaceControlsDictionary["FinishedAllTrialsRound"].BeginInvoke(_mainGuiControlsDelegatesDictionary["FinishedAllTrialsRound"]);

                //choose none rat in the selected rat
                _mainGuiInterfaceControlsDictionary["ResetSelectedRatNameCombobox"].BeginInvoke(_mainGuiControlsDelegatesDictionary["ResetSelectedRatNameCombobox"]);
        }

        /// <summary>
        /// Load all mp3 files that the MediaPlayer object should use.
        /// </summary>
        private void LoadAllSoundPlayers()
        {
            _soundPlayerPathDB.Add("CorrectAnswer", Application.StartupPath + @"\SoundEffects\correct sound effect.wav");
            _soundPlayerPathDB.Add("WrongAnswer", Application.StartupPath + @"\SoundEffects\Wrong Buzzer Sound Effect.wav");
            _soundPlayerPathDB.Add("Ding", Application.StartupPath + @"\SoundEffects\Ding Sound Effects.wav");
            _soundPlayerPathDB.Add("MissingAnswer", Application.StartupPath + @"\SoundEffects\Wrong Buzzer Sound Effect.wav");
            _soundPlayerPathDB.Add("Ding-Left", Application.StartupPath + @"\SoundEffects\Ding Sound Effects - Left.wav");
            _soundPlayerPathDB.Add("Ding-Right", Application.StartupPath + @"\SoundEffects\Ding Sound Effects - Right.wav");
        }

        /// <summary>
        /// Show global experiment parameters.
        /// </summary>
        private void ShowGlobalExperimentDetailsListView()
        {
            //clear the past global details in the global details listview.
            _mainGuiInterfaceControlsDictionary["ClearGlobaExperimentlDetailsViewList"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["ClearGlobaExperimentlDetailsViewList"]);

            //update the number of trials count.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Trial # (count)", ((_totalHeadStabilityInCenterDuringDurationTime + _totalHeadFixationBreaks + _totalHeadFixationBreaksStartDelay + 1)).ToString());

            //update the number of total correct head in center with stabilty during duration time.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Success Trials", (_totalChoices).ToString());

            //update the number of total correct answers.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Correct Answers", (_totalCorrectAnswers).ToString());

            //update the number of total failure trial during duration time.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Failure Trials", (_totalHeadStabilityInCenterDuringDurationTime + _totalHeadFixationBreaks + _totalHeadFixationBreaksStartDelay - _totalChoices).ToString());

            //update the number of total fixation breaks.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Fixation Breaks", (_totalHeadFixationBreaks + _totalHeadFixationBreaksStartDelay).ToString());

            //update the number of left trials.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Remaining Trials", (_varyingIndexSelector.CountRemaining()).ToString());

            //update the number of total correct head in center with stabilty during duration time.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "No choices", (_totalHeadStabilityInCenterDuringDurationTime - _totalChoices).ToString());
        }

        /// <summary>
        /// Show the current trial dynamic details to the ListView.
        /// </summary>
        public void ShowTrialDetailsToTheDetailsListView()
        {
            Dictionary<string , List<double>> currentTrialDetails =  _crossVaryingVals[_currentVaryingTrialIndex];
            _mainGuiInterfaceControlsDictionary["ClearCurrentTrialDetailsViewList"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["ClearCurrentTrialDetailsViewList"]);

            foreach (string varName in currentTrialDetails.Keys)
            {
                string currentParameterDetails;
                //only ratHouseParameter
                if (currentTrialDetails[varName].Count == 1)
                    currentParameterDetails = "[" + currentTrialDetails[varName][0].ToString() + "]";
                else
                    currentParameterDetails = "[" + currentTrialDetails[varName][0].ToString() + "]" + "[" + currentTrialDetails[varName][1].ToString() + "]";
                //both ratHouseParameter and landscapeHouseParameter
                
                _mainGuiInterfaceControlsDictionary["UpdateCurrentTrialDetailsViewList"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateCurrentTrialDetailsViewList"], varName, currentParameterDetails);
            }
        }

        /// <summary>
        /// Initializes the variables , points , trajectories , random varibles ,  etc.
        /// </summary>
        public void InitializationStage()
        {
            _logger.Info("Initialization Stage of trial #" + (_totalHeadStabilityInCenterDuringDurationTime + 1));

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Intialization");

            //determine all current trial timings and delays.
            _currentTrialTimings = DetermineCurrentTrialTimings();
            _currentTrialStimulusType = DetermineCurrentStimulusType();
            //set the reposne to the stimulus direction as no entry to descision stage (and change it after if needed as well).
            _currentRatDecision = RatDecison.NoEntryToResponseStage;
            //set the auto option to default values.
            _autosOptionsInRealTime = new AutosOptions();

            //updatre the trial number for the motoman protocol file creator to send it to the alpha omega.
            _motomanProtocolFileCreator.TrialNum = _totalHeadStabilityInCenterDuringDurationTime + 1;
            
            //Sounds the start beep. Now waiting for the rat to move it's head to the center.
            Console.Beep(2000 ,200);

            //write the beep start event to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.AudioStart);
        }

        /// <summary>
        /// Waiting the rat to response the movement direction abd update the _totalCorrectAnswers counter.
        /// <returns>The rat decision value and it's correctness.</returns>
        /// </summary>
        public Tuple<RatDecison , bool> ResponseTimeStage()
        {
            //Thread.Sleep(1000*(int)(_currentTrialTimings.wResponseTime));

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Waiting for Response");

            //if not trainig continue.
            if (GetVariableValue(0, "STIMULUS_TYPE") == "0")
                return new Tuple<RatDecison,bool>(RatDecison.NoDecision , false);

            //get the current stimulus direction.
            double currentHeadingDirection = double.Parse(GetVariableValue(0, "HEADING_DIRECTION"));

            //determine the current stimulus direaction.
            RatDecison currentStimulationSide = (currentHeadingDirection == 0) ? (RatDecison.Center) : ((currentHeadingDirection > 0) ? (RatDecison.Right) : RatDecison.Left);
            //determine if the current stimulus heading direction is in the random heading direction region.
            if (Math.Abs(currentHeadingDirection) <= double.Parse(_variablesList._variablesDictionary["RR_HEADINGS"]._description["parameters"]._ratHouseParameter[0]))
            {
                //get a random side with probability of RR_PROBABILITY to the right side.
                int sampledBernouli = Bernoulli.Sample(double.Parse(_variablesList._variablesDictionary["RR_PROBABILITY"]._description["parameters"]._ratHouseParameter[0]));

                RatDecison changedStimulusSide = currentStimulationSide;

                if (sampledBernouli == 1)
                {
                    currentStimulationSide = RatDecison.Right;
                }
                else
                {
                    currentStimulationSide = RatDecison.Left;
                }

                if (changedStimulusSide.Equals(currentStimulationSide))
                {
                    _inverseRRDecision = true;
                }
            }
            else
            {
                _inverseRRDecision = false;
            }
            _correctDecision = currentStimulationSide;


            Stopwatch sw = new Stopwatch();
            sw.Start();

            //time to wait for the moving rat response. if decided about a side so break and return the decision and update the _totalCorrectAnsers.
            while(sw.ElapsedMilliseconds < 1000*(int)(_currentTrialTimings.wResponseTime))
            {
                if(_currentRatResponse == (byte)RatDecison.Left)
                {
                    //increase the total choices for wromg or correct choices (some choices).
                    _totalChoices++;

                    //update the current rat decision state.
                    _currentRatDecision = RatDecison.Left;

                    //write the event that te rat enter it's head to the left to the AlphaOmega.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterLeft);

                    if (currentStimulationSide.Equals(RatDecison.Left))
                    {
                        //increase the total correct answers.
                        _totalCorrectAnswers++;

                        //update the psycho online graph.
                        _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType ,  currentHeadingDirection, AnswerStatus.CORRECT);
                        
                        return new Tuple<RatDecison, bool>(RatDecison.Left, true);
                    }

                    _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType ,  currentHeadingDirection, AnswerStatus.WRONG);
                    
                    return new Tuple<RatDecison,bool>(RatDecison.Left , false);
                }

                else if(_currentRatResponse == (byte)RatDecison.Right)
                {
                    //update current rat decision state.
                    _currentRatDecision = RatDecison.Right;

                    //increase the total choices for wromg or correct choices (some choices).
                    _totalChoices++;

                    //write the event that te rat enter it's head to the right to the AlphaOmega.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterRight);

                    if (currentStimulationSide.Equals(RatDecison.Right)) 
                    { 
                        //increase the total coreect answers.
                        _totalCorrectAnswers++;

                        //update the psycho online graph.
                        _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType ,  currentHeadingDirection, AnswerStatus.CORRECT);

                        return new Tuple<RatDecison, bool>(RatDecison.Right, true);
                    }

                    _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType ,  currentHeadingDirection, AnswerStatus.WRONG);

                    return new Tuple<RatDecison,bool>( RatDecison.Right , false);
                }
            }

            _currentRatDecision = RatDecison.NoDecision;

            return new Tuple<RatDecison,bool>(RatDecison.NoDecision , false);
        }

        /// <summary>
        /// Reward water to the rat in the given position with during the given time long.
        /// </summary>
        /// <param name="position">The cellenoid position side to be opened.</param>
        /// <param name="rewardDuration">The duration the selected cellenoid eould be opened.</param>
        /// <param name="rewardDelay">The delay time before opening the selected cellenoid.</param>
        /// <param name="autoreward">Indecation if to give the reward with no delay.</param>
        /// <param name="autoRewardSound">Indecation ig to give the suto reard sound during the reward.</param>
        public void Reward(RewardPosition position, double rewardDuration, double rewardDelay, bool autoreward = false , bool autoRewardSound = false)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //if autoReward than play the sound in the slected side of the water reward in order to help the rat to understand the water reward side.
            _autosOptionsInRealTime.AutoRewardSound = autoRewardSound;
            if(autoRewardSound)
            {
                //play the selected reward side mono sound.
                switch (position)
                {
                    case RewardPosition.Center:
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"];
                        _windowsMediaPlayer.controls.play();
                        break;
                    case RewardPosition.Left:
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Left"];
                        _windowsMediaPlayer.controls.play();
                        break;
                    case RewardPosition.Right:
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Right"];
                        _windowsMediaPlayer.controls.play();
                        break;
                    default:
                        break;
                }
            }

            //wait the reward delay time befor openning the reward if not autoReward.
            //if (!autoreward)
                Thread.Sleep((int)(rewardDelay * 1000));

            sw.Restart();

            //open the center reward for the rat to be rewarded.
            //after the reward duration time and than close it.
            _rewardController.WriteSingleSamplePort(true, (byte)position);

            //wait the reward time and fill the interactive water fill estimation panel.
            _waterRewardFillingTimer.Start();
            Thread.Sleep((int)(rewardDuration * 1000));
            _waterRewardFillingTimer.Stop();

            //close again the reward port.
            _rewardController.WriteSingleSamplePort(true, 0x00);
        }

        /// <summary>
        /// Second reward stage according to the rat response for the stimulus direction.
        /// </summary>
        /// <param name="decision">The rat decision about the stimulus direction and if correct or not.</param>
        /// <param name="autoReward">Indicated if to give reward automatically in the motion side of the stimulus direction.</param>
        public void SecondRewardStage(Tuple<RatDecison, bool> decision,  bool autoReward = false)
        {
            //check if the decision was correct and reward the rat according that decision.
            if (decision.Item2)
            {
                //reward the choosen side cellenoid.
                switch (decision.Item1)
                {
                    case RatDecison.Center:
                        RewardCenterStage(false , AutoRewardSound);
                        break;
                    case RatDecison.Left:
                        RewardLeftStage(false, AutoRewardSound);
                        break;
                    case RatDecison.Right:
                        RewardRightStage(false, AutoRewardSound);
                        break;
                    default:
                        break;
                }
            }

            //if to give reward no matter whether the response was correct or not.
            else if (AutoReward)
            {
                //get the current stimulus direction.
                double currentHeadingDirection = double.Parse(GetVariableValue(0, "HEADING_DIRECTION"));

                //determine the current stimulus direaction.
                RatDecison currentStimulationSide = (currentHeadingDirection == 0) ? (RatDecison.Center) : ((currentHeadingDirection > 0) ? (RatDecison.Right) : RatDecison.Left);

                //give the reward according to the robot direction motion with no delay.
                switch (_correctDecision)
                {
                    case RatDecison.Center:
                        //RewardCenterStage(true , AutoRewardSound);
                        //make the reward randomly chosen right or left.
                        if(_inverseRRDecision)
                        {

                        }
                        break;

                    case RatDecison.Left:
                        RewardLeftStage(true , AutoRewardSound);
                        break;

                    case RatDecison.Right:
                        RewardRightStage(true , AutoRewardSound);
                        break;

                    default:
                        break;
                }
            }

            //if not AutoReward and the answer is wrong play the wrong answer audio
            else
            {
                //play the wrong answer
                //Task.Run(() => { _windowsMediaPlayer.URL = _soundPlayerPathDB["WrongAnswer"]; _windowsMediaPlayer.controls.play(); });

                //write the alpha omega event for playing a wrong answer audio.
                _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.AudioWrong);
            }

            _autosOptionsInRealTime.AutoReward = autoReward;
        }

        /// <summary>
        /// The reward left stage is happening if the rat decide the correct stimulus side.
        /// <param name="autoReward">Indecation if to give the reward with no delay.</param>
        /// <param name="autoRewardSound">Indecation ig to give the suto reard sound during the reward.</param>
        /// </summary>
        public void RewardLeftStage(bool autoReward = false , bool autoRewardSound = false)
        {
            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Getting Reward (Left)");

            Reward(RewardPosition.Left, _currentTrialTimings.wRewardLeftDuration, _currentTrialTimings.wRewardLeftDelay, autoReward , autoRewardSound);

            //write that the rat get left reward to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.LeftReward);
        }

        /// <summary>
        /// The reward right stage is happening if the rat decide the correct stimulus side.
        /// <param name="autoReward">Indecation if to give the reward with no delay.</param>
        /// <param name="autoRewardSound">Indecation ig to give the suto reard sound during the reward.</param>
        /// </summary>
        public void RewardRightStage(bool autoReward = false, bool autoRewardSound = false)
        {
            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Getting Reward (Right)");


            Reward(RewardPosition.Right, _currentTrialTimings.wRewardRightDuration, _currentTrialTimings.wRewardRightDelay  , autoReward ,autoRewardSound);

            //write that the rat get right reward to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.RightReward);
        }

        /// <summary>
        /// The reward center stage is happening if the rat head was consistently stable in the center during the movement.
        /// <param name="autoReward">Indecation if to give the reward with no delay.</param>
        /// <param name="autoRewardSound">Indecation ig to give the suto reard sound during the reward.</param>
        /// </summary>
        public void RewardCenterStage(bool autoReward = false, bool autoRewardSound = false)
        {
            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Getting Reward (Center)");

            Reward(RewardPosition.Center, _currentTrialTimings.wRewardCenterDuration, _currentTrialTimings.wRewardCenterDelay, autoReward ,autoRewardSound);

            //write that the rat get center reward to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.CenterReward);
        }

        /// <summary>
        /// The stage for the time between the end of the reward to the beginnig moving tohe robot to it's home position.
        /// </summary>
        private void RewardToBackwardDelayStage()
        {
            Thread.Sleep((int)(_currentTrialTimings.wRewardToBackwardDelay*1000));
        }

        /// <summary>
        /// Moving the robot stage (it the rat enter the head to the center in the timeOut time and was stable in the center for startDelay time).
        /// This function also , in paralleled to the robot moving , checks that the rat head was consistently in the center during the duration time of the movement time.
        /// </summary>
        /// <returns>True if the head was stable consistently in the center during the movement time.</returns>
        public bool MovingTheRobotDurationWithHeadCenterStabilityStage()
        {
            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Stimulus Duration");

            //The motion of the Yasakawa robot if needed as the current stimulus type (if is both visual&vestibular -3 or only vistibular-1).
            Task robotMotion;
            switch (_currentTrialStimulusType)
            {
                case 0://none
                    robotMotion = Task.Factory.StartNew(() => Thread.Sleep((int)(1000 * _currentTrialTimings.wDuration)));
                    break;
                case 1://vistibular only.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.Both);
                    robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory());
                    break;

                case 2://visual only.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R2Only);
                    robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory());

                    //here should be stimulus type 2 for motion of the second robot for visual only.
                    //should move the robot and also to turn on the leds.
                    LEDsData ledsData = new LEDsData((byte)LEDBrightness, 0, 255, 0, _ledSelector.FillWithBinaryRandomCombination(PercentageOfTurnedOnLeds));
                    _ledController.LEDsDataCommand = ledsData;
                    _ledController.SendData();
                    _ledController.ExecuteCommands();
                    break;

                case 3://vistibular and visual both.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R1Only);
                    robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory());
                    //should move only r1 robot and also to turn on the leds.
                    LEDsData ledsData2 = new LEDsData((byte)LEDBrightness, 0, 255, 0, _ledSelector.FillWithBinaryRandomCombination(PercentageOfTurnedOnLeds));
                    _ledController.LEDsDataCommand = ledsData2;
                    _ledController.SendData();
                    _ledController.ExecuteCommands();
                    break;

                default://if there is no motion , make a delay of waiting the duration time (the time that should take the robot to move).
                    robotMotion = Task.Factory.StartNew(() => Thread.Sleep((int)(1000 * _currentTrialTimings.wDuration)));
                    break;
            };

            //also run the rat center head checking in parallel to the movement time.
            bool headInCenterAllTheTime = true;
            Task.Run(() =>
            {
                while (!robotMotion.IsCompleted)
                {
                    //sample the signal indicating if the rat head is in the center only 60 time per second (because the refresh rate of the signal is that frequency).
                    Thread.Sleep((int)(Properties.Settings.Default.NoldusRatReponseSampleRate));
                    if (_currentRatResponse != 2 && headInCenterAllTheTime)
                    {
                        headInCenterAllTheTime = false;

                        if (!AutoFixation)
                        {
                            if(EnableFixationBreakSound)
                            //sound the break fixation sound - aaaahhhh sound.
                            /*Task.Run(() => */{ _windowsMediaPlayer.URL = _soundPlayerPathDB["MissingAnswer"]; _windowsMediaPlayer.controls.play(); }/*)*/;

                            //write the break fixation event to the AlphaOmega.
                            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadStabilityBreak);
                        }
                        _autosOptionsInRealTime.AutoFixation = AutoFixation;
                    }
                }
            });

            //wait the robot to finish the movement.
            robotMotion.Wait();

            //and turn off the leds visual vistibular (it is o.k for all cases , just reset).
            _ledController.ResetLeds();

            _logger.Info("End MovingTheRobotDurationWithHeadCenterStabilityStage");
            //return the true state of the heading in the center stability during the duration time or always true when AutoFixation.
            return headInCenterAllTheTime || AutoFixation;
        }

        /// <summary>
        /// Stage to check (after the rat enter the head to the center) that the head is stable in the center for startDelay time.
        /// </summary>
        /// <returns></returns>
        public bool CheckDuration1HeadInTheCenterStabilityStage()
        {
            //waits the startdelay time before starting the motion of the robot for the rat to ensure stability with head in the center.
            //reset the stopwatch for new measurement time cycle of startDelay.
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //check if the head is stable in the center during the startDelay time (before starting the movement).
            while (sw.ElapsedMilliseconds < (int)(_currentTrialTimings.wStartDelay * 1000))
            {
                //if AutoFixation no need to check that.
                if (!AutoFixation)
                {
                    //sample the signal indicating if the rat head is in the center only 60 time per second (because the refresh rate of the signal is that frequency).
                    Thread.Sleep((int)(Properties.Settings.Default.NoldusRatReponseSampleRate));

                    //if the head sample mentioned that the head was not in the center during the startDelay time , break , and move to the post trial time.
                    if (_currentRatResponse != 2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Stage for checing if the rat enter the first time the head to the center in order to start the movement.
        /// </summary>
        /// <returns>True if the rat enter the head to the center during the limit of the timeoutTime.</returns>
        public bool WaitForHeadEnteranceToTheCenterStage()
        {
            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Waiting for rat to start trial");

            //if autoStart is checked than should not wait for the rat to enter it's head to the center for starting.
            _autosOptionsInRealTime.AutoStart = AutoStart;
            if(AutoStart)
            {
                return true;
            }

            //waits for the rat to move it's head to the center with timeout time.
            int x = 0;

            //stopwatch for the center head start response timeout.
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (x != 2 && ((int)sw.Elapsed.TotalMilliseconds < (int)(_currentTrialTimings.wTimeOutTime * 1000)))
            {
                //sample the signal indicating if the rat head is in the center only 60 time per second (because the refresh rate of the signal is that frequency).
                Thread.Sleep((int)(Properties.Settings.Default.NoldusRatReponseSampleRate));

                x = _currentRatResponse;
            }

            //write the event of HeadEnterCenter to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterCenter);

            return (x == 2);
        }

        /// <summary>
        /// Updating the robot working JBI file according to the trajectory and the stimulus type.
        /// </summary>
        /// <param name="traj">The trajectory to be send to the controller.</param>
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        public void UpdateYasakawaRobotJBIFile(Tuple<Trajectory , Trajectory> traj , MotomanProtocolFileCreator.UpdateJobType updatJobType , bool inverse = false)
        {
            _logger.Info("Writing job to the robot.");

            //if need to inverse the trajectory in case of PostTrialStage backword trajcetory.
            if(inverse)
                traj = new Tuple<Trajectory, Trajectory>(TrajectoryInverse(traj.Item1), TrajectoryInverse(traj.Item2));

            //setting the trajectory for the JBI file creator and update the file that is being senf to the controller with the new commands.
            _motomanProtocolFileCreator.TrajectoryR1Position = traj.Item1;
            _motomanProtocolFileCreator.TrajectoryR2Position = traj.Item2;
            _motomanProtocolFileCreator.UpdateJobJBIFile(updatJobType);

            //Delete the old JBI file commands stored in the controller.
            try
            {
                _motomanController.DeleteJob("GAUSSIANMOVING2.JBI");
            }
            catch { }

            //wruite the new JBI file to the controller.
            _motomanController.WriteFile(@"C:\Users\User\Desktop\GAUSSIANMOVING2.JBI");

            _logger.Info("Writing job to the robot ended.");
        }

        /// <summary>
        /// Trajectory inversing function for inversing the points backwords.
        /// </summary>
        /// <param name="trajectory">The trajectory to inverse.</param>
        /// <returns>A new trajectory inversed from the original.</returns>
        private Trajectory TrajectoryInverse(Trajectory trajectory)
        {
            //the inversed trajectory to be returned.
            Trajectory inverseTrajectory = new Trajectory();

            //initialization for the the trajectorry yo be retund.
            int length = trajectory.x.Count;
            inverseTrajectory.x = Vector<double>.Build.Dense(length);
            inverseTrajectory.y = Vector<double>.Build.Dense(length);
            inverseTrajectory.z = Vector<double>.Build.Dense(length);
            inverseTrajectory.rx = Vector<double>.Build.Dense(length);
            inverseTrajectory.ry = Vector<double>.Build.Dense(length);
            inverseTrajectory.rz = Vector<double>.Build.Dense(length);

            //inverse the original trajectory into the new trajectory.
            for (int i = 0; i < length; i++)
            {
                int index = length - 1 - i;

                inverseTrajectory.x[i] = trajectory.x[index];
                inverseTrajectory.y[i] = trajectory.y[index];
                inverseTrajectory.z[i] = trajectory.z[index];
                inverseTrajectory.rx[i] = trajectory.rx[index];
                inverseTrajectory.ry[i] = trajectory.ry[index];
                inverseTrajectory.rz[i] = trajectory.rz[index];
            }

            //return the inversed trajectory.
            return inverseTrajectory;
        }

        /// <summary>
        /// Move the motoman with the given trajectory.
        /// </summary>
        public void MoveYasakawaRobotWithTrajectory()
        {
            _motomanController.StartJob("GAUSSIANMOVING2.JBI");
            _logger.Info("Moving the robot begin.");

            //wait for the commands to be executed.
            _motomanController.WaitJobFinished(10000);
            _logger.Info("Moving the robot finished.");
        }

        /// <summary>
        /// Determine the current trial stimulus type bt the stimulus type variable status.
        /// </summary>
        /// <returns>The stimulus type.</returns>
        public int DetermineCurrentStimulusType()
        {
            string stimulusTypeStatus = _variablesList._variablesDictionary["STIMULUS_TYPE"]._description["status"]._ratHouseParameter[0];
            switch (stimulusTypeStatus)
            {
                case "1"://static
                    return int.Parse(_variablesList._variablesDictionary["STIMULUS_TYPE"]._description["parameters"]._ratHouseParameter[0]);
                case "2"://varying
                    return (int)(_crossVaryingVals[_currentVaryingTrialIndex]["STIMULUS_TYPE"][0]);
            }
            return 0;
        }

        /// <summary>
        /// Detrmines all current tiral timings and delays acoording the time types statuses.
        /// </summary>
        /// <returns>Return the TrialTimings struct contains all the timings types.</returns>
        public TrialTimings DetermineCurrentTrialTimings()
        {
            TrialTimings currentTrialTimings;
            currentTrialTimings.wStartDelay = DetermineTimeByVariable("START_DELAY");

            currentTrialTimings.wRewardCenterDelay = DetermineTimeByVariable("REWARD_CENTER_DELAY");
            currentTrialTimings .wRewardRightDelay= DetermineTimeByVariable("REWARD_RIGHT_DELAY");
            currentTrialTimings.wRewardLeftDelay= DetermineTimeByVariable("REWARD_LEFT_DELAY");

            currentTrialTimings.wRewardCenterDuration = DetermineTimeByVariable("REWARD_CENTER_DURATION");
            currentTrialTimings.wRewardRightDuration = DetermineTimeByVariable("REWARD_RIGHT_DURATION");
            currentTrialTimings.wRewardLeftDuration = DetermineTimeByVariable("REWARD_LEFT_DURATION");

            currentTrialTimings.wRewardToBackwardDelay = DetermineTimeByVariable("REWARD_BACKWARD_TIME");

            currentTrialTimings.wPostTrialTime = DetermineTimeByVariable("POST_TRIAL_TIME");

            currentTrialTimings.wTimeOutTime = DetermineTimeByVariable("TIMEOUT_TIME");

            currentTrialTimings.wResponseTime = DetermineTimeByVariable("RESPONSE_TIME");

            currentTrialTimings.wDuration = DetermineTimeByVariable("STIMULUS_DURATION");

            return currentTrialTimings;
        }

        /// <summary>
        /// determine the current trial of the input type time by it's status (random , static , etc...).
        /// </summary>
        /// <param name="timeVarName">The time type to be compute.</param>
        /// <returns>The result time according to the type of the time.</returns>
        public double DetermineTimeByVariable(string timeVarName)
        {
            
           //detrmine variable value by the status of the time type.
           string timeValue = GetVariableValue(0, timeVarName);

            //if not found - it is random type varriable.
            if(timeValue == string.Empty)
            {
                double lowTime = double.Parse(_variablesList._variablesDictionary[timeVarName]._description["low_bound"]._ratHouseParameter[0]);
                double highTime = double.Parse(_variablesList._variablesDictionary[timeVarName]._description["high_bound"]._ratHouseParameter[0]);
                return RandomTimeUniformly(lowTime, highTime);
            }

            return double.Parse(timeValue);
        }

        private List<string> GetVaryingVariablesList()
        {
            List<string> varyingVariablesNames = new List<string>();

            foreach (string item in _variablesList._variablesDictionary.Keys)
            {
                if (_variablesList._variablesDictionary[item]._description["status"]._ratHouseParameter[0].Equals("2"))
                {
                    varyingVariablesNames.Add(_variablesList._variablesDictionary[item]._description["nice_name"]._ratHouseParameter[0].ToString());
                }
            }

            return varyingVariablesNames;
        }

        /// <summary>
        /// Determines the asked variable value at the current stage by it's status (not include random types).
        /// </summary>
        /// <param name="houseParameter">'0' for ratHouseParameter or '1' for landscapeHouseParameter.</param>
        /// <param name="parameterName">The parameter name to get the value for.</param>
        /// <returns>The value of the parameter at the current trial.</returns>
        public string GetVariableValue(int houseParameter , string parameterName)
        {
            //detrmine the status of the variable type.
            string variableStatus = _variablesList._variablesDictionary[parameterName]._description["status"]._ratHouseParameter[houseParameter];

            //decide the time value of the time type according to it's status.
            switch (variableStatus)
            {
                case "1"://static
                    return _variablesList._variablesDictionary[parameterName]._description["parameters"]._ratHouseParameter[houseParameter];

                case "2"://varying
                    return _crossVaryingVals[_currentVaryingTrialIndex][parameterName][houseParameter].ToString("000000.00000000");

                default:
                    return string.Empty;

            }

        }

        /// <summary>
        /// Random a double (4 precision) value uniformly by the given bounds.
        /// </summary>
        /// <param name="lowTime">The low bound.</param>
        /// <param name="highTime">The high bound.</param>
        /// <returns>The random 4 double precision in the bounded range.</returns>
        public double RandomTimeUniformly(double lowTime , double highTime)
        {
            //we cannot really have a randon double number because in uniform countinious distrbution the probability for any value is 0.
            //so mutiplt it by 1000 , make a random number , and party it by 1000 (4 digits precision).
            int lowTimeInteger = (int)(lowTime * 1000);
            int highTimeInteger = (int)(highTime * 1000);

            //get the random integer (the doubled rand time in 4 digits precison).
            int randTimeInteger = _timingRandomizer.Next(lowTimeInteger, highTimeInteger);

            //return the result.
            return (double)(randTimeInteger) / 1000;
        }

        /// <summary>
        /// The main stage - for the trial - moving robot , response and rewards.
        /// </summary>
        public void MainTimerStage()
        {
        }

        /// <summary>
        /// The post trial time - analyaing the response , saving all the trial data into the results file.
        /// <param name="duration1HeadInTheCenterStabilityStage">Indicated if one of the robots ot both moved due to suration 1 head in the center stability stage.</param>
        /// <returns>True if trial succeed otherwise returns false.</returns>
        /// </summary>
        public bool PostTrialStage(bool duration1HeadInTheCenterStabilityStage)
        {
            bool trialSucceed = true;

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Post trial time.");

            //if no answer in the response time or not even coming to tge response time.
            if (!FixationOnlyMode && !(_currentRatDecision.Equals(RatDecison.Left) || _currentRatDecision.Equals(RatDecison.Right)))
                //reset status of the current trial combination index if there was no reponse stage at all.
                //_varyingIndexSelector.ResetTrialStatus(_currentVaryingTrialIndex);
                trialSucceed = false;

            if (FixationOnlyMode && !_currentRatDecision.Equals(RatDecison.PassDurationTime))
                //reset status of the current trial combination index if there was no reponse stage at all.
                //_varyingIndexSelector.ResetTrialStatus(_currentVaryingTrialIndex);
                trialSucceed = false;

            //Task moveRobotHomePositionTask = Task.Factory.StartNew(() => MoveRobotHomePosition());

            //need to get the robot backword only if ther was a rat enterance that trigger thr robot motion.
            UpdateRobotHomePositionBackwordsJBIFile();
            Task moveRobotHomePositionTask;
            if (!duration1HeadInTheCenterStabilityStage)
                moveRobotHomePositionTask = Task.Factory.StartNew(() => NullFunction());
            else
                moveRobotHomePositionTask = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory());

            //save the dat into the result file only if the trial is within success trials (that have any stimulus)
            if (!_currentRatDecision.Equals(RatDecison.NoEntryToResponseStage))
            {
                Task.Run(() =>
                {
                    _savedExperimentDataMaker.SaveTrialDataToFile(new TrialData()
                    {
                        StaticVariables = _staticVariablesList,
                        VaryingVariables = _crossVaryingVals[_currentVaryingTrialIndex],
                        TimingsVariables = _currentTrialTimings,
                        RatName = RatName,
                        RatDecison = _currentRatDecision,
                        TrialNum = _totalHeadStabilityInCenterDuringDurationTime + _totalHeadFixationBreaks,
                        RRInverse = _inverseRRDecision,
                        AutosOptions = _autosOptionsInRealTime
                    });
                });
            }

            Thread.Sleep((int)(_currentTrialTimings.wPostTrialTime * 1000));

            //wait the maximum time of the postTrialTime and the going home position time.
            moveRobotHomePositionTask.Wait();

            return trialSucceed;
        }
        
        /// <summary>
        /// Null bumper function.
        /// </summary>
        private void NullFunction()
        {
        }

        /// <summary>
        /// Writing the home_pos file as the readen parameter in the configuration.
        /// </summary>
        public void WriteHomePosFile()
        {
            MotomanProtocolFileCreator homePositionFile = new MotomanProtocolFileCreator(@"C:\Users\User\Desktop\HOME_POS_BOTH.JBI");

            Position r1HomePosition = new Position();
            r1HomePosition.x=Properties.Settings.Default.R1OriginalX;
            r1HomePosition.y=Properties.Settings.Default.R1OriginalY;
            r1HomePosition.z=Properties.Settings.Default.R1OriginalZ;
            r1HomePosition.rx=Properties.Settings.Default.R1OriginalRX;
            r1HomePosition.ry=Properties.Settings.Default.R1OriginalRY;
            r1HomePosition.rz=Properties.Settings.Default.R1OriginalRZ;

            Position r2HomePosition = new Position();
            r2HomePosition.x = Properties.Settings.Default.R2OriginalX;
            r2HomePosition.y = Properties.Settings.Default.R2OriginalY;
            r2HomePosition.z = Properties.Settings.Default.R2OriginalZ;
            r2HomePosition.rx=Properties.Settings.Default.R2OriginalRX;
            r2HomePosition.ry=Properties.Settings.Default.R2OriginalRY;
            r2HomePosition.rz=Properties.Settings.Default.R2OriginalRZ;

            //update the home_pos_both file.
            homePositionFile.UpdateHomePosJBIFile(r1HomePosition , r2HomePosition , 60);
        }

        /// <summary>
        /// Move the robot to it's home (origin) position.
        /// </summary>
        public void MoveRobotHomePosition()
        {
            try
            {
                _motomanController.DeleteJob("HOME_POS_BOTH.JBI");
            }
            catch
            { }

            _motomanController.WriteFile(@"C:\Users\User\Desktop\HOME_POS_BOTH.JBI");

            _motomanController.StartJob("HOME_POS_BOTH.JBI");

            //should fix this bug
            //_motomanController.WaitJobFinished(10000);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Update the robot trajectory JBI file to it's home (origin) position.
        /// </summary>
        public void UpdateRobotHomePositionBackwordsJBIFile()
        {
            switch (_currentTrialStimulusType)
            {
                case 0://none
                    break;
                case 1://vistibular only.
                    UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.Both , true);
                    break;

                case 2://visual only.
                    UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R2Only , true);
                    break;

                case 3://vistibular and visual both.
                    UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R1Only , true);
                    break;

                default://if there is no motion , make a delay of waiting the duration time (the time that should take the robot to move).
                    break;
            }
        }

        /// <summary>
        /// Giving reward as specified (for the specified directions).
        /// </summary>
        /// <param name="value">The specified direction by xxxxxy-y-y where left-center-right.</param>
        /// <param name="continious">Make the reward continiously (open untill get a close value) or not continiously (by the time of REWARD_CENTER_DURATION parameter.</param>
        public void GiveRewardHandReward(byte value , bool continious = false)
        {
            if (continious)
            {
                _rewardController.WriteSingleSamplePort(true, value);
            }
            else
            {
                //wait the delat time before opening the water and make a sound if AutoSound is on.
                double timeByVariable = DetermineTimeByVariable("REWARD_CENTER_DURATION");
                if ((value & (byte)RatDecison.Left) == (byte)RatDecison.Left)
                {
                    if (AutoRewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Left"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_LEFT_DURATION");
                }
                else if ((value & 0x05) == 0x05)
                {
                    if (AutoRewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_CENTER_DURATION");
                }
                else if ((value & (byte)RatDecison.Center) == (byte)RatDecison.Center)
                {
                    if (AutoRewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_CENTER_DURATION");
                }
                else if ((value & (byte)RatDecison.Right) == (byte)RatDecison.Right)
                {
                    if (AutoRewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Right"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_RIGHT_DURATION");
                }

                _rewardController.WriteSingleSamplePort(true, value);
                Thread.Sleep((int)(timeByVariable * 1000));
                _rewardController.WriteSingleSamplePort(true, 0);
            }
        }
        
        /// <summary>
        /// An event raises every [Properties.Settings.Default.NoldusRatReponseSampleRate] second for sampling the rat head direction.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">Args.</param>
        private void SetRatReponse(object sender , System.Timers.ElapsedEventArgs args)
        {
            //update the variable saving the current rat head direction.
            _currentRatResponse = _ratResponseController.ReadSingleSamplePort();

            //only if the system is running , update the interactive window.
            if(Globals._systemState.Equals(SystemState.RUNNING))
                _mainGuiInterfaceControlsDictionary["SetNoldusRatResponseInteractivePanel"].BeginInvoke(_mainGuiControlsDelegatesDictionary["SetNoldusRatResponseInteractivePanel"] , _currentRatResponse);
        }

        /// <summary>
        /// Handler for raising interval time evemt for the water fill estimation panel.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Args.</param>
        void WaterRewardFillingTimer_Tick(object sender, EventArgs e)
        {
            _mainGuiInterfaceControlsDictionary["SetWaterRewardsMeasure"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["SetWaterRewardsMeasure"] , false);
        }

        /// <summary>
        /// Struct contains all the trial timings.
        /// </summary>
        public struct TrialTimings
        {
            /// <summary>
            /// The delay time between the rat head center and the trial start time.
            /// </summary>
            public double wStartDelay;

            /// <summary>
            /// The delay between the center head tracking (for the trial begin) and the center reward.
            /// </summary>
            public double wRewardCenterDelay;

            /// <summary>
            /// The delay between the right head tracking (for the trial begin) and the right reward.
            /// </summary>
            public double wRewardRightDelay;

            /// <summary>
            /// The delay between the left head tracking (for the trial begin) and the left reward.
            /// </summary>
            public double wRewardLeftDelay;

            /// <summary>
            /// The duration for the center reward.
            /// </summary>
            public double wRewardCenterDuration;

            /// <summary>
            /// The duration for the right reward.
            /// </summary>
            public double wRewardRightDuration;

            /// <summary>
            /// The duration for the left reward.
            /// </summary>
            public double wRewardLeftDuration;

            /// <summary>
            /// The delay between end of water reward to the begining of moving rovot to it's home position.
            /// </summary>
            public double wRewardToBackwardDelay;

            /// <summary>
            /// The duration to wait between the end of the previous trial and the begining of the next trial.
            /// </summary>
            public double wPostTrialTime;

            /// <summary>
            /// The time after the beep of the trial begin time and the time the rat can response with head to the center in order to begin the movement.
            /// </summary>
            public double wTimeOutTime;

            /// <summary>
            /// The time the rat have to response (with head to the left or to the right) after the rewardCenter (ig get).
            /// </summary>
            public double wResponseTime;

            /// <summary>
            /// The robot movement duration.
            /// </summary>
            public double wDuration;
        };

        /// <summary>
        /// Enum for the rat stimulus decision.
        /// </summary>
        public enum RatDecison
        {
            /// <summary>
            /// The rat decision could not happened due to no enterance to the reponse stage (because no stability or enterance to the center).
            /// </summary>
            NoEntryToResponseStage = -1,

            /// <summary>
            /// The rat doesn't decide anything (it's head was not in any of the sides).
            /// </summary>
            NoDecision = 0,

            /// <summary>
            /// The rat decide about the center.
            /// </summary>
            Center = 2,

            /// <summary>
            /// The rat decide about the left side.
            /// </summary>
            Left = 1,

            /// <summary>
            /// The rat decide about the right side.
            /// </summary>
            Right = 4,

            /// <summary>
            /// The rat passed the duration time fixation (movement) as a state.
            /// </summary>
            PassDurationTime = 5,

            /// <summary>
            /// DurationTime state.
            /// </summary>
            DurationTime = 6
        };

        /// <summary>
        /// Enum for the reward poition/direction.
        /// </summary>
        public enum RewardPosition
        {
            /// <summary>
            /// Reward to the center.
            /// </summary>
            Center = 0x02,

            /// <summary>
            /// Reward to the left.
            /// </summary>
            Left = 0x01,

            /// <summary>
            /// Reward to the right.
            /// </summary>
            Right = 0x04,
        };
        #endregion FUNCTIONS
    }
}
