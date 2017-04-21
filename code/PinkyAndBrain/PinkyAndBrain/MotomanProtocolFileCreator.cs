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
        /// Get or set the R1 robot trajectory position to be written to the controller JBI file.
        /// </summary>
        public Trajectory TrajectoryR1Position { get; set; }

        /// <summary>
        /// Get or set the R2 robot trajectory position to be written to the controller JBI file.
        /// </summary>
        public Trajectory TrajectoryR2Position { get; set; }

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
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        /// </summary>
        /// <returns>True if the send was successful.</returns>
        public bool UpdateJobJBIFile(UpdateJobType updateJobType)
        {
            DecodeTrajectoriesToJBIFile(TrajectoryR1Position , TrajectoryR2Position , updateJobType);
            return true;
        }

        public void UpdateHomePosJBIFile(Position r1Pos , Position r2Pos , double velocity)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            StringBuilder lineStringBuilder = new StringBuilder();

            _fileStreamWriter.WriteLine("/JOB");
            _fileStreamWriter.WriteLine("//NAME HOME_POS_BOTH");
            _fileStreamWriter.WriteLine("//POS");
            _fileStreamWriter.WriteLine("///NPOS 0,0,0,4,0,0");
            _fileStreamWriter.WriteLine("///TOOL 0");
            _fileStreamWriter.WriteLine("///POSTYPE ROBOT");
            _fileStreamWriter.WriteLine("///RECTAN");
            _fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
            _fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
            _fileStreamWriter.WriteLine("///POSTYPE BASE");

            lineStringBuilder.Append("P00001=");
            lineStringBuilder.Append(r1Pos.x.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.y.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.z.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.rx.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.ry.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.rz.ToString("0000.0000000"));

            _fileStreamWriter.WriteLine(lineStringBuilder.ToString());
            lineStringBuilder.Clear();

            lineStringBuilder.Append("P00002=");
            lineStringBuilder.Append(r2Pos.x.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.y.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.z.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.rx.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.ry.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.rz.ToString("0000.0000000"));

            _fileStreamWriter.WriteLine(lineStringBuilder.ToString());
            lineStringBuilder.Clear();

            _fileStreamWriter.WriteLine("//INST");
            _fileStreamWriter.WriteLine("///DATE 2017/03/31 08:11");
            _fileStreamWriter.WriteLine("///COMM PLAYINGTWOROBOTS");
            _fileStreamWriter.WriteLine("///ATTR SC,RW");
            _fileStreamWriter.WriteLine("///GROUP1 RB1");
            _fileStreamWriter.WriteLine("///GROUP2 RB2");
            _fileStreamWriter.WriteLine("NOP");

            lineStringBuilder.Append("MOVL P00001 V=");
            lineStringBuilder.Append(velocity.ToString("0000.0000000"));
            lineStringBuilder.Append("  +MOVL P00002 V=");
            lineStringBuilder.Append(velocity.ToString("0000.0000000"));

            _fileStreamWriter.WriteLine(lineStringBuilder);
            lineStringBuilder.Clear();

            _fileStreamWriter.WriteLine("END");

            _fileStreamWriter.Close();
        }

        /// <summary>
        /// Decode the trajectory commands to a JBI file.
        /// <param name="r1Traj">The r1 robot traj to be written to the file as the protocol format.</param>
        /// <param name="r2Traj">The r2 robot traj to be written to the file as the protocol format.</param>
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        /// </summary>
        private void DecodeTrajectoriesToJBIFile(Trajectory r1Traj , Trajectory r2Traj , UpdateJobType updateJobType)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            _fileStreamWriter.WriteLine("/JOB");
            _fileStreamWriter.WriteLine("//NAME GAUSSIANMOVING2");
            _fileStreamWriter.WriteLine("//POS");

            switch (updateJobType)
            {
                case UpdateJobType.R1Only:
                    _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(r1Traj.x.Count + 1); _fileStreamWriter.WriteLine(",0,0");
                    break;
                case UpdateJobType.R2Only:
                    _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(r2Traj.x.Count + 1); _fileStreamWriter.WriteLine(",0,0");
                    break;
                case UpdateJobType.Both:
                    _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(r1Traj.x.Count + r2Traj.x.Count + 1); _fileStreamWriter.WriteLine(",0,0");
                    break;
                default:
                    break;
            }

            _fileStreamWriter.WriteLine("///TOOL 0");
            _fileStreamWriter.WriteLine("///POSTYPE ROBOT");
            _fileStreamWriter.WriteLine("///RECTAN");
            _fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
            _fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
            _fileStreamWriter.WriteLine("///POSTYPE BASE");

            foreach (string lineString in TrajectoriesToLine(r1Traj , r2Traj , updateJobType))
            {
                _fileStreamWriter.WriteLine(lineString);
            }

            _fileStreamWriter.WriteLine("//INST");
            _fileStreamWriter.WriteLine("///DATE 2017/03/31 08:11");

            if(updateJobType.Equals(UpdateJobType.Both))
                _fileStreamWriter.WriteLine("///COMM PLAYINGTWOROBOTS");

            _fileStreamWriter.WriteLine("///ATTR SC,RW");

            switch (updateJobType)
            {
                case UpdateJobType.R1Only:
                    _fileStreamWriter.WriteLine("///GROUP1 RB1");
                    break;
                case UpdateJobType.R2Only:
                    _fileStreamWriter.WriteLine("///GROUP1 RB2");
                    break;
                case UpdateJobType.Both:
                    _fileStreamWriter.WriteLine("///GROUP1 RB1");
                    _fileStreamWriter.WriteLine("///GROUP2 RB2");
                    break;
                default:
                    break;
            }
            
            _fileStreamWriter.WriteLine("NOP");

            StringBuilder sb = new StringBuilder();
            //the selected trajectory is for the for loop to init with r1 or r2 as needed for the UpdateJobType.
            Trajectory selecterRobotTraj = (!updateJobType.Equals(UpdateJobType.R2Only))?(r1Traj):(r2Traj);
            for (int i = 0; i < selecterRobotTraj.x.Count - 1; i++)
            {
                //decode the velocity for the selected robot (if only one of then) or the first robot (r1) if both of them.
                sb.Append("MOVL ");
                sb.Append("P");
                sb.Append((i + 1).ToString("D" + 5));
                double velocity = Velocity3D(selecterRobotTraj.x[i + 1],
                    selecterRobotTraj.x[i],
                    selecterRobotTraj.y[i + 1],
                    selecterRobotTraj.y[i],
                    selecterRobotTraj.z[i + 1],
                    selecterRobotTraj.z[i])
                    * 10000 / (1000 / _frequency);
                sb.Append(" V=");
                sb.Append(velocity.ToString("0000.0000000"));

                if (updateJobType.Equals(UpdateJobType.Both))
                {
                    sb.Append("  +MOVL ");
                    sb.Append("P");
                    sb.Append((selecterRobotTraj.x.Count + 1 + i).ToString("D" + 5));
                    double velocity12 = Velocity3D(r2Traj.x[i + 1], r2Traj.x[i], r2Traj.y[i + 1], r2Traj.y[i], r2Traj.z[i + 1], r2Traj.z[i]) * 10000 / (1000 / _frequency);
                    sb.Append(" V=");
                    sb.Append(velocity12.ToString("0000.0000000"));
                }

                _fileStreamWriter.WriteLine(sb.ToString());
                sb.Clear();
            }

            //decode the velocity for the selected robot (if only one of then) or the first robot (r1) if both of them.
            sb.Append("MOVL ");
            sb.Append("P");
            sb.Append((selecterRobotTraj.x.Count).ToString("D" + 5));
            double velocity2 = Velocity3D(selecterRobotTraj.x[selecterRobotTraj.x.Count - 1],
                selecterRobotTraj.x[selecterRobotTraj.x.Count - 2], 
                selecterRobotTraj.y[selecterRobotTraj.y.Count - 1],
                selecterRobotTraj.y[selecterRobotTraj.y.Count - 2],
                selecterRobotTraj.z[selecterRobotTraj.z.Count - 1],
                selecterRobotTraj.z[selecterRobotTraj.z.Count - 2])
                * 10000 / (1000 / _frequency);
            sb.Append(" V=");
            sb.Append(velocity2.ToString("0000.0000000"));

            if (updateJobType.Equals(UpdateJobType.Both))
            {
                sb.Append("  +MOVL ");
                sb.Append("P");
                sb.Append((selecterRobotTraj.x.Count * 2).ToString("D" + 5));
                double velocity21 = Velocity3D(r2Traj.x[r2Traj.x.Count - 1],
                    r2Traj.x[r2Traj.x.Count - 2], r2Traj.y[r2Traj.y.Count - 1],
                    r2Traj.y[r2Traj.y.Count - 2], r2Traj.z[r2Traj.z.Count - 1],
                    r2Traj.z[r2Traj.z.Count - 2])
                    * 10000 / (1000 / _frequency);
                sb.Append(" V=");
                sb.Append(velocity21.ToString("0000.0000000"));
            }

            _fileStreamWriter.WriteLine(sb.ToString());
            sb.Clear();

            _fileStreamWriter.WriteLine("END");

            _fileStreamWriter.Close();
        }

        /// <summary>
        /// Convert commands of one points in the trajectory to a commands lines in a JBI format.
        /// </summary>
        /// <param name="trajR1">The trajectories of robot r1 to be converted to commands line (if needed as the updateJobType).</param>
        /// <param name="trajR2">The trajectories of robot r2 to be converted to commands line (if needed as the updateJobType).</param>
        /// <returns>
        /// The list of commands strings.
        /// Every item in the list is a line command in the JBI file.
        /// </returns>
        private List<string> TrajectoriesToLine(Trajectory trajR1, Trajectory trajR2 , UpdateJobType updateJobType)
        {
            List<string> stringLinesList = new List<string>();

            StringBuilder currectStringValue = new StringBuilder();
    
            //if need to encode the r1 robot for movement.
            int i = 1;
            if (updateJobType.Equals(UpdateJobType.R1Only) || updateJobType.Equals(UpdateJobType.Both))
            {
                foreach (double point in trajR1.x)
                {
                    currectStringValue.Append("P");
                    currectStringValue.Append(i.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point * 10 + Properties.Settings.Default.R1OriginalX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.y[i - 1] * 10 + Properties.Settings.Default.R1OriginalY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.z[i - 1] * 10 + Properties.Settings.Default.R1OriginalZ)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.rx[i - 1] * 10 + Properties.Settings.Default.R1OriginalRX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.ry[i - 1] * 10 + Properties.Settings.Default.R1OriginalRY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.rz[i - 1] * 10 + Properties.Settings.Default.R1OriginalRZ)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    i++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                }
            }

            //if need to encode the r2 robot for movement.
            if (updateJobType.Equals(UpdateJobType.R2Only) || updateJobType.Equals(UpdateJobType.Both))
            {
                int j = i;
                i = 1;
                foreach (double point in trajR2.x)
                {
                    currectStringValue.Append("P");
                    currectStringValue.Append(j.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point * 10 + Properties.Settings.Default.R2OriginalX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.y[i - 1] * 10 + Properties.Settings.Default.R2OriginalY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.z[i - 1] * 10  +Properties.Settings.Default.R2OriginalZ)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.rx[i - 1] * 10 + Properties.Settings.Default.R2OriginalRX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.ry[i - 1] * 10 + Properties.Settings.Default.R2OriginalRY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.rz[i - 1] * 10 + Properties.Settings.Default.R2OriginalRZ)).ToString("0000.00000000"));
                    i++;
                    j++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                }
            }

            return stringLinesList;
        }
        #endregion FUNCTIONS

        /// <summary>
        /// The update robot/s job trajectory selection.
        /// </summary>
        public enum UpdateJobType
        {
            /// <summary>
            /// Update the job only with R1 trajectory.
            /// </summary>
            R1Only=1,

            /// <summary>
            /// Update the job only with R2 trajectory.
            /// </summary>
            R2Only=2,

            /// <summary>
            /// Update the job for both R1 and R2 trajectories.
            /// </summary>
            Both=3
        }
    }
}
