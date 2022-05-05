using BL.IRepositories;
using BL.Models;


using Ninject;
using BL;
using AccessToDB;

namespace Workers
{
    public class QueueTimeCountingService : BackgroundService
    {
        private readonly ILogger<QueueTimeCountingService> _logger;
        private readonly uint _timeDelta;
        private readonly ILiftsRepository _liftsRepository;
        private readonly ICardReadingsRepository _cardReadingsRepository;

        public QueueTimeCountingService(ILogger<QueueTimeCountingService> logger)//, ILiftsRepository liftsRepository, ICardReadingsRepository cardReadingsRepository, uint timeDelta)
        {
            _logger = logger;

            

            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<TarantoolRepositoriesFactory>();
            IRepositoriesFactory repositoriesFactory = ninjectKernel.Get<IRepositoriesFactory>();

            _timeDelta = 1000;
            _liftsRepository = repositoriesFactory.CreateLiftsRepository();
            _cardReadingsRepository = repositoriesFactory.CreateCardReadingsRepository();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //DateTimeOffset dto = new();
                //uint currentTime = (uint) dto.ToUnixTimeSeconds();
                DateTimeOffset currentTime = DateTimeOffset.Now;

                List<Lift> lifts = await _liftsRepository.GetLiftsAsync();
                foreach (Lift lift in lifts)
                {
                    uint cardReadingsAmout = await _cardReadingsRepository.CountForLiftIdFromDateAsync(lift.LiftID, DateTimeOffset.FromUnixTimeSeconds(currentTime.ToUnixTimeSeconds() - (long) _timeDelta).DateTime);
                    uint previousQueueTime = lift.QueueTime;
                    uint minusQueueTime = _timeDelta;
                    uint plusQueueTime = cardReadingsAmout * (2 * lift.LiftingTime / lift.SeatsAmount);

                    uint newQueueTime = previousQueueTime - minusQueueTime + plusQueueTime;

                    Lift updatedLift = new(lift, newQueueTime);
                    await _liftsRepository.UpdateLiftByIDAsync(updatedLift.LiftID, updatedLift.LiftName, updatedLift.IsOpen, updatedLift.SeatsAmount, updatedLift.LiftingTime);
                }
                
                _logger.LogInformation("QueueTimeCountingService running at: {time}", DateTimeOffset.Now);
                await Task.Delay((int) _timeDelta, stoppingToken);
            }
        }
    }
}