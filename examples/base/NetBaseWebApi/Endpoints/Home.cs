namespace NetBaseWebApi.Endpoints
{
    public static class Home
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => new HomeMessage { Message = Global.Helpers.GetDateTime() })
                .WithName("Get")
                .WithOpenApi();
        }
    }

    public class HomeMessage
    {
        public string? Message { get; set; }
    }
}
