using System;
using Newtonsoft.Json;

namespace NetFwApi.Common.Results
{
    public class ResultServerError
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; }

        [JsonProperty("status")]
        public int Status { get; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        public ResultServerError(Exception exception)
            : this(exception?.ToString()) { }

        public ResultServerError(string detail)
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
