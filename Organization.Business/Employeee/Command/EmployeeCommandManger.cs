using Organization.Entity.Models;
using Organization.Repository.Repository.Employee.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.Employeee.Command
{
    public class EmployeeCommandManger : IEmployeeCommandManger
    {
        private readonly IEmployeeCommandRepository _employeeCommandRepository;

        public EmployeeCommandManger(IEmployeeCommandRepository employeeCommandRepository)
        {
            _employeeCommandRepository = employeeCommandRepository;
        }

        public async Task<EmployeeReadModel> CreateEmployeeAsync(EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken)
        {
            return await _employeeCommandRepository.CreateEmployeeAsync(employeeCreateModel, cancellationToken);
        }

        public async Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            await _employeeCommandRepository.DeleteEmployeeAsync(id, cancellationToken);
        }

        public async Task UpdateEmployeeAsync(EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken)
        {
            await _employeeCommandRepository.UpdateEmployeeAsync(employeeUpdateModel, cancellationToken);
        }
    }
}
