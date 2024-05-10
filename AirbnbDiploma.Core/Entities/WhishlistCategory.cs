namespace AirbnbDiploma.Core.Entities;

public class WhishlistCategory
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }
}
