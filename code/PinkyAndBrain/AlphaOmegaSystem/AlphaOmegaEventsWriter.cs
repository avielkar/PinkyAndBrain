using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.DAQmx;
using System.Threading;
using log4net;

using NationalInstrumentsDaq = NationalInstruments.DAQmx;

namespace AlphaOmegaSystem
{
    /// <summary>
    /// Class controlling the AlphaOmega writing events.
    /// </summary>
    public class AlphaOmegaEventsWriter
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
        /// Logger for writing the information.
        /// </summary>
        private ILog _logger;

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
        public AlphaOmegaEventsWriter(string deviceName, string port, string lines, string channelsNickName ,string strobePort , string strobeLines , string strobeChannelNickName , ILog logger)
        {
            _logger = logger;
            _logger.Info("AlphaOmegaEventsWriter created.");

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
            _logger.Info("Writing event. AlphaOmegaEvent = " + alphaOmegaEvent.ToString() + ".");
            _digitalWriter.WriteSingleSamplePort(autoStart, (byte)alphaOmegaEvent);
            
            Thread.Sleep(1);

            //raise up the strobe bit for writing the event and then raise it down back.
            _digitalWriterStrobe.WriteSingleSamplePort(autoStart, 0x08);
            Thread.Sleep(1);
            _digitalWriterStrobe.WriteSingleSamplePort(autoStart, 0x00);

            Thread.Sleep(1);

            _logger.Info("Writing event. AlphaOmegaEvent = " + AlphaOmegaEvent.EmptyEvent.ToString() + ".");
            _digitalWriter.WriteSingleSamplePort(autoStart, (byte)(AlphaOmegaEvent.EmptyEvent));
        }
    }

    /// <summary>
    /// Evets to write to the AlphaOmega
    /// </summary>
    public enum AlphaOmegaEvent : byte
    {
        //The bits for the AlphaOmega in the NationalInstruments are b3-b7.

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
        HeadEnterLeft = 0x28,

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
        /// Audio sounds for a wrong choice side. (#9)
        /// </summary>
        AudioWrong = 0x48,

        /// <summary>
        /// The rat decide about the right stimulation direction at the second chance. (#10)
        /// </summary>
        HeadEnterLeftSecondChance = 0x50,

        /// <summary>
        /// The rat decide about the left stimulation direction at the second chance. (#11)
        /// </summary>
        HeadEnterRightSecondChance = 0x58,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 1. (#12)
        /// </summary>
        StimulusStart1 = 0x60,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 2. (#13)
        /// </summary>
        StimulusStart2 = 0x68,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 3. (#14)
        /// </summary>
        StimulusStart3 = 0x70,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 4. (#15)
        /// </summary>
        StimulusStart4 = 0x78,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 5. (#16)
        /// </summary>
        StimulusStart5 = 0x80,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 6. (#17)
        /// </summary>
        StimulusStart6 = 0x88,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 7. (#18)
        /// </summary>
        StimulusStart7 = 0x90,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 8. (#19)
        /// </summary>
        StimulusStart8 = 0x98,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 9. (#20)
        /// </summary>
        StimulusStart9 = 0xa0,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 10. (#21)
        /// </summary>
        StimulusStart10 = 0xa8,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 11. (#22)
        /// </summary>
        StimulusStart11 = 0xb0,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 12. (#23)
        /// </summary>
        StimulusStart12 = 0xb8,

        /// <summary>
        /// The side (left or right) sound starts to play. (#24)
        /// </summary>
        GoCueSound = 0xc0, 

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 14. (#25)
        /// </summary>
        StimulusStart14 = 0xc8,

        /// <summary>
        /// The robot get execute to move forward trajectory for stimulus type 15. (#26)
        /// </summary>
        StimulusStart15 = 0xd0,

        /// <summary>
        /// The center reward sound starts to play. (#27)
        /// </summary>
        CenterRewardSound = 0xd8,
        
        /// <summary>
        /// The go cue sound starts to play. (#28)
        /// </summary>
        SideRewardSound = 0xe0,

        /// <summary>
        /// The robot finish to execute the forward moving trajectory. (#29)
        /// </summary>
        RobotEndMovingForward = 0xe8,

        /// <summary>
        /// The robot get execute to move backward trajectory. (#30)
        /// </summary>
        RobotStartMovingBackward = 0xf0,

        /// <summary>
        /// The robot finish to execute the backeard moving trajectory. (#31)
        /// </summary>
        RobotEndMovingBackward = 0xf8,
    }
}
