using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface ILiftsSlopesRepository
    {
        List<LiftSlopeBL> GetList();
        LiftSlopeBL GetById(uint record_id);
        List<LiftBL> GetLiftsBySlopeId(uint slope_id);
        List<SlopeBL> GetSlopesByLiftId(uint lift_id);
        void Add(LiftSlopeBL lift_slope);
        void Update(LiftSlopeBL lift_slope); 
        void Delete(LiftSlopeBL lift_slope); 
    }
}
