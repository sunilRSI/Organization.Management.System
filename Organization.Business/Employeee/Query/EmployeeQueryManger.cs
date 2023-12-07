using Organization.Entity.Models;
using Organization.Repository.Repository.Employee.Query;

namespace Organization.Business.Employeee.Query
{
    public class EmployeeQueryManger : IEmployeeQueryManger
    {
        private readonly IEmployeeQueryRepository _employeeQueryRepository;

        public EmployeeQueryManger(IEmployeeQueryRepository employeeQueryRepository)
        {
            _employeeQueryRepository = employeeQueryRepository;
        }
        public async Task<IEnumerable<EmployeeReadModel>> FindEmployeeAsync(EmployeeCreateModel employeeFindModel, CancellationToken cancellationToken)
        {
            return await _employeeQueryRepository.FindEmployeeAsync(employeeFindModel, cancellationToken);
        }

        public async Task<IEnumerable<EmployeeReadModel>> GetAllEmployeeAsync(CancellationToken cancellationToken)
        {
            return await _employeeQueryRepository.GetAllEmployeeAsync(cancellationToken);
        }

        public async Task<EmployeeReadModel> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _employeeQueryRepository.GetEmployeeByIdAsync(Id, cancellationToken);
        }
    }
}
