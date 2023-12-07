using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Repository.Repository.Employee.Command
{
    public interface IEmployeeCommandRepository
    {
        public Task<EmployeeReadModel> CreateEmployeeAsync(EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken);
        public Task UpdateEmployeeAsync(EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken);
        public Task DeleteEmployeeAsync(Guid Id, CancellationToken cancellationToken);
    }
}
