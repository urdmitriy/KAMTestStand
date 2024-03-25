using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KAMTestStand
{

    public class EntityList
    {
        public EntityList()
        {
            Data = new List<Entity>();
        }

        public readonly List<Entity> Data;

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