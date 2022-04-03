using System.Collections.Generic;
using System.Threading.Tasks;

using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentBL.RepositoriesInterfaces
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReadingBL>> GetList();
        Task<uint> CountForLiftIdFromDate(uint lift_id, uint date_from);
    }
}
