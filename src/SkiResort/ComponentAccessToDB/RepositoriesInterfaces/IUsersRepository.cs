using System.Collections.Generic;
using System.Threading.Tasks;

using SkiResort.ComponentBL.ModelsBL;


namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface IUsersRepository
    {
        Task<List<UserBL>> GetList();
        Task<UserBL> GetById(uint user_id);
        Task Add(UserBL user);
        Task Update(UserBL user); 
        Task Delete(UserBL user); 
    }
}
