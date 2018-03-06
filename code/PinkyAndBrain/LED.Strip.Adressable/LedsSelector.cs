using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using System.Diagnostics;

namespace LED.Strip.Adressable
{
    /// <summary>
    /// A class creating the bytes array statuses of all eds to all rendered frames in the stimulus.
    /// </summary>
    public class LedsSelector
    {
        /// <summary>
        /// The array of all leds statuses for all frames including the furest frame and the last reset frame.
        /// </summary>
        private byte[] _ledsIndexesStatus;

        /// <summary>
        /// A list of all turned on places for a one frame (can include a value more than one time).
        /// </summary>
        private List<int> _turnedOnPlaces;

        /// <summary>
        /// The number of leds in the strip.
        /// </summary>
        private int _numOfLeds;

        /// <summary>
        /// The number of frames to be rendered during the stimulus except the last frame.
        /// </summary>
        private int _numOfFrames;

        /// <summary>
        /// A random generator for generating the random places of leds to be turned on in each frame and on the first frame.
        /// </summary>
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

        /// <summary>
        /// Fill all frames (except of the first frame) with the given coherence and the first frame with the given percentage.
        /// </summary>
        /// <param name="percentageOfFillings">The percentage of fillings.</param>
        /// <param name="coherencePercentage">The coherence percentage.</param>
        /// <param name="flicker">The flicker (discoo) parameter - in how many frames to turn on the leds (1 says each frame , 2 says 1 in each 2 frames , 3 says 1 in each 3 frames).</param>
        /// <returns>The byte array statuses for all leds and all frames from the first frame to the last frame include  the last reset frame.</returns>
        public byte[] FillWithBinaryRandomCombination(double percentageOfFillings , double coherencePercentage , int flicker)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //reset all indexes to be with false.
            ResetLedsStatus();

            FillFirstFrame(percentageOfFillings);

            for (int offset = 1; offset < _numOfFrames; offset++)
            {
                //turn on the frame regulary only if the flicker is on for the frame index.
                if (offset % flicker == 0)
                {
                    //make the coherence to the other indexes
                    for (int i = 0; i < _turnedOnPlaces.Count; i++)
                    {
                        double bernouliSample = Bernoulli.Sample(coherencePercentage);
                        //1 means stay the same state for that led , 0 means change the state.
                        if (bernouliSample == 1)
                        {
                            _ledsIndexesStatus[offset * _numOfLeds + _turnedOnPlaces[i]] = 1;
                        }
                        else
                        {
                            int rand;

                            rand = _randGenerator.Next(0, _numOfLeds);

                            _turnedOnPlaces[i] = rand;

                            _ledsIndexesStatus[offset * _numOfLeds + _turnedOnPlaces[i]] = (byte)1;
                        }
                    }
                }
                //turn off the frame.
                else
                {

                }
            }

            AddResetFrame();

            sw.Stop();

            return _ledsIndexesStatus;
        }

        /// <summary>
        /// Fill the first frame with the given percentage of fillings.
        /// </summary>
        /// <param name="percentageOfFillings"></param>
        public void FillFirstFrame(double percentageOfFillings)
        {
            //choosing numOfFillings percentage to fill with '1' value.
            for (int i = 0; i < _numOfLeds; i++)
            {
                _ledsIndexesStatus[i] = (Bernoulli.Sample(percentageOfFillings) == 1) ? ((byte)1) : ((byte)0);

                if (_ledsIndexesStatus[i] == 1)
                    _turnedOnPlaces.Add(i);
            }
        }

        /// <summary>
        /// Adding a reset frame ath the end of all frames.
        /// </summary>
        public void AddResetFrame()
        {
            int resetStartIndex = _numOfLeds * _numOfFrames;
            for (int i = 0; i < _numOfLeds; i++)
            {
                _ledsIndexesStatus[resetStartIndex + i] = 0;
            }
        }
    }
}
