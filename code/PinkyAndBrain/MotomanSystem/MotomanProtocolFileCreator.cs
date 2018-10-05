using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Trajectories;
using System.Configuration;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.LinearAlgebra.Double;
using MotomanSystem;
using Vector = MathNet.Numerics.LinearAlgebra.Complex.Vector;

namespace PinkyAndBrain
{
    public class MotomanProtocolFileCreator
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
        /// <summary>
        /// Get or set the R1 robot trajectory position to be written to the controller JBI file.
        /// </summary>
        public Trajectory2 TrajectoryR1Position { get; set; }

        /// <summary>
        /// Get or set the R2 robot trajectory position to be written to the controller JBI file.
        /// </summary>
        public Trajectory2 TrajectoryR2Position { get; set; }

        /// <summary>
        /// The trial number to send to the AlphaOmega (with time sharing - before moving the robot sending half of 14 bits and after moving the robot sending the second half).
        /// </summary>
        public int TrialNum { get; set; }
        #endregion SETTERS_GETTERS

        #region FUNCTIONS
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
        /// Calaculates the velocity between 3D Points.
        /// </summary>
        /// <param name="source">The source position.</param>
        /// <param name="destination">The destination poisition.</param>
        /// <returns>The 3D velocity for the points.</returns>
        public double Velocity3D(Position source , Position destination)
        {
            //todo:chek what about the other 3 axes : rx , ry , rz.
            return Velocity3D(source.X, destination.X,
                              source.Y, destination.Y,
                              source.Z, destination.Z);
        }

        /// <summary>
        /// Update (make) the JBI file that would be send to the controller with the new given trajectory.
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        /// <param name="returnBackMotion">Indicate if the motion is backword.</param>
        /// </summary>
        /// <returns>True if the send was successful.</returns>
        public bool UpdateJobJBIFile(UpdateJobType updateJobType ,bool returnBackMotion)
        {
            DecodeTrajectoriesToJBIFile(TrajectoryR1Position , TrajectoryR2Position , updateJobType , returnBackMotion);
            return true;
        }

        public void UpdateSpecificPosJBIFile(string jbiFileName , Position r1Pos, Position r2Pos, double velocity)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            StringBuilder lineStringBuilder = new StringBuilder();

            _fileStreamWriter.WriteLine("/JOB");
            _fileStreamWriter.WriteLine("//NAME " + jbiFileName);
            _fileStreamWriter.WriteLine("//POS");
            _fileStreamWriter.WriteLine("///NPOS 0,0,0,4,0,0");
            _fileStreamWriter.WriteLine("///TOOL 0");
            _fileStreamWriter.WriteLine("///POSTYPE ROBOT");
            _fileStreamWriter.WriteLine("///RECTAN");
            _fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
            _fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
            _fileStreamWriter.WriteLine("///POSTYPE BASE");

            lineStringBuilder.Append("P00001=");
            lineStringBuilder.Append(r1Pos.X.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.Y.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.Z.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.RX.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.RY.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r1Pos.RZ.ToString("0000.0000000"));

            _fileStreamWriter.WriteLine(lineStringBuilder.ToString());
            lineStringBuilder.Clear();

