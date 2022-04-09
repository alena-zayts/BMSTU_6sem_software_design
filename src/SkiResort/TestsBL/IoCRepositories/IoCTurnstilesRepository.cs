using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;



namespace TestsBL.IoCRepositories
{
    public class IoCTurnstilesRepository : ITurnstilesRepository
    {
        private static readonly List<Turnstile> data = new();

        public async Task AddTurnstileAsync(Turnstile turnstile)
        {
            if (await CheckTurnstileIdExistsAsync(turnstile.TurnstileID))
            {
                throw new Exception();
            }
            data.Add(turnstile);
        }

        public async Task<Turnstile> AddTurnstileAutoIncrementAsync(Turnstile turnstile)
        {
            uint maxTurnstileID = 0;
            foreach (var turnstileFromDB in data)
            {
                if (turnstileFromDB.TurnstileID > maxTurnstileID)
                    maxTurnstileID = turnstileFromDB.TurnstileID;
            }
            Turnstile turnstileWithCorrectId = new(maxTurnstileID + 1, turnstile.LiftID, turnstile.IsOpen);
            await AddTurnstileAsync(turnstileWithCorrectId);
            return turnstileWithCorrectId;
        }

        public async Task<bool> CheckTurnstileIdExistsAsync(uint turnstileID)
        {
            foreach (var turnstile in data)
            {
                if (turnstile.TurnstileID == turnstileID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteTurnstileAsync(Turnstile turnstile)
        {
            foreach (var obj in data)
            {
                if (obj.TurnstileID == turnstile.TurnstileID)
                {
                    data.Remove(obj);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task<Turnstile> GetTurnstileByIdAsync(uint turnstileID)
        {
            foreach (var obj in data)
            {
                if (obj.TurnstileID == turnstileID)
                    return obj;
            }
            throw new Exception();
        }

        public async Task<List<Turnstile>> GetTurnstilesAsync(uint offset = 0, uint limit = 0)
        {
            return data.GetRange((int)offset, (int)(limit - offset));
        }

        public async Task UpdateTurnstileAsync(Turnstile turnstile)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Turnstile turnstileFromDB = data[i];
                if (turnstileFromDB.TurnstileID == turnstile.TurnstileID)
                {
                    data.Remove(turnstileFromDB);
                    data.Insert(i, turnstile);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
