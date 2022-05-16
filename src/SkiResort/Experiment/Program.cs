using BL.IRepositories;
using BL.Models;
using Newtonsoft.Json.Linq;

using Ninject;
using BL;
using AccessToDB;
using System;
using System.Diagnostics;
using System.Threading;
using System;
using System.IO;
using System.Text;

IKernel ninjectKernel = new StandardKernel();
ninjectKernel.Bind<IRepositoriesFactory>().To<TarantoolRepositoriesFactory>();
IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();

ICardReadingsRepository _cardReadingsRepository = repositoriesFactory.CreateCardReadingsRepository();
ILiftsRepository _liftsRepository = repositoriesFactory.CreateLiftsRepository();

string _path = "C:/BMSTU_6sem_software_design/src/tarantool/app/json_data/card_readings/";
DateTimeOffset dateFrom = DateTimeOffset.FromUnixTimeSeconds(((uint) 1652659200 + (uint) 1652745600) / 2);
DateTimeOffset dateTo = DateTimeOffset.FromUnixTimeSeconds((uint) 1652745600);

string adding_filename = "adding.txt";
string updating_filename = "updating.txt";

CardReading LoadCardReadingFromJson(string filename)
{
    string data = File.ReadAllText(filename);
    dynamic stuff = JObject.Parse(data);
    CardReading cardReading = new((uint)stuff.RecordID, (uint)stuff.TurnstileID, (uint)stuff.CardID, (DateTimeOffset)(DateTimeOffset.FromUnixTimeSeconds((uint)stuff.ReadingTime)));
    return cardReading;
}

void downloadCardReadings(int from, int to)
{
    var prevCardReadings = _cardReadingsRepository.GetCardReadingsAsync().GetAwaiter().GetResult();
    var prevAmount = prevCardReadings.Count();
    Console.WriteLine($"prev amount {prevAmount}");

    string[] filenames = Directory.GetFiles(_path);
    List<CardReading> cardReadingList = new List<CardReading>();
    for (int i = from; i < to; i++)
    {
        string filename = filenames[i];
        try
        {
            CardReading cardReading = LoadCardReadingFromJson(filename);
            cardReadingList.Add(cardReading);
            //FileInfo fileInfo = new(filename);
            //fileInfo.Delete();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();
    for (int i = 0; i < to  - from; i++)
    {
        try
        {
            CardReading cardReading = cardReadingList[i];
            uint cardReadingID = _cardReadingsRepository.AddCardReadingAutoIncrementAsync(cardReading.TurnstileID, cardReading.CardID, cardReading.ReadingTime).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds / 10);

    Console.WriteLine($"Adding: " + elapsedTime);
    using (StreamWriter writer = new StreamWriter(adding_filename, true))
    {
        writer.WriteLineAsync(elapsedTime);
    }
}


Stopwatch stopWatch = new Stopwatch();

List<int> ns = new List<int>() {0, 2000, 4000, 6000, 8000, 10000};
for (int i = 1; i < ns.Count; i++)
{
    downloadCardReadings(ns[i-1], ns[i]);
    List<Lift> lifts = _liftsRepository.GetLiftsAsync().GetAwaiter().GetResult();

    stopWatch.Start();
    foreach (Lift lift in lifts)
    {
        _cardReadingsRepository.UpdateQueueTime(lift.LiftID, dateFrom, dateTo);
    }
    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds / 10);
    Console.WriteLine($"{ns[i]}   RunTime " + elapsedTime);
    using (StreamWriter writer = new StreamWriter(adding_filename, true))
    {
        writer.WriteLineAsync(elapsedTime);
    }
}
