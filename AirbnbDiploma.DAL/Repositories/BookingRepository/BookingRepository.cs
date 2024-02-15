using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.BookingRepository;
public class BookingRepository : RepositoryBase<Booking, int>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context)
    {
    }
}
