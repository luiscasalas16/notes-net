using System.Diagnostics;

namespace net.Multithread
{
    //https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/
    internal class AsynchronousProgramming
    {
        public static async Task Init()
        {
            MakeCoffe();

            await MakeCoffeAsync();
        }

        #region Sync

        static void MakeCoffe()
        {
            Separator();
            Console.WriteLine("make sync coffee");
            Separator();
            Console.WriteLine();

            Stopwatch sw = Stopwatch.StartNew();

            Console.WriteLine("add coffee");
            var hotWater = HeatWater();
            var hotMilk = HeatMilk();
            Pour(hotWater);
            Pour(hotMilk);
            Console.WriteLine("add sugar");
            Console.WriteLine("drink");

            sw.Stop();

            Console.WriteLine($"total time {sw.ElapsedMilliseconds / 1000}");
            Console.WriteLine();
        }

        static string HeatWater()
        {
            Console.WriteLine("heating water start");

            Console.WriteLine("waiting for water to be hot ...");
            Thread.Sleep(5000);
            Console.WriteLine("heating water complete");

            return "hot water";
        }

        static string HeatMilk()
        {
            Console.WriteLine("heating milk start");

            Console.WriteLine("waiting for milk to be hot ...");
            Thread.Sleep(3000);
            Console.WriteLine("heating milk complete");

            return "hot milk";
        }

        #endregion

        #region Async

        static async Task MakeCoffeAsync()
        {
            Separator();
            Console.WriteLine("make async coffee");
            Separator();
            Console.WriteLine();

            Stopwatch sw = Stopwatch.StartNew();

            Console.WriteLine("add coffee");

            //sequential, take 8 seconds
            //var hotWater = await HeatWaterAsync();
            //var hotMilk = await HeatMilkAsync();
            //

            //parallel  take 4 seconds
            var heatingWater = HeatWaterAsync();
            var heatingMilk = HeatMilkAsync();

            //wait one by one
            //var hotWater = await heatingWater;
            //var hotMilk = await heatingMilk;

            //wait all
            await Task.WhenAll(heatingWater, heatingMilk);

            Pour(heatingWater.Result);
            Pour(heatingMilk.Result);
            Console.WriteLine("add sugar");
            Console.WriteLine("drink");

            sw.Stop();

            Console.WriteLine($"total time {sw.ElapsedMilliseconds / 1000}");
            Console.WriteLine();
        }

        static async Task<string> HeatWaterAsync()
        {
            Console.WriteLine("heating water start");

            Console.WriteLine("waiting for water to be hot ...");
            await Task.Delay(4000);
            Console.WriteLine("heating water complete");

            return "hot water";
        }

        static async Task<string> HeatMilkAsync()
        {
            Console.WriteLine("heating milk start");

            Console.WriteLine("waiting for milk to be hot ...");
            await Task.Delay(4000);
            Console.WriteLine("heating milk complete");

            return "hot milk";
        }

        #endregion

        static void Pour(string drink)
        {
            Console.WriteLine($"pouring {drink}");
        }

        static void Separator()
        {
            Console.WriteLine(new string('-', 40));
        }
    }
}
