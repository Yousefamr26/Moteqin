using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;
using Moteqin.Domain.Entity;

public class JoinGroupHandler : IRequestHandler<JoinGroupCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public JoinGroupHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

        var group = await _unitOfWork.Groups.GetByIdAsync(request.GroupId);
        if (group == null)
            return Result<string>.Failure("Group not found");

        var exists = await _unitOfWork.GroupMembers
            .FindAsync(x => x.GroupId == request.GroupId && x.UserId == userId);

        if (exists.Any())
            return Result<string>.Failure("Already joined");

        var member = new GroupMember
        {
            GroupId = request.GroupId,
            UserId = userId,
            Role = GroupRole.Member,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.GroupMembers.AddAsync(member);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Joined group successfully");
    }
}