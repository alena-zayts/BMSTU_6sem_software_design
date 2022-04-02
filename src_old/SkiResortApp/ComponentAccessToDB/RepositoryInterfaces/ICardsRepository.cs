using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface ICardsRepository
    {
        List<CardDB> GetList();
        CardDB GetById(uint card_id);
        void Add(CardDB card);
        void Update(CardDB card); 
        void Delete(CardDB card); 
    }
}
