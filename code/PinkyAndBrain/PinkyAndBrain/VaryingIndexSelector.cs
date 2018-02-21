using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace PinkyAndBrain
{
    /// <summary>
    /// Class for selecting the index of the crossVaryingList to make the trajectories from.
    /// </summary>
    class VaryingIndexSelector
    {
        #region ATTRIBUTES
        /// <summary>
        /// This array stores the state of all experiment trials combination.
        /// If true , it means that the trial already been used.
        /// Otherwise , it means the trial was not already used and should choose it in onr of next trials.
        /// </summary>
        private bool[] _trialsCombinationIndexesStatus;

        private List<int> _turnedOnPlaces;

        /// <summary>
        /// Random number generator.
        /// </summary>
        Random _randGenerator;
        #endregion ATTRIBUTES

        #region CONSTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        public VaryingIndexSelector()
        {
            _randGenerator = new Random();
        }
        
        public VaryingIndexSelector(int trialsCount)
        {
            _randGenerator = new Random();
            _trialsCombinationIndexesStatus = new bool[trialsCount];
            _turnedOnPlaces = new List<int>();
            ResetTrialsStatus();
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Reset the status of all the trials combinations.
        /// </summary>
        public void ResetTrialsStatus()
        {
            for(int i=0;i<_trialsCombinationIndexesStatus.Length;i++)
            {
                _trialsCombinationIndexesStatus[i] = false;
            }
        }

        /// <summary>
        /// Reset the status of one specific index combination.
        /// </summary>
        public void ResetTrialStatus(int index)
        {
            _trialsCombinationIndexesStatus[index] = false;
        }
        
        /// <summary>
        /// Choosing random combination index that was not used in the past trials,
        /// </summary>
        /// <returns></returns>
        public int ChooseRandomCombination()
        {
            //choose a random index until the combination at that index was not used yet (than this cobination would be at the current trial).
            int rand;
            do
            {
                rand = _randGenerator.Next(0 , _trialsCombinationIndexesStatus.Length);
            }
            while (_trialsCombinationIndexesStatus[rand]);

            //update that the trial combination in the selected index is used.
            _trialsCombinationIndexesStatus[rand] = true;

            //return the selected combination index.
            return rand;
        }

        /// <summary>
        /// Fullify the array with specific number of '1' and '0'.
        /// </summary>
        /// <param name="numOfFillings">The percentage of '1' to be in the random array of bytes.</param>
        /// <returns>The random array fullified with '1' and '0'.</returns>
        public byte[] FillWithBinaryRandomCombination(double percentageOfFillings)
        {
            //reset all indexes to be with false.
            ResetTrialsStatus();

            //the returned array with the numOfFillings '1' in thae arry values.
            byte[] returnedArray = new byte[_trialsCombinationIndexesStatus.Length];

            //choosing numOfFillings percentage to fill with '1' value.
            for (int i = 0; i < _trialsCombinationIndexesStatus.Length; i++)
            {
                _trialsCombinationIndexesStatus[i] = (Bernoulli.Sample(percentageOfFillings) == 1)?(true):(false);
                returnedArray[i] = (byte)(_trialsCombinationIndexesStatus[i] ? 1 : 0);
            }
            
            //return the random array with selected bytes to be with '1' value and '0 values.
            return returnedArray;
        }

        /// <summary>
        /// Fullify the array with specific number of '1' and '0'.
        /// </summary>
        /// <param name="numOfFillings">The percentage of '1' to be in the random array of bytes.</param>
        /// <returns>The random array fullified with '1' and '0'.</returns>
        public byte[] FillWithBinaryRandomCombinationCoherence(double coherencePercentage = 1.0)
        {
            byte[] returnedArray = UpdateTrialsStatus(coherencePercentage);

            //return the random array with selected bytes to be with '1' value and '0 values.
            return returnedArray;
        }

        /// <summary>
        /// Updates the trials statuses according to the coherence value.
        /// </summary>
        /// <param name="coherencePercentage">The cohernce value between 0 and 1.0 which means in each fram what probability each led that set on countinued to be set on , otherwise choose another led randomaly.</param>
        /// <returns>The leds value array.</returns>
        private byte[] UpdateTrialsStatus(double coherencePercentage)
        {
            for (int i = 0; i < _trialsCombinationIndexesStatus.Length; i++)
            {
                double bernouliSample = Bernoulli.Sample(coherencePercentage);

                if (_trialsCombinationIndexesStatus[i])
                {
                    //1 means stay the same state for that led , 0 means change the state.
                    if (bernouliSample == 1)
                    {
                    }
                    else
                    {
                        //change that star place in a random place.
                        _trialsCombinationIndexesStatus[i] = false;

                        //choose another star.
                        //TODO: ask Adam if to peek a new randomly star the not choosed yet (the same size ad before) or proably not be the same size because the same star could be choosen.
                        int rand = _randGenerator.Next(0, _trialsCombinationIndexesStatus.Length);
                        _trialsCombinationIndexesStatus[rand] = true;
                    }
                }
            }

            //convert bool array to byte array.
            byte [] returnArray = new byte[_trialsCombinationIndexesStatus.Length];

            for (int i = 0; i < _trialsCombinationIndexesStatus.Length; i++)
            {
                returnArray[i] = (byte)(_trialsCombinationIndexesStatus[i] ? 1 : 0);
            }

            return returnArray;
        }

        /// <summary>
        /// Check if all combination were used.
        /// </summary>
        /// <returns>True if all combonation were used. False otherwise.
        /// </returns>
        public bool IsFinished()
        {
            return CountRemaining() == 0;
        }

        /// <summary>
        /// Count the number of remaining unused combimations.
        /// </summary>
        /// <returns>The number of remainig combinations.</returns>
        public int CountRemaining()
        {
            return _trialsCombinationIndexesStatus.Count(x => x == false);
        }
        #endregion FUNCTIONS
    }

}
