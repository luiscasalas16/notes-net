using System.ComponentModel.DataAnnotations;
using NetApi.Common.Results;

namespace NetApi.Common.Errores
{
    public class ValidationException : ApplicationException
    {
        public List<ResultError> Errors { get; }

        public ValidationException()
            : base()
        {
            Errors = [];
        }

        public ValidationException(List<ValidationResult> results)
            : this()
        {
            Errors = results.Select(failure => new ResultError(string.Join(',', failure.MemberNames), "", failure.ErrorMessage)).ToList();
        }
    }
}
