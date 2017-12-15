using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;
using MathNet.Numerics.LinearAlgebra;

namespace PinkyAndBrain
{
    public abstract class IVaryingVectorGenerator
    {
        #region ATTRIBUTES
        /// <summary>
        /// Dictionary holds all static variables.
        /// </summary>
        protected Variable _staticVariables;

        /// <summary>
        /// Dictionary holds all varying variables.
        /// </summary>
        protected Variables _varyingVariables;

        /// <summary>
        /// Dictionary holds all acrossStair variables.
        /// </summary>
        protected Variables _acrossStairVariables;

        /// <summary>
        /// Dictionary holds all withinStair variables.
        /// </summary>
        protected Variables _withinStairVariables;

        /// <summary>
        /// Initial Dictionary (nor updated later) describes all the vectors sorted by all trials for each variables by index for the varying values generator.
        /// The vector for each key string (name  of variable) is the paralleled vector for the other strings (variables).
        /// Each paralleled index in all vector describes a vector for one trial.
        /// </summary>
        protected Dictionary<string, Vector<double>> _varyingVectorDictionary;

        /// <summary>
        ///Initial Matrix (not updated later) holds all the generated varying vectors for the experiment. Each row in the matrix represent a varying trial vector.
        /// The num of the columns should be the number of the trials.
        /// </summary>
        protected Matrix<double> _varyingMatrix;

        /// <summary>
        /// Final list holds all the current cross varying vals by dictionary of variables with values for each line(trial) for both ratHouseParameters and landscapeHouseParameters.
        /// </summary>
        public List<Dictionary<string, double>> _crossVaryingValsBoth;
        #endregion ATTRIBUTES

        #region FUNCTIONS
        /// <summary>
        /// Sets the variables dictionary into new variables dictionaries ordered by statuses.
        /// </summary>
        /// <param name="vars"></param>
        public abstract void SetVariablesValues(Variables vars);

        /// <summary>
        /// Creates all the varying vectors the trial in the experiment would use.
        /// </summary>
        public abstract void MakeTrialsVaringVectors();

        /// <summary>
        /// Getting the list of all varying vector. Each veactor is represented by dictionary of variable name and value.
        /// </summary>
        /// <returns>Returns list in the size of generated varying vectors. Each vector represents by the name of the variable and it's value.</returns>
        public abstract List<Dictionary<string, double>> MakeVaryingMatrix();

        /// <summary>
        /// Cretaes varying vectors list according to the varying vectors variables(the list include each variable as a vector with no connection each other).
        /// </summary>
        public abstract Dictionary<string, Vector<double>> MakeSeperatedVaryingVectorsList();

        /// <summary>
        /// Creates a vector include values from the selected bounds.
        /// </summary>
        /// <param name="lowBound">The low bound to start with.</param>
        /// <param name="highBound">The high bound to end with.</param>
        /// <param name="increment">The increament between each elemrnt in the generated vector.</param>
        /// <returns>The generated vector from the input bounds.</returns>
        public abstract Vector<double> CreateVectorFromBounds(double lowBound, double highBound, double increment);
        #endregion FUNCTIONS
    }
}
