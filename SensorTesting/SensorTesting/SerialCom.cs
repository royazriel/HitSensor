using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace SensorTesting
{
 
    class SerialCom
    {
        private SerialPort Com;
        private Thread thread;
        private bool Working = false;
        public void InitSerial( string portName, int baud )
        {
            try
            {
                Com = new SerialPort();
                Com.PortName = portName;
                Com.BaudRate = baud;
                Com.Parity = Parity.None;
                Com.StopBits = StopBits.One;
                Com.DataBits = 8;
                Com.Handshake = Handshake.None;
                Com.WriteTimeout = 500;
                Com.ReadTimeout = 500;
                Com.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ListComNames(ListBox list)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                list.Items.Add(s);
            }
        }

        private void CloseSerial()
        {
            if (Com.IsOpen) Com.Close();
        }

        private void StartThread()
        {
            thread = new Thread(Communicate);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Communicate()
        {
            byte[] data = { 0, 0 };
         
            Working = true;

            while (Working)
            {
                
                  
                Thread.Sleep(300);
            }
        }

        public void SendTxBuffer( string command )
        {
            if (Com.IsOpen)
                Com.Write(command);
        }

        public string ReadRxBuffer()
        {
            string rx = "";
            if (Com.IsOpen)
            {
                do
                {
                    rx += Com.ReadExisting();
                } while (!rx.Contains("\r\n") && !rx.Contains("@"));
                return rx;
            }
            return "";
        }
    }
}
