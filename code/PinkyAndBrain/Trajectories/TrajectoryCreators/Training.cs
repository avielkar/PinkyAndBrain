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
        /// </summary>
        private double _duration;

        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for ratHouseParameters.
        /// </summary>
        private List<Dictionary<string, double>> _crossVaryingVals;

        /// <summary>
        /// The static variables list in double value presentation.
        /// The string is for the variable name.
        /// </summary>
        private Dictionary<string, double> _staticVars;

        /// <summary>
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;
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
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for ratHouseParameters.</param>
        /// <param name="trajectorySampleNumber">The number of sample points for the trajectory.</param>
        public Training(Variables variablesList, List<Dictionary<string, double>> crossVaryingVals, Dictionary<string, double> staticVals, int trajectorySampleNumber)
        {
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
            return returnedVector;
        }

        /// <summary>
        /// Computes the trajectoy tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory).
        /// </summary>
        /// <param name="index">The index from the crossVaryingList to take the attributes of he varying variables from.</param>
        /// <returns>The trajectory tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory). </returns>
        public Tuple<Trajectory2, Trajectory2> CreateTrialTrajectory(int index)
        {
            //reading the needed current trial parameters into the object members.
            ReadTrialParameters(index);

            Trajectory2 ratHouseTrajectory = new Trajectory2()
            {
                X = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                Y = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                Z = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                //rx - roll , ry - pitch , rz = yaw
                RX = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                RY = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                RZ = CreateVector.Dense<double>((int)(_frequency * _duration), 0)
            };


            Trajectory2 landscapeHouseTrajectory = new Trajectory2()
            {
                X = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                Y = CreateVector.Dense<double>((int)(_frequency * _duration), 0),

                Z = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                //rx - roll , ry - pitch , rz = yaw
                RX = CreateVector.Dense<double>(_frequency, 0),
                RY = CreateVector.Dense<double>(_frequency, 0),
                RZ = CreateVector.Dense<double>(_frequency, 0)
            };

            return new Tuple<Trajectory2, Trajectory2>(ratHouseTrajectory, landscapeHouseTrajectory);
        }

        /// <summary>
        /// Read the current trial needed parameters and insert them to the object members.
        /// </summary>
        public void ReadTrialParameters(int index)
        {
            Dictionary<string, double> currentVaryingTrialParameters = _crossVaryingVals[index];

            if (_staticVars.ContainsKey("STIMULUS_DURATION"))
                _duration = _staticVars["STIMULUS_DURATION"];
            else if (_crossVaryingVals[index].Keys.Contains("STIMULUS_DURATION"))
            {
                _duration = currentVaryingTrialParameters["STIMULUS_DURATION"];
            }
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
