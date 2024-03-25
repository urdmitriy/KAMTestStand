using System;
using System.Collections.Generic;
using System.IO;
using GrapeCity.Documents.Excel;
using KAM_Report;

namespace KAMTestStand;

public class Report
{
    public Report(SettingsData settingsData)
    {
        _settingsData = settingsData;
    }

    private readonly SettingsData _settingsData;
    private string _fileName;
    private static List<IoNb> _listNeedData = new List<IoNb>();
    
    public void GenerateReport()
    {
        // var book = new Workbook();
        //
        // var errorCount = 0;
        //
        // var dataToExcel = new DataDto
        // {
        //     Date = DateTime.Today.ToString("d"),
        //     SerialNumber = ResultData[(int)IoNb.SerialNumber].Value.ToString()
        // };
        //
        // //KAM200-14
        // if ((int)ResultData[(int)IoNb.DeviceId].Result ==
        //     (int)ModelId.Kam14)
        // {
        //     if (ResultData[(int)IoNb.Rs2321].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs4851].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs4852].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Kam].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Sim1CountData2G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim1CountData3G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim2CountData2G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim2CountData3G].Value > 0)
        //     {
        //         dataToExcel.InterfacesTest = "Соотв.";
        //     }
        //     else
        //     {
        //         dataToExcel.InterfacesTest = "Не соотв.";
        //         errorCount++;
        //
        //         var message = "Ошибки: ";
        //         if (ResultData[(int)IoNb.Rs2321].Result != ResultE.StatusPass)
        //         {
        //             message += "RS232 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs4851].Result != ResultE.StatusPass)
        //         {
        //             message += "RS485 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs4852].Result != ResultE.StatusPass)
        //         {
        //             message += "RS485 #2; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Kam].Result != ResultE.StatusPass)
        //         {
        //             message += "KAM bus; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Sim1CountData2G].Value == 0 ||
        //             ResultData[(int)IoNb.Sim1CountData3G].Value == 0)
        //         {
        //             message += "GPRS #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Sim2CountData2G].Value == 0 ||
        //             ResultData[(int)IoNb.Sim2CountData3G].Value == 0)
        //         {
        //             message += "GPRS #2; ";
        //         }
        //
        //         dataToExcel.InterfacesTestMessage = message;
        //     }
        // }
        // else if ((int)ResultData[(int)IoNb.DeviceId].Result ==
        //          (int)ModelId.Kam25)
        // {
        //     if (ResultData[(int)IoNb.Rs2321].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs2322].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs4851].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs4852].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Sim1CountData2G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim1CountData3G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim2CountData2G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim2CountData3G].Value > 0)
        //     {
        //         dataToExcel.InterfacesTest = "Соотв.";
        //     }
        //     else
        //     {
        //         dataToExcel.InterfacesTest = "Не соотв.";
        //         errorCount++;
        //         var message = "Ошибки: ";
        //         if (ResultData[(int)IoNb.Rs2321].Result != ResultE.StatusPass)
        //         {
        //             message += "RS232 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs2322].Result != ResultE.StatusPass)
        //         {
        //             message += "RS232 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs4851].Result != ResultE.StatusPass)
        //         {
        //             message += "RS485 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs4852].Result != ResultE.StatusPass)
        //         {
        //             message += "RS485 #2; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Sim1CountData2G].Value == 0 ||
        //             ResultData[(int)IoNb.Sim1CountData3G].Value == 0)
        //         {
        //             message += "GPRS #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Sim2CountData2G].Value == 0 ||
        //             ResultData[(int)IoNb.Sim2CountData3G].Value == 0)
        //         {
        //             message += "GPRS #2; ";
        //         }
        //
        //         dataToExcel.InterfacesTestMessage = message;
        //     }
        // }
        // else if ((int)ResultData[(int)IoNb.DeviceId].Result ==
        //          (int)ModelId.Kam25Mr)
        // {
        //     if (ResultData[(int)IoNb.Rs2321].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs2322].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs4851].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Rs4852].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Kam].Result == ResultE.StatusPass &&
        //         ResultData[(int)IoNb.Sim1CountData2G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim1CountData3G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim2CountData2G].Value > 0 &&
        //         ResultData[(int)IoNb.Sim2CountData3G].Value > 0)
        //     {
        //         dataToExcel.InterfacesTest = "Соотв.";
        //     }
        //     else
        //     {
        //         dataToExcel.InterfacesTest = "Не соотв.";
        //         errorCount++;
        //         var message = "Ошибки: ";
        //         if (ResultData[(int)IoNb.Rs2321].Result != ResultE.StatusPass)
        //         {
        //             message += "RS232 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs2322].Result != ResultE.StatusPass)
        //         {
        //             message += "RS232 #2; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs4851].Result != ResultE.StatusPass)
        //         {
        //             message += "RS485 #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Rs4852].Result != ResultE.StatusPass)
        //         {
        //             message += "RS485 #2; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Kam].Result != ResultE.StatusPass)
        //         {
        //             message += "KAM bus; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Sim1CountData2G].Value == 0 ||
        //             ResultData[(int)IoNb.Sim1CountData3G].Value == 0)
        //         {
        //             message += "GPRS #1; ";
        //         }
        //
        //         if (ResultData[(int)IoNb.Sim2CountData2G].Value == 0 ||
        //             ResultData[(int)IoNb.Sim2CountData3G].Value == 0)
        //         {
        //             message += "GPRS #2; ";
        //         }
        //
        //         dataToExcel.InterfacesTestMessage = message;
        //     }
        // }
        //
        // if (ResultData[(int)IoNb.TimeSync].Result == ResultE.StatusPass)
        // {
        //     dataToExcel.NtpTest = "Соотв.";
        // }
        // else
        // {
        //     dataToExcel.NtpTest = "Не соотв.";
        //     errorCount++;
        // }
        //
        //
        // if (ResultData[(int)IoNb.Sim1Registered2G].Result == ResultE.StatusPass &&
        //     ResultData[(int)IoNb.Sim1Registered3G].Result == ResultE.StatusPass)
        // {
        //     dataToExcel.Sim1Test = "Соотв.";
        //     dataToExcel.Sim1Rssi =
        //         $"RSSI {ResultData[(int)IoNb.Sim1Rssi2G].Result.ToString()} %";
        // }
        // else
        // {
        //     dataToExcel.Sim1Test = "Не соотв.";
        //     errorCount++;
        // }
        //
        // if (ResultData[(int)IoNb.Sim2Registered2G].Result == ResultE.StatusPass &&
        //     ResultData[(int)IoNb.Sim2Registered3G].Result == ResultE.StatusPass)
        // {
        //     dataToExcel.Sim2Test = "Соотв.";
        //     dataToExcel.Sim1Rssi =
        //         $"RSSI {ResultData[(int)IoNb.Sim2Rssi2G].Result.ToString()} %";
        // }
        // else
        // {
        //     dataToExcel.Sim2Test = "Не соотв.";
        //     errorCount++;
        // }
        //
        // if (ResultData[(int)IoNb.CurrentSleepUa].Value < _settingsData.MaxCurrentDeepSleep &&
        //     ResultData[(int)IoNb.CurrentSleepUa].Value > 0)
        // {
        //
        //     dataToExcel.DeepSleepCurrent = "Соотв.";
        //
        // }
        // else
        // {
        //     dataToExcel.DeepSleepCurrent = "Не соотв.";
        //     errorCount++;
        // }
        //
        // dataToExcel.DeepSleepCurrentValue =
        //     $"{ResultData[(int)IoNb.CurrentSleepUa].Value.ToString()} мкА";
        //
        // if (ResultData[(int)IoNb.Sim1IpAddr2G].Value > 0 &&
        //     ResultData[(int)IoNb.Sim1IpAddr3G].Value > 0 &&
        //     ResultData[(int)IoNb.Sim2IpAddr2G].Value > 0 &&
        //     ResultData[(int)IoNb.Sim2IpAddr3G].Value > 0
        //    )
        // {
        //     dataToExcel.GsmRegistration = "Соотв.";
        // }
        // else
        // {
        //     dataToExcel.GsmRegistration = "Не соотв.";
        //     errorCount++;
        //     var message = "Ошибки: ";
        //     if (ResultData[(int)IoNb.Sim1IpAddr2G].Value == 0 ||
        //         ResultData[(int)IoNb.Sim1IpAddr3G].Value == 0)
        //     {
        //         message += "SIM #1; ";
        //     }
        //
        //     if (ResultData[(int)IoNb.Sim2IpAddr2G].Value == 0 ||
        //         ResultData[(int)IoNb.Sim2IpAddr3G].Value == 0)
        //     {
        //         message += "SIM #2; ";
        //     }
        //
        //     dataToExcel.GsmRegistrationMessage = message;
        // }
        //
        // if (ResultData[(int)IoNb.CurrentGsmMa].Value < _settingsData.MaxCurrentGsm &&
        //     ResultData[(int)IoNb.CurrentGsmMa].Value > 0)
        // {
        //     dataToExcel.GsmCurrent = "Соотв.";
        // }
        // else
        // {
        //     dataToExcel.GsmCurrent = "Не соотв.";
        //     errorCount++;
        // }
        //
        // dataToExcel.GsmCurrentValue =
        //     $"{ResultData[(int)IoNb.CurrentGsmMa].Value.ToString()} мА";
        //
        // if (ResultData[(int)IoNb.CurrentGsmPeakMa].Value < _settingsData.MaxCurrentPeak &&
        //     ResultData[(int)IoNb.CurrentGsmPeakMa].Value > 0)
        // {
        //
        //     dataToExcel.PeakCurrent = "Соотв.";
        //
        // }
        // else
        // {
        //     dataToExcel.PeakCurrent = "Не соотв.";
        //     errorCount++;
        // }
        //
        // dataToExcel.PeakCurrentValue =
        //     $"{ResultData[(int)IoNb.CurrentGsmPeakMa].Value.ToString()} мА";
        //
        // if (ResultData[(int)IoNb.CurrentGsmAndSleepMa].Value <= _settingsData.MaxCurrentGsmSleep &&
        //     ResultData[(int)IoNb.CurrentGsmAndSleepMa].Value > 0)
        // {
        //     dataToExcel.GsmSleepCurrent = "Соотв.";
        // }
        // else
        // {
        //     dataToExcel.GsmSleepCurrent = "Не соотв.";
        //     errorCount++;
        // }
        //
        // dataToExcel.GsmSleepCurrentValue =
        //     $"{ResultData[(int)IoNb.CurrentGsmAndSleepMa].Value.ToString()} мА";
        //
        // if (ResultData[(int)IoNb.ReadyTime].Value < _settingsData.MaxTimeReady &&
        //     ResultData[(int)IoNb.ReadyTime].Value > 0)
        // {
        //     dataToExcel.TimeReady = "Соотв.";
        // }
        // else
        // {
        //     dataToExcel.TimeReady = "Не соотв.";
        //     errorCount++;
        // }
        //
        // dataToExcel.TimeReadyValue = $"{ResultData[(int)IoNb.ReadyTime].Value.ToString()} с";
        //
        // if (ResultData[(int)IoNb.Di1].Result == ResultE.StatusPass &&
        //     ResultData[(int)IoNb.Di2].Result == ResultE.StatusPass)
        // {
        //     dataToExcel.Din = "Соотв.";
        // }
        // else
        // {
        //     var message = "Ошибки: ";
        //     dataToExcel.Din = "Не соотв.";
        //     errorCount++;
        //     if (ResultData[(int)IoNb.Di1].Result != ResultE.StatusPass)
        //         message += "Di #1; ";
        //     if (ResultData[(int)IoNb.Di2].Result != ResultE.StatusPass)
        //         message += "Di #2; ";
        //     dataToExcel.DinMessage = message;
        // }
        //
        // var errorText = errorCount > 0 ? " ERROR" : "";
        //
        // if ((int)ResultData[(int)IoNb.DeviceId].Result ==
        //     (int)ModelId.Kam14)
        // {
        //     book.Open("КАМ200-14_template.xlsx");
        //     _fileName =
        //         $"KAM200-14 Report ser.N {ResultData[(int)IoNb.SerialNumber].Value},{DateTime.Today.ToString("d")} {DateTime.Now.Hour.ToString("00")}.{DateTime.Now.Minute.ToString("00")}{errorText}.xlsx";
        // }
        // else if ((int)ResultData[(int)IoNb.DeviceId].Result ==
        //          (int)ModelId.Kam25)
        // {
        //     book.Open("КАМ25_МР_template.xlsx");
        //     _fileName =
        //         $"KAM25 Report ser.N {ResultData[(int)IoNb.SerialNumber].Value},{DateTime.Today.ToString("d")} {DateTime.Now.Hour.ToString("00")}.{DateTime.Now.Minute.ToString("00")}{errorText}.xlsx";
        // }
        // else if ((int)ResultData[(int)IoNb.DeviceId].Result ==
        //          (int)ModelId.Kam25Mr)
        // {
        //     book.Open("КАМ25_МР_template.xlsx");
        //     _fileName =
        //         $"KAM25MR Report ser.N {ResultData[(int)IoNb.SerialNumber].Value},{DateTime.Today.ToString("d")} {DateTime.Now.Hour.ToString("00")}.{DateTime.Now.Minute.ToString("00")}{errorText}.xlsx";
        // }
        //
        // book.AddDataSource("Data", dataToExcel);
        // book.ProcessTemplate();
        //
        // if (_settingsData.PathReport != null)
        // {
        //     book.Save(Path.Combine(_settingsData.PathReport, _fileName));
        //     Console.WriteLine($"Generate report {Path.Combine(_settingsData.PathReport, _fileName)}");
        // }
        //
        // foreach (var dataIo in _listNeedData)
        // {
        //     ResultData[(int)dataIo].Result = ResultE.StatusNotStart;
        //     ResultData[(int)dataIo].Value = 0;
        // }
    }
}