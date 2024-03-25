using System;

namespace KAMTestStand;

public class DataExchange
{
    public DataExchange(ComData comData, TcpData tcpData, Report report)
    {
        _comData = comData;
        _tcpData = tcpData;
        _report = report;
    }
    
    private readonly DataT[] _resultData = new DataT[(int)IoNb.IoCnt];
    private readonly ComData _comData;
    private readonly TcpData _tcpData;
    private readonly Report _report;
    
    private void IncomDataHandler(int commandId, DataT data)
    {
        switch ((IoNb)commandId)
        {
            case IoNb.Sim1CountData2G:
            case IoNb.Sim1CountData3G:
                if (data.Result == ResultE.StatusTesting)
                {
                    _tcpData.SendTestTcpData(ParseIpAddress(_resultData[(int)IoNb.Sim1IpAddr2G].Value));
                }
                else
                {
                    _resultData[commandId].Result = data.Result;
                    _resultData[commandId].Value = data.Value;
                }

                break;
            case IoNb.Sim2CountData2G:
            case IoNb.Sim2CountData3G:
                if (data.Result == ResultE.StatusTesting)
                {
                    _tcpData.SendTestTcpData(ParseIpAddress(_resultData[(int)IoNb.Sim2IpAddr2G].Value));
                }
                else
                {
                    _resultData[commandId].Result = data.Result;
                    _resultData[commandId].Value = data.Value;
                }

                break;
            case IoNb.DiCnt1:
            case IoNb.DiCnt2:
                if (data.Result == ResultE.StatusTesting)
                {
                    Console.WriteLine("Send data to discovery...");

                    byte[] dataByteArray = new byte[16];
                    dataByteArray[0] = Convert.ToByte(commandId);
                    dataByteArray[4] = Convert.ToByte(ResultE.StatusTesting);
                    _comData.SendDataDiscovery(dataByteArray, 0, 16);
                }
                else
                {
                    _resultData[commandId].Result = data.Result;
                    _resultData[commandId].Value = data.Value;
                }

                break;

            default:
                _resultData[commandId].Result = data.Result;
                _resultData[commandId].Value = data.Value;
                break;

        }

        if ((IoNb)commandId == IoNb.ButtonLong)
        {
            _report.GenerateReport();
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
        Console.WriteLine(data);
        var positionIdBegin = data.IndexOf("<CommandId>", StringComparison.Ordinal) + "<CommandId>".Length;
        var positionIdEnd = data.IndexOf("</CommandId>", StringComparison.Ordinal);
        var positionResultBegin = data.IndexOf("<Result>", StringComparison.Ordinal) + "<Result>".Length;
        var positionResultEnd = data.IndexOf("</Result>", StringComparison.Ordinal);
        var positionValueBegin = data.IndexOf("<Value>", StringComparison.Ordinal) + "<Value>".Length;
        var positionValueEnd = data.IndexOf("</Value>", StringComparison.Ordinal);

        var commandId = int.Parse(data.Substring(positionIdBegin, positionIdEnd - positionIdBegin));
        var result = int.Parse(data.Substring(positionResultBegin, positionResultEnd - positionResultBegin));
        var value = int.Parse(data.Substring(positionValueBegin, positionValueEnd - positionValueBegin));

        var dataRcv = new DataT() { Result = (ResultE)result, Value = value };
        IncomDataHandler(commandId, dataRcv);
    }
}