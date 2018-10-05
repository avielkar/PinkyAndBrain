using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Integration;
using System.Drawing;
using System.Windows.Forms;
using MLApp;
using System.Reflection;
using MindFusion.Charting;
using MindFusion.Charting.WinForms;

namespace Trajectories
{
    public class HeadingDiscrimination:ITrajectoryCreator
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
        /// </summary>
        private double _headingDirection;

        /// <summary>
        /// Describes the elevation amplitudes.
        /// </summary>
        private double _discPlaneElevation;

        /// <summary>
        /// Describes the azimuth amplitudes.
        /// </summary>
        private double _discPlaneAzimuth;

        /// <summary>
        /// Describes the tilt amplitudes.
        /// </summary>
        private double _discPlaneTilt;

        /// <summary>
        /// Describes the heading distance should be done.
        /// </summary>
        private double _distance;

        /// <summary>
        /// Describes the duration to make the move.
        /// </summary>
        private double _duration;

        /// <summary>
        /// Describes the sigma of the gaussian for the trajectory.
        /// </summary>
        private double _sigma;

        /// <summary>
        /// Describes the adaptation angle size.
        /// </summary>
        private double _adaptationAngle;

        /// <summary>
        /// The delta value for the difference between the heading directions for Stimuluses type 4 and 5 (combined).
        /// </summary>
        private double _deltaHeading;

        /// <summary>
        /// The Matlab handler object.
        /// </summary>
        private MLApp.MLApp _matlabApp;

        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters.
        /// </summary>
        private List<Dictionary<string, double>> _crossVaryingVals;

        /// <summary>
        /// The static variables list in double value presentation.
        /// The string is for the variable name.
        /// </summary>
        private Dictionary<string, double> _staticVars;

        /// <summary>
        /// The numbers of samples for each trajectory.
        /// </summary>
        private int _frequency;

        /// <summary>
        /// Indicates if to draw or not the movement graph for each trial.
        /// </summary>
        public bool DrawTrialMovementGraph { get; set; }
        #endregion ATTRIBUTES

        #region CONSTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HeadingDiscrimination()
        {

        }

        /// <summary>
        /// ThreeStepAdapdation Constructor.
        /// </summary>
        /// <param name="variablesList">The variables list showen in the readen from the excel and changed by the main gui.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.</param>
        /// <param name="trajectorySampleNumber">The number of sample points for the trajectory.</param>
        public HeadingDiscrimination(MLApp.MLApp matlabApp, Variables variablesList, List<Dictionary<string, double>> crossVaryingVals, Dictionary<string, double> staticVals, int trajectorySampleNumber)
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
            //generate a vector with one more point sampling the gaussian.
            Vector<double> returnedVector = CreateVector.Dense<double>((int)(frequency * duration) + 1, time => magnitude * Normal.CDF(duration / 2, duration / (2 * sigma), (double)time / frequency));
            for (int i = 1; i < returnedVector.Count; i++)
            {
                /*
                 * decrease from each sampled point the first point'
                 * so that the gaussian would begin with the a value that it's distance
                 * from the 0 would not be higher than the distance to the second point. i.e x0-0<x1-x0.
                 */
                returnedVector[i] -= returnedVector[0];
            }

            //remove the first point we need only for decreasing from other points.
            returnedVector = returnedVector.SubVector(1, frequency);

