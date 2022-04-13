using BL.IRepositories;
using BL.Models;
using Newtonsoft.Json.Linq;


namespace CardReadingRecieving
{
    public class CardReadingRecieverService : BackgroundService
    {
        private readonly ILogger<CardReadingRecieverService> _logger;
        private readonly ICardReadingsRepository _cardReadingsRepository;
        private readonly string _path;

        public CardReadingRecieverService(ILogger<CardReadingRecieverService> logger, ICardReadingsRepository cardReadingsRepository, string path)
        {
            _logger = logger;
            _cardReadingsRepository = cardReadingsRepository;
            _path = path;
            //string path = "C:/BMSTU_6sem_software_design/src/tarantool/app/json_data/card_readings/";
        }

        public static CardReading LoadCardReadingFromJson(string filename)
        {
            string data = File.ReadAllText(filename);
            dynamic stuff = JObject.Parse(data);
            CardReading cardReading = new((uint) stuff.RecordID, (uint) stuff.TurnstileID, (uint) stuff.CardID, (uint) stuff.ReadingTime);
            return cardReading;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string[] filenames = Directory.GetFiles(_path);
                foreach (string filename in filenames)
                {
                    try
                    {
                        CardReading cardReading = LoadCardReadingFromJson(filename);
                        cardReading = _cardReadingsRepository.AddCardReadingAutoIncrementAsync(cardReading).GetAwaiter().GetResult();

                        FileInfo fileInfo = new(filename);
                        fileInfo.Delete();
                        
                        string message = $"{cardReading.RecordID}, {cardReading.ReadingTime}";
                        _logger.LogInformation(message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                    }
                }
            }
        }
    }
}