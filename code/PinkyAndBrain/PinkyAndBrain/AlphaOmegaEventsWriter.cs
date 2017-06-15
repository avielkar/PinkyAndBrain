using System.Threading.Tasks;
using NationalInstruments.DAQmx;

using NationalInstrumentsDaq = NationalInstruments.DAQmx;

namespace PinkyAndBrain
{
    /// <summary>
    /// Class controlling the AlphaOmega writing events.
    /// </summary>
    class AlphaOmegaEventsWriter
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
        public AlphaOmegaEventsWriter(string deviceName, string port, string lines, string channelsNickName)
        {
            //make the full name to access the device.
            string channelLines = string.Join("/", deviceName, port, lines);

            //create the device task.
            _digitalOutTask = new NationalInstrumentsDaq.Task();

            //add a new channel connection to the task.
            _digitalOutTask.DOChannels.CreateChannel(channelLines, channelsNickName, ChannelLineGrouping.OneChannelForAllLines);

            //create a stramWriter for writing to the device.
            _digitalWriter = new DigitalSingleChannelWriter(_digitalOutTask.Stream);
