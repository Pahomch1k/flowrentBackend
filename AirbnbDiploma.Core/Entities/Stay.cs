﻿using AirbnbDiploma.Core.Entities.Base;

namespace AirbnbDiploma.Core.Entities;

public class Stay : IEntity<int>
{
    public int Id { get; set; }

    public Guid OwnerId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string CoverImageUrl { get; set; }

    public string Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int MaxGuests { get; set; }

    public int Price { get; set; }

    public int CleaningFee { get; set; }

    public float OverallRating { get; set; }

    public byte CleanlinessRating { get; set; }

    public byte AccuracyRating { get; set; }

    public byte CheckInRating { get; set; }

    public byte CommunicationRating { get; set; }

    public byte LocationRating { get; set; }

    public byte ValueRating { get; set; }

    public ICollection<Image> ImageUrls { get; set; } = new List<Image>();

    public ICollection<Tag> Amenities { get; set; } = new List<Tag>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}