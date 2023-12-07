using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Entity.Constants
{
    public class EmployeeSQSQueueName
    {
        public const string EmpCreated = "mysqscreateemp";

        public const string EmpDeleted = "mysqsdeleteemp";

        public static readonly string[] AllSQSQueueNames = { EmpCreated, EmpDeleted };
    }
}
