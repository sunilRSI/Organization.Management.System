using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime.Internal;
using Organization.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Repository.Repository.Employee.Command
{
    public class EmployeeCommandRepository : IEmployeeCommandRepository
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public EmployeeCommandRepository(IDynamoDBContext context)
        {
            _dynamoDBContext = context;
        }
        public async Task<Entity.Models.Employee> CreateEmployeeAsync(Entity.Models.Employee employee, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(employee, cancellationToken);
            return employee;
        }

        public async Task DeleteEmployeeAsync(Guid Id, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.DeleteAsync<Entity.Models.Employee>(Id, cancellationToken);
        }
        public async Task UpdateEmployeeAsync(Entity.Models.Employee employeeUpdateModel, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(employeeUpdateModel, cancellationToken);
        }
    }
}
