using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.DAQmx;
using System.Threading;

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
        /// The task contains the channel created for the strobe device.
        /// </summary>
        private NationalInstrumentsDaq.Task _digitalOutTaskStrobe;

        /// <summary>
        /// The stream writer to the device with the strobe port.
        /// </summary>
        private DigitalSingleChannelWriter _digitalWriterStrobe;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="deviceName">The device name to be connected to.</param>
        /// <param name="port">The port listen to.</param>
        /// <param name="lines">The lines in the port to listen to.</param>
        /// <param name="channelsNickName">The channel nick name.</param>
        /// <param name="strobePort">The strobe port to listen to.</param>
        /// <param name="strobeLines">The line in the strobe port to listen to.</param>
        /// <param name="strobeChannelNickName">The strobe channel nickname.</param>
        public AlphaOmegaEventsWriter(string deviceName, string port, string lines, string channelsNickName ,string strobePort , string strobeLines , string strobeChannelNickName)
        {
            //make the full name to access the device.
            string channelLines = string.Join("/", deviceName, port, lines);
            string strobeChannelLine = string.Join("/", deviceName, strobePort, strobeLines);

            //create the device task and the strobe task.
            _digitalOutTask = new NationalInstrumentsDaq.Task();
            _digitalOutTaskStrobe = new NationalInstrumentsDaq.Task();

            //add a new channel connection to the task.
            _digitalOutTask.DOChannels.CreateChannel(channelLines, channelsNickName, ChannelLineGrouping.OneChannelForAllLines);
            _digitalOutTaskStrobe.DOChannels.CreateChannel(strobeChannelLine, strobeChannelNickName, ChannelLineGrouping.OneChannelForAllLines);

            //create a stramWriter for writing to the device.
            _digitalWriter = new DigitalSingleChannelWriter(_digitalOutTask.Stream);
            _digitalWriterStrobe = new DigitalSingleChannelWriter(_digitalOutTaskStrobe.Stream);
        }

        /// <summary>
        /// Write data to the device.
        /// </summary>
        /// <param name="autoStart">Auto start flag.</param>
        /// <param name="alphaOmegaEvent">The data (byte) to be written as l7l6l5l4l3l2l1l0 (lines).</param>
        public void WriteEvent(bool autoStart, AlphaOmegaEvent alphaOmegaEvent)
        {         
            _digitalWriter.WriteSingleSamplePort(autoStart, (byte)alphaOmegaEvent);
            
            
            
            //raise up the strobe bit for writing the event and then raise it down back.
            //_digitalWriterStrobe.WriteSingleSamplePort(autoStart, 1);
            //Thread.Sleep(10);
            //_digitalWriterStrobe.WriteSingleSamplePort(autoStart, 0);
        }
    }

    /// <summary>
    /// Evets to write to the AlphaOmega
    /// </summary>
    public enum AlphaOmegaEvent : byte
    {
        /// <summary>
        /// Empty event occured between each of the events.
        /// </summary>
        EmptyEvent = 0x00,

        /// <summary>
        /// Trial begin event.
        /// </summary>
        TrialBegin = 0x86,

        /// <summary>
        /// Beep indicates that system ready for a new trial (#1).
        /// </summary>
        AudioStart = 0x08,

        /// <summary>
        /// The rat breaks head center stability during the duration time. (#2)
        /// </summary>
        HeadStabilityBreak = 0x10,

        /// <summary>
        /// Rat enter it's head the first time to the center (start trial Event). (#3)
        /// </summary>
        HeadEnterCenter = 0x18,

        /// <summary>
        /// The rat decide about the right stimulation direction. (#4)
        /// </summary>
        HeadEnterRight = 0x20,

        /// <summary>
        /// The rat decide about the left stimulation direction. (#5)
        /// </summary>
        HeadEnterLeft = 0x21,

        /// <summary>
        /// The rat got a center water reward. (#6)
        /// </summary>
        CenterReward = 0x30,

        /// <summary>
        /// The rat got a right water reward. (#7)
        /// </summary>
        RightReward = 0x38,

        /// <summary>
        /// The rat got a left water reward. (#8)
        /// </summary>
        LeftReward = 0x40,

        /// <summary>
        /// Audio sounds for a correct left choice.
        /// </summary>
        AudioCorrectLeft = 0xb9,

        /// <summary>
        /// Audio sounds for a correct right choice.
        /// </summary>
        AudioCorrectRight = 0x10,

        /// <summary>
        /// Audio sounds for a wrong choice side. (#9)
        /// </summary>
        AudioWrong = 0x48,

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
