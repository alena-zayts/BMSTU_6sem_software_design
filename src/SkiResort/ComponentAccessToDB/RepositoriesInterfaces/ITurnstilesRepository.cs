using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface ITurnstilesRepository
    {
        List<TurnstileBL> GetList();
        TurnstileBL GetById(uint turnstile_id);
        List<TurnstileBL> GetByLiftId(uint lift_id);
        void Add(TurnstileBL turnstile);
        void Update(TurnstileBL turnstile); 
        void Delete(TurnstileBL turnstile); 
    }
}
