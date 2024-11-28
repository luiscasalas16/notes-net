using System.Text.Json.Serialization;

namespace NetApi.Common.Results
{
    public class ResultClientError
    {
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; }

        [JsonPropertyName("status")]
        public int Status { get; }

        [JsonPropertyName("errors")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ResultError>? Errors { get; set; }

        public ResultClientError(List<ResultError>? errors)
        {
            Title = Result.ClientErrorTitle;
            Status = Result.ClientErrorCode;
            Errors = errors;
        }

        public ResultClientError()
            : this([]) { }

        public string ErrorsText => ResultError.ConvertToString(Errors);
    }
}
