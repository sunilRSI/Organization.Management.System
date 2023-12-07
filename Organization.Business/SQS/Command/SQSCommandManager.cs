using Organization.Entity.Models;
using Organization.Repository.Repository.SQS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.SQS.Command
{
    public class SQSCommandManager : ISQSCommandManager
    {
        private readonly ISQSCommandRepository _sQSCommandRepository;

        public SQSCommandManager(ISQSCommandRepository sQSCommandRepository)
        {
            _sQSCommandRepository = sQSCommandRepository;

        }
        public async Task Initialize()
        {
            await _sQSCommandRepository.Initialize();
        }

        public async Task SendMessageAsync(string QueueName, EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken)
        {
            await _sQSCommandRepository.SendMessageAsync(QueueName, employeeCreateModel, cancellationToken);
        }
    }
}
