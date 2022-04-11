using ProGaudi.Tarantool.Client;


namespace AccessToDB
{
    public class TarantoolContext
    {
        public IBox box;
        public ISpace lifts_space;
        public IIndex lifts_index_primary;
        public IIndex lifts_index_name;

        public ISpace slopes_space;
        public IIndex slopes_index_primary;
        public IIndex slopes_index_name;

        public ISpace lifts_slopes_space;
        public IIndex lifts_slopes_index_primary;
        public IIndex lifts_slopes_index_lift_id;
        public IIndex lifts_slopes_index_slope_id;

        public ISpace turnstiles_space;
        public IIndex turnstiles_index_primary;
        public IIndex turnstiles_index_lift_id;

        public ISpace cardReadings_space;
        public IIndex cardReadings_index_primary;
        public IIndex cardReadings_index_turnstile;

        public ISpace cards_space;
        public IIndex cards_index_primary;

        public ISpace users_space;
        public IIndex users_index_primary;
        public IIndex users_index_email;

        public ISpace messages_space;
        public IIndex messages_index_primary;
        public IIndex messages_index_sender_id;
        public IIndex messages_index_checked_by_id;


        public TarantoolContext(string connection_string) => (
            box,
            lifts_space, lifts_index_primary, lifts_index_name,
            slopes_space, slopes_index_primary, slopes_index_name,
            lifts_slopes_space, lifts_slopes_index_primary, lifts_slopes_index_lift_id, lifts_slopes_index_slope_id,
            turnstiles_space, turnstiles_index_primary, turnstiles_index_lift_id,
            cardReadings_space, cardReadings_index_primary, cardReadings_index_turnstile,
            cards_space, cards_index_primary,
            users_space, users_index_primary, users_index_email,
            messages_space, messages_index_primary, messages_index_sender_id, messages_index_checked_by_id
            ) = Initialize(connection_string).GetAwaiter().GetResult();


        private static async Task<(
        IBox,
        ISpace, IIndex, IIndex,
        ISpace, IIndex, IIndex,
        ISpace, IIndex, IIndex, IIndex,
        ISpace, IIndex, IIndex,
        ISpace, IIndex, IIndex,
        ISpace, IIndex,
        ISpace, IIndex, IIndex,
        ISpace, IIndex, IIndex, IIndex)> 
            Initialize(string connection_string)
        {
            var box = await Box.Connect(connection_string);
            var schema = box.GetSchema();

            var lifts_space = await schema.GetSpace("lifts");
            var lifts_index_primary = await lifts_space.GetIndex("primary");
            var lifts_index_name = await lifts_space.GetIndex("index_name");

            var slopes_space = await schema.GetSpace("slopes");
            var slopes_index_primary = await slopes_space.GetIndex("primary");
            var slopes_index_name = await slopes_space.GetIndex("index_name");

            var lifts_slopes_space = await schema.GetSpace("lifts_slopes");
            var lifts_slopes_index_primary = await lifts_slopes_space.GetIndex("primary");
            var lifts_slopes_index_lift_id = await lifts_slopes_space.GetIndex("index_lift_id");
            var lifts_slopes_index_slope_id = await lifts_slopes_space.GetIndex("index_slope_id");


            var turnstiles_space = await schema.GetSpace("turnstiles");
            var turnstiles_index_primary = await turnstiles_space.GetIndex("primary");
            var turnstiles_index_lift_id = await turnstiles_space.GetIndex("index_lift_id");


            var cardReadings_space = await schema.GetSpace("card_readings");
            var cardReadings_index_primary = await cardReadings_space.GetIndex("primary");
            var cardReadings_index_turnstile = await cardReadings_space.GetIndex("index_turnstile");


            var cards_space = await schema.GetSpace("cards");
            var cards_index_primary = await cards_space.GetIndex("primary");


            var users_space = await schema.GetSpace("users");
            var users_index_primary = await users_space.GetIndex("primary");
            var users_index_email = await users_space.GetIndex("index_email");


            var messages_space = await schema.GetSpace("messages");
            var messages_index_primary = await messages_space.GetIndex("primary");
            var messages_index_sender_id = await messages_space.GetIndex("index_sender_id");
            var messages_index_checked_by_id = await messages_space.GetIndex("index_checked_by_id");




            return (
                box,
                lifts_space, lifts_index_primary, lifts_index_name,
                slopes_space, slopes_index_primary, slopes_index_name,
                lifts_slopes_space, lifts_slopes_index_primary, lifts_slopes_index_lift_id, lifts_slopes_index_slope_id,
                turnstiles_space, turnstiles_index_primary, turnstiles_index_lift_id,
                cardReadings_space, cardReadings_index_primary, cardReadings_index_turnstile,
                cards_space, cards_index_primary,
                users_space, users_index_primary, users_index_email,
                messages_space, messages_index_primary, messages_index_sender_id, messages_index_checked_by_id
            );
        }
    }
}

