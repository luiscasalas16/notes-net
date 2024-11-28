using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace net_client
{
    public class Program
    {
        public const int HTTP_PORT = 8088;
        public const int HTTPS_PORT = 8443;
        public const int NETTCP_PORT = 8089;

        static async Task Main(string[] args)
        {
            Console.Title = "WCF .Net Core Client";

            Console.WriteLine("Press enter to start!");
            Console.ReadLine();

            await CallBasicHttpBinding($"http://localhost:{HTTP_PORT}/basicHttp");
            await CallWsHttpBinding($"http://localhost:{HTTP_PORT}/wsHttp");
            
            //await CallBasicHttpBinding($"https://localhost:{HTTPS_PORT}");
            //await CallWsHttpBinding($"https://localhost:{HTTPS_PORT}");

            await CallNetTcpBinding($"net.tcp://localhost:{NETTCP_PORT}/netTcp");

            Console.WriteLine("Press enter to close!");
            Console.ReadLine();
        }

        private static async Task CallBasicHttpBinding(string url)
        {
            IClientChannel channel = null;

            var binding = new BasicHttpBinding(IsHttps(url) ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None);

            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress(url));
            factory.Open();
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                channel.Open();
                var result = await client.Echo("Hello World!");
                channel.Close();
                Console.WriteLine(result);
            }
            finally
            {
                factory.Close();
            }
        }

        private static async Task CallWsHttpBinding(string url)
        {
            IClientChannel channel = null;

            var binding = new WSHttpBinding(IsHttps(url) ? SecurityMode.Transport : SecurityMode.None);

            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress(url));
            factory.Open();
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                channel.Open();
                var result = await client.Echo("Hello World!");
                channel.Close();
                Console.WriteLine(result);
            }
            finally
            {
                factory.Close();
            }
        }

        private static async Task CallNetTcpBinding(string url)
        {
            IClientChannel channel = null;

            var binding = new NetTcpBinding();

            var factory = new ChannelFactory<IEchoService>(binding, new EndpointAddress(url));
            factory.Open();
            try
            {
                IEchoService client = factory.CreateChannel();
                channel = client as IClientChannel;
                channel.Open();
                var result = await client.Echo("Hello World!");
                channel.Close();
                Console.WriteLine(result);
            }
            finally
            {
                factory.Close();
            }
        }

        private static bool IsHttps(string url)
        {
            return url.ToLower().StartsWith("https://");
        }
    }
}
