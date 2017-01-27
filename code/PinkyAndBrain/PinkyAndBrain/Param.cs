using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkyAndBrain
{
    /// <summary>
    /// Class used to save the parameters for the rat house and the landscape house.
    /// </summary>
    public class Param
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Param()
        {

        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="par">The Param to be copied.</param>
        public Param(Param par)
        {
            _ratHouseParameter = new List<string>(par._ratHouseParameter);
            
            if (par._bothParam)
            {
                _landscapeParameters = new List<string>(par._landscapeParameters);
            }

            _bothParam = par._bothParam;
        }

        /// <summary>
        /// The rat house parameter values for one attribute of one variable.
        /// </summary>
        public List<string> _ratHouseParameter
        {
            get;
            set;
        }

        /// <summary>
        /// /// The landscape house parameter values for one attribute of one variable.
        /// </summary>
        public List<string> _landscapeParameters
        {
            get;
            set;
        }

        /// <summary>
        /// Flag indicates if the attribute of the landscape parameter is on.
        /// </summary>
        public bool _bothParam
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Class include the one variable attributes and values of the attributes.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Variable()
        {

        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="vary"></param>
        public Variable(Variable vary)
        {
            _description = new Dictionary<string, Param>();

            foreach (string attribute in vary._description.Keys)
            {
                _description[attribute] = new Param(vary._description[attribute]);
            }
        }

        /// <summary>
        /// Description for one variable in the protocol.
        /// The string is the attribute of the variable and the Param is the value of this atribute.
        /// </summary>
        public Dictionary<string, Param> _description;
    }

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
                if(_variablesDictionary[varName]._description["status"]._ratHouseParameter[0].Equals(status))
                {
                    filteredVariables._variablesDictionary[varName]  = new Variable(_variablesDictionary[varName]);
                }
            }

            return filteredVariables;
        }
    }
}
