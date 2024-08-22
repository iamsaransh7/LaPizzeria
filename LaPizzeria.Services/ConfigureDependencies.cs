
using LaPizzeria.Core;
using LaPizzeria.Repositories.Implementation;
using LaPizzeria.Repositories.Interfaces;
using LaPizzeria.Services.Implementation;
using LaPizzeria.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LaPizzeria.Services
{
    public static class ConfigureDependencies
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration) 
        {
            //database connection
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"))
            );

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //services
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
