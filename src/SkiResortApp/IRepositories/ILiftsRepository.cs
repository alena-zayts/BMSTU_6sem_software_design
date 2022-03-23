using System;
using System.Collections.Generic;
using SkiResortApp.DbModels;


namespace SkiResortApp.IRepositories
{
    public interface ILiftsRepository
    {
        List<Lift> GetList();
        Lift GetById(uint lift_id);
        Lift GetByName(string name);
        void Add(Lift lift);
        void Update(Lift lift); 
        void Delete(Lift lift); 
    }
}
