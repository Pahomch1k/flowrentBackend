using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.FilteringInfo;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.StaysRepository;

public class StaysRepository : RepositoryBase<Stay, int>, IStaysRepository
{
    public StaysRepository(AppDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<Stay>> GetAllFilteredAsync(StayFilteringInfo filter)
    {
        throw new NotImplementedException();
    }
}
