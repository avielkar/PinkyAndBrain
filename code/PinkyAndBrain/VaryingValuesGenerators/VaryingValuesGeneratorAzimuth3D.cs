using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;
using MathNet.Numerics.LinearAlgebra;

namespace VaryingValuesGenerators
{
    /// <summary>
    /// This class attempt to create all the needed trials  for the Azimuth3D  protocol.
    /// parmaeters for the whole experiment according to the protocol and th GuiInterfae inputs.
    /// </summary>
    public class VaryingValuesGenerator3DAzimuth : IVaryingValuesGenerator
    {
        #region CONSTRUCTOR
        /// <summary>
        /// Default constructor.
        /// </summary>
        public VaryingValuesGenerator3DAzimuth()
        {

        }
        #endregion CONSTRUCTOR

        #region FUNCTIONS
        /// <summary>
        /// Sets the variables dictionary into new variables dictionaries ordered by statuses.
        /// </summary>
        /// <param name="vars"></param>
        public override void SetVariablesValues(Variables vars)
        {
            _varyingVariables = vars.FilterVariablesByStatus("2");
        }

        /// <summary>
        /// Making commulative trials matrix for the varying values spanning vectors.
        /// </summary>
        /// <param name="seperatedVaryingValues">The spanning varying matrix.</param>
        /// <returns>The spanning matrix commulatives all spannig vectors combinations.</returns>
        public Matrix<double> MakeCommulativeMatrix(Dictionary<string, Vector<double>> seperatedVaryingValues)
        {
            //the commulative matrix that incresed 1 line in each iteration and in many rows as the number of values the variables takes.
            Matrix<double> commulativeMatrix = Matrix<double>.Build.DenseOfRowVectors(seperatedVaryingValues[seperatedVaryingValues.Keys.First()]);

            //the previous iteration final matrix.
            Matrix<double> previousStepMatrix = Matrix<double>.Build.DenseOfRowVectors(seperatedVaryingValues[seperatedVaryingValues.Keys.First()]);

            //the previous iteration final matrix but also transposed.
            Matrix<double> previousStepMatrixTransposed = previousStepMatrix.ConjugateTranspose();

            //indicate to skip the first item in the foreach loop because it was already inserted to the accumulative matrix in the two lines before.
            bool skipFirst = true;

            foreach (KeyValuePair<string, Vector<double>> varVecKeyValuePair in seperatedVaryingValues)
            {

                if (!skipFirst)
                {
                    bool first = true;

                    int columnLength = commulativeMatrix.ColumnCount;

                    //run over all values the variable is bounded in.
                    //each iteration in the loop added the repeated values of each value of the variable to the previous matrix with the matrix above the line.
                    //also , it concatinating this new matrix to the other matrixes.
                    //after all the iterations of the loop , there is a previous duplicated matrix x times with new lines of duplicated values(x times).
                    //the x means the number of values in the variables.
                    foreach (double value in varVecKeyValuePair.Value)
                    {

                        Vector<double> addedColumnVector = Vector<double>.Build.Dense(columnLength, value);

                        Matrix<double> addedColumnMatrix = Matrix<double>.Build.DenseOfColumnVectors(addedColumnVector);

                        Matrix<double> addedMatrix = previousStepMatrixTransposed.Append(addedColumnMatrix);

                        //if this is the first iteration, add the new row to the matrix.
                        if (first)
                        {
                            commulativeMatrix = addedMatrix.Transpose();
                        }

                        //append the added matrix to the commulative matrix.
                        else
                        {
                            commulativeMatrix = commulativeMatrix.Append(addedMatrix.Transpose());
                        }

                        //from now, append the commulative matrix because it was updated first to the needed size.
                        first = false;
                    }

                    //update thr previous matrix to be the commulatiuve matrix.
                    previousStepMatrixTransposed = commulativeMatrix.Transpose();
                }

                //skipped the first variable that was already inserted , from now start to insert each variable in the first foreach loop.
                skipFirst = false;
            }

            return commulativeMatrix;
        }

