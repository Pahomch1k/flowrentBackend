using AirbnbDiploma.Core.Dto.Stays;

namespace AirbnbDiploma.BLL.Services.StaysService;

public interface IStayService
{
    Task AddStayAsync(NewStayDto stayDto);

    Task<IEnumerable<StayBriefDto>> GetStaysAsync(int skip, int count);

    Task<StayDto> GetStayAsync(int id);

    Task RemoveStayByIdAsync(int id);
}
