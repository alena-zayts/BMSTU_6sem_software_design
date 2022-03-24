using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface ICardReadingsRepository
    {
        List<CardReadingDB> GetList();
        CardReadingDB GetById(uint record_id);
        List<CardReadingDB> GetByLiftIdFromDate(uint lift_id, DateTime date_from);
        void Add(CardReadingDB card_reading);
        void Update(CardReadingDB card_reading); 
        void Delete(CardReadingDB card_reading); 
    }
}
