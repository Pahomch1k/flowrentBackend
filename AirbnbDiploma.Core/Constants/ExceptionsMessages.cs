#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace AirbnbDiploma.Core.Constants;

public static class ExceptionsMessages
{
    public const string IncorrectLoginOrPassword = "Incorrect login or password.";

    public static string GetMessage_ValueForKeyNotFound(string key)
        => $"Value for key {key} not found";
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
