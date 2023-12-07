using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Business.DbContext.Command;
using Organization.Business.Employeee.Command;
using Organization.Business.Employeee.Query;
using Organization.Business.SQS.Command;
using Organization.Repository;
using Organization.Repository.Repository.DbContext.Command;
using Organization.Repository.Repository.DbContext.Query;
using Organization.Repository.Repository.Employee.Command;
using Organization.Repository.Repository.Employee.Query;
using Organization.Repository.Repository.SQS.Command;
using Organization.Repository.Repository.SQS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business
{
    public static class OrganizationBusinessDI
    {
        public static IServiceCollection AddOrganizationBusinessDI(this
           IServiceCollection services, IConfiguration configuration)
        {
            services.AddOrganizationRepositoryDI(configuration);
            services.AddScoped<IDbContextCommandManager, DbContextCommandManager>();
            services.AddScoped<IEmployeeCommandManger, EmployeeCommandManger>();
            services.AddScoped<IEmployeeQueryManger, EmployeeQueryManger>();
            services.AddScoped<IEmployeeCommandManger, EmployeeCommandManger>();
            services.AddScoped<IEmployeeQueryManger, EmployeeQueryManger>();
            services.AddScoped<ISQSCommandManager, SQSCommandManager>(); 
            return services;
        }
    }
}
