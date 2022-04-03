using System.Collections.Generic;
using System.Threading.Tasks;

using SkiResort.ComponentBL.ModelsBL;


namespace SkiResort.ComponentBL.RepositoriesInterfaces
{
    public interface ITurnstilesRepository
    {
        Task<List<TurnstileBL>> GetList();
        Task<TurnstileBL> GetById(uint turnstile_id);

        Task Add(TurnstileBL turnstile);
        Task Update(TurnstileBL turnstile);
        Task Delete(TurnstileBL turnstile);
        Task<List<TurnstileBL>> GetListByLiftId(uint lift_id);
    }
}
