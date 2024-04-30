using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetFwApi.Client
{
    internal class Program
    {
        static string url = "http://localhost:5002";

        static async Task Main(string[] args)
        {
            Console.WriteLine("netfw");
            Console.WriteLine();

            //Console.Write("press enter key to start");
            //Console.ReadLine();

            Thread.Sleep(1000);

            try
            {
                //await TestGet("Test1", "Get");
                //await TestGet("Test1", "GetA");
                //await TestGet("Test1", "GetB");
                //await TestGet("Test1", "CGet");
                //await TestGet("Test1", "DGet");
                //await TestPost("Test1", "Post");
                //await TestPost("Test1", "PostA");
                //await TestPost("Test1", "PostB");
                //await TestPost("Test1", "CPost");
                //await TestPost("Test1", "DPost");

                //await TestGetAll("Test2");
                //await TestGetId("Test2");
                //await TestInsert("Test2");
                //await TestUpdate("Test2");
                //await TestDelete("Test2");

                //await TestGetAll("Test3");
                //await TestGetId("Test3");
                //await TestInsert("Test3");
                //await TestUpdate("Test3");
                //await TestDelete("Test3");

                //await TestGet("Test4", "ErrorGet");
                await TestValidation(
                    "Test4",
                    "ErrorValidation",
                    @"{
                    ""email"":""hello@world.com""
                }"
                );
                await TestValidation(
                    "Test4",
                    "ErrorValidation",
                    @"{
                    ""fistName"":""Hello"",
                    ""lastName"":""World"",
                    ""email"":""hello@world.com""
                }"
                );

                //await TestGet("Test5", "Get1");
                //await TestGet("Test5", "Get2");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }

            Console.Write("press enter key to finish");
            Console.ReadLine();
        }

        static async Task TestGet(string controller, string action)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/{controller}/{action}");

            await TestExecute($"{controller} - {action}", request);
        }

        static async Task TestPost(string controller, string action)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{url}/{controller}/{action}")
            {
                Content = new StringContent(
                    $"{{ \"InputMessage\": \"{controller} - {action}\" }}",
                    null,
                    "application/json"
                )
            };

            await TestExecute($"{controller} - {action}", request);
        }

        static async Task TestGetAll(string controller)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/{controller}");

            await TestExecute($"{controller}GetAll", request);
        }

        static async Task TestGetId(string controller)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/{controller}/1");

            await TestExecute($"{controller}GetId", request);
        }

        static async Task TestInsert(string controller)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{url}/{controller}");

            request.Content = new StringContent(
                @"{
                    ""fistName"":""Hello"",
                    ""lastName"":""World"",
                    ""email"":""hello@world.com""
                }",
                null,
                "application/json"
            );

            await TestExecute($"{controller}Insert", request);
        }

        static async Task TestUpdate(string controller)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{url}/{controller}/1");

            request.Content = new StringContent(
                @"{
                    ""fistName"":""Hello"",
                    ""lastName"":""World"",
                    ""email"":""hello@world.com""
                }",
                null,
                "application/json"
            );

            await TestExecute($"{controller}Update", request);
        }

        static async Task TestDelete(string controller)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}/{controller}/1");

            await TestExecute($"{controller}Delete", request);
        }

        static async Task TestValidation(string controller, string action, string content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{url}/{controller}/{action}");

            request.Content = new StringContent(content, null, "application/json");

            await TestExecute($"{controller} - {action}", request);
        }

        static async Task TestExecute(string test, HttpRequestMessage request)
        {
            var client = new HttpClient();

            var response = await client.SendAsync(request);

            var responseText = await response.Content.ReadAsStringAsync();

            var responseTextPrint = responseText;

            if (responseTextPrint == null)
                responseTextPrint = "*null*";
            else if (string.IsNullOrEmpty(responseTextPrint))
                responseTextPrint = "*empty*";
            else if (string.IsNullOrWhiteSpace(responseTextPrint))
                responseTextPrint = "*whiteSpace*";
            else
            {
                try
                {
                    var responseObject = JsonConvert.DeserializeObject(responseTextPrint);

                    responseTextPrint = JsonConvert.SerializeObject(
                        responseObject,
                        Formatting.Indented
                    );
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            Console.ForegroundColor = response.IsSuccessStatusCode
                ? ConsoleColor.Green
                : ConsoleColor.Red;

            Console.WriteLine($"{test} - {response.StatusCode}");
            Console.WriteLine(responseTextPrint);

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine();
        }
    }
};
