using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ResetTrialsStatus();
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Reset the status of all the trials combinations.
        /// </summary>
        private void ResetTrialsStatus()
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
        /// fullify the array with specific number of '1' and '0'.
        /// </summary>
        /// <param name="numOfFillings">The numer of '1' to be in the random array of bytes.</param>
        /// <returns>The random array fullified with '1' and '0'.</returns>
        public byte[] FillWithBinaryRandomCombination(int numOfFillings)
        {
            //reset all indexes to be with false.
            ResetTrialsStatus();

            //the returned array with the numOfFillings '1' in thae arry values.
            byte[] returnedArray = new byte[_trialsCombinationIndexesStatus.Length];

            //choosing numOfFillings indexes to fill with '1' value.
            for(int i=0;i<numOfFillings;i++)
            {
                ChooseRandomCombination();
            }

            int index = 0;
            byte trueByte = 1;
            byte falseByte = 0;

            //decide the value of each byte by the randoms indexes.
            foreach (bool value in _trialsCombinationIndexesStatus)
            {
                returnedArray[index] = (value) ? trueByte : falseByte;
                index++;
            }

            //return the random array with selected bytes to be with '1' value and '0 values.
            return returnedArray;
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
