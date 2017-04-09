using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PinkyAndBrain
{
    class MotomanProtocolFileCreator
    {
        #region MEMBERS
        /// <summary>
        /// The JBI fileName to write the commands.
        /// </summary>
        private string _fileName;

        /// <summary>
        /// The frequency the commands rely on (to make the velocity).
        /// </summary>
        private int _frequency;
        #endregion MEMBERS

        #region CONSTRUCTOR
        /// <summary>
        /// Default constructor.
        /// <param name="fileName">
        /// The JBI file name (full path) to write to the JOB commands.
        /// The file is overwritten each time UpdateJobJBIFile is called.
        /// </param>
        /// <param name="frequency">The frequenct of the trajectory (points per second).</param>
        /// </summary>
        public MotomanProtocolFileCreator(string fileName , int frequency = 60)
        {
            _fileName = fileName;

            _frequency = frequency;
        }
        #endregion CONSTRUCTOR

        #region SETTERS_GETTERS
        /// <summary>
        /// Set or get the frequency the JBIFileCreator rely on.
        /// </summary>
        public int Frequency { get { return _frequency; } set { _frequency = value; } }
        #endregion SETTERS_GETTERS

        #region FUNCTIONS
        /// <summary>
        /// Get or set the trajectory position to be written to the controller JBI file.
        /// </summary>
        public Trajectory TrajectoryPosition { get; set; }

        /// <summary>
        /// Velocity between 3D points.
        /// </summary>
        /// <param name="xSource">The source x value.</param>
        /// <param name="xDesdination">The destination x value.</param>
        /// <param name="ySource">The source y value.</param>
        /// <param name="yDestination">The destination y value.</param>
        /// <param name="zSource">The z soure value.</param>
        /// <param name="zDestination">The z destination value.</param>
        /// <returns>The distance between the 2 3D points.</returns>
        public double Velocity3D(double xSource , double xDesdination , double ySource , double yDestination , double zSource , double zDestination)
        {
            double result =0;

            result+=Math.Pow((xDesdination-xSource) , 2);
            result+=Math.Pow((yDestination-ySource) , 2);
            result+=Math.Pow((zDestination-zSource) , 2);

            result = Math.Sqrt(result);

            return result;
        }

        /// <summary>
        /// Update (make) the JBI file that would be send to the controller with the new given trajectory.
        /// </summary>
        /// <returns>True if the send was successful.</returns>
        public bool UpdateJobJBIFile()
        {
            DecodeTrajectoriesToJBIFile(TrajectoryPosition);
            return true;
        }

        /// <summary>
        /// Decode the trajectory commands to a JBI file.
        /// </summary>
        /// <param name="traj">The traj to be written to the file as the protocol format.</param>
        private void DecodeTrajectoriesToJBIFile(Trajectory traj)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            _fileStreamWriter.WriteLine("/JOB");
            _fileStreamWriter.WriteLine("//NAME GAUSSIANMOVING2");
            _fileStreamWriter.WriteLine("//POS");
            _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(traj.x.Count + 1); _fileStreamWriter.WriteLine(",0,0");
            _fileStreamWriter.WriteLine("///TOOL 0");
            _fileStreamWriter.WriteLine("///POSTYPE ROBOT");
            _fileStreamWriter.WriteLine("///RECTAN");
            _fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
            _fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
            _fileStreamWriter.WriteLine("///POSTYPE BASE");

            foreach (string lineString in TrajectoriesToLine(traj))
            {
                _fileStreamWriter.WriteLine(lineString);
            }

            _fileStreamWriter.WriteLine("//INST");
            _fileStreamWriter.WriteLine("///DATE 2017/03/31 08:11");
            _fileStreamWriter.WriteLine("///ATTR SC,RW");
            _fileStreamWriter.WriteLine("///GROUP1 RB1");
            _fileStreamWriter.WriteLine("NOP");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < traj.x.Count - 1; i++)
            {
                sb.Append("MOVL ");
                sb.Append("P");
                sb.Append((i + 1).ToString("D" + 5));
                double velocity = Velocity3D(traj.x[i + 1], traj.x[i], traj.y[i + 1], traj.y[i], traj.z[i + 1], traj.z[i]) * 10000 / (1000 / _frequency);
                sb.Append(" V=");
                sb.Append(velocity.ToString("0000.0000000"));
                _fileStreamWriter.WriteLine(sb.ToString());
                sb.Clear();
            }

            sb.Append("MOVL ");
            sb.Append("P");
            sb.Append((traj.x.Count).ToString("D" + 5));
            double velocity2 = Velocity3D(traj.x[traj.x.Count - 1], traj.x[traj.x.Count - 2], traj.y[traj.y.Count - 1], traj.y[traj.y.Count - 2], traj.z[traj.z.Count - 1], traj.z[traj.z.Count - 2]) * 10000 / (1000 / _frequency);
            sb.Append(" V=");
            sb.Append(velocity2.ToString("0000.0000000"));
            _fileStreamWriter.WriteLine(sb.ToString());
            sb.Clear();

            _fileStreamWriter.WriteLine("END");

            _fileStreamWriter.Close();
        }

        /// <summary>
        /// Convert commands of one points in the trajectory to a commands lines in a JBI format.
        /// </summary>
        /// <param name="traj">The trajectories to be converted to commands line.</param>
        /// <returns>
        /// The list of commands strings.
        /// Every item in the list is a line command in the JBI file.
        /// </returns>
        private List<string> TrajectoriesToLine(Trajectory traj)
        {
            List<string> stringLinesList = new List<string>();

            StringBuilder currectStringValue = new StringBuilder();

            int i = 1;
            foreach (double point in traj.x)
            {
                currectStringValue.Append("P");
                currectStringValue.Append(i.ToString("D" + 5));
                currectStringValue.Append("=");
                currectStringValue.Append(((double)(point * 10 + 237.41)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                currectStringValue.Append(((double)(traj.y[i-1] * 10 -2.881)).ToString("0000.00000000"));
                currectStringValue.Append(",273.306,-178.8807,-3.0241,-161.3872");
                i++;
                stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();
            }

            return stringLinesList;
        }
        #endregion FUNCTIONS
    }
}
