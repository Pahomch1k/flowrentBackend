using AirbnbDiploma.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AirbnbDiploma.DAL;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
}
