using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface ISlopesRepository
    {
        List<SlopeBL> GetList();
        SlopeBL GetById(uint slope_id);
        SlopeBL GetByName(string name);
        void Add(SlopeBL slope);
        void Update(SlopeBL slope); 
        void Delete(SlopeBL slope); 
    }
}
