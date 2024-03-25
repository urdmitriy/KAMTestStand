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
            var modifyEntity = FindEntity(entity.SerialNumber);
            if (modifyEntity is null)
            {
                var newEntity = new Entity()
                {
                    SerialNumber = entity.SerialNumber,
                };
                AddEntity(newEntity);
                modifyEntity = newEntity;
            }
            
            if (entity.ModelId != 0) modifyEntity.ModelId = entity.ModelId;
            if (entity.TimeReady != 0) modifyEntity.TimeReady = entity.TimeReady;
            if (entity.SerialNumber != 0) modifyEntity.SerialNumber = entity.SerialNumber;
            
        }
        private Entity? FindEntity(int serialNumber)
        {
            var firstOrDefault = Data.FirstOrDefault(n => n.SerialNumber == serialNumber);
            return firstOrDefault;
        }
    }
}