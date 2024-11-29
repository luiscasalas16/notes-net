using System;
using System.ServiceModel;

namespace netfw_server
{
    class Program
    {
        public const int HTTP_PORT = 8088;
        public const int HTTPS_PORT = 8443;
        public const int NETTCP_PORT = 8089;

        static void Main()
        {
            Type contract = typeof(IEchoService);
            var host = new ServiceHost(typeof(EchoService));

            host.AddServiceEndpoint(contract, new BasicHttpBinding(BasicHttpSecurityMode.None), $"http://localhost:{HTTP_PORT}/basichttp");
            host.AddServiceEndpoint(contract, new WSHttpBinding(SecurityMode.None), $"http://localhost:{HTTP_PORT}/wsHttp");

            //host.AddServiceEndpoint(contract, new BasicHttpsBinding(BasicHttpsSecurityMode.Transport), "/basichttp");
            //host.AddServiceEndpoint(contract, new WSHttpBinding(SecurityMode.Transport), "/wsHttp");

            host.AddServiceEndpoint(contract, new NetTcpBinding(), $"net.tcp://localhost:{NETTCP_PORT}/nettcp");

            host.Open();

            LogHostUrls(host);

            Console.WriteLine("Hit enter to close");
            Console.ReadLine();

            host.Close();
        }

        private static void LogHostUrls(ServiceHost host)
        {
            foreach (System.ServiceModel.Description.ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                Console.WriteLine("Listening on " + endpoint.ListenUri.ToString());
            }
        }

    }
}
