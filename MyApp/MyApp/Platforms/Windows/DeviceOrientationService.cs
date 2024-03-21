﻿using System.IO.Ports;
using System.Management;

namespace MyApp.Service;

public partial class DeviceOrientationService
{
    SerialPort mySerialPort;
    public partial void ConfigureScanner()
    {
        this.mySerialPort = new();
        this.SerialBuffer = new();

        string ComPort = "";

        using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
        {
            var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

            var portList = SerialPort.GetPortNames().Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();

            foreach (string s in portList)
            {
                if (s.Contains("GMAS"))
                {
                    string[] data = s.Split(" - ");
                    ComPort = data[0];
                }
            }
        }
        ComPort = "COM4"; // CHANGER LE PORT COM SELON LA MACHINE QU'ON UTILISE !!!
        try
        {
            if (!mySerialPort.IsOpen)
            {
                mySerialPort.PortName = ComPort;
                mySerialPort.BaudRate = 9600;
                mySerialPort.Parity = Parity.None;
                mySerialPort.DataBits = 8;
                mySerialPort.StopBits = StopBits.One;

                mySerialPort.ReadTimeout = 1000;
                mySerialPort.WriteTimeout = 1000;

                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataHandler);
                mySerialPort.Open();

            }
        }
        catch
        {
            Shell.Current.DisplayAlert("Error!", "Scanner not detected...", "OK");
        }
    }
    private void DataHandler(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;

        SerialBuffer.Enqueue(sp.ReadTo("\r"));
    }
}