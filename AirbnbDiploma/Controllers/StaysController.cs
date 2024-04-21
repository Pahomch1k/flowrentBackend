using AirbnbDiploma.BLL.Services.StaysService;
using AirbnbDiploma.Core.Dto.Stays;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaysController : ControllerBase
{
    private readonly IStayService _stayService;

    public StaysController(IStayService stayService)
    {
        _stayService = stayService;
    }

    [HttpPost]
    public async Task<ActionResult> AddAsync(NewStayDto stayDto)
    {
        await _stayService.AddStayAsync(stayDto);
        return Created(ControllerContext.HttpContext.Request.Path, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StayBriefDto>>> GetAsync(int skip = 0, int count = int.MaxValue)
    {
        return Ok(await _stayService.GetStaysAsync(skip, count));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<StayDto>> GetAsync(int id)
    {
        return Ok(await _stayService.GetStayAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        await _stayService.RemoveStayByIdAsync(id);
        return NoContent();
    }
}