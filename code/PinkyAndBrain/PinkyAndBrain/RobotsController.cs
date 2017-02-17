using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotocomdotNetWrapper;

namespace PinkyAndBrain
{
    /// <summary>
    /// This class is udes to b the link between the ControlLoop and the YASKWA robot interface.
    /// </summary>
    class RobotsController
    {
        private CYasnac _robotWrapper;

        public RobotsController(string IPAddress , string path)
        {
            _robotWrapper = new CYasnac(IPAddress, path);
        }

        public short BscLinearMove(TrajectoryPoint trajPoint)
        {
            return _robotWrapper.BscLinearMove(trajPoint);
        }
    }
}
