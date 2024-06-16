using AirbnbDiploma.Core.Entities.Base;
using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Entities;
public class Tag : IEntity<int>
{
    public int Id { get; set; }

    public Guid StayId { get; set; }

    public TagCategory Category { get; set; }

    public int Type { get; set; }
}
