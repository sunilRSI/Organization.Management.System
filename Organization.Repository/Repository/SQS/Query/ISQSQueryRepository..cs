using Amazon.SQS.Model;
using Organization.Entity.Models;

namespace Organization.Repository.Repository.SQS.Query
{
    public interface ISQSQueryRepository
    {
        Task<Message> ReceiveMessageAsync(string QueueName, TimeSpan? visibilityTimeout, CancellationToken cancellationToken);
    }
}
