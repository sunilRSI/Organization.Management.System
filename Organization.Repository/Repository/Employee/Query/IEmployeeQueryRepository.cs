using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Repository.Repository.Employee.Query
{
    public interface IEmployeeQueryRepository
    {
        public Task<EmployeeReadModel> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken);
        public Task<IEnumerable<EmployeeReadModel>> GetAllEmployeeAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<EmployeeReadModel>> FindEmployeeAsync(EmployeeCreateModel employeeFindModel, CancellationToken cancellationToken);
    }
}
