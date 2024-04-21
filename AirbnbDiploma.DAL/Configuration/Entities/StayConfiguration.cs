using AirbnbDiploma.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirbnbDiploma.DAL.Configuration.Entities;

public class StayConfiguration : IEntityTypeConfiguration<Stay>
{
    public void Configure(EntityTypeBuilder<Stay> builder)
    {
        builder
            .HasMany(stay => stay.Reviews)
            .WithOne()
            .HasForeignKey(review => review.StayId);
    }
}
