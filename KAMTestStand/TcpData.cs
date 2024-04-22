using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace KAMTestStand;

public class TcpData
{
    public TcpData(SettingsData settingsData, ObservableCollection<string> logList, EntityList entityList)
    {
        _settingsData = settingsData;
        _logList = logList;
        _entityList = entityList;
    }

    private readonly SettingsData _settingsData;
    private TcpClient _tcpClient = new();
    private readonly ObservableCollection<string> _logList;
    private readonly EntityList _entityList;
    
    public async void SendTestTcpData(string ipAddress, IoNb ioNb, int serialNum)
    {
        if (string.IsNullOrEmpty(ipAddress) || ipAddress == "0.0.0.0") return;
        
        try
        {
            await _tcpClient.ConnectAsync(ipAddress, _settingsData.PortSim);
            var stream = _tcpClient.GetStream();
            stream.WriteTimeout = 10000;

            string testData = $"TEST {DateTime.Now.ToString("G")}";
            
            await stream.WriteAsync(Encoding.UTF8.GetBytes(testData));
            _logList.Add($"Отправлены тестовые данные: \"{testData}\" по адресу: {ipAddress}, {testData.Length} байт");
            byte[] rxData = new byte[256];
            int received = await stream.ReadAsync(rxData);
                
            if (received > 0)
            {
                var message = Encoding.UTF8.GetString(rxData, 0, received);
                _logList.Add($"Вернулись данные: \"{message}\", {received} байт");
                _tcpClient.Close();
                _tcpClient = new TcpClient();

                var res = ResultE.StatusPass;
                
                for (int i = 0; i < testData.Length; i++)
                {
                    if (message[i] != testData[i])
                    {
                        res = ResultE.StatusError;
                    }
                }

                switch (ioNb)
                {
                    case IoNb.Sim1Registered2G:
                        _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1Registered2GRes = res, Sim2Registered2GVal = 0});
                        break;
                    
                    case IoNb.Sim1Registered3G:
                        _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1Registered3GRes = res, Sim2Registered2GVal = 0});
                        break;
                    
                    case IoNb.Sim2Registered2G:
                        _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2Registered2GRes = res, Sim2Registered2GVal = 0});
                        break;
                    
                    case IoNb.Sim2Registered3G:
                        _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2Registered3GRes = res, Sim2Registered2GVal = 0});
                        break;
                    
                    default:
                        break;
                }
                
                _tcpClient = new TcpClient();
            }
           
            
        }
        catch (Exception e)
        {
            _logList.Add($"I couldn't connect to {ipAddress} {e.Message}");
        }

    }
}