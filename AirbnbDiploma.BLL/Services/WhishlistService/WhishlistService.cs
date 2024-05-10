using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Whishlists;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.UnitOfWork;

namespace AirbnbDiploma.BLL.Services.WhishlistService;

public class WhishlistService : IWhishlistService
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public WhishlistService(IUserService userService, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
    }

    public async Task AddCategoryAsync(string name)
    {
        WhishlistCategory category = new()
        {
            Name = name,
            UserId = _userService.GetUserId()
        };

        _unitOfWork.WhishlistRepository.AddCategory(category);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<WhishlistCategoryDto>> GetCategoriesAsync()
    {
        var id = _userService.GetUserId();
        var categories = await _unitOfWork.WhishlistRepository.GetCategoriesAsync(id);
        return categories.Select(category => new WhishlistCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Count = 0
        });
    }
}
