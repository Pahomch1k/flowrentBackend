using AirbnbDiploma.DAL.Repositories.BookingRepository;
using AirbnbDiploma.DAL.Repositories.ReviewRepository;
using AirbnbDiploma.DAL.Repositories.StaysRepository;
using AirbnbDiploma.DAL.Repositories.TagRepository;
using AirbnbDiploma.DAL.Repositories.WhishlistRepository;

namespace AirbnbDiploma.DAL.UnitOfWork;

public interface IUnitOfWork
{
    public IBookingRepository BookingRepository { get; set; }

    public IReviewRepository ReviewRepository { get; set; }

    public IStaysRepository StaysRepository { get; set; }

    public ITagRepository TagRepository { get; set; }

    public IWhishlistRepository WhishlistRepository { get; set; }

    Task CommitAsync();

    Task RejectChangesAsync();
}
