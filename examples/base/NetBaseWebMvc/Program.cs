namespace NetBaseWebMvc
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddControllersWithViews();

                var app = builder.Build();

                app.UseStaticFiles();

                app.UseRouting();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                app.Run();
            }
            catch (Exception ex)
            {
                Global.Helpers.LogFailure(ex);
            }
        }
    }
}
