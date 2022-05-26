using BL.IRepositories;
using BL.Models;
using Newtonsoft.Json.Linq;
using Ninject;
using BL;
using AccessToDB;
using System.Diagnostics;
using ProGaudi.Tarantool.Client;

using CardReadingDB = System.ValueTuple<uint, uint, uint, uint>;
using CardReadingDBNoIndex = System.ValueTuple<uint, uint, uint>;
using LiftDB = System.ValueTuple<uint, string, bool, uint, uint, uint>;
using LiftDBNoIndex = System.ValueTuple<string, bool, uint, uint, uint>;
using TurnstileDB = System.ValueTuple<uint, uint, bool>;
using TurnstileDBNoIndex = System.ValueTuple<uint, bool>;


var box = await Box.Connect("ski_admin:Tty454r293300@localhost:3301");


IKernel ninjectKernel = new StandardKernel();
ninjectKernel.Bind<IRepositoriesFactory>().To<TarantoolRepositoriesFactory>();
IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();

ICardReadingsRepository _cardReadingsRepository = repositoriesFactory.CreateCardReadingsRepository();
ILiftsRepository _liftsRepository = repositoriesFactory.CreateLiftsRepository();
ITurnstilesRepository _turnstilesRepository = repositoriesFactory.CreateTurnstilesRepository();

string cardReadingsDir = "C:/BMSTU_6sem_software_design/src/tarantool/app/json_data/card_readings/";
string liftsDir = "C:/BMSTU_6sem_software_design/src/tarantool/app/json_data/lifts/";
string turnstilesDir = "C:/BMSTU_6sem_software_design/src/tarantool/app/json_data/turnstiles/";


string AddResultsFilename = "C:/BMSTU_6sem_software_design/src/SkiResort/Experiment/add.txt";
string UpdateResultsFilename = "C:/BMSTU_6sem_software_design/src/SkiResort/Experiment/update.txt";
string NFilename = "C:/BMSTU_6sem_software_design/src/SkiResort/Experiment/N.txt";

string settingsFilename = "C:/BMSTU_6sem_software_design/src/settings.txt";
string[] lines = System.IO.File.ReadAllLines(settingsFilename);

uint dateFromUint = UInt32.Parse(lines[0]);
uint dateToUint = UInt32.Parse(lines[1]);
uint timeDelta = dateToUint - dateFromUint;
dateToUint = dateFromUint + timeDelta / 2;
DateTimeOffset dateFrom = DateTimeOffset.FromUnixTimeSeconds(dateFromUint);
DateTimeOffset dateTo = DateTimeOffset.FromUnixTimeSeconds(dateToUint);


int n_lifts = Int32.Parse(lines[2]);
int n_turnstiles_per_lift = Int32.Parse(lines[3]);
int n_card_readings_per_turnstile = Int32.Parse(lines[4]);





CardReading GetCardReadingFromJsonFile(string filename)
{
   string data = File.ReadAllText(filename);
   dynamic stuff = JObject.Parse(data);
   CardReading cardReading = new((uint)stuff.RecordID, (uint)stuff.TurnstileID, (uint)stuff.CardID, (DateTimeOffset)(DateTimeOffset.FromUnixTimeSeconds((uint)stuff.ReadingTime)));
   return cardReading;
}

List<CardReading> GetCardReadingsFromJsonFiles(int from, int to)
{
   var prevCardReadings = _cardReadingsRepository.GetCardReadingsAsync().GetAwaiter().GetResult();
   var prevAmount = prevCardReadings.Count();
   Console.WriteLine($"{prevAmount} CardReadings are already in DB:");

   string[] filenames = Directory.GetFiles(cardReadingsDir);
   List<CardReading> cardReadingList = new List<CardReading>();
   for (int i = from; i < to; i++)
   {
       string filename = filenames[i];
       CardReading cardReading = GetCardReadingFromJsonFile(filename);
       cardReadingList.Add(cardReading);
   }
   return cardReadingList;
}

