namespace NetFwApi.Common.Results
{
    public class ResultMessage
    {
        public string Message { get; set; }

        public ResultMessage(string message)
        {
            Message = message;
        }
    }
}
