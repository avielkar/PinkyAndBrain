using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is called at the beginning and the end of each trial.
    /// At the beginning in order to store the current trial parameters.
    /// At the end of the trial in order to store the responses and etc results.
    /// At the end of each trial in the experiment it saves the whole stored data into an txt file.
    /// </summary>
    class SavedDataMaker
    {
        /// <summary>
        /// The current file name (full path) writing to it the data.
        /// </summary>
        private string _cuurentFilePath;

        /// <summary>
        /// The current saving file StreamWriter to save the file with.
        /// </summary>
        private StreamWriter _currentSavedFileStramWriter;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SavedDataMaker()
        {
            _currentSavedFileStramWriter = null;
        }

        /// <summary>
        /// Save (write) a trial in the experiment to the current created new file.
        /// </summary>
        /// <param name="trialData">The trial data struct to written to the file.</param>
        public void SaveTrialDataToFile(TrialData trialData)
        {
            //create a new stringBuilder for line filling in the new created results file.
            StringBuilder lineBuilder = new StringBuilder();
            
            //append the new trial number.
            lineBuilder.Append("Trial:");
            lineBuilder.Append(trialData.TrialNum);
            _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
            lineBuilder.Clear();

            //append the rat name.
            lineBuilder.Append("Rat Name:");
            lineBuilder.Append(trialData.RatName);
            _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
            lineBuilder.Clear();

            //append the rat decision for the stimulus direction.
            lineBuilder.Append("Rat Decison:");
            lineBuilder.Append(trialData.RatDecison);
            _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
            lineBuilder.Clear();

            //append the RR inverse value (if flipped decision).
            lineBuilder.Append("RR Inverse:");
            lineBuilder.Append(trialData.RRInverse);
            _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
            lineBuilder.Clear();

            //append the auto's option statuses in the current trial.
            lineBuilder.Append(trialData.AutosOptions.ToString());
            _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
            lineBuilder.Clear();

            //appends all static variables names and values.
            foreach (string paramName in trialData.StaticVariables.Keys)
            {
                lineBuilder.Append(paramName);
                lineBuilder.Append(":");
                foreach (double value in trialData.StaticVariables[paramName][0])
                {
                    lineBuilder.Append(value);
                    lineBuilder.Append(" ");
                }

                _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
                lineBuilder.Clear();
            }

            //append all varying variables names and values.
            foreach (string paramName in trialData.VaryingVariables.Keys)
            {
                lineBuilder.Append(paramName);
                lineBuilder.Append(":");
                foreach (double value in trialData.VaryingVariables[paramName])
                {
                    lineBuilder.Append(value);
                    lineBuilder.Append(" ");
                }

                _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
                lineBuilder.Clear();
            }

            //append all timngs variables names and values.
            foreach (var field in typeof(ControlLoop.TrialTimings).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                lineBuilder.Append(field.Name);
                lineBuilder.Append(":");
                lineBuilder.Append(field.GetValue(trialData.TimingsVariables));

                _currentSavedFileStramWriter.WriteLine(lineBuilder.ToString());
                lineBuilder.Clear();
            }

            //flush the taxt to be written immediately.
            _currentSavedFileStramWriter.Flush();
        }

        /// <summary>
        /// Close and release the current saved file.
        /// </summary>
        public void CloseFile()
        {
            if (_currentSavedFileStramWriter != null) _currentSavedFileStramWriter.Dispose();
        }

        /// <summary>
        /// Create a new experiment result file to save in it new experiment data.
        /// </summary>
        public void CreateControlNewFile(string ratName)
        {
            //Creates a directory with the rat name abd the day in it if not exists.
            CreateNewDirectory(ratName);

            //create a new results file for the new experiment.
            _currentSavedFileStramWriter = File.CreateText(Application.StartupPath + @"\results\" + ratName + @"\" + DateTime.Now.ToString("yyyy_MM_dd") + @"\" + DateTime.Now.ToString("yyyy_MM_dd_HH-mm") + " Rat " + ratName + ".txt");
        }

        /// <summary>
        /// Creates a directory with the rat name abd the day in it if not exists.
        /// </summary>
        /// <param name="ratName">The rat name.</param>
        public void CreateNewDirectory(string ratName)
        {
            //create a rat directory if there is no rat dirextory with it's name.
            if (!Directory.Exists(Application.StartupPath + @"\results\" + ratName))
                Directory.CreateDirectory(Application.StartupPath + @"\results\" + ratName);

            if (!Directory.Exists(Application.StartupPath + @"\results\" + ratName + @"\" + DateTime.Now.ToString("yyyy_MM_dd")))
                Directory.CreateDirectory(Application.StartupPath + @"\results\" + ratName + @"\" + DateTime.Now.ToString("yyy_MM_dd"));
        }
    }

    /// <summary>
    /// TrialData compact class.
    /// </summary>
    class TrialData
    {
        /// <summary>
        /// The static variables value for one trial.
        /// </summary>
        public Dictionary<string , List<List<double>>> StaticVariables { get; set; }

        /// <summary>
        /// The varying varuiables value for one trial.
        /// </summary>
        public Dictionary<string , List<double>> VaryingVariables { get; set; }

        /// <summary>
        /// The timings variables for one trial.
        /// </summary>
        public ControlLoop.TrialTimings TimingsVariables { get; set; }

        /// <summary>
        /// The name of the rat being experiment.
        /// </summary>
        public String RatName { get; set; }

        /// <summary>
        /// The rat decision for the stimulus direction.
        /// </summary>
        public ControlLoop.RatDecison RatDecison { get; set; }

        /// <summary>
        /// The trial number in the experiment.
        /// </summary>
        public int TrialNum { get; set; }

        /// <summary>
        /// AutoOption class for all Autos values.
        /// </summary>
        public AutosOptions AutosOptions { get; set; }

        /// <summary>
        /// Indicates if in case of Random Heading Direction , the descision was converted to be true.
        /// </summary>
        public bool RRInverse { get; set; }
    }

    /// <summary>
    /// AutosOptions data class.
    /// </summary>
    public class AutosOptions
    {
        /// <summary>
        /// AutoReward state at the current trial.
        /// </summary>
        public bool AutoReward { get; set; }

        /// <summary>
        /// AutoFixation state at the current trial.
        /// </summary>
        public bool AutoFixation { get; set; }

        /// <summary>
        /// AutoStart state at the current trial.
        /// </summary>
        public bool AutoStart { get; set; }

        /// <summary>
        /// AutoRewardSound state at the current trial.
        /// </summary>
        public bool AutoRewardSound { get; set; }

        /// <summary>
        /// Return a string represent the object.
        /// </summary>
        /// <returns>The string represents the obhect.</returns>
        public override string ToString()
        {
            return
                "AutoReward:" + AutoReward + "\r\n" +
                "AutoFixation:" + AutoFixation + "\r\n" +
                "AutoStart:" + AutoStart + "\r\n" +
                "AutoRewardSound:" + AutoRewardSound;
        }
    }
}
