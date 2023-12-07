using Amazon.SQS.Model;
using Organization.Entity.Models;

namespace Organization.Repository.Repository.SQS.Query
{
    public class SQSQueryRepository : ISQSQueryRepository
    {
        public Task<Message> ReceiveMessageAsync(string QueueName, TimeSpan? visibilityTimeout, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
