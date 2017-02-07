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
        Vector<double> GenererateGaussianDirectly(double duration, double sigma, int magnitude, int frequency);
    }
}
