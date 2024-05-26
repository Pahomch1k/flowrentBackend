using AirbnbDiploma.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirbnbDiploma.DAL.Configuration.Entities;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role
        {
            Id = Guid.NewGuid(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Name = "User",
            NormalizedName = "USER",
        },
        new Role
        {
            Id = Guid.NewGuid(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Name = "Admin",
            NormalizedName = "ADMIN"
        });
    }
}
