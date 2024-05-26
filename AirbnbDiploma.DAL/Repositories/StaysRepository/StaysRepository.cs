using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.FilteringInfo;
using AirbnbDiploma.DAL.Filters;
using AirbnbDiploma.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.Repositories.StaysRepository;

public class StaysRepository : RepositoryBase<Stay, int>, IStaysRepository
{
    public StaysRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Stay>> GetAllFilteredAsync(StayFilteringInfo filter)
    {
        var stayFilter = new StayFilter(MainCollection.AsQueryable());
        return await stayFilter.ApplyFilters(filter).ToListAsync();
    }

    public override async Task<Stay> GetByIdAsync(int id)
    {
        var entity = await MainCollection
            .Include(stay => stay.Owner)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
        ThrowIfNull(id, entity);
        return entity;
    }
}
