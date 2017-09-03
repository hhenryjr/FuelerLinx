using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VFM.EDM;

namespace VFM.API.EntityServices
{
    public class PartnerServiceIntegrations : BaseEntityService<PartnerServiceIntegration>, IEntityService<PartnerServiceIntegration>
    {
        public void Delete(PartnerServiceIntegration entity)
        {
            DeleteByKey(entity.Id);
        }

        public PartnerServiceIntegration Update(PartnerServiceIntegration entity)
        {
            return UpdateByKey(entity, entity.Id);
        }

        public PartnerServiceIntegration GetByAccountID(Guid accountId)
        {
            using (var context = new FuelManagementEntities())
            {
                return context.PartnerServiceIntegrations.FirstOrDefault(x => x.AccountNumber == accountId);
            }
        }

        public PartnerServiceIntegration GetByClientID(int clientId, int AdminID)
        {
            using (var context = new FuelManagementEntities())
            {
                return context.PartnerServiceIntegrations.FirstOrDefault(x => x.ClientID == clientId && x.AdminClientID == AdminID);
            }
        }
    }
}
