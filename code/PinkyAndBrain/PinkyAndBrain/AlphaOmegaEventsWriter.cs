using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        /// <summary>
        /// Write data to the device.
        /// </summary>
        /// <param name="autoStart">Auto start flag.</param>
        /// <param name="alphaOmegaEvent">The data (byte) to be written as l7l6l5l4l3l2l1l0 (lines).</param>
        public void WriteEvent(bool autoStart, AlphaOmegaEvent alphaOmegaEvent)
        {
            _digitalWriter.WriteSingleSamplePort(autoStart, (byte)alphaOmegaEvent);
        }
    }

    /// <summary>
    /// Evets to write to the AlphaOmega
    /// </summary>
    public enum AlphaOmegaEvent : byte
    {
        /// <summary>
        /// Trial begin event.
        /// </summary>
        TrialBegin = 0,

        /// <summary>
        /// Beep indicates that system ready for a new trial
        /// </summary>
        AudioStart = 1,

        /// <summary>
        /// The rat breaks head center stability during the duration time.
        /// </summary>
        HeadStabilityBreak = 2,

        /// <summary>
        /// Rat enter it's head the first time to the center (start trial Event)
        /// </summary>
        HeadEnterCenter = 3,

        /// <summary>
        /// The rat decide about the right stimulation direction.
        /// </summary>
        HeadEnterRight = 4,

        /// <summary>
        /// The rat decide about the left stimulation direction.
        /// </summary>
        HeadEnterLeft = 5,

        /// <summary>
        /// The rat got a center water reward.
        /// </summary>
        CenterReward = 6,

        /// <summary>
        /// The rat got a right water reward.
        /// </summary>
        RightReward = 7,

        /// <summary>
        /// The rat got a left water reward.
        /// </summary>
        LeftReward = 8,

        /// <summary>
        /// Audio sounds for a correct left choice.
        /// </summary>
        AudioCorrectLeft = 9,

        /// <summary>
        /// Audio sounds for a correct right choice.
        /// </summary>
        AudioCorrectRight = 10,

        /// <summary>
        /// Audio sounds for a wrong choice side.
        /// </summary>
        AudioWrong = 11,

        /// <summary>
        /// Stimulus 1 on set.
        /// </summary>
        StimulusStart1 = 12,

        /// <summary>
        /// Stimulus 2 on set.
        /// </summary>
        StimulusStart2 = 13,

        /// <summary>
        /// Stimulus 3 on set.
        /// </summary>
        StimulusStart3 = 14,

        /// <summary>
        /// Stimulus 4 on set.
        /// </summary>
        StimulusStart4 = 15,

        /// <summary>
        /// Stimulus 5 on set.
        /// </summary>
        StimulusStart5 = 16,

        /// <summary>
        /// Stimulus 6 on set.
        /// </summary>
        StimulusStart6 = 17,

        /// <summary>
        /// Stimulus 7 on set.
        /// </summary>
        StimulusStart7 = 18,

        /// <summary>
        /// Stimulus 8 on set.
        /// </summary>
        StimulusStart8 = 19,

        /// <summary>
        /// Stimulus 9 on set.
        /// </summary>
        StimulusStart9 = 20,

        /// <summary>
        /// Stimulus 10 on set.
        /// </summary>
        StimulusStart10 = 21,

        /// <summary>
        /// Stimulus off set.
        /// </summary>
        StimulusEnd = 22
    }
}
