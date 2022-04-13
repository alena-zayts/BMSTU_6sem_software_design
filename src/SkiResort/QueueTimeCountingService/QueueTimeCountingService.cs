using BL.IRepositories;
using BL.Models;

namespace QueueTimeCounting
{
    public class QueueTimeCountingService : BackgroundService
    {
        private readonly ILogger<QueueTimeCountingService> _logger;
        private readonly uint _timeDelta;
        private readonly ILiftsRepository _liftsRepository;
        private readonly ICardReadingsRepository _cardReadingsRepository;

        public QueueTimeCountingService(ILogger<QueueTimeCountingService> logger, ILiftsRepository liftsRepository, ICardReadingsRepository cardReadingsRepository, uint timeDelta)
        {
            _logger = logger;
            _timeDelta = timeDelta;
            _liftsRepository = liftsRepository;
            _cardReadingsRepository = cardReadingsRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTimeOffset dto = new();
                uint currentTime = (uint) dto.ToUnixTimeSeconds();

                List<Lift> lifts = await _liftsRepository.GetLiftsAsync();
                foreach (Lift lift in lifts)
                {
                    uint cardReadingsAmout = await _cardReadingsRepository.CountForLiftIdFromDateAsync(lift.LiftID, currentTime - _timeDelta);
                    uint previousQueueTime = lift.QueueTime;
                    uint minusQueueTime = _timeDelta;
                    uint plusQueueTime = cardReadingsAmout * (2 * lift.LiftingTime / lift.SeatsAmount);

                    uint newQueueTime = previousQueueTime - minusQueueTime + plusQueueTime;

                    Lift updatedLift = new(lift, newQueueTime);
                    await _liftsRepository.UpdateLiftAsync(updatedLift);
                }
                
                _logger.LogInformation("QueueTimeCountingService running at: {time}", DateTimeOffset.Now);
                await Task.Delay((int) _timeDelta, stoppingToken);
            }
        }
    }
}