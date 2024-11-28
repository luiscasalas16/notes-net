using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace netfw_client
{
    public class Program
    {
        public const int HTTP_PORT = 8088;
        public const int HTTPS_PORT = 8443;
        public const int NETTCP_PORT = 8089;

        static async Task Main(string[] args)
        {
            Console.Title = "WCF .Net Framework Client";

            Console.WriteLine("Press enter to start!");
            Console.ReadLine();

            await CallBasicHttpBinding($"http://localhost:{HTTP_PORT}/basicHttp");
            await CallWsHttpBinding($"http://localhost:{HTTP_PORT}/wsHttp");

            //await CallBasicHttpBinding($"https://localhost:{HTTPS_PORT}");
            //await CallWsHttpBinding($"https://localhost:{HTTPS_PORT}");
            
            await CallNetTcpBinding($"net.tcp://localhost:{NETTCP_PORT}/nettcp");

            Console.WriteLine("Press enter to close!");
            Console.ReadLine();
        }

        private static async Task CallBasicHttpBinding(string url)
        {
            IClientChannel channel = null;
            var binding = new BasicHttpBinding(IsHttps(url) ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None);
            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress(url));

            await Task.Factory.FromAsync(factory.BeginOpen, factory.EndOpen, null);
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                await Task.Factory.FromAsync(channel.BeginOpen, channel.EndOpen, null);
                var result = await client.Echo("Hello World!");
                await Task.Factory.FromAsync(channel.BeginClose, channel.EndClose, null);
                Console.WriteLine(result);
            }
            finally
            {
                await Task.Factory.FromAsync(factory.BeginClose, factory.EndClose, null);
            }
        }

        private static async Task CallWsHttpBinding(string url)
        {
            IClientChannel channel = null;

            var binding = new WSHttpBinding(IsHttps(url) ? SecurityMode.Transport : SecurityMode.None);

            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress(url));
            await Task.Factory.FromAsync(factory.BeginOpen, factory.EndOpen, null);
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                await Task.Factory.FromAsync(channel.BeginOpen, channel.EndOpen, null);
                var result = await client.Echo("Hello World!");
                await Task.Factory.FromAsync(channel.BeginClose, channel.EndClose, null);
                Console.WriteLine(result);
            }
            finally
            {
                await Task.Factory.FromAsync(factory.BeginClose, factory.EndClose, null);
            }
        }

        private static async Task CallNetTcpBinding(string url)
        {
            IClientChannel channel = null;

            var binding = new NetTcpBinding();

            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress(url));
            await Task.Factory.FromAsync(factory.BeginOpen, factory.EndOpen, null);
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                await Task.Factory.FromAsync(channel.BeginOpen, channel.EndOpen, null);
                var result = await client.Echo("Hello World!");
                await Task.Factory.FromAsync(channel.BeginClose, channel.EndClose, null);
                Console.WriteLine(result);
            }
            finally
            {
                await Task.Factory.FromAsync(factory.BeginClose, factory.EndClose, null);
            }
        }

        private static bool IsHttps(string url)
        {
            return url.ToLower().StartsWith("https://");
        }
    }
}
