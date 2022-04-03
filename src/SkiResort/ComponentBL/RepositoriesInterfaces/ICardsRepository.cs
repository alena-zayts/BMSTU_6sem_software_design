using System.Collections.Generic;
using System.Threading.Tasks;

using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentBL.RepositoriesInterfaces
{
    public interface ICardsRepository
    {
        Task<List<CardBL>> GetList();
        Task<CardBL> GetById(uint card_id);
        Task Add(CardBL card);
        Task Update(CardBL card);
        Task Delete(CardBL card);

    }
}
