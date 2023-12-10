using Organization.Business.Employeee.Models; 
namespace Organization.Business.Employeee.Command
{
    public interface IEmployeeCommandManger
    {
        public Task<EmployeeReadModel> CreateEmployeeAsync(EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken);
        public Task UpdateEmployeeAsync(EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken);
        public Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken);
    }
}
