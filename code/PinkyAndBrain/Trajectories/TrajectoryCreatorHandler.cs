using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;
using MathNet.Numerics.LinearAlgebra;
using MLApp;
using System.Runtime.Remoting;
using System.Reflection;

namespace Trajectories
{
    /// <summary>
    /// This class is called in each trial in order to create the trajectories to the current trial according to the current trial parmaters.
    /// </summary>
    public class TrajectoryCreatorHandler
    {
        #region ATTRIBUTES
        /// <summary>
        /// The trajectory creator name (the name of the type for making the trjectory).
        /// </summary>
        private string _trajectoryCreatorName;

        /// <summary>
        /// The Matlab computing process object for drawing graphs and etc.
        /// </summary>
        private MLApp.MLApp _matlab;

        /// <summary>
        /// The index of the current varying trial in the random indexed varying vector.
        /// </summary>
        private int _varyingCurrentIndex = 0;

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
        private Dictionary<string, List<double>> _staticVals;

        /// <summary>
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;

        /// <summary>
        /// The TrajectoryCreator object decided by the trajectoryName.
        /// </summary>
        private ITrajectoryCreator _trajectoryCreator;
        #endregion ATTRIBUTES

        #region CONSTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="matlab">The Matlab computations handler object.</param>
        public TrajectoryCreatorHandler(MLApp.MLApp matlab)
        {
            //set the matlab engine object pointer.
            _matlab = matlab;

            //reset the varying index to read from.
            _varyingCurrentIndex = 0;
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Setting new trajectory attribute according to a new user input.
        /// </summary>
        /// <param name="trajectoryName">The trajectoryCreator class name to make and call in order to deliver the trajectory.</param>
        /// <param name="variableList">The variables readen from the xlsx protocol file.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.</param>
        /// <param name="staticVariables">The static variables list in double value presentation.</param>
        /// <param name="frequency">The numbers of samples for each trajectory.</param>
        public void SetTrajectoryAttributes(string trajectoryName, Variables variableList, List<Dictionary<string, List<double>>> crossVaryingVals, Dictionary<string, List<double>> staticVariables, int frequency)
        {
            //set the variables.
            _trajectoryCreatorName = trajectoryName;
            _variablesList = variableList;
            _crossVaryingVals = crossVaryingVals;
            _staticVals = staticVariables;
            _frequency = frequency;

            //arguments for the ITrajectoryCreator constructor.
            object[] args = new object[5];
            args[0] = _matlab;
            args[1] = _variablesList;
            args[2] = _crossVaryingVals;
            args[3] = _staticVals;
            args[4] = _frequency;

            //create the _trajectoryCreator by constructor with the match name to the trajectoryName.

            Type e = Type.GetType("Trajectories.TrajectoryCreators." + _trajectoryCreatorName);

            _trajectoryCreator = (ITrajectoryCreator)Activator.CreateInstance(Type.GetType("Trajectories." + _trajectoryCreatorName), args);
        }

        /// <summary>
        /// Create a trajectory for both the ratHouseTrajectory and the landscapeHouseTrjectory for the control loop.
        /// </summary>
        /// <returns>The both ratHouseTrajectory and the landscapeHouseTrjectory.</returns>
        public Tuple<Trajectory ,Trajectory> CreateTrajectory(int index = 0)
        {         
            return _trajectoryCreator.CreateTrialTrajectory(index);
        } 
        #endregion FUNCTIONS
    }

    /// <summary>
    /// Describes the trajectory that should be sent to the robot.
    /// </summary>
    public struct Trajectory
    {
        #region LINEAR_TRAJECTORIES

        /// <summary>
        /// The x axis for the vector list position.
        /// </summary>
        public Vector<double> x;

        /// <summary>
        /// The y axis for the vector list  position.
        /// </summary>
        public Vector<double> y;

        /// <summary>
        /// The z axis for the vector list position.
        /// </summary>
        public Vector<double> z;
        #endregion LINEAR_TRAJECTORIES

        #region ROTATION_TRAJECTORIES
        /// <summary>
        /// The x rotation axis for the vector list  position.
        /// </summary>
        public Vector<double> rx;

        /// <summary>
        /// The y rotation axis for the vector list  position.
        /// </summary>
        public Vector<double> ry;

        /// <summary>
        /// The z rotation axis for thevector list position.
        /// </summary>
        public Vector<double> rz;
        #endregion ROTATION_TRAJECTORIES
    }

    public struct Position
    {
        #region LINEAR_TRAJECTORY

        /// <summary>
        /// The x axis for the position.
        /// </summary>
        public double x;

        /// <summary>
        /// The y axis for the position.
        /// </summary>
        public double y;

        /// <summary>
        /// The z axis for the position.
        /// </summary>
        public double z;
        #endregion LINEAR_TRAJECTORY

        #region ROTATION_TRAJECTORY
        /// <summary>
        /// The x rotation axis for the position.
        /// </summary>
        public double rx;

        /// <summary>
        /// The y rotation axis for the position.
        /// </summary>
        public double ry;

        /// <summary>
        /// The z rotation axis for the position.
        /// </summary>
        public double rz;
        #endregion ROTATION_TRAJECTORY
    }
}
