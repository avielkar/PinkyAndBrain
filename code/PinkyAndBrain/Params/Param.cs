using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Params
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
}
