using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetReport([FromQuery] string type = "weekly")
    {
        var result = await _mediator.Send(new GetUserReportQuery
        {
            Type = type
        });

        return Ok(result);
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeeklyReport()
    {
        var result = await _mediator.Send(new GetUserReportQuery
        {
            Type = "weekly"
        });

        return Ok(result);
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthlyReport()
    {
        var result = await _mediator.Send(new GetUserReportQuery
        {
            Type = "monthly"
        });

        return Ok(result);
    }
}