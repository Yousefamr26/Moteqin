using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/recordings")]
[Authorize]
public class RecordingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecordingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private string GetUserId()
        => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadRecordingCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(new { success = true, data = result });
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetUserRecordingsQuery());

        return Ok(new { success = true, data = result });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteRecordingCommand(id));

        return Ok(new { success = true, data = result });
    }
}