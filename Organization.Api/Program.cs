using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.SQS;
using Organization.Business.SQS.Command;
using Organization.Business.DbContext.Command;
using Organization.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models; 

internal class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var awsOptions = builder.Configuration.GetAWSOptions();
        builder.Services.AddDefaultAWSOptions(awsOptions);
        builder.Services.AddAWSService<IAmazonSQS>();
        builder.Services.AddAWSService<IAmazonDynamoDB>();
        builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        builder.Services.AddOrganizationBusinessDI(builder.Configuration);
        builder.Services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(s =>
        {
            s.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value; ;
            s.Audience = "myApi";
            s.RequireHttpsMetadata = false;
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("EmployeeQuery", new OpenApiInfo
            {
                Title = "EmployeeQuery",
                Version = "v1"
            });
            c.SwaggerDoc("EmployeeCommand", new OpenApiInfo
            {
                Title = "EmployeeCommand",
                Version = "v2"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Please Provide Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference= new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="Bearer"}
                },
                new List<string>{ "myApiScope" }
            }
        });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/DynmodbCommand/swagger.json", "DynmodbCommand");
                c.SwaggerEndpoint("/swagger/EmployeeQuery/swagger.json", "EmployeeQuery");
                c.SwaggerEndpoint("/swagger/EmployeeCommand/swagger.json", "EmployeeCommand");

            });
        }
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
    private static async Task Main2(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); 
        ConfigureServices(builder.Services, builder.Configuration); 
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContextCommandManager>();
            await dbContext.Initialize();
            var sqsProvider = scope.ServiceProvider.GetRequiredService<ISQSCommandManager>();
            await sqsProvider.Initialize();
        } 
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/DynmodbCommand/swagger.json", "DynmodbCommand");
                c.SwaggerEndpoint("/swagger/EmployeeQuery/swagger.json", "EmployeeQuery");
                c.SwaggerEndpoint("/swagger/EmployeeCommand/swagger.json", "EmployeeCommand");

            });
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers();
        services.AddCors();
        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(s =>
        {
            s.Authority = configuration.GetSection("IdentityServerUrl").Value; //"https://localhost:7202";
            s.Audience = "myApi";
            s.RequireHttpsMetadata = false;

        }); 
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {

            c.SwaggerDoc("EmployeeQuery", new OpenApiInfo
            {
                Title = "EmployeeQuery",
                Version = "v1"
            });
            c.SwaggerDoc("EmployeeCommand", new OpenApiInfo
            {
                Title = "EmployeeCommand",
                Version = "v2"
            }); 
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Please Provide Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="Bearer"}
                        },
                        new List<string>{ "myApiScope" }
                    }
                        });


        });
        var awsOptions = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonSQS>();
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        services.AddOrganizationBusinessDI(configuration);
    }
}