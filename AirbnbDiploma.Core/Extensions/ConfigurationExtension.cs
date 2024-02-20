using AirbnbDiploma.Core.Constants;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.Extensions.Configuration;

namespace AirbnbDiploma.Core.Extensions;

public static class ConfigurationExtension
{
    public static string Get(this IConfiguration configuration, string key)
    {
        return configuration[key]
            ?? throw new ConfigurationException(ExceptionsMessages.GetMessage_ValueForKeyNotFound(key));
    }
}
