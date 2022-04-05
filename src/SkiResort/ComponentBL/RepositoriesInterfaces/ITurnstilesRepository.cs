using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;


namespace ComponentBL.RepositoriesInterfaces
{
    public interface ITurnstilesRepository
    {
        Task<List<TurnstileBL>> GetList();
        Task<TurnstileBL> GetById(uint turnstile_id);

        Task Add(TurnstileBL turnstile);
        Task<TurnstileBL> AddAutoIncrement(TurnstileBL obj);
        Task Update(TurnstileBL turnstile);
        Task Delete(TurnstileBL turnstile);
        Task<List<TurnstileBL>> GetListByLiftId(uint lift_id);
    }
}
