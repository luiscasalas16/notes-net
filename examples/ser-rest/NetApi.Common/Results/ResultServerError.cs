using System.Text.Json.Serialization;

namespace NetApi.Common.Results
{
    public class ResultServerError
    {
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; }

        [JsonPropertyName("status")]
        public int Status { get; }

        [JsonPropertyName("detail")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Detail { get; set; }

        public ResultServerError(Exception? exception)
            : this(exception?.ToString()) { }

        public ResultServerError(string? detail)
        {
            Title = Result.ServerErrorTitle;
            Status = Result.ServerErrorCode;
            Detail = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? detail : null;
        }

        public string DetailText
        {
            get
            {
                if (Detail != null)
                    return Detail;
                else
                {
                    if (Title != null)
                        return Title;
                    else
                        return string.Empty;
                }
            }
        }
    }
}
