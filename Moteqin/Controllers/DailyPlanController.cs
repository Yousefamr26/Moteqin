using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/daily-plan")]
[Authorize]
public class DailyPlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public DailyPlanController(IMediator mediator)
    {
        _mediator = mediator;
    }

  
    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] GenerateDailyPlanCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(new
        {
            success = true,
            data = result
        });
    }


    [HttpGet]
    public async Task<IActionResult> GetMyPlans()
    {
        var result = await _mediator.Send(new GetUserPlansQuery());

        return Ok(new
        {
            success = true,
            data = result
        });
    }

   
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var command = new CompleteDailyPlanCommand
        {
            PlanId = id
        };

        var result = await _mediator.Send(command);

        return Ok(new
        {
            success = true,
            data = result
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteDailyPlanCommand
        {
            PlanId = id
        };

        var result = await _mediator.Send(command);

        return Ok(new
        {
            success = true,
            data = result
        });
    }
}