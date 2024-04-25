using AirbnbDiploma.BLL.Services.StaysService;
using AirbnbDiploma.Core.Dto.Stays;
using AirbnbDiploma.Core.Enums;
using AirbnbDiploma.Core.FilteringInfo;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> AddAsync(NewStayDto stayDto)
    {
        await _stayService.AddStayAsync(stayDto);
        return Created(ControllerContext.HttpContext.Request.Path, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StayBriefDto>>> GetAsync([FromQuery] StayFilteringInfo filteringInfo)
    {
        return Ok(await _stayService.GetStaysAsync(filteringInfo));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<StayDto>> GetAsync(int id)
    {
        return Ok(await _stayService.GetStayAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = Roles.User)]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        await _stayService.RemoveStayByIdAsync(id);
        return NoContent();
    }
}