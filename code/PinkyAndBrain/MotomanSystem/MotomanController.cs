﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trajectories;
using log4net;
using MotocomdotNetWrapper;
using MathNet.Numerics.LinearAlgebra;
using System.Windows.Forms;
using MotomanSystem;

namespace PinkyAndBrain
{
    /// <summary>
    /// Represent the motoman controller for updating JBI files and send Commands to the robot.
    /// </summary>
    public class MotomanController
    {
        #region MEMBSERS
        /// <summary>
        /// Logger for writing log information.
        /// </summary>
        private ILog _logger;

        /// <summary>
        /// The YASAKAWA motoman robot controller.
        /// </summary>
        private CYasnac _motomanController;

        /// <summary>
        /// The JBI protocol file creator for each trial trajectory.
        /// </summary>
        public MotomanProtocolFileCreator MotomanProtocolFileCreator;

        /// <summary>
        /// The frquency rate for the trajectory poins per second.
        /// </summary>
        public int Frequency { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="robotIPAddress">The robot IP adress to connect to.</param>
        /// <param name="logger">The application logger.</param>
        public MotomanController(string robotIPAddress, int frequency ,  ILog logger)
        {
            _motomanController = new CYasnac(robotIPAddress, Application.StartupPath);

            MotomanProtocolFileCreator = new MotomanProtocolFileCreator(Application.StartupPath + @"\JBI\GAUSSIANMOVING2.JBI");

            Frequency = frequency;

            _logger = logger;

            _logger.Info("MotomanController created.");
        }
        #endregion

        #region WrappedFunctions
        /// <summary>
        /// Set the robot servo's on.
        /// </summary>
        public void SetServoOn()
        {
            _logger.Info("Setting Yaskawa robot servo on.");

            _motomanController.SetServoOn();
        }

        /// <summary>
        /// Set the robot servo's off.
        /// </summary>
        public void SetServoOff()
        {
            _logger.Info("Setting Yaskawa robot servo off.");

            _motomanController.SetServoOff();
        }

        /// <summary>
        /// Reads one single IO
        /// </summary>
        /// <param name="Address">Address of IO to read</param>
        /// <returns>IO status</returns>
        public bool ReadSingleIO(int address)
        {
            _logger.Info("Reading Single IO from adress " + address + ".");

            return _motomanController.ReadSingleIO(address);
        }

        /// <summary>
        /// Indicate if the robot is making a current job now.
        /// </summary>
        /// <returns></returns>
        public bool JobStatus()
        {
            _logger.Info("Reading Job status.");

            bool jobStatus = _motomanController.ReadSingleIO(50070);

            _logger.Info("Job working is " + jobStatus);

            return jobStatus;
        }

        /// <summary>
        /// Wait the robot to finisg job action.
        /// </summary>
        public void WaitJobFinished()
        {
            _logger.Info("Waiting Job to be finished.");

            while(JobStatus())
            {
                Thread.Sleep(10);
            }

            _logger.Info("Job status finished.");
        }

        /// <summary>
        /// Get the controled robot place.
        /// </summary>
        /// <returns>The vectir place of the control robot.</returns>
        public double[] GetRobotPlace()
        {
            return _motomanController.BscIsLoc();
        }

        /// <summary>
        /// Set the controlled robot.
        /// </summary>
        /// <param name="robotNum">robotNum = 1 for robot 1 or robotNum = 2 for robot 2.</param>
        public void SetRobotControlGroup(short robotNum)
        {
            _motomanController.BscSetCtrlGroupXrc(robotNum, 0);
        }

        #endregion

        #region JBI_FILES_CONTROLLS_FUNCTIONS
        /// <summary>
        /// Updating the robot working JBI file according to the trajectory and the stimulus type.
        /// </summary>
        /// <param name="traj">The trajectory to be send to the controller.</param>
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        /// <param name="inverse"> Indicate if the motion of the robot is backword motion.</param>
        public void UpdateYasakawaRobotJBIFile(Tuple<Trajectory2, Trajectory2> traj, MotomanProtocolFileCreator.UpdateJobType updateJobType, bool inverse = false)
        {
            _logger.Info("Writing job file to the robot.");

            //if need to inverse the trajectory in case of PostTrialStage backword trajcetory.
            if (inverse)
                traj = new Tuple<Trajectory2, Trajectory2>(traj.Item1.ToReverse(), traj.Item2.ToReverse());

            //setting the trajectory for the JBI file creator and update the file that is being senf to the controller with the new commands.
            MotomanProtocolFileCreator.TrajectoryR1Position = traj.Item1;
            MotomanProtocolFileCreator.TrajectoryR2Position = traj.Item2;
            MotomanProtocolFileCreator.UpdateJobJBIFile(updateJobType, inverse);

            //Delete the old JBI file commands stored in the controller.
            try
            {
                _motomanController.DeleteJob("GAUSSIANMOVING2.JBI");
            }
            catch (Exception e)
            {
                _logger.Info("Delete the old JBI file commands stored in the controller exception " + e.Data);
            }

            //wruite the new JBI file to the controller.
            _motomanController.WriteFile(Application.StartupPath +  @"\JBI\GAUSSIANMOVING2.JBI");

            _logger.Info("Writing job file to the robot ended.");
        }

        /// <summary>
        /// Writing the home_pos file as the readen parameter in the configuration.
        /// </summary>
        public void WriteHomePosFile()
        {
            _logger.Info("Writing home position file.");

            Position r1HomePosition = new Position()
            {
                X = MotocomSettings.Default.R1OriginalX,
                Y = MotocomSettings.Default.R1OriginalY,
                Z = MotocomSettings.Default.R1OriginalZ,
                RX = MotocomSettings.Default.R1OriginalRX,
                RY = MotocomSettings.Default.R1OriginalRY,
                RZ = MotocomSettings.Default.R1OriginalRZ
            };

            Position r2HomePosition = new Position()
            {
                X = MotocomSettings.Default.R2OriginalX,
                Y = MotocomSettings.Default.R2OriginalY,
                Z = MotocomSettings.Default.R2OriginalZ,
                RX = MotocomSettings.Default.R2OriginalRX,
                RY = MotocomSettings.Default.R2OriginalRY,
                RZ = MotocomSettings.Default.R2OriginalRZ
            };

            WriteOneTargetPositionFile("HOME_POS_BOTH" ,r1HomePosition, r2HomePosition);

            _logger.Info("Writing home position file ended.");
        }

        /// <summary>
        /// Writing the park pos file as the readen parameter in the configuration.
        /// </summary>
        public void WriteParkPositionFile()
        {
            _logger.Info("Writing park position file.");

            Position r1ParkPosition = new Position()
            {
                X = MotocomSettings.Default.R1OriginalX - MotocomSettings.Default.ParkingBackwordDistance,
                Y = MotocomSettings.Default.R1OriginalY,
                Z = MotocomSettings.Default.R1OriginalZ,
                RX = MotocomSettings.Default.R1OriginalRX,
                RY = MotocomSettings.Default.R1OriginalRY,
                RZ = MotocomSettings.Default.R1OriginalRZ
            };

            Position r2ParkPosition = new Position()
            {
                X = MotocomSettings.Default.R2OriginalX,
                Y = MotocomSettings.Default.R2OriginalY,
                Z = MotocomSettings.Default.R2OriginalZ,
                RX = MotocomSettings.Default.R2OriginalRX,
                RY = MotocomSettings.Default.R2OriginalRY,
                RZ = MotocomSettings.Default.R2OriginalRZ
            };

            WriteOneTargetPositionFile("PARK_POS_BOTH" , r1ParkPosition, r2ParkPosition);

            _logger.Info("Writing park position file ended.");
        }

        /// <summary>
        /// Writing the Aside pos file as the readen parameter in the configuration.
        /// </summary>
        public void WriteASidePositionFile()
        {
            _logger.Info("Writing ASide position file.");

            Position r1ASidePosition = new Position()
            {
                X = MotocomSettings.Default.R1ASideX,
                Y = MotocomSettings.Default.R1ASideY,
                Z = MotocomSettings.Default.R1ASideZ,
                RX = MotocomSettings.Default.R1ASideRX,
                RY = MotocomSettings.Default.R1ASideRY,
                RZ = MotocomSettings.Default.R1ASideRZ
            };

            Position r2ASidePosition = new Position()
            {
                X = MotocomSettings.Default.R2OriginalX,
                Y = MotocomSettings.Default.R2OriginalY,
                Z = MotocomSettings.Default.R2OriginalZ,
                RX = MotocomSettings.Default.R2OriginalRX,
                RY = MotocomSettings.Default.R2OriginalRY,
                RZ = MotocomSettings.Default.R2OriginalRZ
            };

            WriteOneTargetPositionFile("ASIDE_POS_BOTH", r1ASidePosition, r2ASidePosition);

            _logger.Info("Writing ASide position file ended.");
        }

        /// <summary>
        /// Writing the JBI file for one target position moving.
        /// </summary>
        public void WriteOneTargetPositionFile(string jbiFileName , Position r1TargetPosition, Position r2TargetPosition)
        {
            MotomanProtocolFileCreator targetPositionFile = new MotomanProtocolFileCreator(Application.StartupPath +  @"\JBI\" + jbiFileName + ".JBI");

            //update the target position file.
            targetPositionFile.UpdateSpecificPosJBIFile(jbiFileName, r1TargetPosition, r2TargetPosition, Frequency);
        }

        /// <summary>
        /// Move the robot to it's home (origin) position.
        /// </summary>
        public void MoveRobotHomePosition()
        {
            _logger.Info("Moving the robot home position begin.");

            MoveRobotSpecificPosition("HOME_POS_BOTH.JBI");

            _logger.Info("Moving the robot home position ended.");
        }

        /// <summary>
        /// Move the robot to it's park position.
        /// </summary>
        public void MoveRobotParkPosition()
        {
            _logger.Info("Moving the robot park position begin.");

            MoveRobotSpecificPosition("PARK_POS_BOTH.JBI");

            _logger.Info("Moving the robot park position ended.");
        }

        /// <summary>
        /// Move the robot to it's ASide position.
        /// </summary>
        public void MoveRobotASidePosition()
        {
            _logger.Info("Moving the robot aside position begin.");

            MoveRobotSpecificPosition("ASIDE_POS_BOTH.JBI");

            _logger.Info("Moving the robot aside position ended.");
        }

        /// <summary>
        /// Move the robot with the specific job name.
        /// </summary>
        /// <param name="jobName"></param>
        public void MoveRobotSpecificPosition(string jobName)
        {
            try
            {
                _motomanController.DeleteJob(jobName);
            }
            catch
            { }

            _motomanController.WriteFile(Application.StartupPath + @"\JBI\" + jobName);

            _motomanController.StartJob(jobName);

            WaitJobFinished();
        }

        /// <summary>
        /// Move the motoman with the given trajectory.
        /// </summary>
        public void MoveYasakawaRobotWithTrajectory(bool waitJObIOFinished = false, int waitDuration = 1000)
        {
            //todo:Check if to defuncion this function toi 2 seperates function of MoveRobot() and WaitFinish().
            _logger.Info("Moving the robot with trajectory begin.");
            _motomanController.StartJob("GAUSSIANMOVING2.JBI");            

            //wait for the commands to be executed.
            if (waitJObIOFinished)
            {
                WaitJobFinished();
            }
            else
            {
                Thread.Sleep(waitDuration);
            }
            _logger.Info("Moving the robot with trajectory finished.");
        }
        #endregion
    }
}
