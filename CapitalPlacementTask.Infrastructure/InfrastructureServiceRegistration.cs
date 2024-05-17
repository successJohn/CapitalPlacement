using CapitalPlacementTask.Application.Interfaces;
using CapitalPlacementTask.Infrastructure.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((provider) =>
            {
                var endpointUri = configuration["CosmosDbSettings:EndpointUri"];
                var primaryKey = configuration["CosmosDbSettings:PrimaryKey"];
                var databaseName = configuration["CosmosDbSettings:DatabaseName"];

                var cosmosClientOptions = new CosmosClientOptions
                {
                    ApplicationName = databaseName,
                    ConnectionMode = ConnectionMode.Gateway,
                };

                var cosmosClient = new CosmosClient(endpointUri, primaryKey, cosmosClientOptions);


                return cosmosClient;
            });

            services.AddScoped<IProgramFormService, ProgramFormService>();
            services.AddScoped<IApplicationService, CandidateApplicationService>();
            return services;
        }
    }
}
