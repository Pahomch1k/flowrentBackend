namespace AirbnbDiploma.Core.Dto.Reviews;

public class NewReviewDto
{
    public Guid StayId { get; set; }

    public byte Rating { get; set; }

    public DateTime CreationDate { get; set; }

    public int Duration { get; set; }

    public string Description { get; set; }
}
