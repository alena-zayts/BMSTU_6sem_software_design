using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface ILiftsSlopesRepository
    {
        List<LiftSlopeDB> GetList();
        LiftSlopeDB GetById(uint record_id);
        List<LiftDB> GetLiftsBySlopeId(uint slope_id);
        List<SlopeDB> GetSlopesByLiftId(uint lift_id);
        void Add(LiftSlopeDB lift_slope);
        void Update(LiftSlopeDB lift_slope); 
        void Delete(LiftSlopeDB lift_slope); 
    }
}
