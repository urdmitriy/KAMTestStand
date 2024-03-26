using System.Collections.Generic;
using System.Linq;

namespace KAMTestStand
{

    public class EntityList
    {
        public EntityList(DataGridUpdateApp dataGridUpdateApp)
        {
            Data = new List<Entity>();
            _dataGridUpdateApp = dataGridUpdateApp;
        }

        public readonly List<Entity> Data;
        private static List<IoNb> _listNeedData = new List<IoNb>();
        private readonly DataGridUpdateApp _dataGridUpdateApp;

        private void AddEntity(Entity newEntity)
        {
            Data.Add(newEntity);
        }
        public void AddDataEntity(Entity entity)
        {
            var modifyEntity = FindEntity(entity.SerialNumberVal);
            if (modifyEntity is null)
            {
                var newEntity = new Entity()
                {
                    SerialNumberVal = entity.SerialNumberVal,
                };
                AddEntity(newEntity);
                modifyEntity = newEntity;
            }
            
            if (entity.CountDevicesVal != 0) modifyEntity.CountDevicesVal = entity.CountDevicesVal;
            if (entity.CountDevicesRes != 0) modifyEntity.CountDevicesRes = entity.CountDevicesRes;
            if (entity.CurrDeviceVal != 0) modifyEntity.CurrDeviceVal = entity.CurrDeviceVal;
            if (entity.CurrDeviceRes != 0) modifyEntity.CurrDeviceRes = entity.CurrDeviceRes;
            if (entity.DeviceIdVal != 0) modifyEntity.DeviceIdVal = entity.DeviceIdVal;
            if (entity.DeviceIdRes != 0) modifyEntity.DeviceIdRes = entity.DeviceIdRes;
            if (entity.ExtPowerVal != 0) modifyEntity.ExtPowerVal = entity.ExtPowerVal;
            if (entity.ExtPowerRes != 0) modifyEntity.ExtPowerRes = entity.ExtPowerRes;
            if (entity.ButtonShortVal != 0) modifyEntity.ButtonShortVal = entity.ButtonShortVal;
            if (entity.ButtonShortRes != 0) modifyEntity.ButtonShortRes = entity.ButtonShortRes;
            if (entity.ButtonLongVal != 0) modifyEntity.ButtonLongVal = entity.ButtonLongVal;
            if (entity.ButtonLongRes != 0) modifyEntity.ButtonLongRes = entity.ButtonLongRes;
            if (entity.ButtonDoubleVal != 0) modifyEntity.ButtonDoubleVal = entity.ButtonDoubleVal;
            if (entity.ButtonDoubleRes != 0) modifyEntity.ButtonDoubleRes = entity.ButtonDoubleRes;
            if (entity.ButtonResetVal != 0) modifyEntity.ButtonResetVal = entity.ButtonResetVal;
            if (entity.ButtonResetRes != 0) modifyEntity.ButtonResetRes = entity.ButtonResetRes;
            if (entity.Rs2321Val != 0) modifyEntity.Rs2321Val = entity.Rs2321Val;
            if (entity.Rs2321Res != 0) modifyEntity.Rs2321Res = entity.Rs2321Res;
            if (entity.Rs2322Val != 0) modifyEntity.Rs2322Val = entity.Rs2322Val;
            if (entity.Rs2322Res != 0) modifyEntity.Rs2322Res = entity.Rs2322Res;
            if (entity.Rs4851Val != 0) modifyEntity.Rs4851Val = entity.Rs4851Val;
            if (entity.Rs4851Res != 0) modifyEntity.Rs4851Res = entity.Rs4851Res;
            if (entity.Rs4852Val != 0) modifyEntity.Rs4852Val = entity.Rs4852Val;
            if (entity.Rs4852Res != 0) modifyEntity.Rs4852Res = entity.Rs4852Res;
            if (entity.Di1Val != 0) modifyEntity.Di1Val = entity.Di1Val;
            if (entity.Di1Res != 0) modifyEntity.Di1Res = entity.Di1Res;
            if (entity.Di2Val != 0) modifyEntity.Di2Val = entity.Di2Val;
            if (entity.Di2Res != 0) modifyEntity.Di2Res = entity.Di2Res;
            if (entity.DiCnt1Val != 0) modifyEntity.DiCnt1Val = entity.DiCnt1Val;
            if (entity.DiCnt1Res != 0) modifyEntity.DiCnt1Res = entity.DiCnt1Res;
            if (entity.DiCnt2Val != 0) modifyEntity.DiCnt2Val = entity.DiCnt2Val;
            if (entity.DiCnt2Res != 0) modifyEntity.DiCnt2Res = entity.DiCnt2Res;
            if (entity.KamVal != 0) modifyEntity.KamVal = entity.KamVal;
            if (entity.KamRes != 0) modifyEntity.KamRes = entity.KamRes;
            if (entity.ModemStateVal != 0) modifyEntity.ModemStateVal = entity.ModemStateVal;
            if (entity.ModemStateRes != 0) modifyEntity.ModemStateRes = entity.ModemStateRes;
            if (entity.Sim1Registered2GVal != 0) modifyEntity.Sim1Registered2GVal = entity.Sim1Registered2GVal;
            if (entity.Sim1Registered2GRes != 0) modifyEntity.Sim1Registered2GRes = entity.Sim1Registered2GRes;
            if (entity.Sim1Registered3GVal != 0) modifyEntity.Sim1Registered3GVal = entity.Sim1Registered3GVal;
            if (entity.Sim1Registered3GRes != 0) modifyEntity.Sim1Registered3GRes = entity.Sim1Registered3GRes;
            if (entity.Sim2Registered2GVal != 0) modifyEntity.Sim2Registered2GVal = entity.Sim2Registered2GVal;
            if (entity.Sim2Registered2GRes != 0) modifyEntity.Sim2Registered2GRes = entity.Sim2Registered2GRes;
            if (entity.Sim2Registered3GVal != 0) modifyEntity.Sim2Registered3GVal = entity.Sim2Registered3GVal;
            if (entity.Sim2Registered3GRes != 0) modifyEntity.Sim2Registered3GRes = entity.Sim2Registered3GRes;
            if (entity.Sim1IpAddr2GVal != 0) modifyEntity.Sim1IpAddr2GVal = entity.Sim1IpAddr2GVal;
            if (entity.Sim1IpAddr2GRes != 0) modifyEntity.Sim1IpAddr2GRes = entity.Sim1IpAddr2GRes;
            if (entity.Sim1IpAddr3GVal != 0) modifyEntity.Sim1IpAddr3GVal = entity.Sim1IpAddr3GVal;
            if (entity.Sim1IpAddr3GRes != 0) modifyEntity.Sim1IpAddr3GRes = entity.Sim1IpAddr3GRes;
            if (entity.Sim2IpAddr2GVal != 0) modifyEntity.Sim2IpAddr2GVal = entity.Sim2IpAddr2GVal;
            if (entity.Sim2IpAddr2GRes != 0) modifyEntity.Sim2IpAddr2GRes = entity.Sim2IpAddr2GRes;
            if (entity.Sim2IpAddr3GVal != 0) modifyEntity.Sim2IpAddr3GVal = entity.Sim2IpAddr3GVal;
            if (entity.Sim2IpAddr3GRes != 0) modifyEntity.Sim2IpAddr3GRes = entity.Sim2IpAddr3GRes;
            if (entity.Sim1Rssi2GVal != 0) modifyEntity.Sim1Rssi2GVal = entity.Sim1Rssi2GVal;
            if (entity.Sim1Rssi2GRes != 0) modifyEntity.Sim1Rssi2GRes = entity.Sim1Rssi2GRes;
            if (entity.Sim1Rssi3GVal != 0) modifyEntity.Sim1Rssi3GVal = entity.Sim1Rssi3GVal;
            if (entity.Sim1Rssi3GRes != 0) modifyEntity.Sim1Rssi3GRes = entity.Sim1Rssi3GRes;
            if (entity.Sim2Rssi2GVal != 0) modifyEntity.Sim2Rssi2GVal = entity.Sim2Rssi2GVal;
            if (entity.Sim2Rssi2GRes != 0) modifyEntity.Sim2Rssi2GRes = entity.Sim2Rssi2GRes;
            if (entity.Sim2Rssi3GVal != 0) modifyEntity.Sim2Rssi3GVal = entity.Sim2Rssi3GVal;
            if (entity.Sim2Rssi3GRes != 0) modifyEntity.Sim2Rssi3GRes = entity.Sim2Rssi3GRes;
            if (entity.Sim1CountData2GVal != 0) modifyEntity.Sim1CountData2GVal = entity.Sim1CountData2GVal;
            if (entity.Sim1CountData2GRes != 0) modifyEntity.Sim1CountData2GRes = entity.Sim1CountData2GRes;
            if (entity.Sim1CountData3GVal != 0) modifyEntity.Sim1CountData3GVal = entity.Sim1CountData3GVal;
            if (entity.Sim1CountData3GRes != 0) modifyEntity.Sim1CountData3GRes = entity.Sim1CountData3GRes;
            if (entity.Sim2CountData2GVal != 0) modifyEntity.Sim2CountData2GVal = entity.Sim2CountData2GVal;
            if (entity.Sim2CountData2GRes != 0) modifyEntity.Sim2CountData2GRes = entity.Sim2CountData2GRes;
            if (entity.Sim2CountData3GVal != 0) modifyEntity.Sim2CountData3GVal = entity.Sim2CountData3GVal;
            if (entity.Sim2CountData3GRes != 0) modifyEntity.Sim2CountData3GRes = entity.Sim2CountData3GRes;
            if (entity.ToSleepVal != 0) modifyEntity.ToSleepVal = entity.ToSleepVal;
            if (entity.ToSleepRes != 0) modifyEntity.ToSleepRes = entity.ToSleepRes;
            if (entity.ToSleepWithGsmVal != 0) modifyEntity.ToSleepWithGsmVal = entity.ToSleepWithGsmVal;
            if (entity.ToSleepWithGsmRes != 0) modifyEntity.ToSleepWithGsmRes = entity.ToSleepWithGsmRes;
            if (entity.TimeVal != 0) modifyEntity.TimeVal = entity.TimeVal;
            if (entity.TimeRes != 0) modifyEntity.TimeRes = entity.TimeRes;
            if (entity.TimeSyncVal != 0) modifyEntity.TimeSyncVal = entity.TimeSyncVal;
            if (entity.TimeSyncRes != 0) modifyEntity.TimeSyncRes = entity.TimeSyncRes;
            if (entity.CurrentDcMaVal != 0) modifyEntity.CurrentDcMaVal = entity.CurrentDcMaVal;
            if (entity.CurrentDcMaRes != 0) modifyEntity.CurrentDcMaRes = entity.CurrentDcMaRes;
            if (entity.CurrentAcUaVal != 0) modifyEntity.CurrentAcUaVal = entity.CurrentAcUaVal;
            if (entity.CurrentAcUaRes != 0) modifyEntity.CurrentAcUaRes = entity.CurrentAcUaRes;
            if (entity.CurrentSleepUaVal != 0) modifyEntity.CurrentSleepUaVal = entity.CurrentSleepUaVal;
            if (entity.CurrentSleepUaRes != 0) modifyEntity.CurrentSleepUaRes = entity.CurrentSleepUaRes;
            if (entity.CurrentGsmMaVal != 0) modifyEntity.CurrentGsmMaVal = entity.CurrentGsmMaVal;
            if (entity.CurrentGsmMaRes != 0) modifyEntity.CurrentGsmMaRes = entity.CurrentGsmMaRes;
            if (entity.CurrentGsmPeakMaVal != 0) modifyEntity.CurrentGsmPeakMaVal = entity.CurrentGsmPeakMaVal;
            if (entity.CurrentGsmPeakMaRes != 0) modifyEntity.CurrentGsmPeakMaRes = entity.CurrentGsmPeakMaRes;
            if (entity.CurrentGsmAndSleepMaVal != 0) modifyEntity.CurrentGsmAndSleepMaVal = entity.CurrentGsmAndSleepMaVal;
            if (entity.CurrentGsmAndSleepMaRes != 0) modifyEntity.CurrentGsmAndSleepMaRes = entity.CurrentGsmAndSleepMaRes;
            if (entity.ReadyTimeVal != 0) modifyEntity.ReadyTimeVal = entity.ReadyTimeVal;
            if (entity.ReadyTimeRes != 0) modifyEntity.ReadyTimeRes = entity.ReadyTimeRes;
            // if (entity.SerialNumberVal != 0) modifyEntity.SerialNumberVal = entity.SerialNumberVal;
            // if (entity.SerialNumberRes != 0) modifyEntity.SerialNumberVal = entity.SerialNumberVal;
            if (entity.LedLestVal != 0) modifyEntity.LedLestVal = entity.LedLestVal;
            if (entity.LedLestRes != 0) modifyEntity.LedLestRes = entity.LedLestRes;
            if (entity.RunTestsVal != 0) modifyEntity.RunTestsVal = entity.RunTestsVal;
            if (entity.RunTestsRes != 0) modifyEntity.RunTestsRes = entity.RunTestsRes;
            if (entity.ResetVal != 0) modifyEntity.ResetVal = entity.ResetVal;
            if (entity.ResetRes != 0) modifyEntity.ResetRes = entity.ResetRes;
            if (entity.IoCntVal != 0) modifyEntity.IoCntVal = entity.IoCntVal;
            if (entity.IoCntRes != 0) modifyEntity.IoCntRes = entity.IoCntRes;
            _dataGridUpdateApp();
        }
        private Entity? FindEntity(int serialNumber)
        {
            var firstOrDefault = Data.FirstOrDefault(n => n.SerialNumberVal == serialNumber);
            return firstOrDefault;
        }
    }
}