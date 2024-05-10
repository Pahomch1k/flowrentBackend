using AirbnbDiploma.Core.Dto.Whishlists;

namespace AirbnbDiploma.BLL.Services.WhishlistService;

public interface IWhishlistService
{
    Task AddCategoryAsync(string name);

    Task<IEnumerable<WhishlistCategoryDto>> GetCategoriesAsync();
}
