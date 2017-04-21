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

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is the main program controll loop.
    /// It calls all the needed other inerfaces to make what it's needed to be created.
    /// The function is called by the GuiInterface after the statButton is clicked.
    /// </summary>
    class ControlLoop
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
        /// The total trials made from the beginning of the experiment.
        /// </summary>
        private int _numOfPastTrials;

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
        /// The varying index selector for choosing the current combination index.
        /// </summary>
        private VaryingIndexSelector _varyingIndexSelector;

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
        /// Indicated if the control loop should not make another trials.
        /// </summary>
        private bool _stopAfterTheEndOfTheCurrentTrial;

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
        /// The streamwriter for the current experiment results.
        /// </summary>
        private StreamWriter _currentResultFileStrwamWriter;
        #endregion ATTRIBUTES

        #region CONTRUCTORS
        /// <summary>
        /// Default constructor.
        /// <param name="matlabApp">The matlab app handler.</param>
        /// <param name="motomanController">The motoman controller object.</param>
        /// <param name="ledController">The led controller object.</param>
        /// </summary>
        public ControlLoop(MLApp.MLApp matlabApp , CYasnac motomanController , LEDController ledController)
        {
            _matlabApp = matlabApp;
            _trajectoryCreatorHandler = new TrajectoryCreatorHandler(_matlabApp);
            _rewardController = new RewardController("Dev1" , "Port1" ,"Line0:2", "RewardChannels");
            _ratResponseController = new RatResponseController("Dev1", "Port0", "Line0:2", "RatResponseChannels");
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
        }
        #endregion CONTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Transfer the control from the main gui to the control loop until a new gui event is handled by the user.
        /// </summary>
        public void Start(Variables variablesList, List<Dictionary<string, List<double>>> crossVaryingList, Dictionary<string, List<List<double>>> staticVariablesList, int frequency, string trajectoryCreatorName, Dictionary<string, Delegate> ctrlDelegatesDic, Dictionary<string , Control> mainGuiInterfaceControlsDictionary)
        {
            //initialize variables.
            _variablesList = variablesList;
            _crossVaryingVals = crossVaryingList;
            _staticVariablesList = staticVariablesList;
            _frequency = frequency;
            _totalNumOfTrials = _crossVaryingVals.Count();
            _varyingIndexSelector = new VaryingIndexSelector(_totalNumOfTrials);
            _numOfPastTrials = 0;
            _timingRandomizer = new Random();
            _mainGuiControlsDelegatesDictionary = ctrlDelegatesDic;
            _mainGuiInterfaceControlsDictionary = mainGuiInterfaceControlsDictionary;

            //set the trajectory creator name to the given one that should be called in the trajectoryCreatorHandler.
            //also , set the other properties.
            _trajectoryCreatorHandler.SetTrajectoryAttributes(trajectoryCreatorName, _variablesList, _crossVaryingVals, _staticVariablesList, _frequency);

            //reset the RewardController outputs.
            _rewardController.ResetControllerOutputs();

            //set the frequency for the JBI file creator.
            _motomanProtocolFileCreator.Frequency = _frequency;

            //create a new results file for the new experiment.
            if (_currentResultFileStrwamWriter != null) _currentResultFileStrwamWriter.Dispose();
            _currentResultFileStrwamWriter =  File.CreateText(Application.StartupPath + @"\results\" + Guid.NewGuid());

            //run the main control loop function in other thread fom the main thread ( that handling events and etc).
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

        public void MainControlLoop()
        {
            for (int i = 0; i < _crossVaryingVals.Count();i++ )
            {
                //if system has stopped , wait for the end of the current trial ans break,
                if (Globals._systemState.Equals(SystemState.STOPPED) || _stopAfterTheEndOfTheCurrentTrial)
                {
                    Globals._systemState = SystemState.STOPPED;
                    break;
                }

                //choose the random combination index for the current trial.
                _currentVaryingTrialIndex = _varyingIndexSelector.ChooseRandomCombination();

                //craetes the trajectory for both robots for the current trial if not one of the training protocols.
                _currentTrialTrajectories = _trajectoryCreatorHandler.CreateTrajectory(_currentVaryingTrialIndex);

                //show some trial details to the gui trial details panel.
                ShowTrialDetailsToTheDetailsListView();
                ShowGlobalExperimentDetailsListView();

                //initialize the currebt time parameters and all the current trial variables.
                InitializationStage();

                //wait the rat to first (in the current trial - for "start buttom") move it's head to the center.
                bool headEnteredToTheCenterDuringTheTimeoutDuration = WaitForHeadEnteranceToTheCenterStage();

                //if the rat entered it's head to the center in the before timeOut time.
                if(headEnteredToTheCenterDuringTheTimeoutDuration)
                {
                    //if the rat head was stable in the center for the startDelay time as required start the movement.
                    if(CheckDuration1HeadInTheCenterStabilityStage())
                    {
                        //moving the robot with duration time , and checking for the stability of the head in the center.
                        if (MovingTheRobotDurationWithHeadCenterStabilityStage())
                        {
                            //reward the rat in the center with water for duration of reward1Duration for stable head in the center during the movement.
                            Reward1Stage();

                            //wait the rat to response to the movement.
                            ResponseTimeStage();
                        }
                    }

                    //sounds the beep for missing the movement head in the center.
                    else
                    {
                        Task.Run(() => { Console.Beep(300, 200); });
                    }
                }

                //sounds the beep with the missing start gead in the center.
                else
                {
                    Task.Run(() => { Console.Beep(400, 200); });
                }

                //the post trial stage for saving the trial data and for the delay between trials.
                PostTrialStage();

                //increase the num of trials counter indicator.
                _numOfPastTrials++;
            }

            Globals._systemState = SystemState.FINISHED;

            //raise an event for the GuiInterface that the trials round is over.
            _mainGuiInterfaceControlsDictionary["FinishedAllTrialsRound"].BeginInvoke(_mainGuiControlsDelegatesDictionary["FinishedAllTrialsRound"]);
        }

        /// <summary>
        /// Show global experiment parameters.
        /// </summary>
        private void ShowGlobalExperimentDetailsListView()
        {
            //update the number of past trials.
            _mainGuiInterfaceControlsDictionary["UpdateCurrentTrialDetailsViewList"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateCurrentTrialDetailsViewList"], "Trial Number", (_numOfPastTrials+1).ToString());

            //update the number of left trials.
            _mainGuiInterfaceControlsDictionary["UpdateCurrentTrialDetailsViewList"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateCurrentTrialDetailsViewList"], "Left Number", (_totalNumOfTrials - _numOfPastTrials - 1).ToString());
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
            //determine all current trial timings and delays.
            _currentTrialTimings = DetermineCurrentTrialTimings();
            _currentTrialStimulusType = DetermineCurrentStimulusType();
            
            //Sounds the start beep. Now waiting for the rat to move it's head to the center.
            Console.Beep();
        }

        /// <summary>
        /// Waiting the rat to response the movement direction.
        /// </summary>
        public void ResponseTimeStage()
        {
            //time to wait for the moving rat response.
            Thread.Sleep(1000*(int)(_currentTrialTimings.wResponseTime));
        }

        /// <summary>
        /// The reward1 stage is happening if the rat head was consistently stable in the center during the movement.
        /// </summary>
        public void Reward1Stage()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //wait the reward1 delay time befor openning the reward1.
            Thread.Sleep((int)(_currentTrialTimings.wReward1Delay * 1000));

            sw.Restart();

            //open the center reward for the rat to be rewarded.
            //after the reward1 duration time and than close it.
            _rewardController.WriteSingleSamplePort(true, 0x02);

            //wait the reward1 time and fill the interactive water fill estimation panel.
            _waterRewardFillingTimer.Start();
            Thread.Sleep((int)(_currentTrialTimings.wReward1Duration * 1000));
            _waterRewardFillingTimer.Stop();

            //close again the reward1 port.
            _rewardController.WriteSingleSamplePort(true, 0x00);

        }

        /// <summary>
        /// Moving the robot stage (it the rat enter the head to the center in the timeOut time and was stable in the center for startDelay time).
        /// This function also , in paralleled to the robot moving , checks that the rat head was consistently in the center during the duration time of the movement time.
        /// </summary>
        /// <returns>True if the head was stable consistently in the center during the movement time.</returns>
        public bool MovingTheRobotDurationWithHeadCenterStabilityStage()
        {
            //The motion of the Yasakawa robot if needed as the current stimulus type (if is both visual&vestibular -3 or only vistibular-1).
            Task robotMotion;
            switch (_currentTrialStimulusType)
            {
                case 1://vistibular only.
                    robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.Both));
                    break;

                case 2://visual only.
                    robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R2Only));

                    //here should be stimulus type 2 for motion of the second robot for visual only.
                    //should move the robot and also to turn on the leds.
                    LEDsData ledsData = new LEDsData(10, 0, 255, 0, _ledSelector.FillWithBinaryRandomCombination(40));
                    _ledController.LEDsDataCommand = ledsData;
                    _ledController.SendData();
                    _ledController.ExecuteCommands();
                    break;

                case 3://vistibular and visual both.
                    robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory(_currentTrialTrajectories, MotomanProtocolFileCreator.UpdateJobType.R1Only));
                    //should move only r1 robot and also to turn on the leds.
                    LEDsData ledsData2 = new LEDsData(10, 0, 255, 0, _ledSelector.FillWithBinaryRandomCombination(40));
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
                    if (_currentRatResponse != 2)
                    {
                        headInCenterAllTheTime = false;
                    }
                }
            });

            //wait the robot to finish the movement.
            robotMotion.Wait();

            //and turn off the leds visual vistibular (it is o.k for all cases , just reset).
            _ledController.ResetLeds();

            return headInCenterAllTheTime;
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
                //sample the signal indicating if the rat head is in the center only 60 time per second (because the refresh rate of the signal is that frequency).
                Thread.Sleep((int)(Properties.Settings.Default.NoldusRatReponseSampleRate));

                //if the head sample mentioned that the head was not in the center during the startDelay time , break , and move to the post trial time.
                if (_currentRatResponse != 2)
                {
                    return false;
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

            return (x == 2);
        }

        /// <summary>
        /// Move the motoman with the given trajectory.
        /// <param name="traj">The trajectory to be send to the controller.</param>
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        /// </summary>
        public void MoveYasakawaRobotWithTrajectory(Tuple<Trajectory , Trajectory> traj , MotomanProtocolFileCreator.UpdateJobType updateJobType)
        {
            /*foreach (var xTrajectoryPoint in traj.Item1.x)
            {
                //sleep the time frequency for each command of the robot (the robot frequency).
                Thread.Sleep(4);
            }*/
            
            //setting the trajectory for the JBI file creator and update the file that is being senf to the controller with the new commands.
            _motomanProtocolFileCreator.TrajectoryR1Position = traj.Item1;
            _motomanProtocolFileCreator.TrajectoryR2Position = traj.Item2;
            _motomanProtocolFileCreator.UpdateJobJBIFile(updateJobType);

            //Delete the old JBI file commands stored in the controller.
            try
            {
                _motomanController.DeleteJob("GAUSSIANMOVING2.JBI");
            }
            catch { }

            //wruite the new JBI file to the controller.
            _motomanController.WriteFile(@"C:\Users\User\Desktop\GAUSSIANMOVING2.JBI");
            _motomanController.StartJob("GAUSSIANMOVING2.JBI");

            //wait for the commands to be executed.
            _motomanController.WaitJobFinished(10000);
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

            currentTrialTimings.wReward1Delay = DetermineTimeByVariable("REWARD1_DELAY");
            currentTrialTimings .wReward2Delay= DetermineTimeByVariable("REWARD2_DELAY");
            currentTrialTimings.wReward3Delay= DetermineTimeByVariable("REWARD3_DELAY");

            currentTrialTimings.wReward1Duration = DetermineTimeByVariable("REWARD1_DURATION");
            currentTrialTimings.wReward2Duration = DetermineTimeByVariable("REWARD2_DURATION");
            currentTrialTimings.wReward3Duration = DetermineTimeByVariable("REWARD3_DURATION");

            currentTrialTimings.wPostTrialTime = DetermineTimeByVariable("POST_TRIAL_TIME");

            currentTrialTimings.wTimeOutTime = DetermineTimeByVariable("TIMEOUT_TIME");

            currentTrialTimings.wResponseTime = DetermineTimeByVariable("RESPONSE_TIME");

            currentTrialTimings.wDuration = DetermineTimeByVariable("DURATION");

            return currentTrialTimings;
        }

        /// <summary>
        /// determine the current trial of the input type time by it's status (random , static , etc...).
        /// </summary>
        /// <param name="timeVarName">The time type to be compute.</param>
        /// <returns>The result time according to the type of the time.</returns>
        public double DetermineTimeByVariable(string timeVarName)
        {
            //detrmine the status of the time type.
            string startDelayStatus = _variablesList._variablesDictionary[timeVarName]._description["status"]._ratHouseParameter[0];
            
            //decide the time value of the time type according to it's status.
            switch (startDelayStatus)
            {
                case "1"://static
                    return double.Parse(_variablesList._variablesDictionary[timeVarName]._description["parameters"]._ratHouseParameter[0]);

                case "5"://random
                    double lowTime = double.Parse(_variablesList._variablesDictionary[timeVarName]._description["low_bound"]._ratHouseParameter[0]);
                    double highTime = double.Parse(_variablesList._variablesDictionary[timeVarName]._description["high_bound"]._ratHouseParameter[0]);
                    return RandomTimeUniformly(lowTime, highTime);
            }

            return 0;
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
        /// </summary>
        public void PostTrialStage()
        {
            Task moveRobotHomePositionTask = Task.Factory.StartNew(() => MoveRobotHomePosition());

            Task.Run(() =>
            {
                StringBuilder textBuilder = new StringBuilder();
                textBuilder.Append("trial # ");
                textBuilder.Append(_numOfPastTrials);
                _currentResultFileStrwamWriter.WriteLine(textBuilder.ToString());
                textBuilder.Clear();

                WriteAllParametersValues(_crossVaryingVals[_currentVaryingTrialIndex] , _currentTrialTimings , _staticVariablesList);
                _currentResultFileStrwamWriter.Flush();
            });

            Thread.Sleep((int)(_currentTrialTimings.wPostTrialTime * 1000));

            //wait the maximum time of the postTrialTime and the going home position time.
            moveRobotHomePositionTask.Wait();
        }

        public void WriteAllParametersValues(Dictionary<string , List<double>> trialVaryingParameters , TrialTimings trialTimings , Dictionary<string , List<List<double>>> trialStaticParameters)
        {
            StringBuilder lineBuilder = new StringBuilder();

            foreach (string paramName in trialStaticParameters.Keys)
            {
                lineBuilder.Append(paramName);
                lineBuilder.Append("\t\t:\t");
                foreach (double value in trialStaticParameters[paramName][0])
                {
                    lineBuilder.Append(value);
                    lineBuilder.Append(" ");
                }

                _currentResultFileStrwamWriter.WriteLine(lineBuilder.ToString());
                lineBuilder.Clear();
            }

            foreach (string paramName in trialVaryingParameters.Keys)
            {
                lineBuilder.Append(paramName);
                lineBuilder.Append("\t\t:\t");
                foreach (double value in trialVaryingParameters[paramName])
                {
                    lineBuilder.Append(value);
                    lineBuilder.Append(" ");
                }

                _currentResultFileStrwamWriter.WriteLine(lineBuilder.ToString());
                lineBuilder.Clear();
            }

            foreach (var field in typeof(TrialTimings).GetFields(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic))
            {
                lineBuilder.Append(field.Name);
                lineBuilder.Append("\t:\t");
                lineBuilder.Append(field.GetValue(trialTimings));

                _currentResultFileStrwamWriter.WriteLine(lineBuilder.ToString());
                lineBuilder.Clear();
            }
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
            homePositionFile.UpdateHomePosJBIFile(r1HomePosition , r2HomePosition , 90);
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
        /// Giving reward as specified (for the specified directions).
        /// </summary>
        /// <param name="value">The specified direction by xxxxxy-y-y where left-center-right.</param>
        /// <param name="continious">Make the reward continiously (open untill get a close value) or not continiously (by the time of REWARD1_DURATION parameter.</param>
        public void GiveRewardHandReward(byte value , bool continious = false)
        {
            if(continious)
            {
                _rewardController.WriteSingleSamplePort(true, value);
            }
            else
            {
                _rewardController.WriteSingleSamplePort(true , value);
                Thread.Sleep((int)(DetermineTimeByVariable("REWARD1_DURATION") * 1000));
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
                _mainGuiControlsDelegatesDictionary["SetWaterRewardsMeasure"]);
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
            public double wReward1Delay;

            /// <summary>
            /// The delay between the right head tracking (for the trial begin) and the right reward.
            /// </summary>
            public double wReward2Delay;

            /// <summary>
            /// The delay between the left head tracking (for the trial begin) and the left reward.
            /// </summary>
            public double wReward3Delay;

            /// <summary>
            /// The duration for the center reward.
            /// </summary>
            public double wReward1Duration;

            /// <summary>
            /// The duration for the right reward.
            /// </summary>
            public double wReward2Duration;

            /// <summary>
            /// The duration for the left reward.
            /// </summary>
            public double wReward3Duration;

            /// <summary>
            /// The duration to wait between the end of the previous trial and the begining of the next trial.
            /// </summary>
            public double wPostTrialTime;

            /// <summary>
            /// The time after the beep of the trial begin time and the time the rat can response with head to the center in order to begin the movement.
            /// </summary>
            public double wTimeOutTime;

            /// <summary>
            /// The time the rat have to response (with head to the left or to the right) after the reward1 (ig get).
            /// </summary>
            public double wResponseTime;

            /// <summary>
            /// The robot movement duration.
            /// </summary>
            public double wDuration;
        };
        #endregion FUNCTIONS
    }
}
