using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetFwApi.Common.Results
{
    public class ResultClientError
    {
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; }

        [JsonProperty("status")]
        public int Status { get; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<Error> Errors { get; set; }

        public ResultClientError(List<Error> errors)
        {
            Title = Result.ClientErrorTitle;
            Status = Result.ClientErrorCode;
            Errors = errors;
        }

        public ResultClientError()
            : this(new List<Error>()) { }

        public string ErrorsText => Error.ConvertToString(Errors);
    }
}
