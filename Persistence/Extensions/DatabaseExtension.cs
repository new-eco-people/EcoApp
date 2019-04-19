using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence.Extensions
{
    public static class DatabaseExtension
    {
        public static void ConfigureDatabaseConnections(this IServiceCollection services, string connectionString, string assemblyName) {
            
            // Using MSSQL, But any connection can be used in this case
            // Simply add the extension for the Database from Nuget, 
            // change the method in x.UseSqlServer to the desired method
            // Configure the connection string in appsettings.json
            services.AddDbContext<DefaultDataContext>(x => x.UseSqlServer(
                    connectionString, b => b.MigrationsAssembly(assemblyName)
            ));

            // Configure Identity if required else comment this code
            services.ConfigureIdentity();
        }
    }
}