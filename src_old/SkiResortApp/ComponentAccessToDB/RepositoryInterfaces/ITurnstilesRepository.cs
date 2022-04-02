using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface ITurnstilesRepository
    {
        List<TurnstileDB> GetList();
        TurnstileDB GetById(uint turnstile_id);
        List<TurnstileDB> GetByLiftId(uint lift_id);
        void Add(TurnstileDB turnstile);
        void Update(TurnstileDB turnstile); 
        void Delete(TurnstileDB turnstile); 
    }
}
