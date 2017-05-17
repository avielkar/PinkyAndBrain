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

        private Dictionary<string, List<SeriesDetail>> _seriesPointsDetails;

        public OnlinePsychGraphMaker()
        {
            _seriesPointsDetails = new Dictionary<string, List<SeriesDetail>>();
        }

        public void InitSerieses()
        {
            ChartControl.BeginInvoke(SetSeriesDelegate, VaryingParametrsNames);

            foreach (string varyingParameterName in VaryingParametrsNames)
            {
                _seriesPointsDetails[varyingParameterName] = new List<SeriesDetail>();
            }

            for (double i = HeadingDireactionRegion.LowBound; i <= HeadingDireactionRegion.HighBound; i+= HeadingDireactionRegion.Increament)
            {
                foreach (string varyingParameterName in VaryingParametrsNames)
                {
                    ChartControl.BeginInvoke(SetPointDelegate, varyingParameterName, i,0, true);
                    _seriesPointsDetails[varyingParameterName].Add(
                        new SeriesDetail {
                            X = i,
                            SuccessNum = 0,
                            Total = 0
                    }
                    );
                }
            }
        }

        public void AddResult(string varyingParameterName, double regionPoint , AnswerStatus answerStatus)
        {
            _seriesPointsDetails[varyingParameterName].First(series => series.X == regionPoint).Total++;
            
            if(answerStatus.Equals(AnswerStatus.CORRECT))
                _seriesPointsDetails[varyingParameterName].First(series => series.X == regionPoint).SuccessNum++;

            ChartControl.BeginInvoke(SetPointDelegate, varyingParameterName, regionPoint, ((double)_seriesPointsDetails[varyingParameterName].First(series => series.X == regionPoint).SuccessNum / (double)_seriesPointsDetails[varyingParameterName].First(series => series.X == regionPoint).Total), false);
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

    public class SeriesDetail
    {
        public double X { get; set; }

        public int SuccessNum { get; set; }

        public int Total { get; set; }
    }

    public enum AnswerStatus
	{
	      WRONG = 0,

          CORRECT = 1
	};
}
