using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.DAQmx;
using NationalInstrumentsDaq = NationalInstruments.DAQmx;

namespace RatResponseSystem
{
    /// <summary>
    /// This class is used to be the link between the controlLoop requests and the Noldus communication interface.
    /// </summary>
    public class RatResponseController
    {

        /// <summary>
        /// The task contain all the channels created for the device.
        /// </summary>
        private NationalInstrumentsDaq.Task _digitalInTask;

        /// <summary>
        /// The stream reader from the device.
        /// </summary>
        private DigitalSingleChannelReader _digitalReader;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="deviceName">The device name to be connected to.</param>
        /// <param name="port">The port listen to.</param>
        /// <param name="lines">The lines in the port to listen to.</param>
        /// <param name="channelsNickName">The channel nick name.</param>
        public RatResponseController(string deviceName, string port, string lines, string channelsNickName)
        {
            //make the full name to access the device.
            string channelLines = string.Join("/", deviceName, port, lines);

            //create the device task.
            _digitalInTask = new NationalInstrumentsDaq.Task();

            //add a new channel connection to the task.
            _digitalInTask.DIChannels.CreateChannel(channelLines, channelsNickName, ChannelLineGrouping.OneChannelForAllLines);

            //create a stramWriter for reading from the device.
            _digitalReader = new DigitalSingleChannelReader(_digitalInTask.Stream);
        }

        /// <summary>
        /// Read data from the device.
        /// </summary>
        /// <returns>The readen dated from the device.</returns>
        public byte ReadSingleSamplePort()
        {
            return _digitalReader.ReadSingleSamplePortByte();
        }
    }
}
