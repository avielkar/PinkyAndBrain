using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Params
{

    /// <summary>
    /// Class hold a dictionary with all variables names as keys and all variables Variable(attribute list + parameters) as value.
    /// </summary>
    public class Variables
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Variables()
        {

        }

        /// <summary>
        /// The dictionary holds each variable name as a key , and the Variable itself as a value.
        /// </summary>
        public Dictionary<string, Variable> _variablesDictionary
        {
            get;
            set;
        }

        /// <summary>
        /// Filter the variables by a ststus and returns them.
        /// </summary>
        /// <param name="status">The status to be filtered.</param>
        /// <returns>The filtered Variable dictionary.</returns>
        public Variables FilterVariablesByStatus(string status)
        {
            Variables filteredVariables = new Variables();

            filteredVariables._variablesDictionary = new Dictionary<string, Variable>();

            foreach (string varName in _variablesDictionary.Keys)
            {
                if (_variablesDictionary[varName]._description["status"]._ratHouseParameter[0].Equals(status))
                {
                    filteredVariables._variablesDictionary[varName] = new Variable(_variablesDictionary[varName]);
                }
            }

            return filteredVariables;
        }
    }
}
