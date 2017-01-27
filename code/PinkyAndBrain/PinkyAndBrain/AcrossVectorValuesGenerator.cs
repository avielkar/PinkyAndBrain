using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class attempt to create all the needed trials vector parmaeters for the whole experiment according to the protocol and th GuiInterfae inputs.
    /// </summary>
    class AcrossVectorValuesGenerator
    {
        private Variable  _staticVariables;

        private Variables _varyingVariables;

        private Variables _acrossStairVariables;

        private Variables _withinStairVariables;

        /// <summary>
        /// Dictionary describes all the vectors sorted by all trials for each variables by index for the varying values generator.
        /// The vector for each key string (name  of variable) is the paralleled vector for the other strings (variables).
        /// Each paralleled ndex in all vector describes a vector for one trial.
        /// </summary>
        private Dictionary<string, Vector<double>> varyingVectorDictionary;

        public AcrossVectorValuesGenerator()
        {

        }

        /// <summary>
        /// Sets the variables dictionary into new variables dictionaries ordered by statuses.
        /// </summary>
        /// <param name="vars"></param>
        public void SetVariablesValues(Variables vars)
        {
            _varyingVariables = vars.FilterVariablesByStatus("2");
        }

        /// <summary>
        /// Cretaes varying vectors according to the varting vectors variables.
        /// </summary>
        public void MakeVaryingVectors()
        {
            foreach (string varName in _varyingVariables._variablesDictionary.Keys)
            {
                if (_varyingVariables._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter.Count == 1)
                {
                    double low_bound = double.Parse(_varyingVariables._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter[0]);
                    double high_bound = double.Parse(_varyingVariables._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter[0]);
                    double increament = double.Parse(_varyingVariables._variablesDictionary[varName]._description["increament"]._ratHouseParameter[0]);
                    Vector<double> oneVarVector = CreateVectorFromBounds(low_bound, high_bound, increament);
                }
                //Vector<double> oneVarVector = CreateVectorFromBounds()
            }

        }

        /// <summary>
        /// Creates a vector include values from the selected bounds.
        /// </summary>
        /// <param name="lowBound">The low bound to start with.</param>
        /// <param name="highBound">The high bound to end with.</param>
        /// <param name="increment">The increament between each elemrnt in the generated vector.</param>
        /// <returns>The generated vector from the input bounds.</returns>
        private Vector<double> CreateVectorFromBounds(double lowBound, double highBound, double increment)
        {
            Vector<double> createdVector = Vector<double>.Build.Dense((int)((highBound-lowBound)/increment + 1));
            int index = 0;

            while(lowBound<=highBound)
            {
                createdVector.At(index, lowBound);
                lowBound += increment;
                index++;
            }

            return createdVector;
        }
    }
}