long addCardReadings(List<CardReading> cardReadingList)
{ 
   Stopwatch stopWatch = new Stopwatch();

   stopWatch.Start();
   foreach (CardReading cardReading in cardReadingList)
   {
       CardReadingDBNoIndex cardReadingDBNoIndex = new CardReadingDBNoIndex(cardReading.TurnstileID, cardReading.CardID, (uint)cardReading.ReadingTime.ToUnixTimeSeconds());
       var newID = box.Call_1_6<CardReadingDBNoIndex, CardReadingDB>("auto_increment_card_readings", (cardReadingDBNoIndex)).GetAwaiter().GetResult();
   }
   stopWatch.Stop();
   TimeSpan ts = stopWatch.Elapsed;


   string elapsedTime = String.Format("{0:00}.{1:00}.{2:00}.{3:00}",
       ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
   Console.WriteLine($"Adding: " + elapsedTime);

    List<CardReading> tmp = _cardReadingsRepository.GetCardReadingsAsync().GetAwaiter().GetResult();
    Console.WriteLine($"Current CardReading amount: {tmp.Count()}");

    long timeMS = stopWatch.ElapsedMilliseconds;
   return timeMS;
}



Lift GetLiftFromJsonFile(string filename)
{
    string data = File.ReadAllText(filename);
    dynamic stuff = JObject.Parse(data);
    //{"lift_id": 1, "lift_name": "A0", "is_open": false, "seats_amount": 35, "lifting_time": 61, "queue_time": 0}
    Lift lift = new((uint)stuff.lift_id, (string)stuff.lift_name, (bool)stuff.is_open, (uint)stuff.seats_amount, (uint)stuff.lifting_time, (uint)stuff.queue_time);
    return lift;
}

List<Lift> GetLiftsFromJsonFiles(int from, int to)
{
    var prevLifts = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult();
    var prevAmount = prevLifts.Count();
    Console.WriteLine($"{prevAmount} Lifts are already in DB:");

    string[] filenames = Directory.GetFiles(liftsDir);
    List<Lift> liftList = new List<Lift>();
    for (int i = from; i < to; i++)
    {
        string filename = filenames[i];
        Lift lift = GetLiftFromJsonFile(filename);
        liftList.Add(lift);
    }
    return liftList;
}

long addLifts(List<Lift> liftList)
{
    Stopwatch stopWatch = new Stopwatch();

    stopWatch.Start();
    foreach (Lift lift in liftList)
    {
        LiftDBNoIndex liftDBNoIndex = new LiftDBNoIndex(lift.LiftName, lift.IsOpen, lift.SeatsAmount, lift.LiftingTime, lift.QueueTime);
        var newID = box.Call_1_6<LiftDBNoIndex, LiftDB>("auto_increment_lifts", (liftDBNoIndex)).GetAwaiter().GetResult();
    }
    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;


    string elapsedTime = String.Format("{0:00}.{1:00}.{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
    Console.WriteLine($"Adding: " + elapsedTime);

    List<Lift> tmp = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult();
    Console.WriteLine($"Current lifts amount: {tmp.Count()}");

    long timeMS = stopWatch.ElapsedMilliseconds;
    return timeMS;
}

Turnstile GetTurnstileFromJsonFile(string filename)
{
    string data = File.ReadAllText(filename);
    dynamic stuff = JObject.Parse(data);
    //{"turnstile_id": 1, "lift_id": 1, "is_open": false}
    Turnstile turnstile = new((uint)stuff.turnstile_id, (uint)stuff.lift_id, (bool)stuff.is_open);
    return turnstile;
}

List<Turnstile> GetTurnstilesFromJsonFiles(int from, int to)
{
    var prevTurnstiles = _turnstilesRepository.GetTurnstilesAsync().GetAwaiter().GetResult();
    var prevAmount = prevTurnstiles.Count();
    Console.WriteLine($"{prevAmount} Turnstiles are already in DB:");

    string[] filenames = Directory.GetFiles(turnstilesDir);
    List<Turnstile> turnstileList = new List<Turnstile>();
    for (int i = from; i < to; i++)
    {
        string filename = filenames[i];
        Turnstile turnstile = GetTurnstileFromJsonFile(filename);
        turnstileList.Add(turnstile);
    }
    return turnstileList;
}

long addTurnstiles(List<Turnstile> turnstileList)
{
    Stopwatch stopWatch = new Stopwatch();

    stopWatch.Start();
    foreach (Turnstile turnstile in turnstileList)
    {
        TurnstileDBNoIndex turnstileDBNoIndex = new TurnstileDBNoIndex(turnstile.LiftID, turnstile.IsOpen);
        var newID = box.Call_1_6<TurnstileDBNoIndex, TurnstileDB>("auto_increment_turnstiles", (turnstileDBNoIndex)).GetAwaiter().GetResult();
    }
    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;


    string elapsedTime = String.Format("{0:00}.{1:00}.{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
    Console.WriteLine($"Adding: " + elapsedTime);

    List<Turnstile> tmp = _turnstilesRepository.GetTurnstilesAsync().GetAwaiter().GetResult();
    Console.WriteLine($"Current turnstiles amount: {tmp.Count()}");

    long timeMS = stopWatch.ElapsedMilliseconds;
    return timeMS;
}



long updateLifts1()
{
    Stopwatch stopWatch = new Stopwatch();

    //
    stopWatch.Start();
    List<Lift> lifts = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult(); 
    foreach (Lift lift in lifts)
    {
        var result = box.Call_1_6<ValueTuple<uint, uint, uint>, Int32[]>("update_queue_time", (ValueTuple.Create(lift.LiftID, (uint)dateFrom.ToUnixTimeSeconds(), (uint)dateTo.ToUnixTimeSeconds()))).GetAwaiter().GetResult();
    }
    stopWatch.Stop();
    //

    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

    Console.WriteLine($"Updating1: " + elapsedTime);
    long timeMS = stopWatch.ElapsedMilliseconds;
    return timeMS;
}

long updateLifts2()
{
    Stopwatch stopWatch = new Stopwatch();


    //
    stopWatch.Start();
    List<Lift> lifts = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult();
    foreach (Lift lift in lifts)
    {
        var result = box.Call_1_6<ValueTuple<uint, uint, uint>, Int32[]>("count_card_readings", (ValueTuple.Create(lift.LiftID, (uint)dateFrom.ToUnixTimeSeconds(), (uint)dateTo.ToUnixTimeSeconds()))).GetAwaiter().GetResult();
        uint cardReadingsAmout = (uint)result.Data[0][0];

        uint plusQueueTime = cardReadingsAmout * (2 * lift.LiftingTime / lift.SeatsAmount);

        uint newQueueTime = Math.Max(lift.QueueTime - timeDelta + plusQueueTime, 0);

        Lift updatedLift = new(lift, newQueueTime);
        _liftsRepository.UpdateLiftByIDAsync(updatedLift.LiftID, updatedLift.LiftName, updatedLift.IsOpen, updatedLift.SeatsAmount, updatedLift.LiftingTime).GetAwaiter().GetResult();
    }
    stopWatch.Stop();
    //

    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

    Console.WriteLine($"Updating2: " + elapsedTime);
    long timeMS = stopWatch.ElapsedMilliseconds;
    return timeMS;
}

long updateLifts3()
{
    Stopwatch stopWatch = new Stopwatch();

    //
    stopWatch.Start();
    List<Lift> lifts = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult();
    List<CardReading> allCardReadings = _cardReadingsRepository.GetCardReadingsAsync().GetAwaiter().GetResult();
    List<CardReading> cardReadings = new List<CardReading>();
    foreach (CardReading cardReading in allCardReadings)
    {
        if ((cardReading.ReadingTime >= dateFrom) &&
            (cardReading.ReadingTime < dateTo))
            cardReadings.Add(cardReading);
    }

    foreach (Lift lift in lifts)
    {
        List<Turnstile> turnstiles = _turnstilesRepository.GetTurnstilesByLiftIdAsync(lift.LiftID)
            .GetAwaiter().GetResult();
        List<uint> connectedTurnstileIDs = new List<uint>();
        foreach (Turnstile turnstile in turnstiles)
        {
            connectedTurnstileIDs.Add(turnstile.TurnstileID);
        }

        uint cardReadingsAmout = 0;
        foreach (CardReading cardReading in cardReadings)
        {
            if (connectedTurnstileIDs.Contains(cardReading.TurnstileID))
                cardReadingsAmout++;
        }

        uint plusQueueTime = cardReadingsAmout * (2 * lift.LiftingTime / lift.SeatsAmount);

        uint newQueueTime = Math.Max(lift.QueueTime - timeDelta + plusQueueTime, 0);

        Lift updatedLift = new(lift, newQueueTime);
        _liftsRepository.UpdateLiftByIDAsync(updatedLift.LiftID, updatedLift.LiftName, updatedLift.IsOpen, updatedLift.SeatsAmount, updatedLift.LiftingTime).GetAwaiter().GetResult();
    }
    stopWatch.Stop();

    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

    Console.WriteLine($"Updating3: " + elapsedTime);
    long timeMS = stopWatch.ElapsedMilliseconds;
    return timeMS;
}


long updateLifts4()
{
    Stopwatch stopWatch = new Stopwatch();

    //
    stopWatch.Start();
    List<Lift> lifts = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult();
    List<Turnstile> turnstiles = _turnstilesRepository.GetTurnstilesAsync().GetAwaiter().GetResult();
    List<CardReading> allCardReadings = _cardReadingsRepository.GetCardReadingsAsync().GetAwaiter().GetResult();
    List<CardReading> cardReadings = new List<CardReading>();
    foreach (CardReading cardReading in allCardReadings)
    {
        if ((cardReading.ReadingTime >= dateFrom) &&
            (cardReading.ReadingTime < dateTo))
            cardReadings.Add(cardReading);
    }

    foreach (Lift lift in lifts)
    {
        List<uint> connectedTurnstileIDs = new List<uint>();
        foreach (Turnstile turnstile in turnstiles)
        {
            if (turnstile.LiftID == lift.LiftID)
                connectedTurnstileIDs.Add(turnstile.TurnstileID);
        }

        
        uint cardReadingsAmout = 0;
        foreach (CardReading cardReading in cardReadings)
        {
            if (connectedTurnstileIDs.Contains(cardReading.TurnstileID))
                cardReadingsAmout++;
        }

        uint plusQueueTime = cardReadingsAmout * (2 * lift.LiftingTime / lift.SeatsAmount);
        uint newQueueTime = Math.Max(lift.QueueTime - timeDelta + plusQueueTime, 0);

        Lift updatedLift = new(lift, newQueueTime);
        _liftsRepository.UpdateLiftByIDAsync(updatedLift.LiftID, updatedLift.LiftName, updatedLift.IsOpen, updatedLift.SeatsAmount, updatedLift.LiftingTime).GetAwaiter().GetResult();
    }
    stopWatch.Stop();

    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

    Console.WriteLine($"Updating4: " + elapsedTime);
    long timeMS = stopWatch.ElapsedMilliseconds;
    return timeMS;
}


List<int> ns = new List<int>() {0, 50, 100, 200, 300, 400};
//List<int> ns = new List<int>() {0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100};


using (StreamWriter Nwriter = new StreamWriter(NFilename, false))
{
    foreach (int n in ns)
        Nwriter.WriteLine($"{n}");
}

int n_repeats = ns.Count + 1;
using (StreamWriter updateWriter = new StreamWriter(UpdateResultsFilename, false))
{
    using (StreamWriter addWriter = new StreamWriter(AddResultsFilename, false))
    {
        for (int i = 1; i < ns.Count; i++)
        {
            Console.WriteLine($"\n\n[{i}] {ns[i]}");

            int n_lifts_prev = ns[i - 1];
            int n_lifts_cur = ns[i];
            List<Lift> lifts = GetLiftsFromJsonFiles(n_lifts_prev, n_lifts_cur);

            int n_turnstiles_prev = n_lifts_prev * n_turnstiles_per_lift;
            int n_turnstiles_cur = n_lifts_cur * n_turnstiles_per_lift;
            List<Turnstile> turnstiles = GetTurnstilesFromJsonFiles(n_turnstiles_prev, n_turnstiles_cur);

            int n_card_readings_prev = n_turnstiles_prev * n_card_readings_per_turnstile;
            int n_card_readings_cur = n_turnstiles_cur * n_card_readings_per_turnstile;
            List<CardReading> cardReadings = GetCardReadingsFromJsonFiles(n_card_readings_prev, n_card_readings_cur);


            long addingTime = addLifts(lifts);
            addingTime += addTurnstiles(turnstiles);
            addingTime += addCardReadings(cardReadings);

            long updatingTime1mean = 0;
            long updatingTime2mean = 0;
            long updatingTime3mean = 0;
            long updatingTime4mean = 0;

            int cur_n_repeats = n_repeats - i;
            Console.WriteLine($"cur_n_repeats {cur_n_repeats}");

            for (int j = 0; j < cur_n_repeats; j++)
            {
                long updatingTime1 = updateLifts1();
                long updatingTime2 = updateLifts2();
                long updatingTime3 = updateLifts3();
                long updatingTime4 = updateLifts4();

                updatingTime1mean += updatingTime1;
                updatingTime2mean += updatingTime2;
                updatingTime3mean += updatingTime3;
                updatingTime4mean += updatingTime4;
            }

            updatingTime1mean /= cur_n_repeats;
            updatingTime2mean /= cur_n_repeats;
            updatingTime3mean /= cur_n_repeats;
            updatingTime4mean /= cur_n_repeats;

            string upd_str = $"{updatingTime1mean} {updatingTime2mean} {updatingTime3mean} {updatingTime4mean}";
            string add_str = $"{addingTime}";

            Console.WriteLine($"\n\n{upd_str}\n\n");
            Console.WriteLine($"\n\n{add_str}\n\n");

            updateWriter.WriteLineAsync(upd_str);
            addWriter.WriteLineAsync(add_str);
        }
    }
}


//using (StreamWriter updateWriter = new StreamWriter(UpdateResultsFilename, false))
//{
//    using (StreamWriter addWriter = new StreamWriter(AddResultsFilename, false))
//    {
//        for (int i = 1; i < ns.Count; i++)
//        {

//            Console.WriteLine($"\n\n[{i}] {ns[i]}");
//            List<CardReading> cardReadings = GetCardReadingsFromJsonFiles(ns[i - 1], ns[i]);
//            long addingTime = addCardReadings(cardReadings);

//            long updatingTime1mean = 0;
//            long updatingTime2mean = 0;
//            long updatingTime3mean = 0;
//            long updatingTime4mean = 0;

//            int cur_n_repeats = n_repeats - i;
//            Console.WriteLine($"cur_n_repeats {cur_n_repeats}");

//            for (int j = 0; j < cur_n_repeats; j++)
//            {
//                long updatingTime1 = updateLifts1();
//                long updatingTime2 = updateLifts2();
//                long updatingTime3 = updateLifts3();
//                long updatingTime4 = updateLifts4();

//                updatingTime1mean += updatingTime1;
//                updatingTime2mean += updatingTime2;
//                updatingTime3mean += updatingTime3;
//                updatingTime4mean += updatingTime4;
//            }

//            updatingTime1mean /= cur_n_repeats;
//            updatingTime2mean /= cur_n_repeats;
//            updatingTime3mean /= cur_n_repeats;
//            updatingTime4mean /= cur_n_repeats;

//            string upd_str = $"{updatingTime1mean} {updatingTime2mean} {updatingTime3mean} {updatingTime4mean}";
//            string add_str = $"{addingTime}";

//            Console.WriteLine($"\n\n{upd_str}\n\n");
//            Console.WriteLine($"\n\n{add_str}\n\n");

//            updateWriter.WriteLineAsync(upd_str);
//            addWriter.WriteLineAsync(add_str);
//        }
//    }
//}






//public class TheEasiestBenchmark
//{
//    [Benchmark(Description = "Summ100")]
//    public int Test100()
//    {
//        return Enumerable.Range(1, 100).Sum();
//    }

//    [Benchmark(Description = "Summ200")]
//    public int Test200()
//    {
//        return Enumerable.Range(1, 200).Sum();
//    }
//}

//[TestClass]
//public class UnitTest1
//{
//    [TestMethod]
//    public void TestMethod1()
//    {
//        BenchmarkRunner.Run<TheEasiestBenchmark>();
//    }
//}