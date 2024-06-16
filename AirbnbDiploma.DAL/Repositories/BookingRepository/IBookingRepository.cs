using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.BookingRepository;

public interface IBookingRepository : IRepository<Booking, int>
{
    Task<IEnumerable<Booking>> GetAllByUserIdAsync(Guid userId);
}
