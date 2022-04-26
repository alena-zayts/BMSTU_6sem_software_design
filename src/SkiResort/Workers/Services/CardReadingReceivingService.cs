using BL.IRepositories;
using BL.Models;
using Newtonsoft.Json.Linq;


namespace Workers
{
    public class CardReadingReceivingService : BackgroundService
    {
        private readonly ILogger<CardReadingReceivingService> _logger;
        private readonly ICardReadingsRepository _cardReadingsRepository;
        private readonly string _path;

        public CardReadingReceivingService(ILogger<CardReadingReceivingService> logger, ICardReadingsRepository cardReadingsRepository, string path)
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
            CardReading cardReading = new((uint) stuff.RecordID, (uint) stuff.TurnstileID, (uint) stuff.CardID, (DateTimeOffset) (DateTimeOffset.FromUnixTimeSeconds(stuff.ReadingTime)));
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
                        uint cardReadingID = _cardReadingsRepository.AddCardReadingAutoIncrementAsync(cardReading.TurnstileID,cardReading.CardID, cardReading.ReadingTime).GetAwaiter().GetResult();

                        FileInfo fileInfo = new(filename);
                        fileInfo.Delete();
                        
                        string message = $"{cardReading.RecordID}";
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