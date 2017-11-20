using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Integration;

namespace PinkyAndBrain.TrajectoryCreators
{
    class Azimuth3D:ITrajectoryCreator
    {

        public Vector<double> GenerateGaussianSampledCDF(double duration, double sigma, double magnitude, int frequency)
        {
            throw new NotImplementedException();
        }

        public void ReadTrialParameters(int index)
        {
            throw new NotImplementedException();
        }

        public Tuple<Trajectory, Trajectory> CreateTrialTrajectory(int index = 0)
        {
            throw new NotImplementedException();
        }
    }
}
