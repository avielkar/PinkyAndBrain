using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
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
        public Vector<double> X { get; set; }

        /// <summary>
        /// The y axis for the vector list  position.
        /// </summary>
        public Vector<double> Y { get; set; }

        /// <summary>
        /// The z axis for the vector list position.
        /// </summary>
        public Vector<double> Z { get; set; }
        #endregion LINEAR_TRAJECTORIES

        #region ROTATION_TRAJECTORIES
        /// <summary>
        /// The x rotation axis for the vector list  position.
        /// </summary>
        public Vector<double> RX { get; set; }

        /// <summary>
        /// The y rotation axis for the vector list  position.
        /// </summary>
        public Vector<double> RY { get; set; }

        /// <summary>
        /// The z rotation axis for thevector list position.
        /// </summary>
        public Vector<double> RZ { get; set; }

        public int Count => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        public object this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion ROTATION_TRAJECTORIES
        #endregion MEMBERS

        public Trajectory2()
        {

        }

        public void InsertOriginPlace(bool forward = true)
        {

            //todo:decide if to return new one or the input one (chenged).
            if (!forward)
            {
                List<double> x = X.ToList();
                List<double> y = Y.ToList();
                List<double> z = Z.ToList();
                List<double> rx = RX.ToList();
                List<double> ry = RY.ToList();
                List<double> rz = RZ.ToList();
                x.Add(0);
                y.Add(0);
                z.Add(0);
                rx.Add(0);
                ry.Add(0);
                rz.Add(0);               

                X = Vector<double>.Build.Dense(x.ToArray());
                Y = Vector<double>.Build.Dense(y.ToArray());
                Z = Vector<double>.Build.Dense(z.ToArray());
                RX = Vector<double>.Build.Dense(rx.ToArray());
                RY = Vector<double>.Build.Dense(ry.ToArray());
                RZ = Vector<double>.Build.Dense(rz.ToArray());
            }
            else
            {
                List<double> x = X.ToList();
                x.Reverse();
                x.Add(0);
                x.Reverse();
                X = Vector<double>.Build.Dense(x.ToArray());

                List<double> y = Y.ToList();
                y.Reverse();
                y.Add(0);
                y.Reverse();
                Y = Vector<double>.Build.Dense(y.ToArray());

                List<double> z = Z.ToList();
                z.Reverse();
                z.Add(0);
                z.Reverse();
                Z = Vector<double>.Build.Dense(z.ToArray());

                List<double> rx = RX.ToList();
                rx.Reverse();
                rx.Add(0);
                rx.Reverse();
                RX = Vector<double>.Build.Dense(rx.ToArray());

                List<double> ry = RY.ToList();
                ry.Reverse();
                ry.Add(0);
                ry.Reverse();
                RY = Vector<double>.Build.Dense(ry.ToArray());

                List<double> rz = RZ.ToList();
                rz.Reverse();
                rz.Add(0);
                rz.Reverse();
                RZ = Vector<double>.Build.Dense(rz.ToArray());
            }
        }
    }

    public class Point
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double RX { get; set; }

        public double RY { get; set; }

        public double RZ { get; set; }
    }
}
