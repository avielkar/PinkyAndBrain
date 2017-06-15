using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.DAQmx;

using NationalInstrumentsDaq = NationalInstruments.DAQmx;

namespace PinkyAndBrain
{
    class InfraRedController
    {
                /// <summary>
        /// The task contain all the channels created for the device.
        /// </summary>
        private NationalInstrumentsDaq.Task _analogOutTask;

        /// <summary>
        /// The stream writer to the device.
        /// </summary>
        private AnalogSingleChannelWriter _analoglWriter;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="deviceName">The device name to be connected to.</param>
        /// <param name="analogOutputName">The analog port to write to it. </param>
        /// <param name="channelsNickName">The channel nick name.</param>
        public InfraRedController(string deviceName, string analogOutputName, string channelsNickName , double minVolt = 0 , double maxVolt = 5)
        {
            //make the full name to access the device.
            string channelName = string.Join("/", deviceName, analogOutputName);

            //create the device task.
            _analogOutTask = new NationalInstrumentsDaq.Task();

            //add a new channel connection to the task.
            _analogOutTask.AOChannels.CreateVoltageChannel(channelName, channelsNickName, minVolt, maxVolt, AOVoltageUnits.Volts);

            //create a stramWriter for writing to the device.
            _analoglWriter = new AnalogSingleChannelWriter(_analogOutTask.Stream);
        }

        /// <summary>
        /// Write data to the device.
        /// </summary>
        /// <param name="autoStart">Auto start flag.</param>
        /// <param name="infraRedStatus">The data to write to the analog port.</param>
        public void WriteEvent(bool autoStart, InfraRedStatus infraRedStatus)
        {
            _analoglWriter.WriteSingleSample(autoStart, (byte)infraRedStatus);
        }
    }

    /// <summary>
    /// InfraRed status off/on.
    /// </summary>
    public enum InfraRedStatus:byte
    {
        /// <summary>
        /// The infra red turned off status.
        /// </summary>
        TurnedOff = 0,

        /// <summary>
        /// The infra red is turned on status.
        /// </summary>
        TurnedOn = 5
    }
}
