using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface ICardsRepository
    {
        List<CardBL> GetList();
        CardBL GetById(uint card_id);
        void Add(CardBL card);
        void Update(CardBL card); 
        void Delete(CardBL card); 
    }
}
