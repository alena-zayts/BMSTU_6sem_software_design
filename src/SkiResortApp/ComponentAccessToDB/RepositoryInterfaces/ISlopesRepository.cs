using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface ISlopesRepository
    {
        List<SlopeDB> GetList();
        SlopeDB GetById(uint slope_id);
        SlopeDB GetByName(string name);
        void Add(SlopeDB slope);
        void Update(SlopeDB slope); 
        void Delete(SlopeDB slope); 
    }
}
