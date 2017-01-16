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
        public Param()
        {

        }

        public List<string> _ratHouseParameter
        {
            get;
            set;
        }

        public List<string> _landscapeParameters
        {
            get;
            set;
        }

        public bool _bothParam
        {
            get;
            set;
        }

        /*public string _name
        {
            get;
            set;
        }*/
    }

    public class Variable
    {
        public Variable()
        {

        }

        /// <summary>
        /// Description for one variable in the protocol.
        /// The string is the attribute of the variable and the Param is the value of this atribute.
        /// </summary>
        public Dictionary<string, Param> _description;
    }

    public class Variables
    {
        public Variables()
        {

        }

        public Dictionary<string, Variable> _variablesDictionary
        {
            get;
            set;
        }
    }
}
