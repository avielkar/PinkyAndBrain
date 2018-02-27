using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Params;
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
using AlphaOmegaSystem;
using InfraRedSystem;
using RatResponseSystem;
using Trajectories;
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
    public class ControlLoop:IDisposable
    {
        #region ATTRIBUTES
        /// <summary>
        /// The trajectory creator interface for making the trajectory for each trial.
        /// </summary>
        private ITrajectoryCreator _trajectoryCreator;

        /// <summary>
        /// The trajectory creation 
        /// </summary>
        private TrajectoryCreatorHandler _trajectoryCreatorHandler;

        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters.
        /// </summary>
        private List<Dictionary<string, double>> _crossVaryingVals;

        /// <summary>
        /// The static variables list in double value presentation.
        /// The string is for the variable name.
        /// </summary>
        private Dictionary<string, double> _staticVariablesList;

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
        public string ProtocolFullName;

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
        /// The name of the student that makes the experiment.
        /// </summary>
        public string StudentName { get; set; }

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
        /// The red color value for the leds.
        /// </summary>
        public int LEDcolorRed { get; set; }

        /// <summary>
        /// The green color value for the leds.
        /// </summary>
        public int LEDcolorGreen { get; set; }

        /// <summary>
        /// The blue color value for the leds.
        /// </summary>
        public int LEDcolorBlue { get; set; }

        /// <summary>
        /// Timer for raising event to sample the Noldus reponse direction and store it in _currentRatResponse.
        /// </summary>
        private System.Timers.Timer _ratSampleResponseTimer;

        /// <summary>
        /// Timer for raising event for counting the water the rat have rewarded so far.
        /// </summary>
        private System.Timers.Timer _waterRewardFillingTimer;

        /// <summary>
        /// The YASAKAWA motoman robot controller.
        /// </summary>
        private MotomanController _motomanController;

        /// <summary>
        /// The led controller for controlling the leds visibility in the ledstrip connected to the arduino.
        /// </summary>
        private LEDController _ledControllerRight;

        /// <summary>
        /// The second led controller for controlling the leds visibility in the ledstrip connected to the arduino.
        /// </summary>
        private LEDController _ledControllerLeft;

        /// <summary>
        /// The leds selector dor selecting different led to turn on.
        /// </summary>
        private LedsSelector _ledSelectorRight;

        /// <summary>
        /// The leds selector dor selecting different led to turn on.
        /// </summary>
        private LedsSelector _ledSelectorLeft;

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
        /// The task executing the robot forward motion untill finished.
        /// </summary>
        private Task _robotMotionTask;

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
        public bool RewardSound { get; set; }

        /// <summary>
        /// Indicates if the mode of the trial is only untill the fixation stage (include).
        /// </summary>
        public bool FixationOnlyMode { get; set; }

        /// <summary>
        /// Indicates if to enable fixation break sound.
        /// </summary>
        public bool EnableFixationBreakSound { get; set; }

        /// <summary>
        /// Indicates whethear to enable error sound for a wrong choice.
        /// </summary>
        public bool EnableErrorSound { get; set; }

        /// <summary>
        /// Indicates if to enable clue sound in both sides.
        /// </summary>
        public bool EnableClueSoundInBothSide { get; set; }

        /// <summary>
        /// Indicates if to enable that right parameters values and left parameters values must be equals.
        /// </summary>
        public bool EnableRightLeftMustEquals { get; set; }

        /// <summary>
        /// Indicates if to enable clue sound only in the correct side.
        /// </summary>
        public bool EnableClueSoundInCorrectSide { get; set; }

        /// <summary>
        /// Indicates the autos options that are commanded in the real time (when the code use it at the conditions and not only if the user change it betweens).
        /// </summary>
        public AutosOptions _autosOptionsInRealTime { get; set; }

        /// <summary>
        /// Indicates t he special modes that are commanded in the real time.
        /// </summary>
        public SpecialModes _specialModesInRealTime { get; set; }

        /// <summary>
        /// Indicated if to give the rat a second response chance if it wrong anser at the first time (but not include no answer).
        /// </summary>
        public bool SecondResponseChance { get; set; }

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
        public ControlLoop(MLApp.MLApp matlabApp , MotomanController motomanController , LEDController ledController , LEDController ledController2 , InfraRedController infraRedController, Dictionary<string, Delegate> ctrlDelegatesDic, Dictionary<string , Control> mainGuiInterfaceControlsDictionary , ILog logger)
        {
            _matlabApp = matlabApp;
            _trajectoryCreatorHandler = new TrajectoryCreatorHandler(_matlabApp);

            //copy the logger reference to writing lof information
            _logger = logger;
            
            _rewardController = new RewardController("Dev1" , "Port1" ,"Line0:2", "RewardChannels");
            _ratResponseController = new RatResponseController("Dev1", "Port0", "Line0:2", "RatResponseChannels");
            _alphaOmegaEventsWriter = new AlphaOmegaEventsWriter("Dev1", "Port0", "Line3:7", "AlphaOmegaEventsChannels" , "Port1" , "Line3" , "AlphaOmegaStrobeChannel" , _logger);
            _infraredController = infraRedController;
            
            _stopAfterTheEndOfTheCurrentTrial = false;
            
            //configure  rge timer for the sampling Noldus rat response direction.
            _ratSampleResponseTimer = new System.Timers.Timer(Properties.Settings.Default.NoldusRatReponseSampleRate);
            _ratSampleResponseTimer.Elapsed += SetRatReponse;
            
            //configure the water filling timer for the water reward estimation interactive window.
            _waterRewardFillingTimer = new System.Timers.Timer();
            _waterRewardFillingTimer.Interval = 100;
            _waterRewardFillingTimer.Elapsed += WaterRewardFillingTimer_Tick;

            //take the motoman controller object.
            _motomanController = motomanController;

            //take the led controller object.
            _ledControllerRight = ledController;
            _ledControllerLeft = ledController2;
            //initialize the leds index selector.
            _ledSelectorRight = new LedsSelector(150 , 10);
            _ledSelectorLeft = new LedsSelector(150 , 10);

            //initialize the savedDataMaker object once.
            _savedExperimentDataMaker = new SavedDataMaker();

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
        #endregion CONTRUCTORS

        #region FUNCTIONS

        #region GUI_COMMANDS
        /// <summary>
        /// Transfer the control from the main gui to the control loop until a new gui event is handled by the user.
        /// </summary>
        public void Start(Variables variablesList, List<Dictionary<string, double>> crossVaryingList, Dictionary<string, double> staticVariablesList, int frequency, ITrajectoryCreator trajectoryCreatorName)
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
            _motomanController.MotomanProtocolFileCreator.Frequency = _frequency;

            //create a new results file for the new experiment.
            _savedExperimentDataMaker.CreateControlNewFile(RatName);

            //clear and initialize the psyco online graph.
            _onlinePsychGraphMaker.Clear();
            _onlinePsychGraphMaker.VaryingParametrsNames = GetVaryingVariablesList();
            _onlinePsychGraphMaker.HeadingDireactionRegion = new Region
            {
                LowBound = double.Parse(_variablesList._variablesDictionary["HEADING_DIRECTION"]._description["low_bound"]._ratHouseParameter),
                Increament = double.Parse(_variablesList._variablesDictionary["HEADING_DIRECTION"]._description["increament"]._ratHouseParameter),
                HighBound = double.Parse(_variablesList._variablesDictionary["HEADING_DIRECTION"]._description["high_bound"]._ratHouseParameter)
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
        #endregion

        #region STAGES_FUNCTION
        public void MainControlLoop()
        {
            _logger.Info("Main ControlLoop begin.");

            //set robot servo on and go homeposition.
            try
            {
                _motomanController.SetServoOn();
            }
            catch 
            {
                MessageBox.Show("Cannot set the servos on - check if robot is conncted in play mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

                        //Sending all needed data to all interfaces and makes the beep sound.
                        PreTrialStage();

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
                                    RewardCenterStage(AutoReward, RewardSound);

                                    //if not to skip all stages after the fixation stage.
                                    if (!FixationOnlyMode)
                                    {
                                        //clue time stage if needed.
                                        ClueResponseStage();

                                        //wait the rat to response to the movement during the response time.
                                        Tuple<RatDecison, bool> decision = ResponseTimeStage();

                                        //second reward stage (condition if needed in the stage) with false for second chance response.
                                        SecondRewardStage(decision, AutoReward , false);

                                        //if second oppertunity for choice after wrong choice is available.
                                        if(SecondResponseChance && !AutoReward && !decision.Item1.Equals(RatDecison.NoDecision) && decision.Item2 == false)
                                        {
                                            Tuple<RatDecison, bool> secondDecision = SecondChanceResponseTimeStage();

                                            //second reward stage with flag indicate that it was a second chance.
                                            SecondRewardStage(secondDecision, AutoReward, true);
                                        }
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
                        else//if the the trual not succeed - choose another varting index randomly and not the same index (also , if stick number exists make it only of happens at the first time fot the stick on index).
                        {
                            if(_stickOnNumberIndex == 0)
                            {
                                //reset the current trial varying index because it was not successful.
                                _varyingIndexSelector.ResetTrialStatus(_currentVaryingTrialIndex);

                                //choose the random combination index for the current trial.
                                _currentVaryingTrialIndex = _varyingIndexSelector.ChooseRandomCombination();

                                //craetes the trajectory for both robots for the current trial if not one of the training protocols.
                                _currentTrialTrajectories = _trajectoryCreatorHandler.CreateTrajectory(_currentVaryingTrialIndex);
                            }

                        }
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
        /// Initializes the variables , points , trajectories , random varibles ,  etc.
        /// </summary>
        public void InitializationStage()
        {
            //TODO : change the index of the trial to be identical to the trial number in the result file.
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
            //initialize the trial mode options.
            _specialModesInRealTime = new SpecialModes();

            //updatre the trial number for the motoman protocol file creator to send it to the alpha omega.
            //_motomanController.MotomanProtocolFileCreator.TrialNum = _totalHeadStabilityInCenterDuringDurationTime + 1;
            //the adiitiom of 1 is because the ++ of one of them is only at the end of the movement.
            _motomanController.MotomanProtocolFileCreator.TrialNum = _totalHeadStabilityInCenterDuringDurationTime + _totalHeadFixationBreaks + 1;
        }

        /// <summary>
        /// Pre trial stage for sending all pre data for the current trial.
        /// </summary>
        public void PreTrialStage()
        {
            _logger.Info("Pre trail stage begin.");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Intialization");

            _specialModesInRealTime.EnableRightLeftMustEquals = EnableRightLeftMustEquals;

            Task sendDataToRobotTask = new Task(()=>
            {
                SendDataToRobots();
            });

            Task SendDataToLedControllersTask = new Task(()=>
            {
                SendDataToLedControllers();
            });

            Task preTrialWaitingTask = new Task(()=>
            {
                Thread.Sleep((int)(1000*_currentTrialTimings.wPreTrialTime));
            });

            sendDataToRobotTask.Start();
            SendDataToLedControllersTask.Start();
            preTrialWaitingTask.Start();

            Task.WaitAll(preTrialWaitingTask, sendDataToRobotTask, SendDataToLedControllersTask);
        }

        /// <summary>
        /// A stage the rat gets a clue where the correct answer is.
        /// </summary>
        public void ClueResponseStage()
        {
            _logger.Info("ClueResponseStage begin. EnableClueSoundInBothSide = " + EnableClueSoundInBothSide + ";EnableClueSoundInCorrectSide" + EnableClueSoundInCorrectSide + ".");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Clue Stage");

            //determine the current trial correct answer.
            DetermineCurrentStimulusAnswer();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            while (sw.ElapsedMilliseconds < 1000 * (int)(_currentTrialTimings.wClueDelay))
            {
                
            }

            if (EnableClueSoundInBothSide)
            {
                _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"];
                _windowsMediaPlayer.controls.play();

                _specialModesInRealTime.EnableClueSoundInBothSide = true;
            }

            else if ( EnableClueSoundInCorrectSide)
            {
                if (_correctDecision.Equals(RatDecison.Right))
                {
                    _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Right"];
                    _windowsMediaPlayer.controls.play();
                }

                else if (_correctDecision.Equals(RatDecison.Left))
                {
                    _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Left"];
                    _windowsMediaPlayer.controls.play();
                }

                _specialModesInRealTime.EnableClueSoundInCorrectSide = true;
            }

            _logger.Info("ClueResponseStage ended.");
        }

        /// <summary>
        /// Waiting the rat to response the movement direction abd update the _totalCorrectAnswers counter.
        /// <returns>The rat decision value and it's correctness.</returns>
        /// </summary>
        public Tuple<RatDecison, bool> ResponseTimeStage()
        {
            _logger.Info("ResponseTimeStage begin.");
            
            //if not trainig continue.
            if (GetVariableValue("STIMULUS_TYPE") == "0")
            {
                //delete that line for stim 0.
                //return new Tuple<RatDecison, bool>(RatDecison.NoDecision, false);
            }


            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Waiting for Response");

            //get the current stimulus direction.
            double currentHeadingDirection = double.Parse(GetVariableValue("HEADING_DIRECTION"));

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //time to wait for the moving rat response. if decided about a side so break and return the decision and update the _totalCorrectAnsers.
            while (sw.ElapsedMilliseconds < 1000 * (int)(_currentTrialTimings.wResponseTime))
            {
                if (_currentRatResponse == (byte)RatDecison.Left)
                {
                    //increase the total choices for wromg or correct choices (some choices).
                    _totalChoices++;

                    //update the current rat decision state.
                    _currentRatDecision = RatDecison.Left;

                    //write the event that te rat enter it's head to the left to the AlphaOmega.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterLeft);

                    if (_correctDecision.Equals(RatDecison.Left))
                    {
                        //increase the total correct answers.
                        _totalCorrectAnswers++;

                        //update the psycho online graph.
                        _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType, currentHeadingDirection, AnswerStatus.CORRECT);

                        _logger.Info("ResponseTimeStage ended. RatDecison = RatDecison.Left" + "; Correct = True.");
                        return new Tuple<RatDecison, bool>(RatDecison.Left, true);
                    }

                    _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType, currentHeadingDirection, AnswerStatus.WRONG);

                    if (EnableErrorSound)
                    {
                        //error sound if needed.
                        Task.Run(() =>
                        {
                            _windowsMediaPlayer.URL = _soundPlayerPathDB["WrongAnswer"];
                            _windowsMediaPlayer.controls.play();
                        });

                        _specialModesInRealTime.ErrorChoiceSouunOn = true;
                    }

                    _logger.Info("ResponseTimeStage ended. RatDecison = RatDecison.Left" + "; Correct = False.");
                    return new Tuple<RatDecison, bool>(RatDecison.Left, false);
                }

                else if (_currentRatResponse == (byte)RatDecison.Right)
                {
                    //update current rat decision state.
                    _currentRatDecision = RatDecison.Right;

                    //increase the total choices for wromg or correct choices (some choices).
                    _totalChoices++;

                    //write the event that te rat enter it's head to the right to the AlphaOmega.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterRight);

                    if (_correctDecision.Equals(RatDecison.Right))
                    {
                        //increase the total coreect answers.
                        _totalCorrectAnswers++;

                        //update the psycho online graph.
                        _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType, currentHeadingDirection, AnswerStatus.CORRECT);

                        _logger.Info("ResponseTimeStage ended. RatDecison = RatDecison.Right" + "; Correct = True.");
                        return new Tuple<RatDecison, bool>(RatDecison.Right, true);
                    }

                    //update the psycho online graph.
                    _onlinePsychGraphMaker.AddResult("Heading Direction", _currentTrialStimulusType, currentHeadingDirection, AnswerStatus.WRONG);

                    //error sound if needed.
                    if (EnableErrorSound)
                    {
                        Task.Run(() =>
                        {
                            _windowsMediaPlayer.URL = _soundPlayerPathDB["WrongAnswer"];
                            _windowsMediaPlayer.controls.play();
                        });

                        _specialModesInRealTime.ErrorChoiceSouunOn = true;
                    }

                    _logger.Info("ResponseTimeStage ended. RatDecison = RatDecison.Right" + "; Correct = False.");
                    return new Tuple<RatDecison, bool>(RatDecison.Right, false);
                }
            }

            _logger.Info("ResponseTimeStage ended. RatDecison = RatDecison.NoDecision; Correct = False.");
            _currentRatDecision = RatDecison.NoDecision;
            return new Tuple<RatDecison, bool>(RatDecison.NoDecision, false);
        }
        
        /// <summary>
        /// Waiting the rat to second chance response the movement direction with no updating the _totalCorrectAnswers counter and the result psycho graph.
        /// <returns>The rat decision value and it's correctness.</returns>
        /// </summary>
        public Tuple<RatDecison , bool> SecondChanceResponseTimeStage()
        {
            //Thread.Sleep(1000*(int)(_currentTrialTimings.wResponseTime));

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Waiting for Second Chance Response");

            //save the second chance response is on.
            _specialModesInRealTime.SecondChoice = true;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //time to wait for the moving rat response. if decided about a side so break and return the decision and update the _totalCorrectAnsers.
            while (sw.ElapsedMilliseconds < 1000 * (int)(_currentTrialTimings.wResponseTime))
            {
                if (_currentRatResponse == (byte)_correctDecision)
                {
                    //write the event that te rat enter it's head to the left to the AlphaOmega.
                    if (_correctDecision == RatDecison.Left)
                    {
                        _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterLeftSecondChance);

                        return new Tuple<RatDecison, bool>(RatDecison.Left, true);
                    }
                    else
                    {
                        _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadEnterRightSecondChance);

                        return new Tuple<RatDecison, bool>(RatDecison.Right, true);
                    }
                }
            }

            //if no decision or no error correction.
            _currentRatDecision = RatDecison.NoDecision;

            return new Tuple<RatDecison, bool>(RatDecison.NoDecision, false);
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
        /// <param name="secondChance">Indicates if it was a second chance response.</param>
        public void SecondRewardStage(Tuple<RatDecison, bool> decision,  bool autoReward = false , bool secondChance = false)
        {
            _logger.Info("SecondRewardStage begin. AutoReward = " + AutoReward + "; SecondChance =" + secondChance + ".");

            //check if the decision was correct and reward the rat according that decision.
            if (decision.Item2)
            {
                //reward the choosen side cellenoid.
                switch (decision.Item1)
                {
                    case RatDecison.Center:
                        RewardCenterStage(false , RewardSound);
                        break;
                    case RatDecison.Left:
                        RewardLeftStage(false, RewardSound, secondChance);
                        break;
                    case RatDecison.Right:
                        RewardRightStage(false, RewardSound, secondChance);
                        break;
                    default:
                        break;
                }
            }

            //if to give reward no matter whether the response was correct or not.
            else if (AutoReward)
            {
                //get the current stimulus direction.
                double currentHeadingDirection = double.Parse(GetVariableValue("HEADING_DIRECTION"));

                //determine the current stimulus direaction.
                RatDecison currentStimulationSide = (currentHeadingDirection == 0) ? (RatDecison.Center) : ((currentHeadingDirection > 0) ? (RatDecison.Right) : RatDecison.Left);

                //make the rat decision be correct answer.
                _currentRatDecision = _correctDecision;

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
                        RewardLeftStage(true , RewardSound , false);
                        break;

                    case RatDecison.Right:
                        RewardRightStage(true , RewardSound , false);
                        break;

                    default:
                        break;
                }

                //save the auto reward option true.
                _autosOptionsInRealTime.AutoReward = true;
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

            _logger.Info("SecondRewardStage ended.");
        }

        /// <summary>
        /// The reward left stage is happening if the rat decide the correct stimulus side.
        /// <param name="autoReward">Indecation if to give the reward with no delay.</param>
        /// <param name="RewardSound">Indecation ig to give the reward sound during the reward.</param>
        /// <param name="secondChance">Indicate if it is reward for the second chance.</param>
        /// </summary>
        public void RewardLeftStage(bool autoReward = false , bool RewardSound = false , bool secondChance = false)
        {
            _logger.Info("RewardLeftStage begin with AutoReward  = " + autoReward + ".");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Getting Reward (Left)");

            if (!secondChance)
                Reward(RewardPosition.Left, _currentTrialTimings.wRewardLeftDuration, _currentTrialTimings.wRewardLeftDelay, autoReward, RewardSound);
            else
                Reward(RewardPosition.Left, _currentTrialTimings.wRewardLeftDurationSecondChance, _currentTrialTimings.wRewardLeftDelaySecondChance, false, RewardSound);

            //write that the rat get left reward to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.LeftReward);

            _logger.Info("RewardLeftStage ended.");
        }

        /// <summary>
        /// The reward right stage is happening if the rat decide the correct stimulus side.
        /// <param name="autoReward">Indecation if to give the reward with no delay.</param>
        /// <param name="RewardSound">Indecation ig to give the suto reard sound during the reward.</param>
        /// <param name="secondChance">Indicate if it is reward for the second chance.</param>
        /// </summary>
        public void RewardRightStage(bool autoReward = false, bool RewardSound = false , bool secondChance = false)
        {
            _logger.Info("RewardRightStage begin with AutoReward  = " + autoReward + ".");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Getting Reward (Right)");

            if (!secondChance)
                Reward(RewardPosition.Right, _currentTrialTimings.wRewardRightDuration, _currentTrialTimings.wRewardRightDelay, autoReward, RewardSound);
            else
                Reward(RewardPosition.Right, _currentTrialTimings.wRewardRightDurationSecondChance, _currentTrialTimings.wRewardRightDelaySecondChance, false, RewardSound);

            //write that the rat get right reward to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.RightReward);

            _logger.Info("RewardRightStage ended.");
        }

        /// <summary>
        /// The reward center stage is happening if the rat head was consistently stable in the center during the movement.
        /// <param name="autoReward">Indecation if to give the reward with no delay.</param>
        /// <param name="autoRewardSound">Indecation ig to give the suto reard sound during the reward.</param>
        /// </summary>
        public void RewardCenterStage(bool autoReward = false, bool autoRewardSound = false , bool secondChance = false)
        {
            _logger.Info("RewardCenterStage begin with AutoReward  = " + autoReward + ".");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Getting Reward (Center)");

            Reward(RewardPosition.Center, _currentTrialTimings.wRewardCenterDuration, _currentTrialTimings.wRewardCenterDelay, autoReward ,autoRewardSound);

            //write that the rat get center reward to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.CenterReward);

            _logger.Info("RewardCenterStage ended.");
        }

        /// <summary>
        /// The stage for the time between the end of the reward to the beginnig moving tohe robot to it's home position.
        /// </summary>
        private void RewardToBackwardDelayStage()
        {
            _logger.Info("RewardToBackwardDelayStage begin.");

            Thread.Sleep((int)(_currentTrialTimings.wRewardToBackwardDelay*1000));

            _logger.Info("RewardToBackwardDelayStage ended.");
        }

        /// <summary>
        /// Moving the robot stage (it the rat enter the head to the center in the timeOut time and was stable in the center for startDelay time).
        /// This function also , in paralleled to the robot moving , checks that the rat head was consistently in the center during the duration time of the movement time.
        /// </summary>
        /// <returns>True if the head was stable consistently in the center during the movement time.</returns>
        public bool MovingTheRobotDurationWithHeadCenterStabilityStage()
        {
            _logger.Info("Moving the robot with duration time and head center stability check stage is begin.");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Stimulus Duration");

            //start moving the robot according to the stimulus type.
            _logger.Info("Send Executing robot trajectory data starty command");
            _robotMotionTask.Start();

            //write alpha omega that the stimulus start.
            Task.Run(() =>
            {
                WriteAlphaOmegaStimulusBegin();
            });

            //execute the leds command if necessary.
            if (_currentTrialStimulusType == 2 ||
                _currentTrialStimulusType == 3 ||
                _currentTrialStimulusType == 4 ||
                _currentTrialStimulusType == 5)
            {
                Task.Run(() =>
                {
                    ExecuteLedControllersCommand();
                });
            }

            //also run the rat center head checking in parallel to the movement time.
            bool headInCenterAllTheTime = true;
            Task.Run(() =>
            {
                while (!_robotMotionTask.IsCompleted)
                {
                    //sample the signal indicating if the rat head is in the center only 60 time per second (because the refresh rate of the signal is that frequency).
                    Thread.Sleep((int)(Properties.Settings.Default.NoldusRatReponseSampleRate));
                    if (_currentRatResponse != 2 && headInCenterAllTheTime)
                    {
                        _logger.Info("Break fixation during the movement.");

                        headInCenterAllTheTime = false;

                        if (!AutoFixation)
                        {
                            if (EnableFixationBreakSound)
                            //sound the break fixation sound - aaaahhhh sound.
                            //TODO: check if need here a task.
                            {
                                _windowsMediaPlayer.URL = _soundPlayerPathDB["MissingAnswer"];
                                _windowsMediaPlayer.controls.play();
                            }

                            //save the state of the enable fixation break sound on.
                            _specialModesInRealTime.BreakFixationSoundOn = EnableFixationBreakSound;

                            //write the break fixation event to the AlphaOmega.
                            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.HeadStabilityBreak);
                        }
                        _autosOptionsInRealTime.AutoFixation = AutoFixation;
                    }
                }
            });

            //wait the robot task to finish the movement.
            if (_currentTrialStimulusType != 0)
            {
                _robotMotionTask.Wait();
            }

            //also send the AlphaOmega that motion forward ends.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.RobotEndMovingForward);

            _logger.Info("End MovingTheRobotDurationWithHeadCenterStabilityStage with AutoFixation = " + AutoFixation + ".");
            //return the true state of the heading in the center stability during the duration time or always true when AutoFixation.
            return headInCenterAllTheTime || AutoFixation;
        }

        /// <summary>
        /// Stage to check (after the rat enter the head to the center) that the head is stable in the center for startDelay time.
        /// </summary>
        /// <returns></returns>
        public bool CheckDuration1HeadInTheCenterStabilityStage()
        {
            _logger.Info("Head in the center stability stage is begin.");

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
                        _logger.Info("Breaking head fixation during the stability stage occured.");

                        return false;
                    }
                }
            }

            _logger.Info("Heading stability stage successfull with AutoFixation = " + AutoFixation + ".");

            return true;
        }

        /// <summary>
        /// Stage for checing if the rat enter the first time the head to the center in order to start the movement.
        /// </summary>
        /// <returns>True if the rat enter the head to the center during the limit of the timeoutTime.</returns>
        public bool WaitForHeadEnteranceToTheCenterStage()
        {
            //Sounds the start beep. Now waiting for the rat to move it's head to the center.
            Console.Beep(2000, 200);

            //write the beep start event to the AlphaOmega.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.AudioStart);

            _logger.Info("Waiting for Head to be entered to the center stage for the first time.");

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Waiting for rat to start trial");

            //if autoStart is checked than should not wait for the rat to enter it's head to the center for starting.
            _autosOptionsInRealTime.AutoStart = AutoStart;
            if(AutoStart)
            {
                _logger.Info("AutoStart is On so no wait.");

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

            if (x == 2)
                _logger.Info("Waiting for Head to be entered to the center for the first occured.");
            else
                _logger.Info("Waiting for Head to be entered to the center stage for the first time expired.");

            return (x == 2);
        }
        
        /// <summary>
        /// The post trial time - analyaing the response , saving all the trial data into the results file.
        /// <param name="duration1HeadInTheCenterStabilityStage">Indicated if one of the robots ot both moved due to suration 1 head in the center stability stage.</param>
        /// <returns>True if trial succeed otherwise returns false.</returns>
        /// </summary>
        public bool PostTrialStage(bool duration1HeadInTheCenterStabilityStage)
        {
            _logger.Info("PostTrialStage begin.");

            bool trialSucceed = true;

            //update the global details listview with the current stage.
            _mainGuiInterfaceControlsDictionary["UpdateGlobalExperimentDetailsListView"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["UpdateGlobalExperimentDetailsListView"], "Current Stage", "Post trial time.");

            //save the fixation only mode
            _specialModesInRealTime.FixationOnly = FixationOnlyMode;

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
            {
                _logger.Info("Backward not executed because duration1HeadInTheCenterStabilityStage = false. Calling NullFunction().");
                moveRobotHomePositionTask = Task.Factory.StartNew(() => NullFunction());
            }
            else
            {
                moveRobotHomePositionTask = Task.Factory.StartNew(() => _motomanController.MoveYasakawaRobotWithTrajectory());

                //also send the AlphaOmega that motion backward starts.
                _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.RobotStartMovingBackward);
            }

            //save the dat into the result file only if the trial is within success trials (that have any stimulus)
            if (!_currentRatDecision.Equals(RatDecison.NoEntryToResponseStage))
            {
                Task.Run(() =>
                {
                    _logger.Info("Saving trial# " + (_totalHeadStabilityInCenterDuringDurationTime + _totalHeadFixationBreaks) + "to the result file.");
                    _savedExperimentDataMaker.SaveTrialDataToFile(new TrialData()
                    {
                        StaticVariables = _staticVariablesList,
                        VaryingVariables = _crossVaryingVals[_currentVaryingTrialIndex],
                        TimingsVariables = _currentTrialTimings,
                        ProtocolName = ProtocolFullName,
                        RatName = RatName,
                        StudentName =StudentName,
                        RatDecison = _currentRatDecision,
                        TrialNum = _totalHeadStabilityInCenterDuringDurationTime + _totalHeadFixationBreaks,
                        StickOnNumber = NumOfStickOn,
                        NumOfRepetitions = NumOfRepetitions,
                        RRInverse = _inverseRRDecision,
                        AutosOptions = _autosOptionsInRealTime,
                        SpecialModes = _specialModesInRealTime,
                        LedsData = new LedsData {TurnsOnPercentage = PercentageOfTurnedOnLeds , Brightness = LEDBrightness , RedValue = LEDcolorRed , GreenValue = LEDcolorGreen , BlueValue = LEDcolorBlue}
                    });
                });
            }

            Thread.Sleep((int)(_currentTrialTimings.wPostTrialTime * 1000));

            //wait the maximum time of the postTrialTime and the going home position time.
            moveRobotHomePositionTask.Wait();
            //also send the AlphaOmega that motion backwards ends.
            _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.RobotEndMovingBackward);

            _logger.Info("PostTrialStage ended. TrialSucceed = " + trialSucceed + ".");
            return trialSucceed;
        }
        #endregion

        #region STAGES_ADDIION_FUNCTION
        /// <summary>
        /// Sending the leds data to the leds controllers (without execution).
        /// </summary>
        private void SendDataToLedControllers()
        {
            _logger.Info("Send data to LEDs controller begin.");

            LEDsData ledsDataRight;
            LEDsData ledsDataLeft;

            double coherenceLeftStrip = double.Parse(GetVariableValue("COHERENCE_LEFT_STRIP"));
            double coherenceRightStrip = double.Parse(GetVariableValue("COHERENCE_RIGHT_STRIP"));

            //The motion of the Yasakawa robot if needed as the current stimulus type (if is both visual&vestibular -3 or only vistibular-1).
            switch (_currentTrialStimulusType)
            {
                case 0://none
                    break;

                case 1://vistibular only.
                    break;

                case 2://visual only.
                case 3://vistibular and visual both.
                case 4://vistibular and visual both with delta+ for visual.
                case 5://vistibular and visual both with delta+ for vistibular.
                    ledsDataRight = new LEDsData((byte)LEDBrightness, (byte)(LEDcolorRed), (byte)(LEDcolorGreen), (byte)(LEDcolorBlue), _ledSelectorRight.FillWithBinaryRandomCombination(PercentageOfTurnedOnLeds, coherenceRightStrip));
                    _ledControllerRight.LEDsDataCommand = ledsDataRight;
                    _ledControllerRight.SendData();
                    ledsDataLeft = new LEDsData((byte)LEDBrightness, (byte)(LEDcolorRed), (byte)(LEDcolorGreen), (byte)(LEDcolorBlue), _ledSelectorLeft.FillWithBinaryRandomCombination(PercentageOfTurnedOnLeds, coherenceLeftStrip));
                    _ledControllerLeft.LEDsDataCommand = ledsDataLeft;
                    _ledControllerLeft.SendData();
                    break;

                case 10://visual only in the dark.
                    break;

                case 11://combined in the dark.
                    break;

                default://if there is no motion , make a delay of waiting the duration time (the time that should take the robot to move).
                    break;
            }

            _logger.Info("Send data to LEDs controller end.");
        }

        /// <summary>
        /// Executing the leds command (tell the controllers to execute the commands have sent to them before).
        /// </summary>
        private void ExecuteLedControllersCommand()
        {
            Task.Run(() =>
            {
                _logger.Info("New Data Leds Execution for COHERENCE frame for LedController1");
                _ledControllerRight.ExecuteAllFrames();
            });

            Task.Run(() =>
            {
                _logger.Info("New Data Leds Execution for COHERENCE frame for LedController2");
                _ledControllerLeft.ExecuteAllFrames();
            });
        }
        
        /// <summary>
        /// Writing the stimulus type to the AlphaOmega according to the current stimulus type.
        /// </summary>
        private void WriteAlphaOmegaStimulusBegin()
        {
            _logger.Info("Writing AlphaOmega stimulus event start");

            switch (_currentTrialStimulusType)
            {
                case 0://none
                    break;
                case 1://vistibular only.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart1);
                    break;

                case 2://visual only.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart2);
                    break;

                case 3://vistibular and visual both.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart3);
                    break;

                case 4://vistibular and visual both with delta+ for visual.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart4);
                    break;

                case 5://vistibular and visual both with delta+ for vistibular.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart5);
                    break;

                case 10://visual only in the dark.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart10);
                    break;

                case 11://combined in the dark.
                    _alphaOmegaEventsWriter.WriteEvent(true, AlphaOmegaEvent.StimulusStart11);
                    break;

                default://if there is no motion , make a delay of waiting the duration time (the time that should take the robot to move).
                    break;
            }
        }

        /// <summary>
        /// Sending the data trajectories to the robots according to the current trial stimulus type (without executing).
        /// </summary>
        private void SendDataToRobots()
        {
            _logger.Info("Sending trajectories data to robots begin.");
            //The motion of the Yasakawa robot if needed as the current stimulus type (if is both visual&vestibular -3 or only vistibular-1).
            switch (_currentTrialStimulusType)
            {
                case 0://none
                    _robotMotionTask = Task.Factory.StartNew(() => Thread.Sleep((int)(1000 * _currentTrialTimings.wDuration)));
                    break;
                case 1://vistibular only.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.Both);
                    _robotMotionTask = new Task(() => _motomanController.MoveYasakawaRobotWithTrajectory());
                    break;

                case 2://visual only.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R2Only);
                    _robotMotionTask = new Task(() => _motomanController.MoveYasakawaRobotWithTrajectory());
                    break;

                case 3://vistibular and visual both.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R1Only);
                    _robotMotionTask = new Task(() => _motomanController.MoveYasakawaRobotWithTrajectory());
                    break;

                case 4://vistibular and visual both with delta+ for visual.
                case 5://vistibular and visual both with delta+ for vistibular.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    double deltaHeading = 0;
                    if (_staticVariablesList.ContainsKey("DELTA "))
                        deltaHeading = _staticVariablesList["DELTA "];
                    else if (_crossVaryingVals[_currentVaryingTrialIndex].Keys.Contains("DELTA "))
                        deltaHeading = _crossVaryingVals[_currentVaryingTrialIndex]["DELTA "];
                    //if delta is 0 move only the R1 robot.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, (deltaHeading != 0) ? MotomanProtocolFileCreator.UpdateJobType.Both : MotomanProtocolFileCreator.UpdateJobType.R1Only);
                    _robotMotionTask = new Task(() => _motomanController.MoveYasakawaRobotWithTrajectory());
                    break;

                case 10://visual only in the dark.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R2Only);
                    _robotMotionTask = new Task(() => _motomanController.MoveYasakawaRobotWithTrajectory());
                    break;

                case 11://combined in the dark.
                    //first update the JBI file in seperately  , and after that negin both moving the robot and play with the leds for percisely simulatenously.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R1Only);
                    _robotMotionTask = new Task(() => _motomanController.MoveYasakawaRobotWithTrajectory());
                    break;

                default://if there is no motion , make a delay of waiting the duration time (the time that should take the robot to move).
                    _robotMotionTask = new Task(() => Thread.Sleep((int)(1000 * _currentTrialTimings.wDuration)));
                    break;
            };
            _logger.Info("Sending trajectories data to robots end.");
        }

        /// <summary>
        /// Determine the current trial correct side.
        /// </summary>
        public void DetermineCurrentStimulusAnswer()
        {
            //if stim tupe 0 chose uniformly random side.
            if (GetVariableValue("STIMULUS_TYPE") == "0")
            {
                _correctDecision = (Bernoulli.Sample(0.5) == 1) ? RatDecison.Right : RatDecison.Left;
            }

            //get the current stimulus direction.
            double currentHeadingDirection = double.Parse(GetVariableValue("HEADING_DIRECTION"));

            //determine the current stimulus direaction.
            RatDecison currentStimulationSide = (currentHeadingDirection == 0) ? (RatDecison.Center) : ((currentHeadingDirection > 0) ? (RatDecison.Right) : RatDecison.Left);
            //determine if the current stimulus heading direction is in the random heading direction region.
            if (Math.Abs(currentHeadingDirection) <= double.Parse(_variablesList._variablesDictionary["RR_HEADINGS"]._description["parameters"]._ratHouseParameter))
            {
                //get a random side with probability of RR_PROBABILITY to the right side.
                int sampledBernouli = Bernoulli.Sample(double.Parse(_variablesList._variablesDictionary["RR_PROBABILITY"]._description["parameters"]._ratHouseParameter));

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
        }
        #endregion STAGES_ADDIION_FUNCTION

        #region GUI_CONTROLS_FUNCTIONS
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
            Dictionary<string , double> currentTrialDetails =  _crossVaryingVals[_currentVaryingTrialIndex];
            _mainGuiInterfaceControlsDictionary["ClearCurrentTrialDetailsViewList"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["ClearCurrentTrialDetailsViewList"]);

            foreach (string varName in currentTrialDetails.Keys)
            {
                string currentParameterDetails;
                //only ratHouseParameter
                currentParameterDetails = "[" + currentTrialDetails[varName].ToString() + "]";
                
                _mainGuiInterfaceControlsDictionary["UpdateCurrentTrialDetailsViewList"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateCurrentTrialDetailsViewList"], varName, currentParameterDetails);
            }
        }
        #endregion

        #region ADDITIONAL_FUNCTIONS
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
                //choose none student in the selected rat
                //_mainGuiInterfaceControlsDictionary["ResetSelectedStudentNameCombobox"].BeginInvoke(_mainGuiControlsDelegatesDictionary["ResetSelectedStudentNameCombobox"]);
        }

        /// <summary>
        /// Load all mp3 files that the MediaPlayer object should use.
        /// </summary>
        private void LoadAllSoundPlayers()
        {
            _soundPlayerPathDB.Add("CorrectAnswer", Application.StartupPath + @"\SoundEffects\correct sound effect.wav");
            _soundPlayerPathDB.Add("WrongAnswer", Application.StartupPath + @"\SoundEffects\Wrong Buzzer Sound Effect (Raised pitch 400 percent and -3db).wav");
            _soundPlayerPathDB.Add("Ding", Application.StartupPath + @"\SoundEffects\Ding Sound Effects (raised pitch 900 percent).wav");
            _soundPlayerPathDB.Add("MissingAnswer", Application.StartupPath + @"\SoundEffects\Wrong Buzzer Sound Effect (Raised pitch 400 percent and -3db).wav");
            _soundPlayerPathDB.Add("Ding-Left", Application.StartupPath + @"\SoundEffects\Ding Sound Effects - Left (raised pitch 900 percent).wav");
            _soundPlayerPathDB.Add("Ding-Right", Application.StartupPath + @"\SoundEffects\Ding Sound Effects - Right (raised pitch 900 percent).wav");
        }

        /// <summary>
        /// Determine the current trial stimulus type bt the stimulus type variable status.
        /// </summary>
        /// <returns>The stimulus type.</returns>
        public int DetermineCurrentStimulusType()
        {
            string stimulusTypeStatus = _variablesList._variablesDictionary["STIMULUS_TYPE"]._description["status"]._ratHouseParameter;
            switch (stimulusTypeStatus)
            {
                case "1"://static
                    return int.Parse(_variablesList._variablesDictionary["STIMULUS_TYPE"]._description["parameters"]._ratHouseParameter);
                case "2"://varying
                case "6"://Vector
                    return (int)(_crossVaryingVals[_currentVaryingTrialIndex]["STIMULUS_TYPE"]);
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
            currentTrialTimings.wRewardRightDelay = DetermineTimeByVariable("REWARD_RIGHT_DELAY");
            currentTrialTimings.wRewardLeftDelay = DetermineTimeByVariable("REWARD_LEFT_DELAY");
            currentTrialTimings.wRewardLeftDelaySecondChance = DetermineTimeByVariable("REWARD_LEFT_DELAY_SC");
            currentTrialTimings.wRewardRightDelaySecondChance = DetermineTimeByVariable("REWARD_RIGHT_DELAY_SC");

            currentTrialTimings.wRewardCenterDuration = DetermineTimeByVariable("REWARD_CENTER_DURATION");
            currentTrialTimings.wRewardRightDuration = DetermineTimeByVariable("REWARD_RIGHT_DURATION");
            currentTrialTimings.wRewardLeftDuration = DetermineTimeByVariable("REWARD_LEFT_DURATION");
            currentTrialTimings.wRewardLeftDurationSecondChance = DetermineTimeByVariable("REWARD_LEFT_DURATION");
            currentTrialTimings.wRewardRightDurationSecondChance = DetermineTimeByVariable("REWARD_RIGHT_DURATION");

            currentTrialTimings.wRewardToBackwardDelay = DetermineTimeByVariable("REWARD_BACKWARD_TIME");

            currentTrialTimings.wPreTrialTime = DetermineTimeByVariable("PRE_TRIAL_TIME");

            currentTrialTimings.wPostTrialTime = DetermineTimeByVariable("POST_TRIAL_TIME");

            currentTrialTimings.wTimeOutTime = DetermineTimeByVariable("TIMEOUT_TIME");

            currentTrialTimings.wResponseTime = DetermineTimeByVariable("RESPONSE_TIME");

            currentTrialTimings.wDuration = DetermineTimeByVariable("STIMULUS_DURATION");

            currentTrialTimings.wClueDelay = DetermineTimeByVariable("REWARD_CLUE_SOUND_DELAY");

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
           string timeValue = GetVariableValue(timeVarName);

            //if not found - it is random type varriable.
            if(timeValue == string.Empty)
            {
                double lowTime = double.Parse(_variablesList._variablesDictionary[timeVarName]._description["low_bound"]._ratHouseParameter);
                double highTime = double.Parse(_variablesList._variablesDictionary[timeVarName]._description["high_bound"]._ratHouseParameter);
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
        /// <param name="parameterName">The parameter name to get the value for.</param>
        /// <returns>The value of the parameter at the current trial.</returns>
        public string GetVariableValue(string parameterName)
        {
            try
            {
                //detrmine the status of the variable type.
                string variableStatus = _variablesList._variablesDictionary[parameterName]._description["status"]._ratHouseParameter;


                //decide the time value of the time type according to it's status.
                switch (variableStatus)
                {
                    case "1"://static
                        return _variablesList._variablesDictionary[parameterName]._description["parameters"]._ratHouseParameter;

                    case "2"://varying
                    case "6":
                        return _crossVaryingVals[_currentVaryingTrialIndex][parameterName].ToString("000000.00000000");

                    default:
                        return string.Empty;

                }
            }

            catch
            {
                MessageBox.Show("Error", "The parameter " + parameterName + " is not in the excel sheet.", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        /// Null bumper function.
        /// </summary>
        private void NullFunction()
        {
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
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.Both, true);
                    break;

                case 2://visual only.
                case 10://visual only in the dark.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R2Only, true);
                    break;

                case 3://vistibular and visual both.
                case 11://vistibular and visual both in the dark.
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R1Only , true);
                    break;

                case 4://vistibular and visual both with +delta for visual.
                case 5://vistibular and visual both with -delta for visual.
                    //move only R1 if delta is 0
                    double deltaHeading = 0;
                    if (_staticVariablesList.ContainsKey("DELTA "))
                        deltaHeading = _staticVariablesList["DELTA "];
                    else if (_crossVaryingVals[_currentVaryingTrialIndex].Keys.Contains("DELTA "))
                        deltaHeading = _crossVaryingVals[_currentVaryingTrialIndex]["DELTA "];
                    _motomanController.UpdateYasakawaRobotJBIFile(_currentTrialTrajectories, (deltaHeading != 0) ? MotomanProtocolFileCreator.UpdateJobType.Both : MotomanProtocolFileCreator.UpdateJobType.R1Only, true);
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
                    if (RewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding-Left"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_LEFT_DURATION");
                }
                else if ((value & 0x05) == 0x05)
                {
                    if (RewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_CENTER_DURATION");
                }
                else if ((value & (byte)RatDecison.Center) == (byte)RatDecison.Center)
                {
                    if (RewardSound)
                    {
                        _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"];
                        _windowsMediaPlayer.controls.play();
                    }
                    timeByVariable = DetermineTimeByVariable("REWARD_CENTER_DURATION");
                }
                else if ((value & (byte)RatDecison.Right) == (byte)RatDecison.Right)
                {
                    if (RewardSound)
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
        #endregion

        #region EVENTS
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
        #endregion

        #region GUI_EVENTS
        /// <summary>
        /// Plays the Ding sound file.
        /// </summary>
        public void PlayRewardSound()
        {
            _windowsMediaPlayer.URL = _soundPlayerPathDB["Ding"]; _windowsMediaPlayer.controls.play();
        }

        /// <summary>
        /// Plays the WrongAnswer sound file.
        /// </summary>
        public void PlayBreakFixationSound()
        {
            _windowsMediaPlayer.URL = _soundPlayerPathDB["WrongAnswer"]; _windowsMediaPlayer.controls.play();
        }
        #endregion GUI_EVENTS

        #region STRUCT_ENUMS
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
            /// The delay between the right head tracking (for the trial begin) and the right reward for the second chance.
            /// </summary>
            public double wRewardRightDelaySecondChance;

            /// <summary>
            /// The delay between the left head tracking (for the trial begin) and the left reward for the second chance.
            /// </summary>
            public double wRewardLeftDelaySecondChance;

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
            /// The duration for the right reward second chance.
            /// </summary>
            public double wRewardRightDurationSecondChance;

            /// <summary>
            /// The duration for the left reward seconde chance.
            /// </summary>
            public double wRewardLeftDurationSecondChance;

            /// <summary>
            /// The delay between end of water reward to the begining of moving rovot to it's home position.
            /// </summary>
            public double wRewardToBackwardDelay;


            /// <summary>
            /// The pre trial time before start beep and trial starts.
            /// </summary>
            public double wPreTrialTime;

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

            /// <summary>
            /// The delay time between the end of water center reward and the Clue sound.
            /// </summary>
            public double wClueDelay;
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
        #endregion
        #endregion FUNCTIONS
    }
}
