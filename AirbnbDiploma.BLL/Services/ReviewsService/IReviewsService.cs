using AirbnbDiploma.Core.Dto.Reviews;

namespace AirbnbDiploma.BLL.Services.ReviewsService;

public interface IReviewsService
{
    public Task AddReviewByStayId(NewReviewDto reviewDto);

    public Task<IEnumerable<ReviewDto>> GetReviewsByStayIdAsync(int stayId, int skip, int count);

    public Task RemoveReviewById(int id);
}
