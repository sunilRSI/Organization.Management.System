using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using Organization.Business.Employeee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.ApiTests
{
    public class ApiTestConfig
    {
        public static string accessKey = "AKIAQXCLMONQ4F4RW4CD";
        public static string secretKey = "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";

        public static DynamoDBContext GetDynamoDBClient()
        {
            return new DynamoDBContext(new AmazonDynamoDBClient(accessKey, secretKey, RegionEndpoint.USEast1));
        }
        public static AmazonSQSClient GetAmazonSQSClient()
        {
            return new AmazonSQSClient(accessKey, secretKey, RegionEndpoint.USEast1);
        }
    }
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Entity.Models.Employee, EmployeeReadModel>();
            CreateMap<EmployeeReadModel, Entity.Models.Employee>();
            CreateMap<EmployeeCreateModel, Entity.Models.Employee>();
        }
    }
}
