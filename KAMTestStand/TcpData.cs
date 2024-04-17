using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace KAMTestStand;

public class TcpData
{
    public TcpData(SettingsData settingsData, List<string> logList)
    {
        _settingsData = settingsData;
        _logList = logList;
    }

    private readonly SettingsData _settingsData;
    private TcpClient _tcpClient = new();
    private readonly List<string> _logList;
    public async void SendTestTcpData(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress) || ipAddress == "0.0.0.0") return;

        try
        {

            await _tcpClient.ConnectAsync(ipAddress, _settingsData.PortSim);
            Console.WriteLine($"Connect to {ipAddress}!");
            var stream = _tcpClient.GetStream();
            await stream.WriteAsync(Encoding.UTF8.GetBytes("#0D#0A#0D#0A#0D#0A#0D#0AB0D#0A#0D#0A#0D#0"));
            Console.WriteLine("Data send to GSM");
            _tcpClient.Close();
            _tcpClient = new TcpClient();
        }
        catch (Exception e)
        {
            _logList.Add($"I couldn't connect to {ipAddress} {e.Message}");
        }

    }
}