
using Organization.Api;
using Organization.Business.DbContext.Command;
using Organization.Business.SQS.Command;
public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContextCommandManager>();
            await dbContext.Initialize();
            var sqsProvider = scope.ServiceProvider.GetRequiredService<ISQSCommandManager>();
            await sqsProvider.Initialize();
        }
        await host.RunAsync();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}