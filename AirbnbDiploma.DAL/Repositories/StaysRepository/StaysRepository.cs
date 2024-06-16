using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.FilteringInfo;
using AirbnbDiploma.DAL.Filters;
using AirbnbDiploma.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.Repositories.StaysRepository;

public class StaysRepository : RepositoryBase<Stay, Guid>, IStaysRepository
{
    public StaysRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Stay>> GetAllFilteredAsync(StayFilteringInfo filter)
    {
        var stayFilter = new StayFilter(MainCollection.AsQueryable());
        return await stayFilter.ApplyFilters(filter).ToListAsync();
    }

    public async Task<IEnumerable<Stay>> GetAllByOwnerId(Guid id)
    {
        return await MainCollection.Where(stay => stay.OwnerId == id).ToListAsync();
    }

    public override async Task<Stay> GetByIdAsync(Guid id)
    {
        var entity = await MainCollection
            .Include(stay => stay.Owner)
            .Include(stay => stay.Tags)
            .Include(stay => stay.ImageUrls)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
        ThrowIfNull(id, entity);
        return entity;
    }
}
