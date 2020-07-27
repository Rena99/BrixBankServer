using Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Data.Repositories;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;

namespace Transaction.Messaging
{
    class Program
    {
        public static async Task Main()
        {
            Console.Title = "Transaction";

            var endpointConfiguration = new EndpointConfiguration("Transaction");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped<IUpdateTransactionService, UpdateTransactionService>();
            containerSettings.ServiceCollection.AddScoped<IUpdateTransactionRepository, UpdateTransactionRepository>();
            containerSettings.ServiceCollection.AddDbContext<TransactionContext>(options =>
                options.UseSqlServer(ConfigurationManager.AppSettings["TransactionDB"]));
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            //endpointConfiguration.AuditSagaStateChanges(
            //        serviceControlQueue: "Particular.brixbank");
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(
                customizations: delayed =>
                {
                    delayed.TimeIncrease(TimeSpan.FromSeconds(1));
                });
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(ConfigurationManager.AppSettings["RabbitMQ"]);
            endpointConfiguration.EnableOutbox();
            endpointConfiguration.EnableInstallers();
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(ConfigurationManager.AppSettings["MessagingDB"]);
                });
            var routing = transport.Routing();
            routing.RouteToEndpoint(assembly: typeof(AddTransaction).Assembly, destination: "Account");
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