            lineStringBuilder.Append("P00002=");
            lineStringBuilder.Append(r2Pos.X.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.Y.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.Z.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.RX.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.RY.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(r2Pos.RZ.ToString("0000.0000000"));

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
        /// <param name="returnBackMotion">Indicate if the motion is backword.</param>
        /// </summary>
        private void DecodeTrajectoriesToJBIFile(Trajectory2 r1Traj , Trajectory2 r2Traj , UpdateJobType updateJobType , bool returnBackMotion)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            _fileStreamWriter.WriteLine("/JOB");
            _fileStreamWriter.WriteLine("//NAME GAUSSIANMOVING2");
            _fileStreamWriter.WriteLine("//POS");

            switch (updateJobType)
            {
                case UpdateJobType.R1Only:
                    _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(r1Traj.Count + 1); _fileStreamWriter.WriteLine(",0,0");
                    break;
                case UpdateJobType.R2Only:
                    _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(r2Traj.Count + 1); _fileStreamWriter.WriteLine(",0,0");
                    break;
                case UpdateJobType.Both:
                    _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(r1Traj.Count + r2Traj.Count + 1); _fileStreamWriter.WriteLine(",0,0");
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

            //adding the zero point place for the trajectory (for the velocity calculaion behind) at the end if it is backward or at the beginning if it is forward movement.
            //also, for the backward movement it skip the last point (because the robot is already there from the forward movement) and added the 0 placed to the end of the trajectory.
            if (!returnBackMotion)
            {
                foreach (string lineString in TrajectoriesToLine(r1Traj, r2Traj, updateJobType))
                {
                    _fileStreamWriter.WriteLine(lineString);
                }

                r1Traj.InsertOriginPlace(true);
                r2Traj.InsertOriginPlace(true);
            }
            else
            {
                r1Traj.InsertOriginPlace(false);
                r2Traj.InsertOriginPlace(false);

                Position firstPositionR1 = r1Traj[0];
                Position firstPositionR2 = r2Traj[0];

                r1Traj.RemoveAt(0);
                r2Traj.RemoveAt(0);

                foreach (string lineString in TrajectoriesToLine(r1Traj, r2Traj, updateJobType))
                {
                    _fileStreamWriter.WriteLine(lineString);
                }

                r1Traj.Insert(0, firstPositionR1);
                r2Traj.Insert(0, firstPositionR2);
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

            if (!returnBackMotion)
            {
                //turn off the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) OFF");

                //add the trial number with turnning the 2-14 indexes bits.
                //_fileStreamWriter.Write(MakeDoutsPins(DecToBin(TrialNum)));
                _fileStreamWriter.WriteLine("DOUT OT#(14) ON");

                //turn on the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) ON");
            }

            else
            {
                //turn on the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) OFF");

                //turn on the digital output indication the robot start moving backword.
                _fileStreamWriter.WriteLine("DOUT OT#(15) ON");
                
                //turn on the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) ON");
            }

            StringBuilder sb = new StringBuilder();
            //the selected trajectory is for the for loop to init with r1 or r2 as needed for the UpdateJobType.
            Trajectory2 selecterRobotTraj = (!updateJobType.Equals(UpdateJobType.R2Only))?(r1Traj):(r2Traj);
            double originalX = (!updateJobType.Equals(UpdateJobType.R2Only)) ? (MotocomSettings.Default.R1OriginalX) : (MotocomSettings.Default.R2OriginalX);
            double originalY = (!updateJobType.Equals(UpdateJobType.R2Only)) ? (MotocomSettings.Default.R1OriginalY) : (MotocomSettings.Default.R2OriginalY);
            double originalZ = (!updateJobType.Equals(UpdateJobType.R2Only)) ? (MotocomSettings.Default.R1OriginalZ) : (MotocomSettings.Default.R2OriginalZ);

            //make the f * duration velocity points vector from the f * duratoin + 1 places points in the trajectory.
            for (int i = 0; i < selecterRobotTraj.Count - 1; i++)
            {
                //decode the velocity for the selected robot (if only one of then) or the first robot (r1) if both of them.
                sb.Append("MOVL ");
                sb.Append("P");
                sb.Append((i + 1).ToString("D" + 5));
                double velocity = Velocity3D(selecterRobotTraj[i + 1], selecterRobotTraj[i]) * 10000.0 / (1000.0 / (double)(_frequency));
                sb.Append(" V=");
                sb.Append(velocity.ToString("0000.00000000"));

                if (updateJobType.Equals(UpdateJobType.Both))
                {
                    sb.Append("  +MOVL ");
                    sb.Append("P");
                    sb.Append((selecterRobotTraj.Count + i + 1).ToString("D" + 5));
                    double velocity12 = Velocity3D(r2Traj[i + 1], r2Traj[i]) * 10000.0 / (1000.0 / (double)(_frequency));
                    sb.Append(" V=");
                    sb.Append(velocity12.ToString("0000.00000000"));
                }

                _fileStreamWriter.WriteLine(sb.ToString());
                sb.Clear();
            }
            

            if (!returnBackMotion)
            {
                //turn off the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) OFF");

                //reset the trial number bits(2-14)
                //_fileStreamWriter.Write(ResetDoutPins());
                _fileStreamWriter.WriteLine("DOUT OT#(14) OFF");

                //turn on the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) ON");
            }

            else
            {
                //turn off the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) OFF");

                //turn off the digital output indication the robot start moving backword.
                _fileStreamWriter.WriteLine("DOUT OT#(15) OFF");

                //turn off the strobe bit (16)
                _fileStreamWriter.WriteLine("DOUT OT#(16) ON");
            }

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
        private List<string> TrajectoriesToLine(Trajectory2 trajR1, Trajectory2 trajR2 , UpdateJobType updateJobType)
        {
            List<string> stringLinesList = new List<string>();

            StringBuilder currectStringValue = new StringBuilder();
    
            //if need to encode the r1 robot for movement.
            int i = 1;
            if (updateJobType.Equals(UpdateJobType.R1Only) || updateJobType.Equals(UpdateJobType.Both))
            {
                //setting the tool0 for the robot0.
                currectStringValue.Append("///TOOL0");
                stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();

                //setting all the points for the robot0.
                foreach (double point in trajR1.X)
                {
                    currectStringValue.Append("P");
                    currectStringValue.Append(i.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point * 10 + MotocomSettings.Default.R1OriginalX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.Y[i - 1] * 10 + MotocomSettings.Default.R1OriginalY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.Z[i - 1] * 10 + MotocomSettings.Default.R1OriginalZ)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.RX[i - 1] * 10 + MotocomSettings.Default.R1OriginalRX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.RY[i - 1] * 10 + MotocomSettings.Default.R1OriginalRY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR1.RZ[i - 1] * 10 + MotocomSettings.Default.R1OriginalRZ)).ToString("0000.00000000"));
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

                //setting the tool1 for the robot1
                currectStringValue.Append("///TOOL1");
                stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();

                //setting all the points for the robot1.
                foreach (double point in trajR2.X)
                {
                    currectStringValue.Append("P");
                    currectStringValue.Append(j.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point * 10 + MotocomSettings.Default.R2OriginalX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.Y[i - 1] * 10 + MotocomSettings.Default.R2OriginalY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.Z[i - 1] * 10 + MotocomSettings.Default.R2OriginalZ)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.RX[i - 1] * 10 + MotocomSettings.Default.R2OriginalRX)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.RY[i - 1] * 10 + MotocomSettings.Default.R2OriginalRY)).ToString("0000.00000000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(trajR2.RZ[i - 1] * 10 + MotocomSettings.Default.R2OriginalRZ)).ToString("0000.00000000"));
                    i++;
                    j++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                }
            }

