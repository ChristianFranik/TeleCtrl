using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace TeleCtrl
{
    public static class Arduino
    {
        public static SerialPort Serial { get; } = new SerialPort("COM4", 115200);

        public enum Command : int
        {
            RELAY1_ON = 48,         
            RELAY1_OFF = 49,
            RELAY2_ON = 50,
            RELAY2_OFF = 51,
            RELAY3_ON = 52,
            RELAY3_OFF = 53,
            RELAY4_ON = 54,
            RELAY4_OFF = 65,
            FOCUS_IN = 66,              
            FOCUS_OUT = 67,             
            STOP_FOCUS = 68,
            INCREASE_FOCUS_SPEED = 69,
            DECREASE_FOCUS_SPEED = 70,
            GET_FOCUS_SPEED = 71
        }

        static Arduino()
        {
            Serial.DtrEnable = true;
        }
        public static void OpenConnection()
        {
            Serial.Open();
        }

        public static void CloseConnection()
        {
            Serial.Close();
        }

        public static void Send(Command command)
        {
            char[] cmd = { (char)command };
            if (Serial.IsOpen)
            {
                Serial.Write(cmd, 0, 1);
            }
        }
    }
}
