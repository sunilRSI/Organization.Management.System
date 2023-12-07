 using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Organization.Entity.Constants;
using Organization.Entity.Models;
using System.Text.Json;

namespace Organization.Repository.Repository.SQS.Command
{
    public class SQSCommandRepository : ISQSCommandRepository
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger<SQSCommandRepository> _logger;
        public SQSCommandRepository(IAmazonSQS sqsClient, ILogger<SQSCommandRepository> logger)
        {
            _sqsClient = sqsClient;
            _logger = logger;
        }
        public async Task Initialize()
        {
            foreach (var QueueName in EmployeeSQSQueueName.AllSQSQueueNames)
            {
                bool isQueueExistAsync = await IsQueueExistAsync(QueueName);
                if (!isQueueExistAsync)
                {
                    CreateQueueRequest createQueueRequest = new CreateQueueRequest
                    {
                        QueueName = QueueName,

                    };
                    CreateQueueResponse createQueueResponse = await _sqsClient.CreateQueueAsync(createQueueRequest);
                }
            }
        }
        public async Task DeleteMessageAsync(string QueueName, Message message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageAsync(string QueueName, EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken)
        {

            GetQueueUrlResponse getQueueUrlResponse = await _sqsClient.GetQueueUrlAsync(QueueName, cancellationToken);
            SendMessageRequest sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = getQueueUrlResponse.QueueUrl,
                MessageBody = JsonSerializer.Serialize(employeeCreateModel),
            };
            SendMessageResponse messageResponse = await _sqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);
        }
        private async Task<bool> IsQueueExistAsync(string QueueName, CancellationToken cancellationToken = default)
        {
            try
            {
                GetQueueUrlResponse getQueueUrlResponse = await _sqsClient.GetQueueUrlAsync(QueueName, cancellationToken);
            }
            catch (QueueDoesNotExistException)
            {
                _logger.LogInformation($"QueueName {QueueName} not exist");
                return false;
            }
            return true;
        }
    }
}
