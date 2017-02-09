using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MLApp;
using System.Runtime.Remoting;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is called in each trial in order to create the trajectories to the current trial according to the current trial parmaters.
    /// </summary>
    class TrajectoryCreatorHandler
    {

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
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;

        private ITrajectoryCreator _trajectoryCreator;



        public TrajectoryCreatorHandler(MLApp.MLApp matlab)
        {
            //set the matlab engine object pointer.
            _matlab = matlab;

            //reset the varying index to read from.
            _varyingCurrentIndex = 0;
        }

        public void SetTrajectoryAttributes(string trajectoryName , Variables variableList , List<Dictionary<string,List<double>>> crossVaryingVals , int frequency)
        {
            _trajectoryCreatorName = trajectoryName;
            _variablesList = variableList;
            _crossVaryingVals = crossVaryingVals;
            _frequency = frequency;

            object[] args = new object[4];
            args[0] = _matlab;
            args[1] = _variablesList;
            args[2] = _crossVaryingVals;
            args[3] = _frequency;
            _trajectoryCreator = (ITrajectoryCreator)Activator.CreateInstance(Type.GetType("PinkyAndBrain.TrajectoryCreators." + _trajectoryCreatorName) , args);
        }

        public Vector<double> CreateTrajectory()
        {
            return _trajectoryCreator.GenererateGaussianSampledCDF(1, 3, 5, 1000);
        }
    }
}
