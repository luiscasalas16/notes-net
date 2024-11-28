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
        public List<ResultError> Errors { get; set; }

        public ResultClientError(List<ResultError> errors)
        {
            Title = Result.ClientErrorTitle;
            Status = Result.ClientErrorCode;
            Errors = errors;
        }

        public ResultClientError()
            : this(new List<ResultError>()) { }

        public string ErrorsText => ResultError.ConvertToString(Errors);
    }
}
