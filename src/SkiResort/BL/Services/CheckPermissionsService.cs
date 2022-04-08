using BL.IRepositories;
using BL.Models;
using BL.Exceptions;

namespace BL.Services
{
    public class CheckPermissionsService
    {
        public static async Task<bool> CheckPermissionsAsync(IUsersRepository usersRepository, uint userID, 
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            PermissionsEnum permissions = (await usersRepository.GetUserByIdAsync(userID)).Permissions;

            if (permissions == PermissionsEnum.ADMIN)
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
                return permissions == PermissionsEnum.SKI_PATROL;
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
                    return permissions == PermissionsEnum.AUTHORIZED;


                case "LogIn":

                    return permissions == PermissionsEnum.UNAUTHORIZED;


                case "Register":
                    return permissions == PermissionsEnum.UNAUTHORIZED;


                case "LogOut":
                    return permissions != PermissionsEnum.UNAUTHORIZED;
            }

            throw new PermissionsException("unknown function", userID, memberName);
        }

    }
}
