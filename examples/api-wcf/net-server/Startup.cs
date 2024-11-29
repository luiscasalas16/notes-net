using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace net_server
{
    public class Startup
    {
        public const int HTTP_PORT = 8088;
        public const int HTTPS_PORT = 8443;
        public const int NETTCP_PORT = 8089;

        public void ConfigureServices(IServiceCollection services)
        {
            // Enable CoreWCF Services, enable metadata
            // Use the Url used to fetch WSDL as that service endpoint address in generated WSDL 
            services.AddServiceModelServices()
                    .AddServiceModelMetadata()
                    .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseServiceModel(builder =>
            {
                // Add the Echo Service
                builder.AddService<EchoService>()

                // Add a BasicHttpBinding endpoint
                .AddServiceEndpoint<EchoService, IEchoService>(new BasicHttpBinding(), $"http://localhost:{HTTP_PORT}/basichttp")
                //.AddServiceEndpoint<EchoService, IEchoService>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/basichttp")

                // Add WSHttpBinding endpoints
                .AddServiceEndpoint<EchoService, IEchoService>(new WSHttpBinding(SecurityMode.None), $"http://localhost:{HTTP_PORT}/wsHttp")
                //.AddServiceEndpoint<EchoService, IEchoService>(new WSHttpBinding(SecurityMode.Transport), "/wsHttp")

                // Add NetTcpBinding
                .AddServiceEndpoint<EchoService, IEchoService>(new NetTcpBinding(), $"net.tcp://localhost:{NETTCP_PORT}/netTcp");

                // Configure WSDL to be available over http & https
                var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
                serviceMetadataBehavior.HttpGetEnabled = true;
                serviceMetadataBehavior.HttpsGetEnabled = true;
            });
        }
    }

}
