using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComponentBL.RepositoriesInterfaces;
using ComponentBL.ModelsBL;

namespace ComponentBL.Services
{
    public class CheckPermissionsService
    {
        public static async Task<bool> CheckPermissions(IUsersRepository rep, uint user_id, string func_name)
        {
            uint perms = (await rep.GetById(user_id)).permissions;



            return true;
        }
    }
}