            return stringLinesList;
        }

        /// <summary>
        /// Converts a 14 digits decimal number to a binary array. Throw exception if the number is bigger than 14 disits representation.
        /// </summary>
        /// <param name="num">The number top be converted to a binary representation.</param>
        /// <returns>The binary representation of the number.</returns>
        public bool[] DecToBin(int num)
        {
            if (num > Math.Pow(2, 14))
            {
                throw new Exception("The number cannot be represented by 13 digits");
            }

            else
            {
                int index = 0;
                bool[] binValue = new bool[14];

                while(num > 0)
                {
                    binValue[index] = !((num % 2) == 0);

                    index++;

                    num = num / 2;
                }

                return binValue;
            }
        }

        /// <summary>
        /// Reset the Dout Pins 1-13 for the trial data number.
        /// </summary>
        /// <returns>The reset trial data number string.</returns>
        public string ResetDoutPins()
        {
            bool[] binValue = new bool[14];

            for (int i = 0; i < binValue.Length;i++ )
            {
                binValue[i] = false;
            }

                return MakeDoutsPins(binValue);
        }

        /// <summary>
        /// Making the string for the output pins (1-13) for the AlphaOmega.
        /// </summary>
        /// <param name="binValue">The binary value to be sent to the AlphaOmega.</param>
        /// <returns>The string represents the command for the AlphaOmega for the number sending.</returns>
        public string MakeDoutsPins(bool [] binValue)
        {
            int bitIndex = 0;
            StringBuilder sb = new StringBuilder();

            foreach (bool bitValue in binValue)
            {
                sb.Append("DOUT OT#(");
                if(bitIndex +1 <9)
                sb.Append((bitIndex+ 1).ToString("0"));
                else
                    sb.Append((bitIndex + 1).ToString("00"));    

                if (bitValue)
                    sb.Append(") ON");
                else
                    sb.Append(") OFF");

                sb.AppendLine();

                bitIndex++;
            }

            return sb.ToString();
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

        /// <summary>
        /// The function inserts the zero place (at the first of at the end of the trajectory) to the trajectory according to the trajectory forward/backward type.
        /// </summary>
        /// <param name="traj">The trajectory.</param>
        /// <param name="forward">Indicates if the movement is forward of backward.</param>
        /// <returns></returns>
        /*public Trajectory InsertOriginPlace(Trajectory traj, bool forward = true)
        {
            //todo:decide if to return new one or the input one (chenged).
            if (!forward)
            {
                List<double> x = traj.x.ToList();
                List<double> y = traj.y.ToList();
                List<double> z = traj.z.ToList();
                List<double> rx = traj.rx.ToList();
                List<double> ry = traj.ry.ToList();
                List<double> rz = traj.rz.ToList();
                x.Add(0);
                y.Add(0);
                z.Add(0);
                rx.Add(0);
                ry.Add(0);
                rz.Add(0);

                traj.x = Vector<double>.Build.Dense(x.ToArray());
                traj.y = Vector<double>.Build.Dense(y.ToArray());
                traj.z = Vector<double>.Build.Dense(z.ToArray());
                traj.rx = Vector<double>.Build.Dense(rx.ToArray());
                traj.ry = Vector<double>.Build.Dense(ry.ToArray());
                traj.rz = Vector<double>.Build.Dense(rz.ToArray());

                return traj;
            }
            else
            {
                List<double> x = traj.x.ToList();
                x.Reverse();
                x.Add(0);
                x.Reverse();
                traj.x = Vector<double>.Build.Dense(x.ToArray());

                List<double> y = traj.y.ToList();
                y.Reverse();
                y.Add(0);
                y.Reverse();
                traj.y = Vector<double>.Build.Dense(y.ToArray());

                List<double> z = traj.z.ToList();
                z.Reverse();
                z.Add(0);
                z.Reverse();
                traj.z = Vector<double>.Build.Dense(z.ToArray());

                List<double> rx = traj.rx.ToList();
                rx.Reverse();
                rx.Add(0);
                rx.Reverse();
                traj.rx = Vector<double>.Build.Dense(rx.ToArray());

                List<double> ry = traj.ry.ToList();
                ry.Reverse();
                ry.Add(0);
                ry.Reverse();
                traj.ry = Vector<double>.Build.Dense(ry.ToArray());

                List<double> rz = traj.rz.ToList();
                rz.Reverse();
                rz.Add(0);
                rz.Reverse();
                traj.rz = Vector<double>.Build.Dense(rz.ToArray());

                return traj;
            }
        }*/
    }
}
