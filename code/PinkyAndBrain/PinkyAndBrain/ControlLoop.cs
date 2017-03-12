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

        #endregion ATTRIBUTES

        #region CONTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ControlLoop(MLApp.MLApp matlabApp)
        {
            _matlabApp = matlabApp;
            _trajectoryCreatorHandler = new TrajectoryCreatorHandler(_matlabApp);
            _rewardController = new RewardController("Dev1" , "Port1" ,"Line0:2", "RewardChannels");
            _ratResponseController = new RatResponseController("Dev1", "Port0", "Line0:2", "RatResponseChannels");
            _stopAfterTheEndOfTheCurrentTrial = false;
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
            _timingRandomizer = new Random();
            _mainGuiControlsDelegatesDictionary = ctrlDelegatesDic;
            _mainGuiInterfaceControlsDictionary = mainGuiInterfaceControlsDictionary;

            //set the trajectory creator name to the given one that should be called in the trajectoryCreatorHandler.
            //also , set the other properties.
            _trajectoryCreatorHandler.SetTrajectoryAttributes(trajectoryCreatorName, _variablesList, _crossVaryingVals, _staticVariablesList, _frequency);

            //reset the RewardController outputs.
            _rewardController.ResetControllerOutputs();

            //run the main control loop function in other thread fom the main thread ( that handling events and etc).
            _stopAfterTheEndOfTheCurrentTrial = false;
            Globals._systemState = SystemState.RUNNING;
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

                ShowTrialDetailsToTheDetailsListView();

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
                        Task.Run(() => { Console.Beep(8000, 100); });
                    }
                }

                //sounds the beep with the missing start gead in the center.
                else
                {
                    Task.Run(() => { Console.Beep(3275, 100); });
                }

                //the post trial stage for saving the trial data and for the delay between trials.
                PostTrialStage();
            }

            Globals._systemState = SystemState.FINISHED;
        }

        public void ShowTrialDetailsToTheDetailsListView()
        {
            Dictionary<string , List<double>> currentTrialDetails =  _crossVaryingVals[_currentVaryingTrialIndex];
            _mainGuiInterfaceControlsDictionary["ClearCurrentTrialDetailsViewList"].BeginInvoke(
            _mainGuiControlsDelegatesDictionary["ClearCurrentTrialDetailsViewList"]);

            foreach (string varName in currentTrialDetails.Keys)
            {
                _mainGuiInterfaceControlsDictionary["UpdateCurrentTrialDetailsViewList"].BeginInvoke(
                _mainGuiControlsDelegatesDictionary["UpdateCurrentTrialDetailsViewList"], varName, currentTrialDetails[varName][0].ToString());
            }
        }

        /// <summary>
        /// Initializes the variables , points , trajectories , random varibles ,  etc.
        /// </summary>
        public void InitializationStage()
        {
            //determine all current trial timings and delays.
            _currentTrialTimings = DetermineCurrentTrialTimings();
            
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
            while (sw.ElapsedMilliseconds < (int)(_currentTrialTimings.wReward1Delay * 1000))
            { }


            //open the center reward for the rat to be rewarded.
            //after the reward1 duration time and than close it.
            _rewardController.WriteSingleSamplePort(true, 0x02);

            while (sw.ElapsedMilliseconds < (int)(_currentTrialTimings.wReward1Duration * 1000))
            { }

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
            //here should be the motion of the Yasakawa robot(now it's only delay of the duration movement according to the robot frequency and the number of points in the trajectory).
            Task robotMotion = Task.Factory.StartNew(() => MoveYasakawaRobotWithTrajectory(_currentTrialTrajectories));

            //also run the rat center head checking in parallel to the movement time.
            bool headInCenterAllTheTime = true;
            Task.Run(() =>
            {
                while (!robotMotion.IsCompleted)
                {
                    //sample the signal indicating if the rat head is in the center only 60 time per second (because the refresh rate of the signal is that frequency.
                    Thread.Sleep(1000 / 60);
                    if (_ratResponseController.ReadSingleSamplePort() != 2)
                    {
                        headInCenterAllTheTime = false;
                    }
                }
            });

            //wait the robot to finish the movement.
            robotMotion.Wait();

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
                //if the head sample mentioned that the head was not in the center during the startDelay time , break , and move to the post trial time.
                if (_ratResponseController.ReadSingleSamplePort() != 2)
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
                x = _ratResponseController.ReadSingleSamplePort();
            }

            return (x == 2);
        }

        public void MoveYasakawaRobotWithTrajectory(Tuple<Trajectory , Trajectory> traj)
        {
            foreach (var xTrajectoryPoint in traj.Item1.x)
            {
                //sleep the time frequency for each command of the robot (the robot frequency).
                Thread.Sleep(4);
            }
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
            Thread.Sleep((int)(_currentTrialTimings.wPostTrialTime * 1000));
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
        };
        #endregion FUNCTIONS
    }
}
