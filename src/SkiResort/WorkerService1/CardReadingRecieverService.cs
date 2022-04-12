using BL.IRepositories;
using BL.Models;
using AccessToDB.RepositoriesTarantool;
using AccessToDB;
using System.Text.Json;


namespace CardReadingReciever
{
    public class CardReadingRecieverService : BackgroundService
    {
        private readonly ILogger<CardReadingRecieverService> _logger;

        public CardReadingRecieverService(ILogger<CardReadingRecieverService> logger)
        {
            _logger = logger;
        }

        public CardReading? LoadCardReadingFromJson(string filename)
        {
            // чтение данных
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                CardReading? cardReading = JsonSerializer.Deserialize<CardReading>(fs);
                return cardReading;
            }

            //string data = File.ReadAllText(filename);
            //CardReading cardReadings = JsonSerializer.Deserialize<CardReading>(data);
            //return cardReadings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            string path = "C:/BMSTU_6sem_software_design/src/tarantool/app/json_data/card_readings/";
            TarantoolContext _context = new TarantoolContext(connection_string);
            ICardReadingsRepository cardReadingsRepository = new TarantoolCardReadingsRepository(_context);

            while (!stoppingToken.IsCancellationRequested)
            {
                string[] filenames = Directory.GetFiles(path);
                foreach (string filename in filenames)
                {
                    CardReading cardReading = LoadCardReadingFromJson(filename);
                    FileInfo fileInfo = new FileInfo(filename);
                    fileInfo.Delete();
                    cardReading = cardReadingsRepository.AddCardReadingAutoIncrementAsync(cardReading).GetAwaiter().GetResult();
                    _logger.LogInformation($"{cardReading.RecordID}, {cardReading.ReadingTime}");
                }
            }
        }
    }
}