using System;
using System.Collections.Generic;
using SkiResortApp.DbModels;


namespace SkiResortApp.IRepositories
{
    public interface ITurnstilesRepository
    {
        List<Turnstile> GetList();
        Turnstile GetById(uint turnstile_id);
        List<Turnstile> GetByLiftId(uint lift_id);
        void Add(Turnstile turnstile);
        void Update(Turnstile turnstile); 
        void Delete(Turnstile turnstile); 
    }
}
