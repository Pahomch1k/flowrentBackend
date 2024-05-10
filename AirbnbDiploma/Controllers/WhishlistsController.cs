using AirbnbDiploma.BLL.Services.WhishlistService;
using AirbnbDiploma.Core.Constants;
using AirbnbDiploma.Core.Dto.Whishlists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WhishlistsController : ControllerBase
{
    private readonly IWhishlistService _whishlistService;

    public WhishlistsController(IWhishlistService whishlistService)
    {
        _whishlistService = whishlistService;
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.User)]
    public async Task<ActionResult> AddCategory(NewWhishlistCategoryDto newWhishlistCategory)
    {
        await _whishlistService.AddCategoryAsync(newWhishlistCategory.Name);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = RoleNames.User)]
    public async Task<ActionResult<IEnumerable<WhishlistCategoryDto>>> GetCategories()
    {
        return Ok(await _whishlistService.GetCategoriesAsync());
    }
}
