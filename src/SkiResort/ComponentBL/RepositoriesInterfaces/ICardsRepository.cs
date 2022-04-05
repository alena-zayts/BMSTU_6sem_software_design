using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL.RepositoriesInterfaces
{
    public interface ICardsRepository
    {
        Task<List<CardBL>> GetList();
        Task<CardBL> GetById(uint card_id);
        Task Add(CardBL card);
        Task<CardBL> AddAutoIncrement(CardBL obj);
        Task Update(CardBL card);
        Task Delete(CardBL card);

    }
}
