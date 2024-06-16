using AirbnbDiploma.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AirbnbDiploma.DAL.Configuration.Entities;
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder
            .HasOne(booking => booking.Stay)
            .WithMany()
            .HasForeignKey(booking => booking.StayId);

        builder
            .HasOne(booking => booking.User)
            .WithMany()
            .HasForeignKey(booking => booking.UserId);
    }
}
