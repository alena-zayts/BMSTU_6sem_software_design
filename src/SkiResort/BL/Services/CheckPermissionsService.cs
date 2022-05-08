using BL.IRepositories;
using BL.Models;
using BL.Exceptions.PermissionExceptions;

namespace BL.Services
{
    public class CheckPermissionsService
    {
        public static async Task CheckPermissionsAsync(IUsersRepository usersRepository, uint userID,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            PermissionsEnum permissions = (await usersRepository.GetUserByIdAsync(userID)).Permissions;

            if (permissions != PermissionsEnum.ADMIN)
            {
                // admin only
                if (memberName.Contains("Admin"))
                {
                    throw new PermissionException("", userID, memberName);
                }

                // ski_patrol
                List<string> admin_patrol_only = new List<string> { "MarkMessageReadByUserAsync", "GetMessagesAsync", "GetLiftsSlopesInfoAsync"};

                if (admin_patrol_only.Contains(memberName) || memberName.Contains("Update"))
                {
                    if (permissions != PermissionsEnum.SKI_PATROL)
                    {
                        throw new PermissionException("", userID, memberName);
                    }
                }

                // everyone (GetLiftsSlopesInfo -- ski_patrol)
                else if (!memberName.Contains("Get"))
                {
                    switch (memberName)
                    {
                        // authorized but not ski patrol
                        case "SendMessageAsync":
                            if (permissions == PermissionsEnum.AUTHORIZED) { return; }
                            throw new PermissionException("", userID, memberName);


                        case "LogInAsync":
                            if (permissions == PermissionsEnum.UNAUTHORIZED) { return; }
                            throw new PermissionException("", userID, memberName);


                        case "RegisterAsync":
                            if (permissions == PermissionsEnum.UNAUTHORIZED) { return; }
                            throw new PermissionException("", userID, memberName);


                        case "LogOutAsync":
                            if (permissions != PermissionsEnum.UNAUTHORIZED) { return; }
                            throw new PermissionException("", userID, memberName);
                    }

                    throw new PermissionException($"unknown function {memberName}", userID, memberName);
                }
            }
        }
    }
}
