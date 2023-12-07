using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.Employeee.Query
{
    public interface IEmployeeQueryManger
    {
        public Task<EmployeeReadModel> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken);
        public Task<IEnumerable<EmployeeReadModel>> GetAllEmployeeAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<EmployeeReadModel>> FindEmployeeAsync(EmployeeCreateModel employeeFindModel, CancellationToken cancellationToken);
    }
}
