using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.ReviewRepository;

public class ReviewRepository : RepositoryBase<Review, int>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }
}
