using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.DAQmx;
using NationalInstrumentsDaq =  NationalInstruments.DAQmx;

namespace RatResponseSystem
{
    public class RewardController
    {
        /// <summary>
        /// The task contain all the channels created for the device.
        /// </summary>
        private NationalInstrumentsDaq.Task _digitalOutTask;

        /// <summary>
        /// The stream writer to the device.
        /// </summary>
        private DigitalSingleChannelWriter _digitalWriter;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="deviceName">The device name to be connected to.</param>
        /// <param name="port">The port listen to.</param>
        /// <param name="lines">The lines in the port to listen to.</param>
        /// <param name="channelsNickName">The channel nick name.</param>
        public RewardController(string deviceName , string port , string lines , string channelsNickName)
        {
            //make the full name to access the device.
            string channelLines = string.Join("/", deviceName, port, lines);

            //create the device task.
            _digitalOutTask = new NationalInstrumentsDaq.Task();

            //add a new channel connection to the task.
            _digitalOutTask.DOChannels.CreateChannel(channelLines, channelsNickName, ChannelLineGrouping.OneChannelForAllLines);

            //create a stramWriter for writing to the device.
            _digitalWriter = new DigitalSingleChannelWriter(_digitalOutTask.Stream);
        }

        /// <summary>
        /// Write data to the device.
        /// </summary>
        /// <param name="autoStart">Auto start flag.</param>
        /// <param name="data">The data (byte) to be written as l7l6l5l4l3l2l1l0 (lines).</param>
        public void WriteSingleSamplePort(bool autoStart, byte data)
        {
            _digitalWriter.WriteSingleSamplePort(autoStart, data);
        }

        /// <summary>
        /// Reset all the outputs of the controller to 0 volts.
        /// </summary>
        public void ResetControllerOutputs()
        {
            WriteSingleSamplePort(true, 0);
        }
    }
}
