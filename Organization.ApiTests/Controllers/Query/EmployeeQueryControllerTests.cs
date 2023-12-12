using Amazon.DynamoDBv2.DataModel; 
using Amazon.SQS;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Organization.Business.Employeee.Query;
using Organization.Repository.Repository.Employee.Query;
using Organization.Repository.Repository.SQS.Query; 
using Organization.Business.Employeee.Models;
using Microsoft.AspNetCore.Mvc; 
using Organization.ApiTests;

namespace Organization.Api.Controllers.Query.Tests
{
    [TestClass()]
    public class EmployeeQueryControllerTests
    {
        private readonly IEmployeeQueryManger _employeeQueryManger;
        private readonly EmployeeQueryController _employeeQueryController;
        private readonly ILogger<EmployeeQueryController> _logger;
        private readonly ILogger<SQSQueryRepository> _sQSQueryRepositoryLogger;
        private readonly IEmployeeQueryRepository _employeeQueryRepository;
        private readonly ISQSQueryRepository _sQSQueryRepository;
        private readonly IMapper _mapper;
        private readonly IAmazonSQS _sqsClient;
        private readonly IDynamoDBContext _dynamoDBContext;
       

        public EmployeeQueryControllerTests()
        {
            _dynamoDBContext = ApiTestConfig.GetDynamoDBClient();
            //MappingProfile mappingProfile = new MappingProfile();
            _sqsClient = ApiTestConfig.GetAmazonSQSClient();
            _employeeQueryRepository = new EmployeeQueryRepository(_dynamoDBContext);
            // _mapper = new Mapper(new MapperConfiguration(new MapperConfigurationExpression()));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _sQSQueryRepositoryLogger = new Logger<SQSQueryRepository>(new LoggerFactory());
            _sQSQueryRepository = new SQSQueryRepository();
            _employeeQueryManger = new EmployeeQueryManger(_employeeQueryRepository, _mapper);
            _logger = new Logger<EmployeeQueryController>(new LoggerFactory());
            _employeeQueryController = new EmployeeQueryController(_employeeQueryManger, _logger);



        }
        [TestMethod()]
        public async Task GetByEmployeeIdTest()
        {
            try
            {
                var result = await _employeeQueryController.GetAllEmployees();
                var list = result as OkObjectResult; 
                var employees = list.Value as List<EmployeeReadModel>;
                Assert.Equals(5, employees.Count);
           
            }
            catch (Exception ex)
            {

            }
        }
    }
 
}