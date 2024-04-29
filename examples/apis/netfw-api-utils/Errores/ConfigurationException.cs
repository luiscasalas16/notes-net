using System;

namespace netfw_api_utils.Errores
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message)
            : base(message) { }

        public ConfigurationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
