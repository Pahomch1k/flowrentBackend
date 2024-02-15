﻿using Microsoft.AspNetCore.Identity;

namespace AirbnbDiploma.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? GovernmentId { get; set; }

    public string? Address { get; set; }
}