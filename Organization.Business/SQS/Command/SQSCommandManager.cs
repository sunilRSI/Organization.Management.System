using AutoMapper;
using Organization.Business.Employeee.Models;
using Organization.Entity.Models;
using Organization.Repository.Repository.SQS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.SQS.Command
{
    public class SQSCommandManager : ISQSCommandManager
    {
        private readonly ISQSCommandRepository _sQSCommandRepository;
        private readonly IMapper _mapper;

        public SQSCommandManager(ISQSCommandRepository sQSCommandRepository,IMapper mapper)
        {
            _sQSCommandRepository = sQSCommandRepository;
            _mapper= mapper;

        }
        public async Task Initialize()
        {
            await _sQSCommandRepository.Initialize();
        }

        public async Task SendMessageAsync(string QueueName, EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken)
        {
            var employye= _mapper.Map<Entity.Models.Employee>(employeeCreateModel);
            await _sQSCommandRepository.SendMessageAsync(QueueName, employye, cancellationToken);
        }
    }
}
