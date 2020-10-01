using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DowntimeRobot.JobScheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        private const string _apiKey = "7967FB42-1E41-46BA-BD7C-3CEFDAC1FD7F";
        private const string _secretKey = "9C17381D-71C5-4785-8B3C-0A2C20DF114C";
        //private const string _serviceUrl = "http://localhost:3317";
        private const string _serviceUrl = "http://downtimerobotapi.gear.host";

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The Worker service running...");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The Worker service has been stopped...");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var executer = new ExecuterService(_apiKey, _secretKey, _serviceUrl);
                try
                {

                    var result = await executer.ExecuteJobs();
                    if (result)
                        _logger.LogInformation("Working perfectly");
                    else
                        _logger.LogError("Not Working");

                }
                catch (Exception ex)
                {
                    _logger.LogError($"-------------------------{Environment.NewLine}Exception: {ex.StackTrace}{Environment.NewLine}-------------------------");
                }
                finally
                {
                    if (executer != null)
                        ((IDisposable)executer).Dispose();
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
