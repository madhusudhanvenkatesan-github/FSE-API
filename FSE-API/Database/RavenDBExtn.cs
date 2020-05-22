using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace FSE_API.Database
{
    public static class RavenDBExtn
    {
        public static IServiceCollection AddAsyncDocumentSession(this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = new Settings();
            configuration.Bind(settings);
            var store = new DocumentStore
            {
                Urls = settings.RavenDB.Urls,
                Database = settings.RavenDB.DatabaseName,
                Certificate = (string.IsNullOrWhiteSpace(settings.RavenDB.CertPath) ? default :
                new X509Certificate2(settings.RavenDB.CertPath, settings.RavenDB.CertPass))
            };
            store.Initialize();
            services.AddSingleton<IDocumentStore>(store);

            services.AddScoped<IAsyncDocumentSession>(serviceProvider =>
            {
                return serviceProvider
                    .GetService<IDocumentStore>()
                    .OpenAsyncSession();
            });
            return services;
        }
    }
}
