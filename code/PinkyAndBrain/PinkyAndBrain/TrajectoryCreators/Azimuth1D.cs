using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Integration;
using MindFusion.Charting;
using MindFusion.Charting.WinForms;
using System.Drawing;
using System.Windows.Forms;


namespace PinkyAndBrain.TrajectoryCreators
{    
    /// <summary>
    /// Trajectory creation according to Azimuth1D creator.
    /// It's method for 'Create' called each trial by it's handler.
    /// </summary>
    class Azimuth1D:ITrajectoryCreator
    {
        #region ATTRIBUTES
        /// <summary>
        /// The varying index to read now from the varyingCrossValues.
        /// </summary>
        private int _varyingCurrentIndex;

        /// <summary>
        /// The current stimulus type.
        /// </summary>
        private int _stimulusType;

        /// <summary>
        /// The number os points returned for a trajectory.
        /// </summary>
        private int _trajectorySampleNumber;

        /// <summary>
        /// Describes the heading direction.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _headingDirection;

        /// <summary>
        /// Describes the elevation amplitudes.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _discPlaneElevation;

        /// <summary>
        /// Describes the azimuth amplitudes.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _discPlaneAzimuth;

        /// <summary>
        /// Describes the tilt amplitudes.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _discPlaneTilt;

        /// <summary>
        /// Describes the heading distance should be done.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _distance;

        /// <summary>
        /// Describes the duration to make the move.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _duration;

        /// <summary>
        /// Describes the sigma of the gaussian for the trajectory.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _sigma;

        /// <summary>
        /// Describes the origin each trajectory begin from.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<Tuple<double, double, double>, Tuple<double, double, double>> _origin;

        /// <summary>
        /// Describes the adaptation angle size.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _adaptationAngle;

        /// <summary>
        /// The Matlab handler object.
        /// </summary>
        private MLApp.MLApp _matlabApp;

        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.
        /// </summary>
        private List<Dictionary<string, List<double>>> _crossVaryingVals;

        /// <summary>
        /// The static variables list in double value presentation.
        /// The string is for the variable name.
        /// The outer list is for the two inner list (or one , conditioned in the landscapeHouseParameter).
        /// The inners lists are for the values for each of the ratHouseParameter and landscapeHouseParameter (if there).
        /// The inners kist is with size 1 if the input is a scalar.
        /// Otherwise ,  if a vector , it would be a list with the size of the vector.
        /// </summary>
        private Dictionary<string, List<List<double>>> _staticVars;

        /// <summary>
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;
        #endregion ATTRIBUTES

        #region CONSTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Azimuth1D()
        {

        }

