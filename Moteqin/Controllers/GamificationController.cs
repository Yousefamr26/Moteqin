using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/gamification")]
public class GamificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public GamificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("activity")]
    public async Task<IActionResult> AddActivity(AddUserActivityCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("my-stats")]
    public async Task<IActionResult> GetMyStats()
    {
        var result = await _mediator.Send(new GetMyStatsQuery());
        return Ok(result);
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard([FromQuery] int take = 10)
    {
        var result = await _mediator.Send(new GetLeaderboardQuery
        {
            Take = take
        });

        return Ok(result);
    }
    [HttpGet("my-rank")]
    public async Task<IActionResult> GetMyRank()
    {
        return Ok(await _mediator.Send(new GetMyRankQuery()));
    }

    [HttpGet("can-earn")]
    public async Task<IActionResult> CanEarn()
    {
        return Ok(await _mediator.Send(new CanEarnPointsQuery()));
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        return Ok(await _mediator.Send(new GetGamificationDashboardQuery()));
    }
}