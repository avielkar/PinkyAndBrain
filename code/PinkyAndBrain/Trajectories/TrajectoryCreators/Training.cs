using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;
using MathNet.Numerics.LinearAlgebra;

namespace Trajectories
{
    /// <summary>
    /// Training creation according to Training creator.
    /// It's method for 'Create' called each trial by it's handler.
    /// </summary>
    public class Training : ITrajectoryCreator
    {
        #region ATTRIBUTES
        /// <summary>
        /// Describes the duration to make the move.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _duration;

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
        private Dictionary<string, List<List<double>>> _staticVars;

        /// <summary>
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;

        /// <summary>
        /// The Matlab handler object.
        /// </summary>
        private MLApp.MLApp _matlabApp;

        /// <summary>
        /// Indicates if to draw or not the movement graph for each trial.
        /// </summary>
        public bool DrawTrialMovementGraph { get; set; }
        #endregion ATTRIBUTES

        #region CONSTRUCTORS
        /// <summary>
        /// Defaulte Constructor.
        /// </summary>
        public Training()
        {

        }

        /// <summary>
        /// Training Constructor.
        /// </summary>
        /// <param name="variablesList">The variables list showen in the readen from the excel and changed by the main gui.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.</param>
        /// <param name="trajectorySampleNumber">The number of sample points for the trajectory.</param>
        public Training(MLApp.MLApp matlabApp, Variables variablesList, List<Dictionary<string, List<double>>> crossVaryingVals, Dictionary<string, List<List<double>>> staticVals, int trajectorySampleNumber)
        {
            _matlabApp = matlabApp;
            _variablesList = variablesList;
            _crossVaryingVals = crossVaryingVals;
            _staticVars = staticVals;
            _frequency = trajectorySampleNumber;
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Generating a vector of sampled gaussian cdf with the given attributes.
        /// </summary>
        /// <param name="duration">The duraon for the trajectory.</param>
        /// <param name="sigma">The number of sigmas for the trajectory in the generated gayssian cdf.</param>
        /// <param name="magnitude">The mfgnitude of the trajectory.</param>
        /// <param name="frequency">The number of samples for the gaussian cdf to the trajectory.</param>
        /// <returns>
        /// The sampled gaussian cdf trajector.
        /// The vector length is as the fgiven frequency.
        /// </returns>
        public Vector<double> GenerateGaussianSampledCDF(double duration, double sigma, double magnitude, int frequency)
        {
            Vector<double> returnedVector = CreateVector.Dense<double>(frequency * (int)duration,0);
            //MatlabPlotFunction(returnedVector);
            return returnedVector;
        }

        /// <summary>
        /// Computes the trajectoy tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory).
        /// </summary>
        /// <param name="index">The index from the crossVaryingList to take the attributes of he varying variables from.</param>
        /// <returns>The trajectory tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory). </returns>
        public Tuple<Trajectory, Trajectory> CreateTrialTrajectory(int index)
        {
            //reading the needed current trial parameters into the object members.
            ReadTrialParameters(index);

            Trajectory ratHouseTrajectory = new Trajectory();

            ratHouseTrajectory.x = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            ratHouseTrajectory.y = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            ratHouseTrajectory.z = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);

            //rx - roll , ry - pitch , rz = yaw
            ratHouseTrajectory.rx = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            ratHouseTrajectory.ry = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            ratHouseTrajectory.rz = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);


            Trajectory landscapeHouseTrajectory = new Trajectory();
            landscapeHouseTrajectory.x = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            landscapeHouseTrajectory.y = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            landscapeHouseTrajectory.z = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);

            //rx - roll , ry - pitch , rz = yaw
            landscapeHouseTrajectory.rx = CreateVector.Dense<double>(_frequency, 0);
            landscapeHouseTrajectory.ry = CreateVector.Dense<double>(_frequency, 0);
            landscapeHouseTrajectory.rz = CreateVector.Dense<double>(_frequency, 0);

            //if need to plot the trajectories
            if (DrawTrialMovementGraph)
            {
                MatlabPlotTrajectoryFunction(ratHouseTrajectory);
            }

            return new Tuple<Trajectory, Trajectory>(ratHouseTrajectory, landscapeHouseTrajectory);
        }

        /// <summary>
        /// Read the current trial needed parameters and insert them to the object members.
        /// </summary>
        public void ReadTrialParameters(int index)
        {
            Dictionary<string, List<double>> currentVaryingTrialParameters = _crossVaryingVals[index];

            if (_staticVars.ContainsKey("STIMULUS_DURATION"))
                _duration = new Tuple<double, double>(_staticVars["STIMULUS_DURATION"][0][0], _staticVars["STIMULUS_DURATION"].Count() > 1 ? _staticVars["STIMULUS_DURATION"][1][0] : _staticVars["STIMULUS_DURATION"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("STIMULUS_DURATION"))
            {
                _duration = new Tuple<double, double>(currentVaryingTrialParameters["STIMULUS_DURATION"][0], (currentVaryingTrialParameters["STIMULUS_DURATION"].Count > 1) ? currentVaryingTrialParameters["STIMULUS_DURATION"][1] : currentVaryingTrialParameters["STIMULUS_DURATION"][0]);
            }
        }

        /// <summary>
        /// Plotting a vector into  new window for 2D function with MATLAB.
        /// </summary>
        /// <param name="drawingVector">
        /// The vector to be drawn into the graph.
        /// The x axis is the size of the vecor.
        /// The y axis is the vector.
        /// </param>
        public void MatlabPlotFunction(Vector<double> drawingVector)
        {
            double[] dArray = ConvertVectorToArray(drawingVector);
            _matlabApp.Execute("figure;");
            _matlabApp.Execute("title('Trajectories')");
            _matlabApp.Execute("plot(drawingVector)");

        }

        /// <summary>
        /// Plotting all 6 attributes for the given trajectory.
        /// </summary>
        /// <param name="traj">The trajectory to be decomposed to it's 6 components and to plot in a figure.</param>
        public void MatlabPlotTrajectoryFunction(Trajectory traj)
        {

            _matlabApp.Execute("figure;");
            _matlabApp.Execute("title('Trajectories')");

            _matlabApp.PutWorkspaceData("rows", "base", (double)3);
            _matlabApp.PutWorkspaceData("columns", "base", (double)2);

            double[] dArray = ConvertVectorToArray(traj.x);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "x");
            _matlabApp.PutWorkspaceData("index", "base", (double)1);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.y);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "y");
            _matlabApp.PutWorkspaceData("index", "base", (double)2);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.z);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "z");
            _matlabApp.PutWorkspaceData("index", "base", (double)3);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.rx);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "rx");
            _matlabApp.PutWorkspaceData("index", "base", (double)4);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.ry);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "ry");
            _matlabApp.PutWorkspaceData("index", "base", (double)5);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.rz);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "rz");
            _matlabApp.PutWorkspaceData("index", "base", (double)6);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");
        }

        /// <summary>
        /// Converts a double vector type to double array type.
        /// </summary>
        /// <param name="vector">The vector to be converted.</param>
        /// <returns>The converted array.</returns>
        public double[] ConvertVectorToArray(Vector<double> vector)
        {
            double[] dArray = new double[vector.Count];
            for (int i = 0; i < dArray.Length; i++)
                dArray[i] = vector[i];

            return dArray;
        }
        #endregion FUNCTIONS
    }
}