        /// <summary>
        /// ThreeStepAdapdation Constructor.
        /// </summary>
        /// <param name="variablesList">The variables list showen in the readen from the excel and changed by the main gui.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.</param>
        /// <param name="trajectorySampleNumber">The number of sample points for the trajectory.</param>
        public Azimuth1D(MLApp.MLApp matlabApp, Variables variablesList, List<Dictionary<string, List<double>>> crossVaryingVals, Dictionary<string, List<List<double>>> staticVals, int trajectorySampleNumber)
        {
            _matlabApp = matlabApp;
            _variablesList = variablesList;
            _crossVaryingVals = crossVaryingVals;
            _staticVars = staticVals;
            _frequency = trajectorySampleNumber;
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Generating a vector of sampled gaussian cdf with the given attributes.
        /// </summary>
        /// <param name="duration">The duraon for the trajectory.</param>
        /// <param name="sigma">The number of sigmas for the trajectory in the generated gayssian cdf.</param>
        /// <param name="magnitude">The mfgnitude of the trajectory.</param>
        /// <param name="frequency">The number of samples for the gaussian cdf to the trajectory.</param>
        /// <returns>
        /// The sampled gaussian cdf trajector.
        /// The vector length is as the fgiven frequency.
        /// </returns>
        public Vector<double> GenerateGaussianSampledCDF(double duration, double sigma, double magnitude, int frequency)
        {
            Vector<double> returnedVector = CreateVector.Dense<double>((int)(frequency * duration), time => magnitude * Normal.CDF(duration / 2, duration / (2 * sigma), (double)time / frequency));
            //MatlabPlotFunction(returnedVector);
            return returnedVector;
        }

        /// <summary>
        /// Computes the trajectoy tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory).
        /// </summary>
        /// <param name="index">The index from the crossVaryingList to take the attributes of he varying variables from.</param>
        /// <returns>The trajectory tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory). </returns>
        public Tuple<Trajectory, Trajectory> CreateTrialTrajectory(int index)
        {
            //reading the needed current trial parameters into the object members.
            ReadTrialParameters(index);

            //make the ratHouseDistance vector and the landscapeHouseDistance vector.
            Vector<double> ratHouseDistanceVector = GenerateGaussianSampledCDF(_duration.Item1, _sigma.Item1, _distance.Item1, _frequency);
            Vector<double> landscapeHouseDistanceVector = GenerateGaussianSampledCDF(_duration.Item2, _sigma.Item2, _distance.Item2, _frequency);

            //combined.
            if (_stimulusType == 3)
            {
                _headingDirection = new Tuple<double, double>(_headingDirection.Item1 + _adaptationAngle.Item1 * Math.PI * 180 / 2, _headingDirection.Item2 + _adaptationAngle.Item2 * Math.PI * 180 / 2);
            }

            //create the multiplication matrix to multiply with the distances vectors.
            Tuple<double, double, double> multiplyRatDistance = CreateMultiplyTuple(_headingDirection.Item1, _discPlaneAzimuth.Item1, _discPlaneElevation.Item1, _discPlaneTilt.Item1);
            Tuple<double, double, double> multiplyLandscapeDistance;
            //if it is to move both robots (vestribular only) or not.
            if (_stimulusType != 1)
                multiplyLandscapeDistance = CreateMultiplyTuple(_headingDirection.Item2, _discPlaneAzimuth.Item2, _discPlaneElevation.Item2, _discPlaneTilt.Item2);
            else
                multiplyLandscapeDistance = CreateMultiplyTuple(180 + _headingDirection.Item2, _discPlaneAzimuth.Item2, _discPlaneElevation.Item2, _discPlaneTilt.Item2);

            //multuply the distance vectors.
            Vector<double> lateralRatHouse = multiplyRatDistance.Item1 * ratHouseDistanceVector;
            Vector<double> surgeRatHouse = multiplyRatDistance.Item2 * ratHouseDistanceVector;
            Vector<double> heaveRatHouse = multiplyRatDistance.Item3 * ratHouseDistanceVector;
            Vector<double> lateralLandscapeHouse = multiplyLandscapeDistance.Item1 * landscapeHouseDistanceVector;
            Vector<double> surgeLandscapeHouse = multiplyLandscapeDistance.Item2 * landscapeHouseDistanceVector;
            Vector<double> heaveLandscapeHouse = multiplyLandscapeDistance.Item3 * landscapeHouseDistanceVector;

            Trajectory ratHouseTrajectory = new Trajectory();

            ratHouseTrajectory.x = lateralRatHouse;
            ratHouseTrajectory.y = surgeRatHouse;
            ratHouseTrajectory.z = heaveRatHouse;

            //rx - roll , ry - pitch , rz = yaw
            ratHouseTrajectory.rx = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            ratHouseTrajectory.ry = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            ratHouseTrajectory.rz = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);


            Trajectory landscapeHouseTrajectory = new Trajectory();
            landscapeHouseTrajectory.x = lateralLandscapeHouse;
            landscapeHouseTrajectory.y = surgeLandscapeHouse;
            landscapeHouseTrajectory.z = heaveLandscapeHouse;

            //rx - roll , ry - pitch , rz = yaw
            landscapeHouseTrajectory.rx = CreateVector.Dense<double>((int)(_frequency * _duration.Item2), 0);
            landscapeHouseTrajectory.ry = CreateVector.Dense<double>((int)(_frequency * _duration.Item2), 0);
            landscapeHouseTrajectory.rz = CreateVector.Dense<double>((int)(_frequency * _duration.Item2), 0);

            //visual only (landscapeHouseDistance only).
            if (_stimulusType == 2)
            {
                ratHouseTrajectory.x = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
                ratHouseTrajectory.y = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
                ratHouseTrajectory.z = CreateVector.Dense<double>((int)(_frequency * _duration.Item1), 0);
            }

            //if need to plot the trajectories
            if (Properties.Settings.Default.DrawTrialMovementGraph)
            {
                MatlabPlotTrajectoryFunction(ratHouseTrajectory);
            }

            return new Tuple<Trajectory, Trajectory>(ratHouseTrajectory, landscapeHouseTrajectory);
        }

