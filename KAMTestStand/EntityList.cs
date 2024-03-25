using System.Collections.Generic;
using System.Linq;

namespace KAMTestStand
{

    public class EntityList
    {
        public EntityList()
        {
            Data = new List<Entity>();
        }

        public readonly List<Entity> Data;
        private static List<IoNb> _listNeedData = new List<IoNb>();

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
            
            if (entity.DeviceIdVal != 0) modifyEntity.DeviceIdVal = entity.DeviceIdVal;
            if (entity.ReadyTimeVal != 0) modifyEntity.ReadyTimeVal = entity.ReadyTimeVal;
            if (entity.SerialNumberVal != 0) modifyEntity.SerialNumberVal = entity.SerialNumberVal;
            
        }
        private Entity? FindEntity(int serialNumber)
        {
            var firstOrDefault = Data.FirstOrDefault(n => n.SerialNumberVal == serialNumber);
            return firstOrDefault;
        }
    }
}