using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace KAMTestStand;

public class DataExchange
{
    public DataExchange(ComData comData, TcpData tcpData, Report report, EntityList entityList, 
        List<String> logIncomMessageList, MessagePrintApp messagePrintApp, DataGridUpdateApp dataGridUpdateApp, MainWindow windowData)
    {
        _comData = comData;
        _tcpData = tcpData;
        _report = report;
        _entityList = entityList;
        _messagePrintApp = messagePrintApp;
        _logIncomMessageList = logIncomMessageList;
        _dataGridUpdateApp = dataGridUpdateApp;
        _window = windowData;
    }
    
    private readonly ComData _comData;
    private readonly TcpData _tcpData;
    private readonly Report _report;
    private readonly EntityList _entityList;
    private readonly MessagePrintApp _messagePrintApp;
    private MainWindow _window;
    private List<String> _logIncomMessageList;
    private DataGridUpdateApp _dataGridUpdateApp;
    private DateTime _timeRunDi1Count;
    private DateTime _timeRunDi2Count;

    private void IncomDataHandler(int serialNum, int commandId, DataT data)
    {
        if (serialNum == 0) return;
        
        switch ((IoNb)commandId)
        {
            case IoNb.Sim1CountData2G:
                if (data.Result == ResultE.StatusTesting)
                {
                    string ipaddr =
                        ParseIpAddress(_entityList.Data.FirstOrDefault((x) => x.SerialNumberVal == serialNum)!
                            .Sim1IpAddr2GVal);
                    _tcpData.SendTestTcpData(ipaddr);
                    _messagePrintApp(_window.TextBlockMessage,$"Отправлены данные на {ipaddr}");
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1IpAddr2GRes = data.Result, Sim1IpAddr2GVal = data.Value});
                }
                else
                {
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1IpAddr2GRes = data.Result, Sim1IpAddr2GVal = data.Value});
                }
                break;
            
            case IoNb.Sim1CountData3G:
                if (data.Result == ResultE.StatusTesting)
                {
                    string ipaddr =
                        ParseIpAddress(_entityList.Data.FirstOrDefault((x) => x.SerialNumberVal == serialNum)!
                            .Sim1IpAddr2GVal);
                    _tcpData.SendTestTcpData(ipaddr);
                    _messagePrintApp(_window.TextBlockMessage, $"Отправлены данные на {ipaddr}");
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1IpAddr3GRes = data.Result, Sim1IpAddr3GVal = data.Value});
                }
                else
                {
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1IpAddr3GRes = data.Result, Sim1IpAddr3GVal = data.Value});
                }
                break;
            
            case IoNb.Sim2CountData2G:
                if (data.Result == ResultE.StatusTesting)
                {
                    string ipaddr =
                        ParseIpAddress(_entityList.Data.FirstOrDefault((x) => x.SerialNumberVal == serialNum)!
                            .Sim2IpAddr2GVal);
                    _tcpData.SendTestTcpData(ipaddr);
                    _messagePrintApp(_window.TextBlockMessage, $"Отправлены данные на {ipaddr}");
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2IpAddr3GRes = data.Result, Sim2IpAddr3GVal = data.Value});
                }
                else
                {
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2IpAddr2GRes = data.Result, Sim2IpAddr2GVal = data.Value});
                }
                break;
            
            case IoNb.Sim2CountData3G:
                if (data.Result == ResultE.StatusTesting)
                {
                    string ipaddr =
                        ParseIpAddress(_entityList.Data.FirstOrDefault((x) => x.SerialNumberVal == serialNum)!
                            .Sim2IpAddr2GVal);
                    _tcpData.SendTestTcpData(ipaddr);
                    _messagePrintApp(_window.TextBlockMessage, $"Отправлены данные на {ipaddr}");
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2IpAddr3GRes = data.Result, Sim2IpAddr3GVal = data.Value});
                }
                else
                {
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2IpAddr3GRes = data.Result, Sim2IpAddr3GVal = data.Value});
                }
                break;
            
            case IoNb.DiCnt1:
                if (data.Result == ResultE.StatusTesting)
                {
                    if ((DateTime.Now - _timeRunDi1Count) < TimeSpan.FromSeconds(2)) return;
                    _messagePrintApp(_window.TextBlockMessage, "Данные отправлены на Discovery (cnt1)");

                    byte[] dataByteArray = new byte[16];
                    dataByteArray[0] = Convert.ToByte(commandId);
                    dataByteArray[4] = Convert.ToByte(ResultE.StatusTesting);
                    _comData.SendDataDiscovery(dataByteArray, 0, 16);
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, DiCnt1Res = data.Result, DiCnt1Val = data.Value});
                    _timeRunDi1Count = DateTime.Now;
                }
                else
                {
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, DiCnt1Res = data.Result, DiCnt1Val = data.Value});
                }
                break;

            case IoNb.DiCnt2:
                if (data.Result == ResultE.StatusTesting)
                {
                    if ((DateTime.Now - _timeRunDi2Count) < TimeSpan.FromSeconds(2)) return;
                    _messagePrintApp(_window.TextBlockMessage, "Данные отправлены на Discovery (cnt2)");

                    byte[] dataByteArray = new byte[16];
                    dataByteArray[0] = Convert.ToByte(commandId);
                    dataByteArray[4] = Convert.ToByte(ResultE.StatusTesting);
                    _comData.SendDataDiscovery(dataByteArray, 0, 16);
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, DiCnt2Res = data.Result, DiCnt2Val = data.Value});
                    _timeRunDi2Count = DateTime.Now;
                }
                else
                {
                    _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, DiCnt2Res = data.Result, DiCnt2Val = data.Value});
                }
                break;

            case IoNb.CountDevices:
                break;
            
            case IoNb.CurrDevice:
                _messagePrintApp(_window.TextBlockDeviceNum, data.Value.ToString());
                break;
            
            case IoNb.DeviceId:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, DeviceIdRes = data.Result, DeviceIdVal = data.Value});
                break;
            
            case IoNb.ExtPower:
                break;
            
            case IoNb.ButtonShort:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ButtonShortRes = data.Result, ButtonShortVal = data.Value});
                break;

            case IoNb.ButtonLong:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ButtonLongRes = data.Result, ButtonLongVal = data.Value});
                break;

            case IoNb.ButtonDouble:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ButtonDoubleRes = data.Result, ButtonDoubleVal = data.Value});
                break;

            case IoNb.ButtonReset:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ButtonResetRes = data.Result, ButtonResetVal = data.Value});
                break;

            case IoNb.Rs2321:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Rs2321Res = data.Result, Rs2321Val = data.Value});
                break;

            case IoNb.Rs2322:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Rs2322Res = data.Result, Rs2322Val = data.Value});
                break;

            case IoNb.Rs4851:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Rs4851Res = data.Result, Rs4851Val = data.Value});
                break;

            case IoNb.Rs4852:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Rs4852Res = data.Result, Rs4852Val = data.Value});
                break;

            case IoNb.Di1:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Di1Res = data.Result, Di1Val = data.Value});
                break;

            case IoNb.Di2:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Di2Res = data.Result, Di2Val = data.Value});
                break;

            case IoNb.Kam:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, KamRes = data.Result, KamVal = data.Value});
                break;

            case IoNb.ModemState:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ModemStateRes = data.Result, ModemStateVal = data.Value});
                switch ((ModemStateE)data.Result)
                {
                    case ModemStateE.GsmModemStatusPwrOff:
                        _messagePrintApp(_window.TextBlockModemState, "Питание отключено");
                        break;
                    case ModemStateE.GsmModemStatusHwInit:
                        _messagePrintApp(_window.TextBlockModemState, "Инициализация");
                        break;
                    case ModemStateE.GsmModemStatusGsmRegProcessing:
                        _messagePrintApp(_window.TextBlockModemState, "Регистрация в GSM");
                        break;
                    case ModemStateE.GsmModemStatusGsmRegOk:
                        _messagePrintApp(_window.TextBlockModemState, "Зарегистрирован в GSM");
                        break;
                    case ModemStateE.GsmModemStatus2GRegProcessing:
                        _messagePrintApp(_window.TextBlockModemState, "Регистрация в 2G");
                        break;
                    case ModemStateE.GsmModemStatus2GRegOk:
                        _messagePrintApp(_window.TextBlockModemState, "Зарегистрирован в 2G");
                        break;
                    case ModemStateE.GsmModemStatus3GRegProcessing:
                        _messagePrintApp(_window.TextBlockModemState, "Регистрация в 3G");
                        break;
                    case ModemStateE.GsmModemStatus3GRegOk:
                        _messagePrintApp(_window.TextBlockModemState, "Зарегистрирован в 3G");
                        break;
                    case ModemStateE.GsmModemStatusCallCtrl:
                        _messagePrintApp(_window.TextBlockModemState, "Контроль вызовов");
                        break;
                    default:
                        _messagePrintApp(_window.TextBlockModemState, "Не определено");
                        break;
                }
                break;

            case IoNb.Sim1Registered2G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1Registered2GRes = data.Result, Sim1Registered2GVal = data.Value});
                break;

            case IoNb.Sim1Registered3G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1Registered3GRes = data.Result, Sim1Registered3GVal = data.Value});
                break;

            case IoNb.Sim2Registered2G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2Registered2GRes = data.Result, Sim2Registered2GVal = data.Value});
                break;

            case IoNb.Sim2Registered3G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2Registered3GRes = data.Result, Sim2Registered3GVal = data.Value});
                break;

            case IoNb.Sim1IpAddr2G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1IpAddr2GRes = data.Result, Sim1IpAddr2GVal = data.Value});
                break;

            case IoNb.Sim1IpAddr3G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1IpAddr3GRes = data.Result, Sim1IpAddr3GVal = data.Value});
                break;

            case IoNb.Sim2IpAddr2G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2IpAddr2GRes = data.Result, Sim2IpAddr2GVal = data.Value});
                break;

            case IoNb.Sim2IpAddr3G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2IpAddr3GRes = data.Result, Sim2IpAddr3GVal = data.Value});
                break;

            case IoNb.Sim1Rssi2G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1Rssi2GRes = data.Result, Sim1Rssi2GVal = data.Value});
                break;

            case IoNb.Sim1Rssi3G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim1Rssi3GRes = data.Result, Sim1Rssi3GVal = data.Value});
                break;

            case IoNb.Sim2Rssi2G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2Rssi2GRes = data.Result, Sim2Rssi2GVal = data.Value});
                break;

            case IoNb.Sim2Rssi3G:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, Sim2Rssi3GRes = data.Result, Sim2Rssi3GVal = data.Value});
                break;

            case IoNb.ToSleep:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ToSleepRes = data.Result, ToSleepVal = data.Value});
                break;

            case IoNb.ToSleepWithGsm:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ToSleepWithGsmRes = data.Result, ToSleepWithGsmVal = data.Value});
                break;

            case IoNb.Time:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, TimeRes = data.Result, TimeVal = data.Value});
                break;

            case IoNb.TimeSync:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, TimeSyncRes = data.Result, TimeSyncVal = data.Value});
                break;

            case IoNb.CurrentDcMa:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, CurrentDcMaRes = data.Result, CurrentDcMaVal = data.Value});
                break;

            case IoNb.CurrentAcUa:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, CurrentAcUaRes = data.Result, CurrentAcUaVal = data.Value});
                break;

            case IoNb.CurrentSleepUa:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, CurrentSleepUaRes = data.Result, CurrentSleepUaVal = data.Value});
                break;

            case IoNb.CurrentGsmMa:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, CurrentGsmMaRes = data.Result, CurrentGsmMaVal = data.Value});
                break;

            case IoNb.CurrentGsmPeakMa:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, CurrentGsmPeakMaRes = data.Result, CurrentGsmPeakMaVal = data.Value});
                break;

            case IoNb.CurrentGsmAndSleepMa:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, CurrentGsmAndSleepMaRes = data.Result, CurrentGsmAndSleepMaVal = data.Value});
                break;

            case IoNb.ReadyTime:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ReadyTimeRes = data.Result, ReadyTimeVal = data.Value});
                break;

            case IoNb.SerialNumber:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, SerialNumberRes = data.Result});
                break;

            case IoNb.LedLest:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, LedLestRes = data.Result, LedLestVal = data.Value});
                break;

            case IoNb.RunTests:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, RunTestsRes = data.Result, RunTestsVal = data.Value});
                break;

            case IoNb.Reset:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, ResetRes = data.Result, ResetVal = data.Value});
                break;

            case IoNb.IoCnt:
                _entityList.AddDataEntity(new Entity(){SerialNumberVal = serialNum, IoCntRes = data.Result, IoCntVal = data.Value});
                break;

            default:
                break;
        }
    }
    
    private string ParseIpAddress(int ipAddress)
    {
        var addressByte = BitConverter.GetBytes(ipAddress);
        var result =
            $"{addressByte[3].ToString()}.{addressByte[2].ToString()}.{addressByte[1].ToString()}.{addressByte[0].ToString()}";
        return result;

    }
    
    public void ParseData(string data)
    {
        _logIncomMessageList.Add(data);
        _dataGridUpdateApp();
        var positionSerialBegin = data.IndexOf("<SerialNum>", StringComparison.Ordinal) + "<SerialNum>".Length;
        var positionSerialEnd = data.IndexOf("</SerialNum>", StringComparison.Ordinal);
        var positionIdBegin = data.IndexOf("<CommandId>", StringComparison.Ordinal) + "<CommandId>".Length;
        var positionIdEnd = data.IndexOf("</CommandId>", StringComparison.Ordinal);
        var positionResultBegin = data.IndexOf("<Result>", StringComparison.Ordinal) + "<Result>".Length;
        var positionResultEnd = data.IndexOf("</Result>", StringComparison.Ordinal);
        var positionValueBegin = data.IndexOf("<Value>", StringComparison.Ordinal) + "<Value>".Length;
        var positionValueEnd = data.IndexOf("</Value>", StringComparison.Ordinal);

        var serialNum = int.Parse(data.Substring(positionSerialBegin, positionSerialEnd - positionSerialBegin));
        var commandId = int.Parse(data.Substring(positionIdBegin, positionIdEnd - positionIdBegin));
        var result = int.Parse(data.Substring(positionResultBegin, positionResultEnd - positionResultBegin));
        var value = int.Parse(data.Substring(positionValueBegin, positionValueEnd - positionValueBegin));

        var dataRcv = new DataT() { Result = (ResultE)result, Value = value };
        IncomDataHandler(serialNum, commandId, dataRcv);
    }
}