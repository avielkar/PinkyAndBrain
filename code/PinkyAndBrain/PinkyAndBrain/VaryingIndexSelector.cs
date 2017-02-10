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
    }
}