        /// <summary>
        /// Creating the multiplier for each x,y,z movement by the amplitude , aimuth , elevtion and tilt.
        /// </summary>
        /// <param name="amplitude">Amplitude (degree).</param>
        /// <param name="azimuth">Azimuth (degree).</param>
        /// <param name="elevation">Elevation (degree).</param>
        /// <param name="tilt">Tilt (degree).</param>
        /// <returns></returns>
        public Tuple<double, double, double> CreateMultiplyTuple(double amplitude, double azimuth, double elevation, double tilt)
        {
            //converting the angles to radian.
            amplitude = amplitude * Math.PI / 180;
            azimuth = azimuth * Math.PI / 180;
            elevation = elevation * Math.PI / 180;
            tilt = tilt * Math.PI / 180;

            //making the multipliers vector.
            double xM = -Trig.Sin(amplitude) * Trig.Sin(azimuth) * Trig.Cos(tilt) +
                    Trig.Cos(amplitude) *
                    (Trig.Cos(azimuth) * Trig.Cos(elevation) + Trig.Sin(azimuth) * Trig.Sin(tilt) * Trig.Sin(elevation));

            double yM = -Trig.Sin(amplitude) * Trig.Cos(azimuth) * Trig.Cos(tilt) +
                        Trig.Cos(amplitude) *
                        (Trig.Sin(azimuth) * Trig.Cos(elevation) +
                        -Trig.Sin(azimuth) * Trig.Sin(tilt) * Trig.Sin(elevation));

            double zM = -Trig.Sin(amplitude) * Trig.Sin(tilt) - Trig.Cos(amplitude) * Trig.Sin(elevation) * Trig.Cos(tilt);

            //return the requested vector.
            return new Tuple<double, double, double>(xM, yM, zM);
        }

