using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AirbnbDiploma.DAL.Repositories.ReviewRepository;

public class ReviewRepository : RepositoryBase<Review, int>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Review>> GetAllByStayIdAsync(int stayId, int skip, int count)
    {
        return await Context.Reviews.Where(review => review.StayId == stayId).Skip(skip).Take(count).ToListAsync();
    }
}
