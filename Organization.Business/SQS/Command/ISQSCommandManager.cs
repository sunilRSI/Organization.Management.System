using Organization.Business.Employeee.Models; 

namespace Organization.Business.SQS.Command
{
    public interface ISQSCommandManager
    {
        Task SendMessageAsync(string QueueName, EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken);
        public Task Initialize();
    }
}
