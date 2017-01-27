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
        /// Each paralleled index in all vector describes a vector for one trial.
        /// </summary>
        private Dictionary<string, Vector<double>> _varyingVectorDictionary;

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


        public void TrialsVaringVectors()
        {
            List<Vector<double>> seperatedVaryingValues = MakeSeperatedVaryingVectorsList();

            Matrix<double> commulativeMatrix = Matrix<double>.Build.DenseOfRowVectors(seperatedVaryingValues.ElementAt(0));

            Matrix<double> previousStepMatrix = Matrix<double>.Build.DenseOfRowVectors(seperatedVaryingValues.ElementAt(0));

            Matrix<double> previousStepMatrixTransposed = previousStepMatrix.ConjugateTranspose();

            bool skipFirst = true;

            foreach (Vector<double> varVec in seperatedVaryingValues)
            {
                if (!skipFirst)
                {
                    bool first = true;

                    int columnLength = commulativeMatrix.ColumnCount;
                    foreach (double value in varVec)
                    {
                        Vector<double> addedColumnVector = Vector<double>.Build.Dense(columnLength, value);

                        Matrix<double> addedColumnMatrix = Matrix<double>.Build.DenseOfColumnVectors(addedColumnVector);

                        Matrix<double> addedMatrix = previousStepMatrixTransposed.Append(addedColumnMatrix);

                        if (first)
                        {
                            commulativeMatrix = addedMatrix.Transpose();
                        }

                        else
                        {
                            commulativeMatrix = commulativeMatrix.Append(addedMatrix.Transpose());
                        }
                        first = false;
                    }

                    previousStepMatrixTransposed = commulativeMatrix.Transpose();
                }

                skipFirst = false;
            }
        }

        /// <summary>
        /// Cretaes varying vectors list according to the varying vectors variables(the list include each variable as a vector with no connection each other).
        /// </summary>
        private List<Vector<double>> MakeSeperatedVaryingVectorsList()
        {
            //a list include all varying vectors by themselves only.
            List <Vector<double>> varyingVectorsList = new List<Vector<double>>();

            #region MAKING_VARYING_VECTOR_LIST
            foreach (string varName in _varyingVariables._variablesDictionary.Keys)
            {
                //if the variable has only one attributes and is the atribute is scalar.
                if (_varyingVariables._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter.Count == 1)
                {
                    double low_bound = double.Parse(_varyingVariables._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter[0]);
                    double high_bound = double.Parse(_varyingVariables._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter[0]);
                    double increament = double.Parse(_varyingVariables._variablesDictionary[varName]._description["increament"]._ratHouseParameter[0]);
                    
                    //add the vector to the return list.
                    Vector<double> oneVarVector = CreateVectorFromBounds(low_bound, high_bound, increament);
                    varyingVectorsList.Add(oneVarVector);
                }
                
                //if the variable has only one attribute and the attrbute is not a scalar(is a vector)
                else { }
            }
            #endregion MAKING_VARYING_VECTOR_LIST

            return varyingVectorsList;
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
