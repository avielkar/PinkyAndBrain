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
        #endregion ATTRIBUTES

        #region CONTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ControlLoop(MLApp.MLApp matlabApp)
        {
            _matlabApp = matlabApp;
            _trajectoryCreatorHandler = new TrajectoryCreatorHandler(_matlabApp);
        }
        #endregion CONTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Transfer the control from the main gui to the control loop until a new gui event is handled by the user.
        /// </summary>
        public void Start(Variables variablesList, List<Dictionary<string, List<double>>> crossVaryingList, Dictionary<string , List<List<double>>> staticVariablesList  , int frequency , string trajectoryCreatorName)
        {
            _variablesList = variablesList;
            _crossVaryingVals = crossVaryingList;
            _staticVariablesList = staticVariablesList;
            _frequency = frequency;
            //_trajectoryCrator = new ThreeStepAdaptaion(_matlabApp, _variablesList, _crossVaryingVals, _frequency);

            //set the trajectory creator name to the given one that should be called in the trajectoryCreatorHandler.
            //also , set the other properties.
            _trajectoryCreatorHandler.SetTrajectoryAttributes(trajectoryCreatorName, _variablesList, _crossVaryingVals, _staticVariablesList, _frequency);

            _trajectoryCreatorHandler.CreateTrajectory();
        }
        #endregion FUNCTIONS
    }
}
