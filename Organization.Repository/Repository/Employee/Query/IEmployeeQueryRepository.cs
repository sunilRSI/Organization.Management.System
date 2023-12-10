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
        public Task<Entity.Models.Employee> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken);
        public Task<IEnumerable<Entity.Models.Employee>> GetAllEmployeeAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<Entity.Models.Employee>> FindEmployeeAsync(Entity.Models.Employee employeeFindModel, CancellationToken cancellationToken);
    }
}
