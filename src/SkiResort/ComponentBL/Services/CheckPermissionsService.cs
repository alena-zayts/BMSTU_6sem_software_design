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
        public static async Task<bool> CheckPermissions(IUsersRepository rep, uint user_id, 
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            uint perms = (await rep.GetById(user_id)).permissions;

            if (perms == (uint) Permissions.ADMIN)
            {
                return true;
            }

            // admin only
            if (memberName.Contains("Admin"))
            {
                return false;
            }

            // ski_patrol
            List<string> admin_patrol_only = new List<string> { "MarkMessageReadByUser", "ReadMessagesList", "GetLiftsSlopesInfo"};

            if (admin_patrol_only.Contains(memberName) || memberName.Contains("Update"))
            {
                return perms == (uint) Permissions.SKI_PATROL;
            }

            

            // everyone (GetLiftsSlopesInfo -- ski_patrol)
            if (memberName.Contains("Get"))
            {
                return true;
            }

            switch (memberName)
            {
                // authorized but not ski patrol
                case "SendMessge":
                    return perms == (int)Permissions.AUTHORIZED;
                    break;

                case "LogIn":

                    return perms == (int)Permissions.UNAUTHORIZED;
                    break;

                case "Register":
                    return perms == (int)Permissions.UNAUTHORIZED;
                    break;

                case "LogOut":
                    return perms != (int)Permissions.UNAUTHORIZED;
                    break;
            }

            throw new PermissionsException(user_id, $"unknown function {memberName}");
        }

    }
}
