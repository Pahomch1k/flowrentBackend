using AirbnbDiploma.Core.Entities.Base;

namespace AirbnbDiploma.Core.Entities;

public class Review : IEntity<int>
{
    public int Id { get; set; }

    public Guid AuthorId { get; set; }

    public int StayId { get; set; }

    public byte Rating { get; set; }

    public DateTime CreationDate { get; set; }

    public int Duration { get; set; }

    public string Description { get; set; }
}