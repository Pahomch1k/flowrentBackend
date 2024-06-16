using AirbnbDiploma.Core.Dto.Stays;
using AirbnbDiploma.Core.FilteringInfo;

namespace AirbnbDiploma.BLL.Services.StaysService;

public interface IStayService
{
    Task AddStayAsync(NewStayDto stayDto);

    Task<IEnumerable<StayBriefDto>> GetStaysAsync(StayFilteringInfo filteringInfo);

    Task<IEnumerable<StayBriefDto>> GetStaysOfAuthorizedUser();

    Task<StayDto> GetStayAsync(Guid id);

    Task RemoveStayByIdAsync(Guid id);
}
