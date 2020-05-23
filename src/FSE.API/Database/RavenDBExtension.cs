using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FSE.API.Database
{
    [ExcludeFromCodeCoverage]
    public  static class RavenDBExtension
    {
        public static IServiceCollection AddAsyncDocumentSession(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var settings = new DBSettings();
            configuration.Bind(settings);
            var store = new DocumentStore
            {
                Urls = settings.RavenDb.Urls,
                Database = settings.RavenDb.DatabaseName,
                Certificate = (string.IsNullOrWhiteSpace(settings.RavenDb.CertPath)? default:
                new X509Certificate2(settings.RavenDb.CertPath, settings.RavenDb.CertPass))
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
