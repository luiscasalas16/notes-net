using Api.Contracts;
using Api.Entities;
using Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            // Gives rights to the user to access resources from the server on a different domain

            // Instead of the AllowAnyOrigin() method which allows requests from any source,
            // we can use WithOrigins("http://www.something.com") that which allow requests just from the specified source. 

            // instead of AllowAnyMethod() that allows all HTTP methods,
            // we can use WithMethods("POST", "GET") that will allow only specified HTTP methods.

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            // Configure an IIS integration for IIS deployment with the default values.

            services.Configure<IISOptions>(options =>
            {
            });
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            // Configure the repository wrapper to be instantiated once per request.

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            // Configure de database context.

            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:DefaultContext");
            });
        }
    }
}
