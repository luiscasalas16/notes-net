using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NetFwApi.Common.Results
{
    public class Error
    {
        [JsonProperty("property", NullValueHandling = NullValueHandling.Ignore)]
        public string Property { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public Error(string property, string code, string message)
        {
            Property = property;
            Code = code;
            Message = message;
        }

        public Error(string code, string message)
            : this(null, code, message) { }

        public Error()
            : this(null, null, null) { }

        public override string ToString()
        {
            if (Property != null)
            {
                if (Code != null)
                    return $"{Property} : {Code} : {Message}";
                else
                    return $"{Property} : {Message}";
            }
            else
            {
                if (Code != null)
                    return $"{Code} : {Message}";
                else
                    return $"{Message}";
            }
        }

        public static string ConvertToString(Error error) => error != null ? error.ToString() : string.Empty;

        public static string ConvertToString(List<Error> errors) => errors != null ? string.Join(" - ", errors.Select(t => t.ToString())) : string.Empty;
    }
}