        /// <summary>
        /// Read the current trial needed parameters and insert them to the object members.
        /// </summary>
        public void ReadTrialParameters(int index)
        {
            Dictionary<string, List<double>> currentVaryingTrialParameters = _crossVaryingVals[index];

            //if there is no landscapeHouseParameter put the value of the ratHouseParameter for both (and at the sending , and before sending check the status of the stimulus type.
            if (_staticVars.ContainsKey("DIST"))
                _distance = new Tuple<double, double>(_staticVars["DIST"][0][0], _staticVars["DIST"].Count() > 1 ? _staticVars["DIST"][1][0] : _staticVars["DIST"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("DIST"))
                _distance = new Tuple<double, double>(currentVaryingTrialParameters["DIST"][0], (currentVaryingTrialParameters["DIST"].Count > 1) ? currentVaryingTrialParameters["DIST"][1] : currentVaryingTrialParameters["DIST"][0]);

            if (_staticVars.ContainsKey("DURATION"))
                _duration = new Tuple<double, double>(_staticVars["DURATION"][0][0], _staticVars["DURATION"].Count() > 1 ? _staticVars["DURATION"][1][0] : _staticVars["DURATION"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("DURATION"))
                _duration = new Tuple<double, double>(currentVaryingTrialParameters["DURATION"][0], (currentVaryingTrialParameters["DURATION"].Count > 1) ? currentVaryingTrialParameters["DURATION"][1] : currentVaryingTrialParameters["DURATION"][0]);

            if (_staticVars.ContainsKey("SIGMA"))
                _sigma = new Tuple<double, double>(_staticVars["SIGMA"][0][0], _staticVars["SIGMA"].Count() > 1 ? _staticVars["SIGMA"][1][0] : _staticVars["SIGMA"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("SIGMA"))
                _sigma = new Tuple<double, double>(currentVaryingTrialParameters["SIGMA"][0], (currentVaryingTrialParameters["SIGMA"].Count > 1) ? currentVaryingTrialParameters["SIGMA"][1] : currentVaryingTrialParameters["SIGMA"][0]);

            if (_staticVars.ContainsKey("ADAPTATION_ANGLE"))
                _adaptationAngle = new Tuple<double, double>(_staticVars["ADAPTATION_ANGLE"][0][0], _staticVars["ADAPTATION_ANGLE"].Count() > 1 ? _staticVars["ADAPTATION_ANGLE"][1][0] : _staticVars["ADAPTATION_ANGLE"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("ADAPTATION_ANGLE"))
                _adaptationAngle = new Tuple<double, double>(currentVaryingTrialParameters["ADAPTATION_ANGLE"][0], (currentVaryingTrialParameters["ADAPTATION_ANGLE"].Count > 1) ? currentVaryingTrialParameters["ADAPTATION_ANGLE"][1] : currentVaryingTrialParameters["ADAPTATION_ANGLE"][0]);

            if (_staticVars.ContainsKey("HEADING_DIRECTION"))
                _headingDirection = new Tuple<double, double>(_staticVars["HEADING_DIRECTION"][0][0], _staticVars["HEADING_DIRECTION"].Count() > 1 ? _staticVars["HEADING_DIRECTION"][1][0] : _staticVars["HEADING_DIRECTION"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("HEADING_DIRECTION"))
                _headingDirection = new Tuple<double, double>(currentVaryingTrialParameters["HEADING_DIRECTION"][0], (currentVaryingTrialParameters["HEADING_DIRECTION"].Count > 1) ? currentVaryingTrialParameters["HEADING_DIRECTION"][1] : currentVaryingTrialParameters["HEADING_DIRECTION"][0]);

            if (_staticVars.ContainsKey("DISC_PLANE_AZIMUTH"))
                _discPlaneAzimuth = new Tuple<double, double>(_staticVars["DISC_PLANE_AZIMUTH"][0][0], _staticVars["DISC_PLANE_AZIMUTH"].Count() > 1 ? _staticVars["DISC_PLANE_AZIMUTH"][1][0] : _staticVars["DISC_PLANE_AZIMUTH"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("DISC_PLANE_AZIMUTH"))
                _discPlaneAzimuth = new Tuple<double, double>(currentVaryingTrialParameters["DISC_PLANE_AZIMUTH"][0], (currentVaryingTrialParameters["DISC_PLANE_AZIMUTH"].Count > 1) ? currentVaryingTrialParameters["DISC_PLANE_AZIMUTH"][1] : currentVaryingTrialParameters["DISC_PLANE_AZIMUTH"][0]);

            if (_staticVars.ContainsKey("DISC_PLANE_ELEVATION"))
                _discPlaneElevation = new Tuple<double, double>(_staticVars["DISC_PLANE_ELEVATION"][0][0], _staticVars["DISC_PLANE_ELEVATION"].Count() > 1 ? _staticVars["DISC_PLANE_ELEVATION"][1][0] : _staticVars["DISC_PLANE_ELEVATION"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("DISC_PLANE_ELEVATION"))
                _discPlaneElevation = new Tuple<double, double>(currentVaryingTrialParameters["DISC_PLANE_ELEVATION"][0], (currentVaryingTrialParameters["DISC_PLANE_ELEVATION"].Count > 1) ? currentVaryingTrialParameters["DISC_PLANE_ELEVATION"][1] : currentVaryingTrialParameters["DISC_PLANE_ELEVATION"][0]);

            if (_staticVars.ContainsKey("DISC_PLANE_TILT"))
                _discPlaneTilt = new Tuple<double, double>(_staticVars["DISC_PLANE_TILT"][0][0], _staticVars["DISC_PLANE_TILT"].Count() > 1 ? _staticVars["DISC_PLANE_TILT"][1][0] : _staticVars["DISC_PLANE_TILT"][0][0]);
            else if (_crossVaryingVals[index].Keys.Contains("DISC_PLANE_TILT"))
                _discPlaneTilt = new Tuple<double, double>(currentVaryingTrialParameters["DISC_PLANE_TILT"][0], (currentVaryingTrialParameters["DISC_PLANE_TILT"].Count > 1) ? currentVaryingTrialParameters["DISC_PLANE_TILT"][1] : currentVaryingTrialParameters["DISC_PLANE_TILT"][0]);

            //check what about the stimulus type (that not must be static).
            if (_staticVars.ContainsKey("STIMULUS_TYPE"))
                _stimulusType = (int)(_staticVars["STIMULUS_TYPE"][0][0]);
            else if (currentVaryingTrialParameters.ContainsKey("STIMULUS_TYPE"))
            {
                _stimulusType = (int)currentVaryingTrialParameters["STIMULUS_TYPE"][0];
            }
            else if (_crossVaryingVals[index].Keys.Contains("STIMULUS_TYPE"))
            {
                _stimulusType = int.Parse(_variablesList._variablesDictionary["STIMULUS_TYPE"]._description["parameters"]._ratHouseParameter[0]);
            }
        }

        /// <summary>
        /// Plotting a vector into  new window for 2D function with MindFusion.
        /// </summary>
        /// <param name="drawingVector">
        /// The vector to be drawn into the graph.
        /// The x axis is the size of the vecor.
        /// The y axis is the vector.
        /// </param>
        public void MindFusionPlotFunction(Vector<double> drawingVector)
        {
            //the point array to insret to the point series object.
            PointF[] point = new PointF[drawingVector.Count()];

            //the list to put all the points to.
            List<PointF> pointsList = new List<PointF>();

            //the list to put the label for each point (can be 'x' , '.' , or "")
            List<string> labelsList = new List<string>();

            //put all the points and labels for each point to the list.
            for (int i = 0; i < point.Count(); i++)
            {
                point[i] = new PointF(i, (float)drawingVector[i]);
                pointsList.Add(point[i]);
                labelsList.Add("");
            }

            //determine the poit series with both the lists.
            PointSeries ps = new PointSeries(pointsList, labelsList);

            //draw the line chart in new window.
            LineChart lc = new LineChart();
            lc.Series.Add(ps);
            lc.Width = 400;
            lc.Height = 400;

            lc.ShowZoomWidgets = true;
            lc.AllowZoom = true;

            lc.XAxis.MinValue = 0;
            lc.XAxis.MaxValue = point.Count();

            //make a new form to put the graph on.
            Form f = new Form();
            f.Controls.Add(lc);
            f.Show();
        }

        /// <summary>
        /// Plotting a vector into  new window for 2D function with MATLAB.
        /// </summary>
        /// <param name="drawingVector">
        /// The vector to be drawn into the graph.
        /// The x axis is the size of the vecor.
        /// The y axis is the vector.
        /// </param>
        public void MatlabPlotFunction(Vector<double> drawingVector)
        {
            double[] dArray = ConvertVectorToArray(drawingVector);
            _matlabApp.Execute("figure;");
            _matlabApp.Execute("title('Trajectories')");
            _matlabApp.Execute("plot(drawingVector)");

        }

        /// <summary>
        /// Plotting all 6 attributes for the given trajectory.
        /// </summary>
        /// <param name="traj">The trajectory to be decomposed to it's 6 components and to plot in a figure.</param>
        public void MatlabPlotTrajectoryFunction(Trajectory traj)
        {

            _matlabApp.Execute("figure;");
            _matlabApp.Execute("title('Trajectories')");

            _matlabApp.PutWorkspaceData("rows", "base", (double)3);
            _matlabApp.PutWorkspaceData("columns", "base", (double)2);

            double[] dArray = ConvertVectorToArray(traj.x);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "x");
            _matlabApp.PutWorkspaceData("index", "base", (double)1);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.y);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "y");
            _matlabApp.PutWorkspaceData("index", "base", (double)2);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.z);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "z");
            _matlabApp.PutWorkspaceData("index", "base", (double)3);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.rx);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "rx");
            _matlabApp.PutWorkspaceData("index", "base", (double)4);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.ry);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "ry");
            _matlabApp.PutWorkspaceData("index", "base", (double)5);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.rz);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "rz");
            _matlabApp.PutWorkspaceData("index", "base", (double)6);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");
        }

        /// <summary>
        /// Converts a double vector type to double array type.
        /// </summary>
        /// <param name="vector">The vector to be converted.</param>
        /// <returns>The converted array.</returns>
        public double[] ConvertVectorToArray(Vector<double> vector)
        {
            double[] dArray = new double[vector.Count];
            for (int i = 0; i < dArray.Length; i++)
                dArray[i] = vector[i];

            return dArray;
        }
        #endregion FUNCTIONS
    }
}
