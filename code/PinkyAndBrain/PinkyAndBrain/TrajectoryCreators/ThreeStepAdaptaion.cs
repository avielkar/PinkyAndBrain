using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Integration;
using MindFusion.Charting;
using MindFusion.Charting.WinForms;
using System.Drawing;
using System.Windows.Forms;
using MLApp;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;


namespace PinkyAndBrain.TrajectoryCreators
{
    /// <summary>
    /// Trajectory creation according to ThreeStepAdaptation creator.
    /// It's method for 'Create' called each trial by it's handler.
    /// </summary>
    class ThreeStepAdaptaion:ITrajectoryCreator
    {
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
        /// Describes the heading distance should be done.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        /// </summary>
        private Tuple<double, double> _distance;

        /// Describes the duration to make the move.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        private Tuple<double, double> _duration;

        /// Describes the sigma of the gaussian for the trajectory.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        private Tuple<double, double> _sigma;

        /// Describes the origin each trajectory begin from.
        /// The first is for the ratHouseDescription.
        /// The second is for the lanscapeHouseDescription.
        private Tuple<Tuple<double, double, double>, Tuple<double, double, double>> origin;

        private MLApp.MLApp _matlabApp;


        private Vector<double> _vestibularVelocityVector;

        private Vector<double> _vestibularDistanceVector;

        /// <summary>
        /// ThreeStepAdapdation Constructor.
        /// </summary>
        /// <param name="variablesList">The variables list showen in the readen from the excel and changed by the main gui.</param>
        /// <param name="crossVaryingVals">Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.</param>
        /// <param name="trajectorySampleNumber">The number of sample points for the trajectory.</param>
        public ThreeStepAdaptaion(MLApp.MLApp matlabApp , Variables variablesList , List<Dictionary<string , List<double>>> crossVaryingVals , int trajectorySampleNumber)
        {
            _matlabApp = matlabApp;
        }

        /// <summary>
        /// Generates a gaussian velocity vector for the trajectory accoeding to the parameters.
        /// </summary>
        /// <param name="duration">Trajectory duration.</param>
        /// <param name="sigma">Trajectory standart deviatian.</param>
        /// <param name="magnitude">Trajectory amplitude.</param>
        /// <param name="frequency">Num of samples for the trajectory.</param>
        /// <returns>The sampled velocity vector for the trajectory.</returns>
        private Vector<double> GenererateGaussian(double duration , double sigma , int magnitude , int frequency)
        {
            Vector<double> sampleVector = CreateVector.Dense<double>(frequency, x => Normal.PDF(duration / 2, sigma, x));

            sampleVector = sampleVector * Math.Sqrt(2 * (Math.Pow(sigma, 2) * Math.PI));

            sampleVector = sampleVector / sampleVector.Max();

            sampleVector *= magnitude;

            return sampleVector;
        }

        public Vector<double> GenererateGaussianDirectly(double duration, double sigma, int magnitude, int frequency)
        {
            Normal.PDF(duration / 2, duration/(2 * sigma), frequency);
            //Vector<double> returnedVector = CreateVector.Dense<double>(frequency, time => magnitude * Normal.CDF(0, duration / (2 * sigma), ((time - frequency / 2)) * (duration / (8 * sigma))));
            Vector<double> returnedVector = CreateVector.Dense<double>(frequency, time => magnitude * Normal.CDF(duration/2, duration / (2 * sigma), (double)time/frequency));
            MindFusionPlotFunction(returnedVector);
            MatlabPlotFunction(returnedVector);
            return returnedVector;
        }

        /// <summary>
        /// Plotting a vector into  new window for 2D function.
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

        public void MatlabPlotFunction(Vector<double> drawingVector)
        {
            int data = 24;
            MWCellArray cell = new MWCellArray(2, 5);
            
            //mArray
            _matlabApp.Execute("gaussian_pdf");
            //_matlabApp.PutWorkspaceData("a", "base", mArray);
            _matlabApp.Execute("a+a");
        }
    }
}
