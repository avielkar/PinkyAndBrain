using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace LED.Strip.Adressable
{
    public class LEDController
    {
        /// <summary>
        /// The serial port to communicate with the arduino led controller.
        /// </summary>
        SerialPort _ledArduinoSerialPort;

        /// <summary>
        /// The objects saves the all Led strip data color and places to turn on.
        /// </summary>
        private LEDsData _ledsData;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LEDController()
        {
            _ledArduinoSerialPort = new SerialPort("COM4", 2000000, Parity.None, 8, StopBits.One);
        }

        /// <summary>
        /// Open the port connection with arduino led controller.
        /// </summary>
        public void OpenConnection()
        {
            _ledArduinoSerialPort.Open();
        }

        /// <summary>
        /// Close the port connection with the arduino led controller.
        /// </summary>
        public void CloseConnection()
        {
            _ledArduinoSerialPort.Close();
        }

        /// <summary>
        /// Get or Set the data that should be executed on the arduino led command.
        /// </summary>
        public LEDsData LEDsDataCommand { get { return _ledsData; } set { _ledsData = value; } }

        /// <summary>
        /// Send the data to the arduino led contoller.
        /// </summary>
        public void SendData()
        {
            //means a start of data transmit.
            _ledArduinoSerialPort.Write("#");

            //sending the colors.
            byte[] colorData = { 255, 0, 0 };
            _ledArduinoSerialPort.Write(colorData, 0, colorData.Length);

            //means end sending te colors and start sending te points of places to turn on it's places.
            _ledArduinoSerialPort.Write("@");

            //sending the places.
            for (int i = 0; i < _ledsData.TurnedOnPlaces.Length / 50; i++)
            {
                _ledArduinoSerialPort.Write(_ledsData.TurnedOnPlaces, i * 50, 50);
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
            //means the data execution command.
            _ledArduinoSerialPort.Write("!");
        }
    }
}
