using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Organization.Entity.Constants;
using Organization.Worker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Worker.Worker
{
    public class SQSMsgSubscriberBackgroudService : IHostedService
    {
        private readonly ILogger<SQSMsgSubscriberBackgroudService> logger;
        private readonly SQSQueueMessageListenerService _sQSQueueMessageListener;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public SQSMsgSubscriberBackgroudService(ILogger<SQSMsgSubscriberBackgroudService> logger, SQSQueueMessageListenerService sQSQueueMessageListener)
        {
            this.logger = logger;
            _sQSQueueMessageListener = sQSQueueMessageListener;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Backgroud service started");
            cancellationToken = _cancellationTokenSource.Token;
            foreach (var queueName in EmployeeSQSQueueName.AllSQSQueueNames)
            {
                _ = Task.Factory.StartNew(async () => await _sQSQueueMessageListener.StartListenAsync(queueName, cancellationToken), TaskCreationOptions.LongRunning);
                // _ = Task.Factory.StartNew(async () => await queueListener.SendMessageAsync(cancellationToken), TaskCreationOptions.LongRunning);
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
