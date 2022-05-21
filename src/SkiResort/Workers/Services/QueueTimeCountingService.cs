using BL.IRepositories;
using BL.Models;
using BL;
using AccessToDB;

namespace Workers
{
    public class QueueTimeCountingService : BackgroundService
    {
        private readonly ILogger<QueueTimeCountingService> _logger;
        private readonly uint _sleepTime;
        private readonly ILiftsRepository _liftsRepository;
        private readonly ICardReadingsRepository _cardReadingsRepository;

        public QueueTimeCountingService(ILogger<QueueTimeCountingService> logger)
        {
            IRepositoriesFactory repositoriesFactory = new TarantoolRepositoriesFactory();
            _liftsRepository = repositoriesFactory.CreateLiftsRepository();
            _cardReadingsRepository = repositoriesFactory.CreateCardReadingsRepository();
            _logger = logger;
            _sleepTime = 5000; //ms
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTimeOffset prevTime = DateTimeOffset.Now;
            DateTimeOffset currentTime = DateTimeOffset.Now;
            while (!stoppingToken.IsCancellationRequested)
            {
                prevTime = currentTime;
                currentTime = DateTimeOffset.Now;
                List<Lift> lifts = await _liftsRepository.GetLiftsAsync();
                
                foreach (Lift lift in lifts)
                {
                    uint newTime = await _cardReadingsRepository.UpdateQueueTime(lift.LiftID, prevTime, currentTime);
                }
                
                _logger.LogInformation($"QueueTimeCountingService running at: {DateTimeOffset.Now} with delta {(currentTime - prevTime).TotalSeconds}");
                await Task.Delay((int) _sleepTime, stoppingToken);
            }
        }
    }
}