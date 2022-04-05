using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL.RepositoriesInterfaces
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReadingBL>> GetList();
        Task<uint> CountForLiftIdFromDate(uint lift_id, uint date_from);


<<<<<<< HEAD

        // для тестов
        Task Add(CardReadingBL card_reading);
        Task Delete(CardReadingBL card_reading);
        Task<CardReadingBL> AddAutoIncrement(CardReadingBL card_reading);
=======
        // пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
        Task Add(CardReadingBL card_reading);
        Task Delete(CardReadingBL card_reading);
        Task<CardReadingBL> AddAutoIncrement(CardReadingBL obj);
>>>>>>> 8950ca6ea3c2a8e22094a779b1fc45d2a0e819ea
    }
}
