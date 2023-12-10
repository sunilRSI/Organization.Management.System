using AutoMapper;
using Organization.Business.Employeee.Models;
using Organization.Repository.Repository.Employee.Command;

namespace Organization.Business.Employeee.Command
{
    public class EmployeeCommandManger : IEmployeeCommandManger
    {
        private readonly IEmployeeCommandRepository _employeeCommandRepository;
        private readonly IMapper _mapper;

        public EmployeeCommandManger(IEmployeeCommandRepository employeeCommandRepository, IMapper mapper)
        {
            _employeeCommandRepository = employeeCommandRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeReadModel> CreateEmployeeAsync(EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken)
        {
            Entity.Models.Employee employee = _mapper.Map<Entity.Models.Employee>(employeeCreateModel);
            employee.Id = Guid.NewGuid();
            //employee.Name = employeeCreateModel.Name;
            //employee.Designation = employeeCreateModel.Designation;
            //employee.Age = employeeCreateModel.Age;
            employee = await _employeeCommandRepository.CreateEmployeeAsync(employee, cancellationToken);
            EmployeeReadModel employeeReadModel = _mapper.Map<EmployeeReadModel>(employee);
            return employeeReadModel;
        }

        public async Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            await _employeeCommandRepository.DeleteEmployeeAsync(id, cancellationToken);
        }

        public async Task UpdateEmployeeAsync(EmployeeReadModel employeeUpdateModel, CancellationToken cancellationToken)
        {
            Entity.Models.Employee employee = _mapper.Map<Entity.Models.Employee>(employeeUpdateModel);
            await _employeeCommandRepository.UpdateEmployeeAsync(employee, cancellationToken);
        }
    }
}
