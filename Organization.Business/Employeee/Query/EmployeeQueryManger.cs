
using AutoMapper;
using Organization.Business.Employeee.Models;
using Organization.Entity.Models;
using Organization.Repository.Repository.Employee.Query;
using System.Collections.Generic;

namespace Organization.Business.Employeee.Query
{
    public class EmployeeQueryManger : IEmployeeQueryManger
    {
        private readonly IEmployeeQueryRepository _employeeQueryRepository;
        private readonly IMapper _mapper;

        public EmployeeQueryManger(IEmployeeQueryRepository employeeQueryRepository, IMapper mapper)
        {
            _employeeQueryRepository = employeeQueryRepository;
            _mapper = mapper;

            //_mapper = new Mapper(new MapperConfiguration(config => { config.CreateMap<> }) );
        }
        public async Task<IEnumerable<EmployeeReadModel>> FindEmployeeAsync(EmployeeCreateModel employeeFindModel, CancellationToken cancellationToken)
        {
            Entity.Models.Employee employee = _mapper.Map<Entity.Models.Employee>(employeeFindModel);
            IEnumerable<Entity.Models.Employee> employees = await _employeeQueryRepository.FindEmployeeAsync(employee, cancellationToken);
            return _mapper.Map<IEnumerable<EmployeeReadModel>>(employees);

        }

        public async Task<IEnumerable<EmployeeReadModel>> GetAllEmployeeAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Entity.Models.Employee> employees = await _employeeQueryRepository.GetAllEmployeeAsync(cancellationToken);
            return _mapper.Map<IEnumerable<EmployeeReadModel>>(employees);
        }

        public async Task<EmployeeReadModel> GetEmployeeByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var employee = await _employeeQueryRepository.GetEmployeeByIdAsync(Id, cancellationToken);
            return _mapper.Map<EmployeeReadModel>(employee);
        }
    }
}
