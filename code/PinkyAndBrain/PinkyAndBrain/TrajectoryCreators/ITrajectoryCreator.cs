using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace PinkyAndBrain
{
    /// <summary>
    /// Describes all base functions and members that protocol trajectory creators should implement.
    /// </summary>
    interface ITrajectoryCreator
    {
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
        Vector<double> GenerateGaussianSampledCDF(double duration, double sigma, double magnitude, int frequency);

        /// <summary>
        /// Read the current trial needed parameters and insert them to the object members.
        /// </summary>
        void ReadTrialParameters(int index);

        /// <summary>
        /// Computes the trajectoy tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory).
        /// </summary>
        /// <param name="index">The index from the crossVaryingList to take the attributes of he varying variables from.</param>
        /// <returns>The trajectory tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory). </returns>
        Tuple<Trajectory, Trajectory> CreateTrialTrajectory(int index);
    }
}
