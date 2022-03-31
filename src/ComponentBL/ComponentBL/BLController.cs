using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkiResortApp.ComponentAccessToDB.DBModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SkiResortApp.ComponentAccessToDB.RepositoryInterfaces;
using SkiResortApp.ComponentAccessToDB.RepositoriesTarantool;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;

namespace SkiResortApp.Controllers
{
    public class BLController : Controller
    {
        private ICardReadingsRepository rep_card_readings;
        private ICardsRepository rep_cards;
        private ILiftsRepository rep_lifts;
        private ILiftsSlopesRepository rep_lifts_slopes;
        private ISlopesRepository rep_slopes;
        private ITurnstilesRepository rep_turnstiles;
        private IUsersRepository rep_users;

        private enum Permissions
        {
            UnauthorisedUser,
            AuthorizedUser,
            SkiPatrol,
            Admin
        }

        public enum RC
        {
            OK,
            ERR_PERMISSIONS,
        }

        public BLController(ISchema schema)
        {
            rep_card_readings = new TarantoolCardReadingsRepository(schema);
            rep_cards = new TarantoolCardsRepository(schema);
            rep_lifts = new TarantoolLiftsRepository(schema);
            rep_lifts_slopes = new TarantoolLiftsSlopesRepository(schema);
            rep_slopes = new TarantoolSlopesRepository(schema);
            rep_turnstiles = new TarantoolTurnstilesRepository(schema);
            rep_users = new TarantoolUsersRepository(schema);
        }

        private Permissions GetPermissions(uint interface_user_id)
        {
            //TODO
            return Permissions.Admin;

        }

        //public RC Register()
        //{


        //}



    }
}
