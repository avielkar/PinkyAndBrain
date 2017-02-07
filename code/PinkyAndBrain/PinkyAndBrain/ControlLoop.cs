using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinkyAndBrain.TrajectoryCreators;
using MathNet.Numerics.LinearAlgebra;
using MLApp;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is the main program controll loop.
    /// It calls all the needed other inerfaces to make what it's needed to be created.
    /// The function is called by the GuiInterface after the statButton is clicked.
    /// </summary>
    class ControlLoop
    {
        /// <summary>
        /// The trajectory creator interface for making the trajectory for each trial.
        /// </summary>
        private ITrajectoryCreator _trajectoryCrator;

        private Variables _variablesList;

        private List<Dictionary<string, List<double>>> _crossVaryingVals;

        private int _frequency;

        private MLApp.MLApp _matlabApp;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ControlLoop(MLApp.MLApp matlabApp)
        {
            _matlabApp = matlabApp;
        }

        /// <summary>
        /// Transfer the control from the main gui to the control loop until a new gui event is handled by the user.
        /// </summary>
        public void Start(Variables variablesList, List<Dictionary<string, List<double>>> crossVaryingList, int frequency)
        {
            _variablesList = variablesList;
            _crossVaryingVals = crossVaryingList;
            _frequency = frequency;
            _trajectoryCrator = new ThreeStepAdaptaion(_matlabApp, _variablesList, _crossVaryingVals, _frequency);
            _trajectoryCrator.GenererateGaussianDirectly(1, 3, 8, _frequency);
            
        }
    }
}
