using System;
using System.IO.Ports;

namespace cnc_controller_gtk
{
	public class SerialPort
	{
		private System.IO.Ports.SerialPort mySerial;
		
		public SerialPort ()
		{
			mySerial = new System.IO.Ports.SerialPort("/dev/ttyUSB0", 9600);
			mySerial.Open();
			mySerial.ReadTimeout = 400;
			SendData("0x030");
		}
		
		public static String[] PortsAvailable()
		{
			return System.IO.Ports.SerialPort.GetPortNames();
		}
		
		public static bool ArduinoPresent()
		{
			foreach(var port in PortsAvailable())
			{
				// only for linux and maybe mac will this work
				if (port.Contains("USB"))
					return true;
			}
			return false;
		}
		
		public string ReadData()
		{
			byte tmpByte = 0;
			string rxString = "";
			
			while(tmpByte != 255)
			{
				rxString += ((char) tmpByte);
				tmpByte = (byte)mySerial.ReadByte();
			}
			
			return rxString;	
		}
		
		public bool SendData(string data)
		{
			mySerial.Write(data);
			
			return true;
		}
	}
}

