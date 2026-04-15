using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/feedback")]
[Authorize]
public class FeedbackController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeedbackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId()
        => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

   
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddFeedbackCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(new
        {
            success = true,
            data = result
        });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyFeedback()
    {
        var userId = GetUserId();

        var result = await _mediator.Send(new GetUserFeedbackQuery(userId));

        return Ok(new
        {
            success = true,
            data = result
        });
    }

   
    [HttpGet("recording/{recordingId}")]
    public async Task<IActionResult> GetByRecording(int recordingId)
    {
        var result = await _mediator.Send(new GetFeedbackByRecordingQuery(recordingId));

        return Ok(new
        {
            success = true,
            data = result
        });
    }
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateFeedbackStatusCommand command)
    {
        command.FeedbackId = id;

        var result = await _mediator.Send(command);

        return Ok(new
        {
            success = true,
            data = result
        });
    }
}