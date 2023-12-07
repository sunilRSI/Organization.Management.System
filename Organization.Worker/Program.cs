

using Amazon.DynamoDBv2;
using Amazon.SQS;
using Organization.Worker.Service;
using Organization.Worker.Worker;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var awsOptions = builder.Configuration.GetAWSOptions();
        builder.Services.AddDefaultAWSOptions(awsOptions);
        builder.Services.AddAWSService<IAmazonSQS>();
        builder.Services.AddAWSService<IAmazonDynamoDB>();

        builder.Services.AddHostedService<SQSMsgSubscriberBackgroudService>();
        builder.Services.AddTransient<SQSQueueMessageProcessorService>();
        builder.Services.AddTransient<SQSQueueMessageListenerService>();
        //builder.Services.AddTransient<Models.QueueConfiguration>();
        //builder.Services.Configure<Models.SQSQueueConfiguration>(builder.Configuration.GetSection("AWS"));
        var app = builder.Build();
        app.Run();
    } 
   
}