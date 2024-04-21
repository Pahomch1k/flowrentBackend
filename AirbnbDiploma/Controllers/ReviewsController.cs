using AirbnbDiploma.BLL.Services.ReviewsService;
using AirbnbDiploma.Core.Dto.Reviews;
using AirbnbDiploma.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewsService _reviewsService;

    public ReviewsController(IReviewsService reviewsService)
    {
        _reviewsService = reviewsService;
    }

    [HttpPost]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> AddAsync(NewReviewDto stayDto)
    {
        await _reviewsService.AddReviewByStayId(stayDto);
        return Created(ControllerContext.HttpContext.Request.Path, null);
    }

    [HttpGet]
    [Route("/Stays/{id}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAsync(int id, int skip = 0, int count = int.MaxValue)
    {
        return Ok(await _reviewsService.GetReviewsByStayIdAsync(id, skip, count));
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        await _reviewsService.RemoveReviewById(id);
        return NoContent();
    }
}
