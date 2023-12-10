 

namespace Organization.Repository.Repository.Employee.Command
{
    public interface IEmployeeCommandRepository
    {
        public Task<Entity.Models.Employee> CreateEmployeeAsync(Entity.Models.Employee employee, CancellationToken cancellationToken);
        public Task UpdateEmployeeAsync(Entity.Models.Employee employee, CancellationToken cancellationToken);
        public Task DeleteEmployeeAsync(Guid Id, CancellationToken cancellationToken);
    }
}
