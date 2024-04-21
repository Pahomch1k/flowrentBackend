namespace AirbnbDiploma.Core.Dto.Reviews;

public class ReviewDto
{
    public byte Rating { get; set; }

    public DateTime CreationDate { get; set; }

    public int Duration { get; set; }

    public string Description { get; set; }
}
