using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class DeactivateWorker : BackgroundService
    {
        private readonly ILogger<DeactivateWorker> _logger;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly IServiceProvider _serviceProvider;

        private string Schedule => "*/5 * * * * *"; //  -- Runs every 5 Second: 

        public DeactivateWorker(ILogger<DeactivateWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);

        }
        protected override async Task ExecuteAsync(CancellationToken cancelationToken)
        {
            do
            {
                var now = DateTime.Now;
                _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<ApiMovie>();
                        await service.Deactivate();
                        //_logger.LogInformation("123");
                    }
                    _logger.LogInformation("Movie was deactivated using of Worker");
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
            }
            while (!cancelationToken.IsCancellationRequested);
        }
    }
}
