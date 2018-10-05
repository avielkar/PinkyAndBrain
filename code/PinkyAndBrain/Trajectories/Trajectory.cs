using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trajectories
{
    public class Trajectory2:IList<Point>
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

        public int Count => X.Count;

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        Point IList<Point>.this[int index]
        {
            get => new Point()
            {
                X = X[index],
                Y = Y[index],
                Z = Z[index],
                RX = RX[index],
                RY = RY[index],
                RZ = RZ[index],
            };
            set
            {
                X[index] = value.X;
                Y[index] = value.Y;
                Z[index] = value.Z;
                RX[index] = value.RX;
                RY[index] = value.RY;
                RZ[index] = value.RZ;
            }
        }
        public object this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion ROTATION_TRAJECTORIES
        #endregion MEMBERS

        public Trajectory2()
        {

        }

        public void InsertOriginPlace(bool forward = true)
        {

            Point originPoint = new Point() { X = 0, Y = 0, Z = 0, RX = 0, RY = 0, RZ = 0 };

            //todo:decide if to return new one or the input one (chenged).
            if (!forward)
            {
                this.Add(originPoint);
            }
            else
            {
                this.Insert(0, originPoint);
            }
        }

        public int IndexOf(Point item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Point item)
        {
            List<double> x = X.ToList();
            List<double> y = Y.ToList();
            List<double> z = Z.ToList();
            List<double> rx = RX.ToList();
            List<double> ry = RY.ToList();
            List<double> rz = RZ.ToList();
            x.Insert(index, item.X);
            y.Insert(index,item.Y);
            z.Insert(index, item.Z);
            rx.Insert(index,item.RX);
            ry.Insert(index, item.RY);
            rz.Insert(index , item.RZ);

            X = Vector<double>.Build.Dense(x.ToArray());
            Y = Vector<double>.Build.Dense(y.ToArray());
            Z = Vector<double>.Build.Dense(z.ToArray());
            RX = Vector<double>.Build.Dense(rx.ToArray());
            RY = Vector<double>.Build.Dense(ry.ToArray());
            RZ = Vector<double>.Build.Dense(rz.ToArray());
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(Point item)
        {
            this.Insert(this.Count-1, item);
        }

        public void Clear()
        {
            X.Clear();
            Y.Clear();
            Z.Clear();
            RX.Clear();
            RY.Clear();
            RZ.Clear();
        }

        public bool Contains(Point item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Point[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Point item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Point> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
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
