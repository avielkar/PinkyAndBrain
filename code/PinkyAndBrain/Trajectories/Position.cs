using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trajectories
{
    public class Position
    {
        #region LINEAR_TRAJECTORY
        /// <summary>
        /// The x axis for the position.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The y axis for the position.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The z axis for the position.
        /// </summary>
        public double Z { get; set; }
        #endregion LINEAR_TRAJECTORY

        #region ROTATION_TRAJECTORY
        /// <summary>
        /// The x rotation axis for the position.
        /// </summary>
        public double RX { get; set; }
        /// <summary>
        /// The y rotation axis for the position.
        /// </summary>
        public double RY { get; set; }
        /// <summary>
        /// The z rotation axis for the position.
        /// </summary>
        public double RZ { get; set; }
        #endregion ROTATION_TRAJECTORY
    }
}
