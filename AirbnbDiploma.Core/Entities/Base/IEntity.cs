namespace AirbnbDiploma.Core.Entities.Base;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}
