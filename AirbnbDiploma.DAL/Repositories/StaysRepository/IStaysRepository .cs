using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.FilteringInfo;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.StaysRepository;

public interface IStaysRepository : IRepository<Stay, int>
{
    Task<IEnumerable<Stay>> GetAllFilteredAsync(StayFilteringInfo filter);

    Task<IEnumerable<Stay>> GetAllByOwnerId(Guid id);
}
