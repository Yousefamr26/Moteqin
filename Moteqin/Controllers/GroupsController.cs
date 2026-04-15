using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/groups")]
[Authorize]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGroupCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(new { success = true, data = result });
    }

    [HttpPost("{groupId}/join")]
    public async Task<IActionResult> Join(int groupId)
    {
        var result = await _mediator.Send(new JoinGroupCommand { GroupId = groupId });

        return Ok(new { success = true, data = result });
    }

    [HttpDelete("{groupId}/leave")]
    public async Task<IActionResult> Leave(int groupId)
    {
        var result = await _mediator.Send(new LeaveGroupCommand { GroupId = groupId });

        return Ok(new { success = true, data = result });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyGroups()
    {
        var result = await _mediator.Send(new GetMyGroupsQuery());

        return Ok(new { success = true, data = result });
    }
}