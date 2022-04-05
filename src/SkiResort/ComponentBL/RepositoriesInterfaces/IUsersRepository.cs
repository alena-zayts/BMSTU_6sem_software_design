using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;


namespace ComponentBL.RepositoriesInterfaces
{
    public interface IUsersRepository
    {
        Task<List<UserBL>> GetList();
        Task<UserBL> GetById(uint user_id);
        Task<bool> CheckIdExists(uint user_id);
        Task<bool> CheckEmailExists(string user_email);
        Task Add(UserBL user);
        Task<UserBL> AddAutoIncrement(UserBL obj);
        Task Update(UserBL user); 
        Task Delete(UserBL user); 
    }
}
