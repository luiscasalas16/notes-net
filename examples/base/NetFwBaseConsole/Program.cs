using System;
using System.Diagnostics;

namespace NetFwBaseConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(".Net Framework Console");
                Console.WriteLine("Hello World " + DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd_HH-mm-ss-fffff"));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

                Console.WriteLine(ex.ToString());
            }

            Console.Write("Press enter to close this window . . .");
            Console.ReadLine();
        }
    }
}
