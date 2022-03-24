using System;
using System.Collections.Generic;
using SkiResortApp.DbModels;


namespace SkiResortApp.IRepositories
{
    public interface ISlopesRepository
    {
        List<Slope> GetList();
        Slope GetById(uint slope_id);
        Slope GetByName(string name);
        void Add(Slope slope);
        void Update(Slope slope); 
        void Delete(Slope slope); 
    }
}
