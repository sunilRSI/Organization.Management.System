using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Organization.Worker.Service
{
    public class SQSQueueMessageListenerService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger<SQSQueueMessageListenerService> _logger;
        private readonly SQSQueueMessageProcessorService _sQSQueueMessageProcessor;
        string _queueName = string.Empty;

        public SQSQueueMessageListenerService(IAmazonSQS sqsClient, ILogger<SQSQueueMessageListenerService> logger, SQSQueueMessageProcessorService sQSQueueMessageProcessor)
        {
            _sqsClient = sqsClient;
            _logger = logger;
            _sQSQueueMessageProcessor = sQSQueueMessageProcessor;
        }
        public async ValueTask StartListenAsync(string QueueName, CancellationToken cancellationToken = default)
        {
            string queueUrl = await GetQueueUrlAsync(QueueName, cancellationToken);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest()
                    {
                        QueueUrl = queueUrl,
                        MaxNumberOfMessages = 10,
                    };
                    _queueName = QueueName;
                    var readRespone = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);
                    if (readRespone.Messages != null && readRespone.Messages.Count > 0)
                    {
                        await Parallel.ForEachAsync(readRespone.Messages, SendMessageForProcessing);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is AmazonSQSException amazonex &&
                        string.Equals(amazonex.ErrorCode, "AWS.SimpleQueueService.NonExistentQueue", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _logger.LogError($"Failed to read message from queue either Invalid queue url or queue doesn't exist. Error: {ex.Message}", ex);

                    }
                    _logger.LogError($"Error while processing message from {QueueName}: {ex.Message}", ex);
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
        }

        public async Task CreateQueue(string QueueName, CancellationToken cancellationToken = default)
        {
            string queueUrl = await GetQueueUrlAsync(QueueName, cancellationToken);
            if (string.IsNullOrWhiteSpace(queueUrl))
            {
                CreateQueueRequest createQueueRequest = new CreateQueueRequest
                {
                    QueueName = QueueName,
                };
                CreateQueueResponse createQueueResponse = await this._sqsClient.CreateQueueAsync(createQueueRequest, cancellationToken);
            }
        }
        public async Task SendMessageAsync(string QueueName, CancellationToken cancellationToken = default)
        {
            string queueUrl = await GetQueueUrlAsync(QueueName, cancellationToken);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    SendMessageRequest sendMessageRequest = new SendMessageRequest
                    {
                        QueueUrl = queueUrl,
                        MessageBody = JsonSerializer.Serialize($"hiiiiiii at {DateTime.UtcNow}"),
                    };
                    SendMessageResponse messageResponse = await _sqsClient.SendMessageAsync(sendMessageRequest);
                }
                catch (Exception)
                {

                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
        }
        private async ValueTask SendMessageForProcessing(Message message, CancellationToken cancellationToken = default)
        {
            string queueUrl = await GetQueueUrlAsync(_queueName, cancellationToken);
            bool MesaageProcessed = await _sQSQueueMessageProcessor.ProcessMessageAsync(message, cancellationToken);
            if (MesaageProcessed)
            {
                var deleteRequest = new DeleteMessageRequest
                {
                    QueueUrl = queueUrl,
                    ReceiptHandle = message.ReceiptHandle
                };

                await _sqsClient.DeleteMessageAsync(deleteRequest, cancellationToken);
            }
        }
        private async Task<string> GetQueueUrlAsync(string QueueName, CancellationToken cancellationToken)
        {
            string queueUrl = string.Empty;
            try
            {
                GetQueueUrlResponse getQueueUrlResponse = await this._sqsClient.GetQueueUrlAsync(QueueName, cancellationToken);
                queueUrl = getQueueUrlResponse.QueueUrl;
            }
            catch (QueueDoesNotExistException)
            {
                return queueUrl;
            }
            return queueUrl;
        }
    }
}