            //MatlabPlotFunction(returnedVector);
            return returnedVector;
        }

        /// <summary>
        /// Computes the trajectoy tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory).
        /// </summary>
        /// <param name="index">The index from the crossVaryingList to take the attributes of he varying variables from.</param>
        /// <returns>The trajectory tuple (for the ratHouseTrajectory and for the landscapeHouseTrajectory). </returns>
        public Tuple<Trajectory2, Trajectory2> CreateTrialTrajectory(int index)
        {
            //reading the needed current trial parameters into the object members.
            ReadTrialParameters(index);

            //make the ratHouseDistance vector.
            Vector<double> ratHouseDistanceVector = GenerateGaussianSampledCDF(_duration, _sigma, _distance, _frequency);

            //combined.
            if (_stimulusType == 3)
            {
                _headingDirection = _headingDirection + _adaptationAngle * Math.PI * 180 / 2;
            }

            //create the multiplication matrix to multiply with the distances vectors.
            Tuple<double, double, double> multiplyRatDistance;
            Tuple<double, double, double> multiplyLandscapeDistance;

            //if it is to move the landscapeHouseRobot with no Delta.
            if (_stimulusType == 1)
            {
                //todo::check what to add to the tilt in the future (now th tilt is always 0 - so nevermind midwhile).
                multiplyRatDistance = CreateMultiplyTuple(_headingDirection, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
                multiplyLandscapeDistance = CreateMultiplyTuple(_headingDirection, 180 + _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
            }
            else if (_stimulusType == 2 || _stimulusType == 10 || _stimulusType == 12)//stim type 12 would repace stim type 10
            {
                multiplyRatDistance = new Tuple<double, double, double>(0, 0, 0);
                multiplyLandscapeDistance = CreateMultiplyTuple(_headingDirection, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
            }
            //if not to move the landscapeHouseRobot.
            else if (_stimulusType == 3 || _stimulusType == 11 || _stimulusType == 13)//stim type 13 would repace stim type 11
            {
                multiplyRatDistance = CreateMultiplyTuple(_headingDirection, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
                multiplyLandscapeDistance = new Tuple<double, double, double>(0, 0, 0);
            }
            //_stimulusType = 4 move the landscapeHouseParameter with  negative delta.
            //_stimulusType = 14 move the landscapeHouseParameter with  negative delta in the dark.
            else if (_stimulusType == 4 || _stimulusType == 14)
            {
                //todo::check here with adam about adding the 180 here also (i think they dont run 3d protocol woth delta - but may run in the future.
                multiplyRatDistance = CreateMultiplyTuple(_headingDirection - _deltaHeading / 2, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
                multiplyLandscapeDistance = CreateMultiplyTuple(_headingDirection + _deltaHeading / 2, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
            }
            //_stimulusType = 5 move the landscapeHouseParameter with  positive delta.
            //_stimulusType = 15 move the landscapeHouseParameter with  positive delta in the dark.
            else
            {
                //todo::check here with adam about adding the 180 here also (i think they dont run 3d protocol woth delta - but may run in the future.
                multiplyRatDistance = CreateMultiplyTuple(_headingDirection + _deltaHeading / 2, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
                multiplyLandscapeDistance = CreateMultiplyTuple(_headingDirection - _deltaHeading / 2, _discPlaneAzimuth, _discPlaneElevation, _discPlaneTilt);
            }

            //multuply the distance vectors for ratHouseRobot.
            Vector<double> lateralRatHouse = multiplyRatDistance.Item1 * ratHouseDistanceVector;
            Vector<double> surgeRatHouse = multiplyRatDistance.Item2 * ratHouseDistanceVector;
            Vector<double> heaveRatHouse = multiplyRatDistance.Item3 * ratHouseDistanceVector;

            //multuply the distance vectors for lanscapeHouseRobot.
            Vector<double> lateralLandscapeHouse = multiplyLandscapeDistance.Item1 * ratHouseDistanceVector;
            Vector<double> surgeLandscapeHouse = multiplyLandscapeDistance.Item2 * ratHouseDistanceVector;
            Vector<double> heaveLandscapeHouse = multiplyLandscapeDistance.Item3 * ratHouseDistanceVector;

            if (_stimulusType == 4 || _stimulusType == 5 || _stimulusType == 14 || _stimulusType == 15)
            {
                //decreaere the needed value for the delta to the landscapeHouseRobot.
                lateralLandscapeHouse = lateralLandscapeHouse - lateralRatHouse;
                surgeLandscapeHouse = surgeLandscapeHouse - surgeRatHouse;
                heaveLandscapeHouse = heaveLandscapeHouse - heaveRatHouse;
            }

            Trajectory2 ratHouseTrajectory = new Trajectory2()
            {
                X = lateralRatHouse,
                Y = surgeRatHouse,
                Z = heaveRatHouse,
                //rx - roll , ry - pitch , rz = yaw
                RX = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                RY = CreateVector.Dense<double>((int)(_frequency * _duration), 0),
                RZ = CreateVector.Dense<double>((int)(_frequency * _duration), 0)
            };
                                                               

            Trajectory2 landscapeHouseTrajectory = new Trajectory2();
            landscapeHouseTrajectory.X = lateralLandscapeHouse;
            landscapeHouseTrajectory.Y = surgeLandscapeHouse;
            landscapeHouseTrajectory.Z = heaveLandscapeHouse;
            //rx - roll , ry - pitch , rz = yaw
            landscapeHouseTrajectory.RX = CreateVector.Dense<double>((int)(_frequency*_duration), 0);
            landscapeHouseTrajectory.RY = CreateVector.Dense<double>((int)(_frequency * _duration), 0);
            landscapeHouseTrajectory.RZ = CreateVector.Dense<double>((int)(_frequency * _duration), 0);

            //visual only (landscapeHouseDistance only).
            if(_stimulusType == 2)
            {
                ratHouseTrajectory.X = CreateVector.Dense<double>((int)(_frequency*_duration), 0);
                ratHouseTrajectory.Y = CreateVector.Dense<double>((int)(_frequency*_duration), 0);
                ratHouseTrajectory.Z = CreateVector.Dense<double>((int)(_frequency*_duration), 0);
            }

            //combined - only ratHouseDistance
            if(_stimulusType == 3)
            {
                landscapeHouseTrajectory.X = CreateVector.Dense<double>((int)(_frequency * _duration), 0);
                landscapeHouseTrajectory.Y = CreateVector.Dense<double>((int)(_frequency * _duration), 0);
                landscapeHouseTrajectory.Z = CreateVector.Dense<double>((int)(_frequency * _duration), 0);
            }

            //if need to plot the trajectories
            if (DrawTrialMovementGraph)
            {
                MatlabPlotTrajectoryFunction(ratHouseTrajectory);
            }

            return new Tuple<Trajectory2, Trajectory2>(ratHouseTrajectory, landscapeHouseTrajectory);
        }

        /// <summary>
        /// Creating the multiplier for each x,y,z movement by the amplitude , aimuth , elevtion and tilt.
        /// </summary>
        /// <param name="amplitude">Amplitude (degree).</param>
        /// <param name="azimuth">Azimuth (degree).</param>
        /// <param name="elevation">Elevation (degree).</param>
        /// <param name="tilt">Tilt (degree).</param>
        /// <returns></returns>
        public Tuple<double , double , double> CreateMultiplyTuple(double amplitude , double azimuth , double elevation , double tilt)
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

            double yM = Trig.Sin(amplitude) * Trig.Cos(azimuth) * Trig.Cos(tilt) +
                        Trig.Cos(amplitude) *
                        (Trig.Sin(azimuth) * Trig.Cos(elevation) +
                        -Trig.Cos(azimuth) * Trig.Sin(tilt) * Trig.Sin(elevation));
            //the axis for y and z are opposite in the Moog.
            yM = -yM;

            double zM = -Trig.Sin(amplitude) * Trig.Sin(tilt) - Trig.Cos(amplitude) * Trig.Sin(elevation) * Trig.Cos(tilt);
            //the axis for y and z are opposite in the Moog.
            zM = -zM;

            //return the requested vector.
            return new Tuple<double, double, double>(xM, yM, zM);
        }

        /// <summary>
        /// Read the current trial needed parameters and insert them to the object members.
        /// </summary>
        public void ReadTrialParameters(int index)
        {
            Dictionary<string, double> currentVaryingTrialParameters = _crossVaryingVals[index];

            //if there is no landscapeHouseParameter put the value of the ratHouseParameter for both (and at the sending , and before sending check the status of the stimulus type.
            if (_staticVars.ContainsKey("DIST"))
                _distance = _staticVars["DIST"];
            else if (_crossVaryingVals[index].Keys.Contains("DIST"))
                _distance = currentVaryingTrialParameters["DIST"];

            if (_staticVars.ContainsKey("STIMULUS_DURATION"))
                _duration = _staticVars["STIMULUS_DURATION"];
            else if (_crossVaryingVals[index].Keys.Contains("STIMULUS_DURATION"))
                _duration = currentVaryingTrialParameters["STIMULUS_DURATION"];

            if (_staticVars.ContainsKey("SIGMA"))
                _sigma = _staticVars["SIGMA"];
            else if (_crossVaryingVals[index].Keys.Contains("SIGMA"))
                _sigma = currentVaryingTrialParameters["SIGMA"];

            if (_staticVars.ContainsKey("ADAPTATION_ANGLE"))
                _adaptationAngle = _staticVars["ADAPTATION_ANGLE"];
            else if (_crossVaryingVals[index].Keys.Contains("ADAPTATION_ANGLE"))
                _adaptationAngle = currentVaryingTrialParameters["ADAPTATION_ANGLE"];

            if (_staticVars.ContainsKey("HEADING_DIRECTION"))
                _headingDirection = _staticVars["HEADING_DIRECTION"];
            else if (_crossVaryingVals[index].Keys.Contains("HEADING_DIRECTION"))
                _headingDirection = currentVaryingTrialParameters["HEADING_DIRECTION"];

            if (_staticVars.ContainsKey("DISC_PLANE_AZIMUTH"))
                _discPlaneAzimuth = _staticVars["DISC_PLANE_AZIMUTH"];
            else if (_crossVaryingVals[index].Keys.Contains("DISC_PLANE_AZIMUTH"))
                _discPlaneAzimuth = currentVaryingTrialParameters["DISC_PLANE_AZIMUTH"];

            if (_staticVars.ContainsKey("DISC_PLANE_ELEVATION"))
                _discPlaneElevation = _staticVars["DISC_PLANE_ELEVATION"];
            else if (_crossVaryingVals[index].Keys.Contains("DISC_PLANE_ELEVATION"))
                _discPlaneElevation = currentVaryingTrialParameters["DISC_PLANE_ELEVATION"];

            if (_staticVars.ContainsKey("DISC_PLANE_TILT"))
                _discPlaneTilt = _staticVars["DISC_PLANE_TILT"];
            else if (_crossVaryingVals[index].Keys.Contains("DISC_PLANE_TILT"))
                _discPlaneTilt = currentVaryingTrialParameters["DISC_PLANE_TILT"];

            if (_staticVars.ContainsKey("DELTA"))
                _deltaHeading = _staticVars["DELTA"];
            else if (_crossVaryingVals[index].Keys.Contains("DELTA"))
                _deltaHeading = currentVaryingTrialParameters["DELTA"];

            //check what about the stimulus type (that not must be static).
            if (_staticVars.ContainsKey("STIMULUS_TYPE"))
                _stimulusType = (int)_staticVars["STIMULUS_TYPE"];
            else if (currentVaryingTrialParameters.ContainsKey("STIMULUS_TYPE"))
            {
                _stimulusType = (int)currentVaryingTrialParameters["STIMULUS_TYPE"];
            }
            else if (_crossVaryingVals[index].Keys.Contains("STIMULUS_TYPE"))
            {
                _stimulusType = int.Parse(_variablesList._variablesDictionary["STIMULUS_TYPE"]._description["parameters"]._ratHouseParameter);
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
            PointSeries ps = new PointSeries(pointsList , labelsList);
            
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
        public void MatlabPlotTrajectoryFunction(Trajectory2 traj)
        {

            _matlabApp.Execute("figure;");
            _matlabApp.Execute("title('Trajectories')");

            _matlabApp.PutWorkspaceData("rows", "base", (double)3);
            _matlabApp.PutWorkspaceData("columns", "base", (double)2);

            double[] dArray = ConvertVectorToArray(traj.X);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "x");
            _matlabApp.PutWorkspaceData("index", "base", (double)1);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.Y);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "y");
            _matlabApp.PutWorkspaceData("index", "base", (double)2);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.Z);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "z");
            _matlabApp.PutWorkspaceData("index", "base", (double)3);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.RX);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "rx");
            _matlabApp.PutWorkspaceData("index", "base", (double)4);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.RY);
            _matlabApp.PutWorkspaceData("drawingVector", "base", dArray);
            _matlabApp.PutWorkspaceData("subplotGraphName", "base", "ry");
            _matlabApp.PutWorkspaceData("index", "base", (double)5);
            _matlabApp.Execute("subplot(rows , columns , index)");
            _matlabApp.Execute("plot(drawingVector)");
            _matlabApp.Execute("title(subplotGraphName)");

            dArray = ConvertVectorToArray(traj.RZ);
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
        public double [] ConvertVectorToArray(Vector<double> vector)
        {
            double[] dArray = new double[vector.Count];
            for (int i = 0; i < dArray.Length; i++)
                dArray[i] = vector[i];

            return dArray;
        }
        #endregion FUNCTIONS
    }
}
