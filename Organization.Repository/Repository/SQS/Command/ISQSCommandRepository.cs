﻿
using Amazon.SQS.Model;
using Organization.Entity.Models;

namespace Organization.Repository.Repository.SQS.Command
{
    public interface ISQSCommandRepository
    {
        Task SendMessageAsync(string QueueName, Entity.Models.Employee employee, CancellationToken cancellationToken);
        Task DeleteMessageAsync(string QueueName, Message message, CancellationToken cancellationToken);
        Task Initialize();
    }
}
