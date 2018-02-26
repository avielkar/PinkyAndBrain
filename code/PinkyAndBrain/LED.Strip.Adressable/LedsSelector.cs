using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using System.Diagnostics;

namespace LED.Strip.Adressable
{
    public class LedsSelector
    {
        private byte[] _ledsIndexesStatus;

        private List<int> _turnedOnPlaces;

        private int _numOfLeds;

        private int _numOfFrames;

        private Random _randGenerator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="numOfLeds">The number of leds in the strip.</param>
        /// <param name="numOfFrames">The number of total frames to render without the last reset frame.</param>
        public LedsSelector(int numOfLeds , int numOfFrames)
        {
            _numOfLeds = numOfLeds;

            _numOfFrames = numOfFrames;

            //numOfFrames + 1 in order to add the last reset frame.
            _ledsIndexesStatus = new byte[(numOfFrames + 1) * numOfLeds];
            _turnedOnPlaces = new List<int>();

            _randGenerator = new Random();
        }

        public void ResetLedsStatus()
        {
            for (int i = 0; i < _ledsIndexesStatus.Length; i++)
            {
                _ledsIndexesStatus[i] = 0;
            }

            _turnedOnPlaces.RemoveAll(x => true);
        }

        public byte[] FillWithBinaryRandomCombination(double percentageOfFillings , double coherencePercentage)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //reset all indexes to be with false.
            ResetLedsStatus();

            //the returned array with the numOfFillings '1' in thae arry values.
            byte[] returnedArray = new byte[_ledsIndexesStatus.Length];

            //choosing numOfFillings percentage to fill with '1' value.
            for (int i = 0; i < _numOfLeds; i++)
            {
                _ledsIndexesStatus[i] = (Bernoulli.Sample(percentageOfFillings) == 1) ? ((byte)1) : ((byte)0);

                if (_ledsIndexesStatus[i] == 1)
                    _turnedOnPlaces.Add(i);
            }

            for (int offset = 1; offset < _numOfFrames; offset++)
            {
                //make the coherence to the other indexes
                for (int i = 0; i < _numOfLeds; i++)
                {
                    double bernouliSample = Bernoulli.Sample(coherencePercentage);
                    //1 means stay the same state for that led , 0 means change the state.
                    if (bernouliSample == 1)
                    {
                        _ledsIndexesStatus[offset * _numOfLeds + i] = _ledsIndexesStatus[(offset - 1) * _numOfLeds + i];
                    }
                    else
                    {
                        int rand;

                        rand = _randGenerator.Next(0, _numOfLeds);

                        //_turnedOnPlaces[i] = rand;

                        _ledsIndexesStatus[offset * _numOfLeds + i] = 0;
                        _ledsIndexesStatus[offset * _numOfLeds + rand] = (byte)1;
                    }
                }
            }

            int resetStartIndex = _numOfLeds * _numOfFrames;
            for (int i = 0; i < _numOfLeds;i++)
            {
                _ledsIndexesStatus[resetStartIndex + i] = 0;
            }

            sw.Stop();

            return _ledsIndexesStatus;
        }
    }
}
