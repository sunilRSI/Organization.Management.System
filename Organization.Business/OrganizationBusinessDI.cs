using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.Business.DbContext.Command;
using Organization.Business.Employeee.Command;
using Organization.Business.Employeee.Models;
using Organization.Business.Employeee.Query;
using Organization.Business.SQS.Command;
using Organization.Repository;
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper(typeof(Entity.Models.Employee), typeof(EmployeeReadModel)); 
            return services;
        }
    }
    public class MappingProfile : Profile
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
