using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Organization.Entity.Models;
using Organization.Repository.Repository.DbContext.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Repository.Repository.DbContext.Command
{
    public class DbContextCommandRepository : IDbContextCommandRepository
    {
        private readonly ILogger<DbContextCommandRepository> _logger;
        private readonly IAmazonDynamoDB _client;
        private readonly IConfiguration _configuration;
        private readonly string? _tableName;

        public DbContextCommandRepository(ILogger<DbContextCommandRepository> logger, IAmazonDynamoDB amazonDynamoDBClient, IConfiguration configuration)
        {
            _logger = logger;
            _client = amazonDynamoDBClient;
            _configuration = configuration;
            _tableName = _configuration["dynamoDBTableName"];
        }
        public async Task Initilize()
        {
            await CreateTable(_tableName);

        }
        public async Task CreateTable(string TableName)
        {
            Entity.Models.Employee employee;
            try
            {
                bool tableExist = await isTableExistAsync(TableName);
                if (!tableExist)
                {
                    var request = new CreateTableRequest
                    {
                        TableName = TableName,
                        AttributeDefinitions = new List<AttributeDefinition>()
                        {
                            new AttributeDefinition
                            {
                                AttributeName = nameof(employee.Id),
                                AttributeType = ScalarAttributeType.S
                            },
                            // new AttributeDefinition
                            //{
                            //    AttributeName = nameof(emp.Name),
                            //    AttributeType = ScalarAttributeType.S
                            //},
                        },
                        KeySchema = new List<KeySchemaElement>()
                        {
                             new KeySchemaElement
                                {
                                    AttributeName = nameof(employee.Id),
                                    KeyType =KeyType.HASH
                                },
                             //new KeySchemaElement
                             //   {
                             //       AttributeName = nameof(emp.Name),
                             //       KeyType =KeyType.RANGE
                             //   },
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 10,
                            WriteCapacityUnits = 5
                        }
                    };
                    var createResponse = await _client.CreateTableAsync(request);
                    while (true)
                    {
                        if (_client.DescribeTableAsync(TableName).GetAwaiter().GetResult().Table.TableStatus == TableStatus.ACTIVE)
                        {
                            break;
                        }
                    }
                    var item = new Dictionary<string, AttributeValue>
                    {
                        [nameof(employee.Id)] = new AttributeValue { S = Guid.NewGuid().ToString() },
                        [nameof(employee.Name)] = new AttributeValue { S = "Default" },
                        [nameof(employee.Designation)] = new AttributeValue { S = "Default" },
                        [nameof(employee.Age)] = new AttributeValue { N = "0" }
                    };

                    var request2 = new PutItemRequest
                    {
                        TableName = TableName,
                        Item = item,
                    };

                    var putResponse = await _client.PutItemAsync(request2);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;

            }
        }
        public async Task<bool> isTableExistAsync(string TableName)
        {
            try
            {
                var result = await _client.DescribeTableAsync(TableName);
            }
            catch (ResourceNotFoundException)
            {
                return false;
            }
            return true;
        }
    }
}
