using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.Repositories.BookingRepository;
public class BookingRepository : RepositoryBase<Booking, int>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Booking>> GetAllByUserIdAsync(Guid userId)
    {
        var stayIds = Context.Stays
            .Where(stay => stay.OwnerId == userId)
            .Select(stay => stay.Id).ToList();

        return await Context.Countries
            .Include(country => country.User)
            .Include(country => country.Stay)
            .Where(booking => stayIds.Contains(booking.StayId))
            .ToListAsync();
    }
}
