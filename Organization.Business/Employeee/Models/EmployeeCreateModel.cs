using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.Employeee.Models
{
    public class EmployeeCreateModel
    {  
        public string? Name { get; set; } 
        public string? Designation { get; set; } 
        public int? Age { get; set; }
    }
}
