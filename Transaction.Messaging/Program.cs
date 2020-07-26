using Messages;
using NServiceBus;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Transaction.Messaging
{
    class Program
    {
        public static async Task Main()
        {
            Console.Title = "Transaction";

            var endpointConfiguration = new EndpointConfiguration("Transaction");
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
