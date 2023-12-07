using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.Employeee.Command
{
    public interface IEmployeeCommandManger
    {
        public Task<EmployeeReadModel> CreateEmployeeAsync(EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken);
        public Task UpdateEmployeeAsync(EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken);
        public Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken);
    }
}
