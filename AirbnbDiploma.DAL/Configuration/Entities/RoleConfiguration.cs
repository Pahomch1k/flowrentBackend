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
            Id = Guid.Parse("e13c14cb-0051-41bb-aeb3-6b121c7f5ce7"),
            ConcurrencyStamp = Guid.Parse("24f9cc94-c835-403d-9d49-a81caec56e53").ToString(),
            Name = "User",
            NormalizedName = "USER",
        },
        new Role
        {
            Id = Guid.Parse("a6705c7c-9dee-4ef3-a478-107b1dab3a6f"),
            ConcurrencyStamp = Guid.Parse("34c3de4e-2ff1-40e8-bb5e-b9ff1970a385").ToString(),
            Name = "Admin",
            NormalizedName = "ADMIN"
        });
    }
}
