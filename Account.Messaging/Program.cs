using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Account.Data;
using Account.Data.Repositories;
using Account.Services.Interfaces;
using Account.Services.Services;
using Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

namespace Transaction.Messaging
{
    class Program
    {
        public static async Task Main()
        {
            Console.Title = "Account";

            var endpointConfiguration = new EndpointConfiguration("Account");
            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped<IAddTransactionService, AddTransactionService>();
            containerSettings.ServiceCollection.AddScoped<IAddTransactionRepository, AddTransactionRepository>();
            containerSettings.ServiceCollection.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(ConfigurationManager.AppSettings["AccountDB"]));
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
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
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
