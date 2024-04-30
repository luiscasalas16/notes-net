using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NetApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder
                .Services.AddControllers()
                // Enables automatic validation of objects that have validation annotations.
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        return new ResultInvalid(context.ModelState);
                    };
                })
                // Sets Newtonsoft as default serializer and sets the default serializer settings.
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            // Implements default exception handler.
            app.UseExceptionHandler("/error");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
