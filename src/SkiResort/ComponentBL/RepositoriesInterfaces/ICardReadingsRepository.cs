using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL.RepositoriesInterfaces
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReadingBL>> GetList();
        Task<uint> CountForLiftIdFromDate(uint lift_id, uint date_from);


        // для тестов
        Task Add(CardReadingBL card_reading);
        Task Delete(CardReadingBL card_reading);
    }
}
