using Microsoft.IdentityModel.Logging;
using Organization.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;
// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServer()
                        .AddInMemoryClients(IdentityConfiguration.Clients)
                        .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                         .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                        .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                        .AddTestUsers(IdentityConfiguration.TestUsers)
                        .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseIdentityServer();
app.Run();
