using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Params;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class attempt to create from all the given variables , the static vector values.
    /// </summary>
    class StaticValuesGenerator
    {
        #region ATTRIBUTES
        /// <summary>
        /// The variables readen from the xlsx protocol file.
        /// </summary>
        private Variables _variablesList;

        /// <summary>
        /// The static variables list in double value presentation.
        /// The string is for the variable name.
        /// The list is for the values for the ratHouseParameter.
        /// The inners kist is with size 1 if the input is a scalar.
        /// Otherwise ,  if a vector , it would be a list with the size of the vector.
        /// </summary>
        public Dictionary<string, List<double>> _staticVariableList;
        #endregion ATTRIBUTES

        #region CONSTRUCTORS
        /// <summary>
        /// Default constructor.
        /// </summary>
        public StaticValuesGenerator()
        {
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Sets the total variables of all types readen from the xlsx file.
        /// </summary>
        /// <param name="variableList"></param>
        public void SetVariables(Variables variableList)
        {
            _variablesList = variableList;
            CreateDoubleStaticVariablesList();
        }

        /// <summary>
        /// Create the static variable list with double presentation instead of strings.
        /// </summary>
        private void CreateDoubleStaticVariablesList()
        {
            //initialize the dictionary.
            _staticVariableList = new Dictionary<string,List<double>>();

            foreach (string varName in _variablesList._variablesDictionary.Keys)
            {
                //if the variable is static type , add it to the static variables list with it's attributes.
                if(_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter[0].Equals("1"))
                {
                    //it's static variable , so need to take only it's parameters value to the experiment round.
                    _staticVariableList.Add(varName, CreateDoubleListsFromStringLists(_variablesList._variablesDictionary[varName]._description["parameters"]));
                }
            }
        }

        /// <summary>
        /// Convert a Param object with string attributes to be with double attributes.
        /// </summary>
        /// <param name="par">The Param to be converted into a list of doubles.</param>
        /// <returns>
        /// A outer list with as items.
        /// </returns>
        private List<double> CreateDoubleListsFromStringLists(Param par)
        {
            List<double> returnedList = new List<double>();

            //convert each value in the string list to the double list.
            foreach (string value in par._ratHouseParameter)
            {
                returnedList.Add(double.Parse(value));
            }

            //return the double attributes list.
            return returnedList;
        }
        #endregion FUNCTIONS

    }
}
