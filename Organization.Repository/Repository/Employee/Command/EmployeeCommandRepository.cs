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
        public async Task<EmployeeReadModel> CreateEmployeeAsync(EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken)
        {
            EmployeeReadModel employee = new EmployeeReadModel();
            employee.Id = Guid.NewGuid();
            employee.Name = employeeCreateModel.Name;
            employee.Designation = employeeCreateModel.Designation;
            employee.Age = employeeCreateModel.Age;
            await _dynamoDBContext.SaveAsync(employee, cancellationToken);
            return employee;
        }

        public async Task DeleteEmployeeAsync(Guid Id, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.DeleteAsync<EmployeeReadModel>(Id, cancellationToken);
        }
        public async Task UpdateEmployeeAsync(EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(employeeUpdateModel, cancellationToken);
        }
    }
}
