using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// The outer list is for the two inner list (or one , conditioned in the landscapeHouseParameter).
        /// The inners lists are for the values for each of the ratHouseParameter and landscapeHouseParameter (if there).
        /// The inners kist is with size 1 if the input is a scalar.
        /// Otherwise ,  if a vector , it would be a list with the size of the vector.
        /// </summary>
        public Dictionary<string, List<List<double>>> _staticVariableList;
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
            _staticVariableList = new Dictionary<string,List<List<double>>>();

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
        /// <param name="par">The Param to be converted into a list of list of doubles.</param>
        /// <returns>
        /// A outer list with inner lists as items.
        /// The outer list has 2 inner lists if the lanscapeParmeter is turned on.
        /// The first list item is for the ratHouseParameters.
        /// The second list item (if has) is for the landscapeHouseParameter.
        /// </returns>
        private List<List<double>> CreateDoubleListsFromStringLists(Param par)
        {
            List<List<double>> returnedList = new List<List<double>>();

            //put only one inner list.
            returnedList.Add(new List<double>());

            //convert each value in the string list to the double list.
            foreach (string value in par._ratHouseParameter)
            {
                returnedList[0].Add(double.Parse(value));
            }

            //if both parameters , add th othe (landscapeHouseParameter inner lists to the outer one.
            if (par._bothParam)
            {
                returnedList.Add(new List<double>());

                //convert each value in the string list to the double list.
                foreach (string value in par._ratHouseParameter)
                {
                    returnedList[1].Add(double.Parse(value));
                }
            }

            //return the double attributes list.
            return returnedList;
        }
        #endregion FUNCTIONS

    }
}
