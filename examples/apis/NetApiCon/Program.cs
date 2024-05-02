using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetApi.Common.Results;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NetApiCon
{
    public class Program
    {
        protected Program() { }

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
                        var result = new ResultClientError(context.ModelState.Values.SelectMany(m => m.Errors).Select(e => new Error("", e.ErrorMessage)).ToList());

                        return new JsonResult(result) { StatusCode = result.Status };
                    };
                })
                // Sets Newtonsoft as default serializer and sets the default serializer settings.
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            // Implements swagger.
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Implements swagger.
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
