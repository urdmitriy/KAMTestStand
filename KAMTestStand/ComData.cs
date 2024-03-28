using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using GrapeCity.Documents.Excel;
using KAM_Report;

namespace KAMTestStand;

public class ComData
{
    public ComData(SettingsData settingsData)
    {
        _settingsData = settingsData;

    }

    private readonly SettingsData _settingsData;
    private SerialPort? _portAxi;
    private SerialPort? _portDiscovery;
    private ParseDataApp _parseDataApp;

    public void ParseDataAppSet(ParseDataApp app)
    {
        _parseDataApp = app;
    }
    
    public bool Init()
    {
        bool result = true;
        try
        {
            _portDiscovery = new SerialPort(_settingsData.PortDiscoveryName, 115200, Parity.None, 8, StopBits.One);
            _portDiscovery?.Open();
            if (_portDiscovery != null) _portDiscovery.DataReceived += PortDiscoveryOnDataReceived;
        }
        catch (Exception e)
        {
            MessageBox.Show("Не могу открыть порт discovery");
            result = false;
        }

        try
        {
            _portAxi = new SerialPort(_settingsData.PortAxiName, 115200, Parity.None, 8, StopBits.One);
            _portAxi?.Open();
            if (_portAxi != null) _portAxi.DataReceived += PortAxiOnDataReceived;
        }
        catch (Exception e)
        {
            MessageBox.Show("Не могу открыть порт AxiDebug");
            result = false;
        }

        return result;
    }

    public void Deinit()
    {
        bool result = true;
        try
        {
            _portDiscovery?.Close();
            if (_portDiscovery != null) _portDiscovery.DataReceived -= PortDiscoveryOnDataReceived;
        }
        catch (Exception e)
        {
            MessageBox.Show("Не могу  закрыть порт discovery");
        }

        try
        {
            _portAxi?.Close();
            if (_portAxi != null) _portAxi.DataReceived -= PortAxiOnDataReceived;
        }
        catch (Exception e)
        {
            MessageBox.Show("Не могу закрыть порт AxiDebug");
            result = false;
        }
    }

    public void SendDataDiscovery(byte[] dataArray, int offset, int count)
    {
        _portDiscovery?.Write(dataArray, offset, count);
    }
    
    private void PortAxiOnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var data = _portAxi?.ReadLine();
        if (data != null && !data.Contains("CommandId")) return;
        if (data != null) _parseDataApp(data);
        if (_portAxi != null)
        {
            _portAxi.DiscardInBuffer();
        }
    }

    private void PortDiscoveryOnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        _portDiscovery?.ReadLine();
    }
    
}