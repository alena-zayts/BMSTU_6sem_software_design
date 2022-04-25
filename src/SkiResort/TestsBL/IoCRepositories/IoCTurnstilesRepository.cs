using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL;
using BL.Models;
using BL.IRepositories;



namespace TestsBL.IoCRepositories
{
    public class IoCTurnstilesRepository : ITurnstilesRepository
    {
        private static readonly List<Turnstile> data = new();

        public async Task AddTurnstileAsync(uint turnstileID, uint liftID, bool isOpen)
        {
            if (await CheckTurnstileIdExistsAsync(turnstileID))
            {
                throw new Exception();
            }
            data.Add(new Turnstile(turnstileID, liftID, isOpen));
        }

        public async Task<uint> AddTurnstileAutoIncrementAsync(uint liftID, bool isOpen)
        {
            uint maxTurnstileID = 0;
            foreach (var turnstileFromDB in data)
            {
                if (turnstileFromDB.TurnstileID > maxTurnstileID)
                    maxTurnstileID = turnstileFromDB.TurnstileID;
            }
            Turnstile turnstileWithCorrectId = new(maxTurnstileID + 1, liftID, isOpen);
            await AddTurnstileAsync(turnstileWithCorrectId.TurnstileID, turnstileWithCorrectId.LiftID, turnstileWithCorrectId.IsOpen);
            return turnstileWithCorrectId.TurnstileID;
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

        public async Task DeleteTurnstileByIDAsync(uint turnstileID)
        {
            foreach (var obj in data)
            {
                if (obj.TurnstileID == turnstileID)
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

        public async Task<List<Turnstile>> GetTurnstilesAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }

        public async Task<List<Turnstile>> GetTurnstilesByLiftIdAsync(uint liftID)
        {
            List<Turnstile> turnstiles = new();
            foreach (Turnstile turnstile in data)
                if (turnstile.LiftID == liftID)
                    turnstiles.Add(turnstile);

            return turnstiles;
        }

        public async Task UpdateTurnstileByIDAsync(uint turnstileID, uint newLiftID, bool newIsOpen)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Turnstile turnstileFromDB = data[i];
                if (turnstileFromDB.TurnstileID == turnstileID)
                {
                    data.Remove(turnstileFromDB);
                    data.Insert(i, new Turnstile(turnstileID, newLiftID, newIsOpen));
                    return;
                }
            }
            throw new Exception();
        }
    }
}
