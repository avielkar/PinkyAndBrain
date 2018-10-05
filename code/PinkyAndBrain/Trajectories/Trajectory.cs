using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trajectories
{
    public class Trajectory2
    {
        #region MEMBERS
        #region LINEAR_TRAJECTORIES

        /// <summary>
        /// The x axis for the vector list position.
        /// </summary>
        private Vector<double> _x;

        /// <summary>
        /// The y axis for the vector list  position.
        /// </summary>
        private Vector<double> _y;

        /// <summary>
        /// The z axis for the vector list position.
        /// </summary>
        private Vector<double> _z;
        #endregion LINEAR_TRAJECTORIES

        #region ROTATION_TRAJECTORIES
        /// <summary>
        /// The x rotation axis for the vector list  position.
        /// </summary>
        private Vector<double> _rx;

        /// <summary>
        /// The y rotation axis for the vector list  position.
        /// </summary>
        private Vector<double> _ry;

        /// <summary>
        /// The z rotation axis for thevector list position.
        /// </summary>
        private Vector<double> _rz;
        #endregion ROTATION_TRAJECTORIES
        #endregion MEMBERS


    }
}
