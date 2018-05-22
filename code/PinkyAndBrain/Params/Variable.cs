using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Params
{
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
}
