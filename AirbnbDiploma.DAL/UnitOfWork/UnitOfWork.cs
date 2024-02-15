using AirbnbDiploma.DAL.Repositories.BookingRepository;
using AirbnbDiploma.DAL.Repositories.ReviewRepository;
using AirbnbDiploma.DAL.Repositories.StaysRepository;
using AirbnbDiploma.DAL.Repositories.TagRepository;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        BookingRepository = new BookingRepository(dbContext);
        ReviewRepository = new ReviewRepository(dbContext);
        StaysRepository = new StaysRepository(dbContext);
        TagRepository = new TagRepository(dbContext);
    }

    public IBookingRepository BookingRepository { get; set; }
    public IReviewRepository ReviewRepository { get; set; }
    public IStaysRepository StaysRepository { get; set; }
    public ITagRepository TagRepository { get; set; }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task RejectChangesAsync()
    {
        foreach (var entry in _dbContext.ChangeTracker.Entries()
              .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    await entry.ReloadAsync();
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    break;
            }
        }
    }
}
