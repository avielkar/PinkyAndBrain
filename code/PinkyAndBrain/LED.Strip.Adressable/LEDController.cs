using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using log4net;

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
        #endregion MEMBERS

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor.
        /// <param name="portName">The port COM to connect with.</param>
        /// <param name="baudRate">The communication baud rate.</param>
        /// <param name="numOfLeds">The number of leds to controll with in the strip.</param>
        /// </summary>
        public LEDController(string portName , int baudRate , int numOfLeds , ILog logger)
        {
            _ledArduinoSerialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

            byte[] b = new byte[250];
            _ledsData = new LEDsData(0, 0, 0, 0, b);

            _logger = logger;
        }
        #endregion CONSTRUCTORS

        #region FUNCTIONS
        /// <summary>
        /// Open the port connection with arduino led controller.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                _ledArduinoSerialPort.Open();
            }

            catch
            {
                //show the error window.
                MessageBox.Show("Error - The COM4 port for LED Arduino is not available , Exit and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            //if not connected nothing to do.
            if (!Connected) return;

            _ledArduinoSerialPort.Close();
        }

        /// <summary>
        /// Send the data to the arduino led contoller.
        /// </summary>
        public void SendData()
        {
            //if not connected nothing to do.
            if (!Connected) return;

            //means a start of data transmit.
            _ledArduinoSerialPort.Write("#");

            //sending the colors.
            byte[] colorData = {_ledsData.Brightness,  _ledsData.Red, _ledsData.Green , _ledsData.Blue };
            _ledArduinoSerialPort.Write(colorData, 0, colorData.Length);

            //means end sending te colors and start sending te points of places to turn on it's places.
            _ledArduinoSerialPort.Write("@");

            //sending the places.
            _ledArduinoSerialPort.WriteTimeout = 1000;
            for (int i = 0; i < _ledsData.TurnedOnPlaces.Length / 50; i++)
            {
                _logger.Info("Leds packet send.");
                try { _ledArduinoSerialPort.Write(_ledsData.TurnedOnPlaces, i * 50, 50); }
                catch { _logger.Error("Timeout Writing Leds."); }
                Thread.Sleep(10);
            }

            //means the end of the data.
            _ledArduinoSerialPort.Write("#");
            _ledArduinoSerialPort.Write("#");
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

            //execute the data commands.
            ExecuteCommands();

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
