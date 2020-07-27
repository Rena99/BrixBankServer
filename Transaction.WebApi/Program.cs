using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Transaction.WebApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
         .Build();
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder()
           .UseNServiceBus(context =>
           {
               var endpointConfiguration = new EndpointConfiguration("TransactionService");
               endpointConfiguration.EnableInstallers();
               var outboxSettings = endpointConfiguration.EnableOutbox();
               outboxSettings.KeepDeduplicationDataFor(TimeSpan.FromDays(6));
               outboxSettings.RunDeduplicationDataCleanupEvery(TimeSpan.FromMinutes(15));
               var recoverability = endpointConfiguration.Recoverability();
               recoverability.Delayed(
                   customizations: delayed =>
                   {
                       delayed.NumberOfRetries(2);
                       delayed.TimeIncrease(TimeSpan.FromMinutes(4));
                   });
               recoverability.Immediate(
                   customizations: immediate =>
                   {
                       immediate.NumberOfRetries(3);

                   });

               var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
               transport.UseConventionalRoutingTopology()
               .ConnectionString(Configuration.GetConnectionString("RabbitMQ"));
               var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
               var connection = Configuration.GetConnectionString("MessagingDB");
               persistence.SqlDialect<SqlDialect.MsSqlServer>();
               persistence.ConnectionBuilder(
                   connectionBuilder: () =>
                   {
                       return new SqlConnection(connection);
                   });
               var subscriptions = persistence.SubscriptionSettings();
               subscriptions.CacheFor(TimeSpan.FromMinutes(10));
               endpointConfiguration.SendFailedMessagesTo("error");
               endpointConfiguration.AuditProcessedMessagesTo("audit");
               endpointConfiguration.AuditSagaStateChanges(
                       serviceControlQueue: "Particular.brixbank");
               var routing = transport.Routing();
               var conventions = endpointConfiguration.Conventions();
               conventions.DefiningCommandsAs(type => type.Namespace == "MessagesClasses.Commands");
               conventions.DefiningEventsAs(type => type.Namespace == "MessagesClasses.Events");
               return endpointConfiguration;
           })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>()
                         .UseConfiguration(Configuration);
           });
    }
}
