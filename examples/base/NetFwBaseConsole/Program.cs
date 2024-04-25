using System;
using System.Diagnostics;

namespace NetFwBaseConsole
{
    internal static class Program
    {
        static void Main()
        {
            try
            {
                Global.Helpers.LogSuccess(".Net Framework Console");
            }
            catch (Exception ex)
            {
                Global.Helpers.LogFailure(ex);
            }

            Global.Helpers.ConsoleWait();
        }
    }
}
