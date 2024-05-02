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
        public List<Error>? Errors { get; set; }

        public ResultClientError(List<Error>? errors)
        {
            Title = Result.ClientErrorTitle;
            Status = Result.ClientErrorCode;
            Errors = errors;
        }

        public ResultClientError()
            : this([]) { }

        public string ErrorsText => Error.ConvertToString(Errors);
    }
}
