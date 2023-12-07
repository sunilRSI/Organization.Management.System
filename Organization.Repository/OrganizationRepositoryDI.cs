using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

namespace Organization.Repository
{
    public static class OrganizationRepositoryDI
    {
        public static IServiceCollection AddOrganizationRepositoryDI(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbContextCommandRepository, DbContextCommandRepository>();
            services.AddScoped<IDbContextQueryRepository, DbContextQueryRepository>();
            services.AddScoped<IEmployeeCommandRepository, EmployeeCommandRepository>();
            services.AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>();
            // services.AddTransient<ISQSCommandRepository>(provider => provider.GetService<SQSCommandRepository>());
            services.AddScoped<ISQSCommandRepository, SQSCommandRepository>();
            services.AddScoped<ISQSQueryRepository, SQSQueryRepository>();
            return services;
        }
    }
}
