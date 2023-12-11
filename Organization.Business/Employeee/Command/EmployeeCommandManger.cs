using AutoMapper;
using Organization.Business.Employeee.Models;
using Organization.Entity.Constants;
using Organization.Repository.Repository.Employee.Command;
using Organization.Repository.Repository.SQS.Command;

namespace Organization.Business.Employeee.Command
{
    public class EmployeeCommandManger : IEmployeeCommandManger
    {
        private readonly IEmployeeCommandRepository _employeeCommandRepository;
        private readonly IMapper _mapper;
        private readonly ISQSCommandRepository _sQSCommandRepository;

        public EmployeeCommandManger(IEmployeeCommandRepository employeeCommandRepository, ISQSCommandRepository sQSCommandRepository, IMapper mapper)
        {
            _employeeCommandRepository = employeeCommandRepository;
            _mapper = mapper;
            _sQSCommandRepository= sQSCommandRepository;
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

            await _sQSCommandRepository.SendMessageAsync(EmployeeSQSQueueName.EmpCreated, employee, cancellationToken);
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
