using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFM.EDM;

namespace VFM.API.EntityServices.FuelOrder
{
    public class FuelOrderMessages : BaseEntityService<FuelOrderMessage>, IEntityService<FuelOrderMessage>
    {
        public void Delete(FuelOrderMessage entity)
        {
            DeleteByKey(entity.Id);
        }

        public FuelOrderMessage Update(FuelOrderMessage entity)
        {
            return UpdateByKey(entity, entity.Id);
        }

        public List<FuelOrderMessage> GetListByFuelOrder(int fuelOrderID)
        {
            return GetList((message => message.FuelOrderID == fuelOrderID));
        }

        public List<FuelOrderMessage> GetListByFuelOrderSerializable(int fuelOrderID)
        {
            return GetListSerializable((message => message.FuelOrderID == fuelOrderID), "FuelOrderMessageAttachments");
        }
    }
}
