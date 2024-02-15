using AirbnbDiploma.Core.Entities.Base;
using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Entities;
public class Tag : IEntity<int>
{
    public int Id { get; set; }

    public int TagTypeId { get; set; }

    public string Name { get; set; }

    public TagCategory Category { get; set; }

    public TagType Type { get; set; }
}
