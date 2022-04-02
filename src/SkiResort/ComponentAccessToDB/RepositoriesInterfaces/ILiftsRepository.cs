using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface ILiftsRepository
    {
        List<LiftBL> GetList();
        LiftBL GetById(uint lift_id);
        LiftBL GetByName(string name);
        void Add(LiftBL lift);
        void Update(LiftBL lift); 
        void Delete(LiftBL lift); 
    }
}
