using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFM.EDM;

namespace VFM.API.EntityServices
{
    public class LoginPermissions : BaseEntityService<LoginPermission>, IEntityService<LoginPermission>
    {
        public void Delete(LoginPermission entity)
        {
            DeleteByKey(entity.Id);
        }

        public LoginPermission Update(LoginPermission entity)
        {
            return UpdateByKey(entity, entity.Id);
        }

        public LoginPermission GetById(int id)
        {
            using (var context = new FuelManagementEntities())
            {
                return context.LoginPermissions.FirstOrDefault(x => x.Id == id);
            }
        }

        public LoginPermission GetByUserID(int userId)
        {
            using (var context = new FuelManagementEntities())
            {
                return context.LoginPermissions.FirstOrDefault(x => x.UserID == userId);
            }
        }
    }
}
