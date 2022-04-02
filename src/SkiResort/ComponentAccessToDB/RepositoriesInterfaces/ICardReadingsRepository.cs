using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface ICardReadingsRepository
    {
        List<CardReadingBL> GetList();
        CardReadingBL GetById(uint record_id);
        void Add(CardReadingBL card_reading);
        void Update(CardReadingBL card_reading); 
        void Delete(CardReadingBL card_reading);
        List<CardReadingBL> GetByLiftIdFromDate(uint lift_id, uint date_from);
    }
}
