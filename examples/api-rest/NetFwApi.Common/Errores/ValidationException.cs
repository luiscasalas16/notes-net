using System;

namespace NetFwApi.Common.Errores
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
