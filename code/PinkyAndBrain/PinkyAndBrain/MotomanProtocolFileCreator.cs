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
        private string _fileName;

        private int _frequency;

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

        public int Frequency { get { return _frequency; } set { _frequency = value; } }

        public Trajectory TrajectoryPosition { get; set; }

        public Trajectory TrajectoryVelocity { get; set; }

        public bool UpdateJobJBIFile()
        {
            DecodeTrajectoriesToJBIFile(TrajectoryPosition);
            return true;
        }

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
                double velocity = (traj.x[i + 1] - traj.x[i]) * 10000 / (1000/_frequency);
                sb.Append(" V=");
                sb.Append(velocity.ToString("0000.0000000"));
                _fileStreamWriter.WriteLine(sb.ToString());
                sb.Clear();
            }

            sb.Append("MOVL ");
            sb.Append("P");
            sb.Append((traj.x.Count).ToString("D" + 5));
            double velocity2 = (traj.x[traj.x.Count - 1] - traj.x[traj.x.Count - 2]) * 10000 / (1000/_frequency);
            sb.Append(" V=");
            sb.Append(velocity2.ToString("0000.0000000"));
            _fileStreamWriter.WriteLine(sb.ToString());
            sb.Clear();

            _fileStreamWriter.WriteLine("END");

            _fileStreamWriter.Close();

            //_fileStreamWriter = new StreamWriter(@"C:\Users\User\Desktop\GAUSSIANMOVING2.JBI");
        }

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
                currectStringValue.Append(((double)(point * 10 + 249)).ToString("0000.00000000"));
                currectStringValue.Append(",16.200,273.300,-179.4000,-0.79,-160.6000");
                i++;
                stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();
            }

            return stringLinesList;
        }
    }
}
