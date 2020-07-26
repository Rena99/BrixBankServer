using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Account.Data.Repositories;
using Account.Services.Services;
using Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Transaction.Data;

namespace Transaction.Messaging
{
    class Program
    {
        public static async Task Main()
        {
            Console.Title = "Account";

            var endpointConfiguration = new EndpointConfiguration("Account");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped<Account.Services.Interfaces.ITransactionService, TransactionService>();
            containerSettings.ServiceCollection.AddScoped<Account.Services.Interfaces.ITransactionRepository, TransactionRepository>();
            containerSettings.ServiceCollection.AddDbContext<TransactionContext>(options =>
                options.UseSqlServer(ConfigurationManager.AppSettings["TransactionDB"]));
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.AuditSagaStateChanges(
                    serviceControlQueue: "Particular.brixbank");
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
            routing.RouteToEndpoint(assembly: typeof(UpdateTransaction).Assembly, destination: "Transaction");
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
