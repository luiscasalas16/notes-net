using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: Setting up the configuration for a logger service.

            NLog.GlobalDiagnosticsContext.Set("appbasepath", System.IO.Directory.GetCurrentDirectory());

            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Configure NLog.

                builder.Logging.ClearProviders();

                builder.Host.UseNLog();

                // Cors extension.
                builder.Services.ConfigureCors();

                // IIS extension.
                builder.Services.ConfigureIISIntegration();

                // Repository extension.
                builder.Services.ConfigureRepositoryWrapper();

                //Configure AutoMapper.
                builder.Services.AddAutoMapper(typeof(Program));

                builder.Services.AddControllers();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                    app.UseDeveloperExceptionPage();
                else
                    app.UseHsts();

                app.UseHttpsRedirection();

                // Enables using static files (wwwroot).
                app.UseStaticFiles();

                // Forward proxy headers to the current request (linux).
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.All
                });

                // EnableCors
                app.UseCors("CorsPolicy");

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Program exception");
                throw;
            }
            finally
            {
                // NLog: ensure to flush and stop internal timers/threads before application-exit.
                LogManager.Shutdown();
            }
        }
    }
}