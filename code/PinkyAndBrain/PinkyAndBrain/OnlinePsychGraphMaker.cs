using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class attempt to show the reponses psycometric reponses acoording to the user responses untill the current round.
    /// In the case of staicases it should show as many graphs as the staircases.
    /// This class may use the MATLAB Engine.
    /// </summary>
    class OnlinePsychGraphMaker
    {

        public List<string> VaryingParametrsNames { get; set; }

        public Region HeadingDireactionRegion { get; set; }

        public Chart ChartControl { get; set; }

        public Delegate ClearDelegate { get; set; }

        public Delegate SetSeriesDelegate { get; set; }

        public Delegate SetPointDelegate { get; set; }

        public OnlinePsychGraphMaker()
        {

        }

        public void InitSerieses()
        {
            ChartControl.BeginInvoke(SetSeriesDelegate, VaryingParametrsNames);
        }

        public void AddResult(string varyingParameterName, double regionPoint, double value)
        {

        }

        public void Clear()
        {
            ChartControl.BeginInvoke(ClearDelegate);
        }
    }

    public class Region
    {
        public double LowBound { get; set; }

        public double Increament { get; set; }

        public double HighBound { get; set; }
    }
}
