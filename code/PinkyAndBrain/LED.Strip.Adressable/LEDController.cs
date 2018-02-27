using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using log4net;
using System.Diagnostics;

namespace LED.Strip.Adressable
{
    /// <summary>
    /// Led controller for controlling the leds in the ledstrip statuses (turn on/off) connected to the arduino.
    /// </summary>
    public class LEDController
    {
        #region MEMBERS
        /// <summary>
        /// The serial port to communicate with the arduino led controller.
        /// </summary>
        SerialPort _ledArduinoSerialPort;

        /// <summary>
        /// The objects saves the all Led strip data color and places to turn on.
        /// </summary>
        private LEDsData _ledsData;

        /// <summary>
        /// Logger for writing log information.
        /// </summary>
        private ILog _logger;

        /// <summary>
        /// Indicated if the port is connected or not.
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// The number of data leds frames with out the last reset frame.
        /// </summary>
        public int _numOfFrames;
        #endregion MEMBERS

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor.
        /// <param name="portName">The port COM to connect with.</param>
        /// <param name="baudRate">The communication baud rate.</param>
        /// <param name="numOfLeds">The number of leds to controll with in the strip.</param>
        /// <param name="numOfFrames">The number of data leds frames without the last reset frame.</param>
        /// <param name="logger">The program main logger.</param>
        /// </summary>
        public LEDController(string portName , int baudRate , int numOfLeds , int numOfFrames , ILog logger)
        {
            _logger = logger;

            _logger.Info("LEDController created.");

            _ledArduinoSerialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

            byte[] b = new byte[numOfLeds * numOfFrames];
            _ledsData = new LEDsData(0, 0, 0, 0, b);

            _numOfFrames = numOfFrames;
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Open the port connection with arduino led controller.
        /// </summary>
        public void OpenConnection()
        {
            _logger.Info("Openning Connection.");

            try
            {
                _ledArduinoSerialPort.Open();
            }

            catch
            {
                //show the error window.
                MessageBox.Show("Error - The COM4 port for LED Arduino is not available , Exit and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Info("Openning Connection failed.");

                //connection failed
                Connected = false;

                //brek the function
                return;
            }

            //connection successful
            Connected = true;
        }

        /// <summary>
        /// Close the port connection with the arduino led controller.
        /// </summary>
        public void CloseConnection()
        {
            _logger.Info("Closing Connection.");

            //if not connected nothing to do.
            if (!Connected) return;

            _ledArduinoSerialPort.Close();
        }

        /// <summary>
        /// Sebd the led strip color.
        /// </summary>
        public void SendLedsColorData()
        {
            //sending the colors.
            byte[] colorData = { _ledsData.Brightness, _ledsData.Red, _ledsData.Green, _ledsData.Blue };
            _ledArduinoSerialPort.Write(colorData, 0, colorData.Length);

            //means end sending te colors and start sending te points of places to turn on it's places.
            _ledArduinoSerialPort.Write("@");
        }

        /// <summary>
        /// Send all frames with teir places statuses for the led strip to each frame.
        /// </summary>
        public void SendPlacesDataFrames()
        {
            //sending the places.
            _ledArduinoSerialPort.WriteTimeout = 1000;
            for (int i = 0; i < _ledsData.TurnedOnPlaces.Length / 50; i++)
            {
                _logger.Info("Leds packet send.");
                try { _ledArduinoSerialPort.Write(_ledsData.TurnedOnPlaces, i * 50, 50); }
                catch { _logger.Error("Timeout Writing Leds."); }
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Sending a sign that the data is coming to be sent.
        /// </summary>
        public void SendInitDataSign()
        {
            _logger.Info("Sending a sign of init data.");

            //if not connected nothing to do.
            if (!Connected) return;

            //means a start of data transmit.
            _ledArduinoSerialPort.Write("#");
        }

        /// <summary>
        /// Sending a sign that data sending is over.
        /// </summary>
        public void SendEndOfDataSign()
        {
            //means the end of the data.
            _ledArduinoSerialPort.Write("#");
            _ledArduinoSerialPort.Write("#");

            _logger.Info("Sending sign of data ended.");
        }

        /// <summary>
        /// Send a command to execute one frame.
        /// </summary>
        public void ExecuteFrame()
        {
            //if not connected nothing to do.
            if (!Connected) return;

            //means the data execution command.
            _logger.Info("Leds execution command sent");
            _ledArduinoSerialPort.Write("!");
        }

        /// <summary>
        /// Sends commands one by one to execute all frames.
        /// </summary>
        public void ExecuteAllFrames()
        {
            _logger.Info("Executing all frames one by one");

            //_numOfFrames + 1 because the last reset frame.
            for (int i  = 0; i  < _numOfFrames + 1; i ++)
            {
                ExecuteFrame();

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Send the data to the arduino led contoller.
        /// </summary>
        public void SendData()
        {
            SendInitDataSign();

            SendLedsColorData();

            SendPlacesDataFrames();

            SendEndOfDataSign();
        }

        /// <summary>
        /// Execute the transmitted data on the arduino led controller.
        /// </summary>
        public void ExecuteCommands()
        {
            //if not connected nothing to do.
            if (!Connected) return;

            //means the data execution command.
            _logger.Info("Leds execution command sent");
            _ledArduinoSerialPort.Write("!");

            #region Checking time arduino executing the command.
            /*Stopwatch sw = new Stopwatch();
            sw.Start();
            string str = _ledArduinoSerialPort.ReadLine();
            while (str == "")
            {
                str = _ledArduinoSerialPort.ReadLine();    
            }
            str = "";
            str = _ledArduinoSerialPort.ReadLine();
            while (str == "")
            {
                str = _ledArduinoSerialPort.ReadLine();
            }
            sw.Stop();*/
            #endregion
        }

        /// <summary>
        /// Turning off all leds in the ledstrip.
        /// </summary>
        public void ResetLeds()
        {
            //if not connected nothing to do.
            if (!Connected) return;

            _logger.Info("Reset Leds begin");

            //rset leds data
            _ledsData.Red = 0;
            _ledsData.Green = 0;
            _ledsData.Blue = 0;

            //send the data for leds turning on/off as the default value of arrays (which is 0).
            int len = _ledsData.TurnedOnPlaces.Length;
            _ledsData.TurnedOnPlaces = new byte[len];
            SendData();

            _logger.Info("Reset Leds finished");
        }
        #endregion FUNCTIONS

        #region SETTERS_AND_GETTERS
        /// <summary>
        /// Get or Set the data that should be executed on the arduino led command.
        /// </summary>
        public LEDsData LEDsDataCommand { get { return _ledsData; } set { _ledsData = value; } }
        #endregion SETTERS_AND_GETTERS
    }
}