        /// <summary>
        /// Creates all the varying vectors the trial in the experiment would use.
        /// </summary>
        public override void MakeTrialsVaringVectors()
        {
            //initialize the matrix that include all the spanning vectors.
            Dictionary<string, Vector<double>> seperatedVaryingValues = MakeSeperatedVaryingVectorsList();

            //run over all the varying variables.
            //each iteration in this loop add a new line with duplicated previous matrix with current variables values.
            Matrix<double> commulativeMatrix = MakeCommulativeMatrix(seperatedVaryingValues);

            Dictionary<string, Vector<double>> seperatedVaryingValuesWithOnlyOneElevationValue = new Dictionary<string, Vector<double>>();
            foreach (KeyValuePair<string, Vector<double>> item in seperatedVaryingValues)
            {
                //if the azimuth key
                if (item.Key == "DISC_PLANE_AZIMUTH")
                {
                    seperatedVaryingValuesWithOnlyOneElevationValue.Add(item.Key, Vector<double>.Build.Dense(new double[] { -90, 90 }));
                }
                //if the elevation key
                else if (item.Key == "DISC_PLANE_ELEVATION")
                {
                    seperatedVaryingValuesWithOnlyOneElevationValue.Add(item.Key, Vector<double>.Build.Dense(1, 0));
                }
                else
                {
                    seperatedVaryingValuesWithOnlyOneElevationValue.Add(item.Key, item.Value);
                }
            }

            var commulativaMatrixAzimuthSpecials = MakeCommulativeMatrix(seperatedVaryingValuesWithOnlyOneElevationValue);

            commulativeMatrix = commulativeMatrix.Append(commulativaMatrixAzimuthSpecials);

            #region SECOND_ITERATION
            #endregion SECOND_ITERATION


            _varyingMatrix = commulativeMatrix;
        }

        /// <summary>
        /// Getting the list of all varying vector. Each veactor is represented by dictionary of variable name and value.
        /// </summary>
        /// <returns>Returns list in the size of generated varying vectors. Each vector represents by the name of the variable and it's value.</returns>
        public override List<Dictionary<string, double>> MakeVaryingMatrix()
        {
            //make trials vectoes by matrix operations.
            MakeTrialsVaringVectors();

            List<Dictionary<string, double>> returnList = new List<Dictionary<string, double>>();

            List<string> varyingVariablesNames = _varyingVariables._variablesDictionary.Keys.ToList();

            IEnumerator<string> nameEnumerator = varyingVariablesNames.GetEnumerator();

            foreach (Vector<double> varRow in _varyingMatrix.EnumerateColumns())
            {
                Dictionary<string, double> dictionaryItem = new Dictionary<string, double>();
                nameEnumerator.Reset();
                foreach (double value in varRow)
                {
                    nameEnumerator.MoveNext();

                    dictionaryItem.Add(nameEnumerator.Current, value);
                }
                returnList.Add(dictionaryItem);
            }

            //insert this list to the cross varying values attribute.
            _crossVaryingValsBoth = returnList;

            //return this list that can be edited.
            return _crossVaryingValsBoth;
        }

        /// <summary>
        /// Cretaes varying vectors list according to the varying vectors variables(the list include each variable as a vector with no connection each other).
        /// </summary>
        public override Dictionary<string, Vector<double>> MakeSeperatedVaryingVectorsList()
        {
            //a list include all varying vectors by themselves only.
            Dictionary<string, Vector<double>> varyingVectorsList = new Dictionary<string, Vector<double>>();

            #region MAKING_VARYING_VECTOR_LIST
            foreach (string varName in _varyingVariables._variablesDictionary.Keys)
            {
                double low_bound = double.Parse(_varyingVariables._variablesDictionary[varName]._description["low_bound"]._ratHouseParameter);
                double high_bound = double.Parse(_varyingVariables._variablesDictionary[varName]._description["high_bound"]._ratHouseParameter);
                double increament = double.Parse(_varyingVariables._variablesDictionary[varName]._description["increament"]._ratHouseParameter);

                //add the vector to the return list.
                Vector<double> oneVarVector;
                if (varName != "DISC_PLANE_AZIMUTH")
                {
                    oneVarVector = CreateVectorFromBounds(low_bound, high_bound, increament);
                }
                else
                {
                    oneVarVector = CreateVectorFromBoundsAzimuth(low_bound, high_bound, increament);
                }
                varyingVectorsList.Add(varName, oneVarVector);
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
        public override Vector<double> CreateVectorFromBounds(double lowBound, double highBound, double increment)
        {
            Vector<double> createdVector = Vector<double>.Build.Dense((int)((highBound - lowBound) / increment + 1));
            int index = 0;

            while (lowBound <= highBound)
            {
                createdVector.At(index, lowBound);
                lowBound += increment;
                index++;
            }

            return createdVector;
        }

        /// <summary>
        /// Creates the vector boumd for the Azimuth spanning vector wothout the specials values of +90 and -90.
        /// </summary>
        /// <param name="lowBound">The low bound to start with.</param>
        /// <param name="highBound">The high bound to end with.</param>
        /// <param name="increment">The increament between each elemrnt in the generated vector.</param>
        /// <returns>The generated vector from the input bounds with no special values of +90 and -90.</returns>
        private Vector<double> CreateVectorFromBoundsAzimuth(double lowBound, double highBound, double increment)
        {
            Vector<double> createdVector = Vector<double>.Build.Dense((int)((highBound - lowBound) / increment + 1));
            int index = 0;

            while (lowBound <= highBound)
            {
                if (lowBound != 90 && lowBound != -90)
                    createdVector.At(index, lowBound);
                lowBound += increment;
                index++;
            }

            return createdVector.SubVector(0, index);
        }
        #endregion FUNCTIONS
    }
}
