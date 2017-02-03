using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Integration;

namespace PinkyAndBrain.TrajectoryCreators
{
    /// <summary>
    /// Trajectory creation according to ThreeStepAdaptation creator.
    /// It's method for 'Create' called each trial by it's handler.
    /// </summary>
    class ThreeStepAdaptaion:ITrajectoryCreator
    {
        /// <summary>
        /// The varying index to read now from the varyingCrossValues.
        /// </summary>
        private int _varyingCurrentIndex;

        /// <summary>
        /// The current stimulus type.
        /// </summary>
        private int _stimulusType;

        /// <summary>
        /// The number os points returned for a trajectory.
        /// </summary>
        private int _trajectorySampleNumber;

        /// <summary>
        /// Describes the heading direction.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _headingDirection;

        /// <summary>
        /// Describes the heading distance should be done.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _distance;

        /// Describes the duration to make the move.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        private Tuple<double, double> _duration;

        /// Describes the sigma of the gaussian for the trajectory.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        private Tuple<double, double> _sigma;

        /// Describes the origin each trajectory begin from.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        private Tuple<Tuple<double, double, double>, Tuple<double, double, double>> origin;


        private Vector<double> _vestibularVelocityVector;

        private Vector<double> _vestibularDistanceVector;

        /// <summary>
        /// ThreeStepAdapdation Constructor.
        /// </summary>
        /// <param name="variablesList">The variables list showen in the readen from the excel and changed by the main gui.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.</param>
        /// <param name="trajectorySampleNumber">The number of sample points for the trajectory.</param>
        public ThreeStepAdaptaion(Variables variablesList , List<Dictionary<string , List<double>>> crossVaryingVals , int trajectorySampleNumber)
        {

        }

        /// <summary>
        /// Generates a gaussian velocity vector for the trajectory accoeding to the parameters.
        /// </summary>
        /// <param name="duration">Trajectory duration.</param>
        /// <param name="sigma">Trajectory standart deviatian.</param>
        /// <param name="magnitude">Trajectory amplitude.</param>
        /// <param name="frequency">Num of samples for the trajectory.</param>
        /// <returns>The sampled velocity vector for the trajectory.</returns>
        private Vector<double> GenererateGaussian(double duration , double sigma , int magnitude , int frequency)
        {
            Vector<double> sampleVector = CreateVector.Dense<double>(frequency, x => Normal.PDF(duration / 2, sigma, x));

            sampleVector = sampleVector * Math.Sqrt(2 * (Math.Pow(sigma, 2) * Math.PI));

            sampleVector = sampleVector / sampleVector.Max();

            sampleVector *= magnitude;

            return sampleVector;
        }

        private Vector<double> Create()
        {
            NewtonCotesTrapeziumRule.IntegrateAdaptive((x => _vestibularVelocityVector[x]), 0, _trajectorySampleNumber,0.0001);
        }
    }
}
