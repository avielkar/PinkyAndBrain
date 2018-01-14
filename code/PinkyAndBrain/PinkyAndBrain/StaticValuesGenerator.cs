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
        /// </summary>
        public Dictionary<string, double> _staticVariableList;
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
            _staticVariableList = new Dictionary<string,double>();

            foreach (string varName in _variablesList._variablesDictionary.Keys)
            {
                //if the variable is static type or const type , add it to the static variables list with it's attributes.
                if (_variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter.Equals("1")
                    || _variablesList._variablesDictionary[varName]._description["status"]._ratHouseParameter.Equals("0"))
                {
                    //it's static variable , so need to take only it's parameters value to the experiment round.
                    _staticVariableList.Add(varName, double.Parse(_variablesList._variablesDictionary[varName]._description["parameters"]._ratHouseParameter));
                }
            }
        }
        #endregion FUNCTIONS

    }
}
