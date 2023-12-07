using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.SQS.Command
{
    public interface ISQSCommandManager
    {
        Task SendMessageAsync(string QueueName, EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken);
        public Task Initialize();
    }
}
