using Amazon.DynamoDBv2.DataModel; 

namespace Organization.Entity.Models
{
    [DynamoDBTable("Employee")]
    public class EmployeeReadModel
    { 
            [DynamoDBProperty("Id")]
            [DynamoDBHashKey]
            public Guid Id { get; set; }

            //[DynamoDBRangeKey]
            [DynamoDBProperty("Name")]
            public string? Name { get; set; }

            [DynamoDBProperty("Designation")]
            public string? Designation { get; set; }

            [DynamoDBProperty("Age")]
            public int? Age { get; set; } 
    }
}
