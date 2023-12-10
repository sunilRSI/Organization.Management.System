using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.Employeee.Models
{
    public class EmployeeReadModel :EmployeeCreateModel
    {
        public Guid Id { get; set; }
    }
}
