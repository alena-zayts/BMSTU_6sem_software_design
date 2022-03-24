using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface ILiftsRepository
    {
        List<LiftDB> GetList();
        LiftDB GetById(uint lift_id);
        LiftDB GetByName(string name);
        void Add(LiftDB lift);
        void Update(LiftDB lift); 
        void Delete(LiftDB lift); 
    }
}
