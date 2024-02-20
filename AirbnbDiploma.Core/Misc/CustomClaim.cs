using System.Security.Claims;

namespace AirbnbDiploma.Core.Misc;

public class CustomClaim : Claim
{
    public CustomClaim(string type, string value)
        : base(type, value)
    {
    }

    public override bool Equals(object? obj)
    {
        return obj is CustomClaim claim && Type == claim.Type && Value == claim.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }
}
