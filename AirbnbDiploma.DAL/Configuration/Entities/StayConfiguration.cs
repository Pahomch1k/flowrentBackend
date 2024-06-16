using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Enums;
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

        SeedData(builder);
    }

    public static void SeedData(EntityTypeBuilder<Stay> builder)
    {
        builder.HasData(
            new Stay
            {
                Id = Guid.Parse("ff6effab-0f3d-4a2a-b7c3-9ea770f75bd3"),
                OwnerId = Guid.Parse("46e7ba0c-d5ca-4f12-abe8-cb6de810f922"),
                Title = "Cozy Mountain Cabin",
                Description = "A cozy cabin in the mountains with stunning views.",
                CoverImageUrl = "https://picsum.photos/300/300",
                Location = "Asheville, NC",
                StartDate = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                RegionId = 5,
                MaxGuests = 4,
                Beds = 2,
                Bedrooms = 1,
                Bathrooms = 1,
                Price = 150,
                CleaningFee = 50,
                InstantBook = true,
                SelfCheckIn = true,
                PropertyType = PropertyType.Any,
                PlaceType = PlaceType.EntirePlace,
                OverallRating = 4.8f,
                CleanlinessRating = 4.9f,
                AccuracyRating = 4.7f,
                CheckInRating = 4.8f,
                CommunicationRating = 4.9f,
                LocationRating = 4.8f,
                ValueRating = 4.7f,
            },
            new Stay
            {
                Id = Guid.Parse("ebc8b6d9-af0d-45a8-bf11-9c6608116ba7"),
                OwnerId = Guid.Parse("46e7ba0c-d5ca-4f12-abe8-cb6de810f922"),
                Title = "Beachfront Bungalow",
                Description = "A beautiful bungalow right on the beach.",
                CoverImageUrl = "https://picsum.photos/300/300",
                Location = "Malibu, CA",
                StartDate = new DateTime(2024, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 7, 20, 0, 0, 0, DateTimeKind.Utc),
                RegionId = 2,
                MaxGuests = 6,
                Beds = 3,
                Bedrooms = 2,
                Bathrooms = 2,
                Price = 300,
                CleaningFee = 75,
                InstantBook = false,
                SelfCheckIn = false,
                PropertyType = PropertyType.Any,
                PlaceType = PlaceType.EntirePlace,
                OverallRating = 4.9f,
                CleanlinessRating = 5.0f,
                AccuracyRating = 4.8f,
                CheckInRating = 4.7f,
                CommunicationRating = 4.9f,
                LocationRating = 5.0f,
                ValueRating = 4.6f,
            },
            new Stay
            {
                Id = Guid.Parse("5055883b-7c4f-41a3-9709-fe7aed818124"),
                OwnerId = Guid.Parse("46e7ba0c-d5ca-4f12-abe8-cb6de810f922"),
                Title = "Modern City Apartment",
                Description = "A sleek apartment in the heart of downtown.",
                CoverImageUrl = "https://picsum.photos/300/300",
                Location = "New York, NY",
                StartDate = new DateTime(2024, 8, 5, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 8, 15, 0, 0, 0, DateTimeKind.Utc),
                RegionId = 1,
                MaxGuests = 3,
                Beds = 1,
                Bedrooms = 1,
                Bathrooms = 1,
                Price = 200,
                CleaningFee = 60,
                InstantBook = true,
                SelfCheckIn = true,
                PropertyType = PropertyType.Any,
                PlaceType = PlaceType.EntirePlace,
                OverallRating = 4.7f,
                CleanlinessRating = 4.8f,
                AccuracyRating = 4.6f,
                CheckInRating = 4.9f,
                CommunicationRating = 4.7f,
                LocationRating = 5.0f,
                ValueRating = 4.5f,
            },
            new Stay
            {
                Id = Guid.Parse("4624dc54-c728-436d-a907-7b357b6649e2"),
                OwnerId = Guid.Parse("46e7ba0c-d5ca-4f12-abe8-cb6de810f922"),
                Title = "Rustic Farmhouse",
                Description = "Experience life on a farm with modern amenities.",
                CoverImageUrl = "https://picsum.photos/300/300",
                Location = "Nashville, TN",
                StartDate = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 9, 15, 0, 0, 0, DateTimeKind.Utc),
                RegionId = 3,
                MaxGuests = 8,
                Beds = 4,
                Bedrooms = 3,
                Bathrooms = 2,
                Price = 250,
                CleaningFee = 70,
                InstantBook = false,
                SelfCheckIn = true,
                PropertyType = PropertyType.Any,
                PlaceType = PlaceType.EntirePlace,
                OverallRating = 4.6f,
                CleanlinessRating = 4.7f,
                AccuracyRating = 4.5f,
                CheckInRating = 4.6f,
                CommunicationRating = 4.7f,
                LocationRating = 4.8f,
                ValueRating = 4.4f,
            },
            new Stay
            {
                Id = Guid.Parse("0b30dfe1-46a5-4be9-9192-00831a78404e"),
                OwnerId = Guid.Parse("46e7ba0c-d5ca-4f12-abe8-cb6de810f922"),
                Title = "Luxury Villa",
                Description = "A stunning villa with a private pool and ocean views.",
                CoverImageUrl = "https://picsum.photos/300/300",
                Location = "Maui, HI",
                StartDate = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                RegionId = 6,
                MaxGuests = 10,
                Beds = 5,
                Bedrooms = 4,
                Bathrooms = 4,
                Price = 500,
                CleaningFee = 100,
                InstantBook = true,
                SelfCheckIn = false,
                PropertyType = PropertyType.Any,
                PlaceType = PlaceType.EntirePlace,
                OverallRating = 4.9f,
                CleanlinessRating = 5.0f,
                AccuracyRating = 4.9f,
                CheckInRating = 4.8f,
                CommunicationRating = 4.9f,
                LocationRating = 5.0f,
                ValueRating = 4.7f,
            });
    }
}
