using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirbnbDiploma.DAL.Configuration.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = Guid.Parse("46e7ba0c-d5ca-4f12-abe8-cb6de810f922"),
            ImageUrl = "https://picsum.photos/300/300",
            Email = "example@domain.com",
            NormalizedEmail = "EXAMPLE@DOMAIN.COM",
            UserName = "Example",
            NormalizedUserName = "EXAMPLE",
            RegisteredAt = DateTime.UtcNow.AddYears(-2),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
            DateOfBirth = DateTime.UtcNow.AddYears(-20),
            Gender = GenderType.NonBinary,
            SecurityStamp = Guid.NewGuid().ToString(),
        });
    }
}
