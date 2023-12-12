 
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Organization.ApiTests;
using Organization.Business.Employeee.Command;
using Organization.Business.Employeee.Models; 
using Organization.Repository.Repository.Employee.Command;
using Organization.Repository.Repository.SQS.Command; 

namespace Organization.Api.Controllers.Command.Tests
{
    [TestClass()]
    public class EmployeeCommandControllerTests
    {
        private readonly IEmployeeCommandManger _employeeCommandManger;
        private readonly EmployeeCommandController _employeeCommandController;
        private readonly ILogger<EmployeeCommandController> _logger;
        private readonly ILogger<SQSCommandRepository> _sQSCommandRepositoryLogger;
        private readonly IEmployeeCommandRepository _employeeCommandRepository;
        private readonly ISQSCommandRepository _sQSCommandRepository;
        private readonly IMapper _mapper;
        private readonly IAmazonSQS _sqsClient;
        private readonly IDynamoDBContext _dynamoDBContext; 
        public EmployeeCommandControllerTests()
        {
            _dynamoDBContext = ApiTestConfig.GetDynamoDBClient();
            //MappingProfile mappingProfile = new MappingProfile();
            _sqsClient = ApiTestConfig.GetAmazonSQSClient();
            _employeeCommandRepository = new EmployeeCommandRepository(_dynamoDBContext);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _sQSCommandRepositoryLogger = new Logger<SQSCommandRepository>(new LoggerFactory());
            _sQSCommandRepository = new SQSCommandRepository(_sqsClient, _sQSCommandRepositoryLogger);
            _employeeCommandManger = new EmployeeCommandManger(_employeeCommandRepository, _sQSCommandRepository, _mapper);
            _logger = new Logger<EmployeeCommandController>(new LoggerFactory());
            _employeeCommandController = new EmployeeCommandController(_employeeCommandManger, _logger);
        }

        [TestMethod]
        public async Task Create_InvalidModelState()
        {
            // _employeeCommandController.ModelState.AddModelError("Name", "Name is required"); 
            var employee = new EmployeeCreateModel { Name = "Test", Age = 25, Designation = "Developer" };

            var result = await _employeeCommandController.AddEmployeeAsync(employee);

            //Assert.AreEqual(employee.Name, result.);
            //Assert.AreEqual(employee.Age, testEmployee.Age);
        }

        [TestMethod()]
        public async Task AddEmployeeAsyncTest()
        {
            var result = await _employeeCommandController.DeleteEmployeeAsync(Guid.NewGuid());
            var list = result is NoContentResult;

        }
        [TestMethod()]
        public async Task UpdateEmployeeAsyncTest()
        {
            var employee = new EmployeeReadModel { Name = "Test", Age = 25, Designation = "Developer" };
            await _employeeCommandController.UpdateEmployeeAsync(employee);

        }
    }
   
}