using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;
using MathNet.Numerics.LinearAlgebra;
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
        /// The index of the current varying trial in the random indexed varying vector.
        /// </summary>
        private int _varyingCurrentIndex = 0;

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
        private Dictionary<string, double> _staticVals;

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
        public TrajectoryCreatorHandler()
        {
            //reset the varying index to read from.
            _varyingCurrentIndex = 0;
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Setting new trajectory attribute according to a new user input.
        /// </summary>
        /// <param name="trajectoryCreator">The trajectoryCreator class to make and call in order to deliver the trajectory.</param>
        /// <param name="variableList">The variables readen from the xlsx protocol file.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters.</param>
        /// <param name="staticVariables">The static variables list in double value presentation.</param>
        /// <param name="frequency">The numbers of samples for each trajectory.</param>
        public void SetTrajectoryAttributes(ITrajectoryCreator trajectoryCreator, Variables variableList, List<Dictionary<string, double>> crossVaryingVals, Dictionary<string, double> staticVariables, int frequency)
        {
            //set the variables.
            _variablesList = variableList;
            _crossVaryingVals = crossVaryingVals;
            _staticVals = staticVariables;
            _frequency = frequency;

            //arguments for the ITrajectoryCreator constructor.
            object[] args = new object[5];
            //args[0] = _matlab;
            args[1] = _variablesList;
            args[2] = _crossVaryingVals;
            args[3] = _staticVals;
            args[4] = _frequency;

            _trajectoryCreator = trajectoryCreator;
        }

        /// <summary>
        /// Create a trajectory for both the ratHouseTrajectory and the landscapeHouseTrjectory for the control loop.
        /// </summary>
        /// <returns>The both ratHouseTrajectory and the landscapeHouseTrjectory.</returns>
        public Tuple<Trajectory2 ,Trajectory2> CreateTrajectory(int index = 0)
        {         
            return _trajectoryCreator.CreateTrialTrajectory(index);
        } 
        #endregion FUNCTIONS
    }
}
