using System.ServiceModel;

namespace netfw_server
{
    public class EchoService : IEchoService
    {
        public string Echo(string text)
        {
            System.Console.WriteLine($"Received {text} from client!");
            return text;
        }

        public string ComplexEcho(EchoMessage text)
        {
            System.Console.WriteLine($"Received {text.Text} from client!");
            return text.Text;
        }

        public string FailEcho(string text)
        {
            System.Console.WriteLine($"Received {text} and Fault generated on client!");
            throw new FaultException<EchoFault>(new EchoFault() { Text = "CoreWCF Fault OK" });
        }
    }
}
