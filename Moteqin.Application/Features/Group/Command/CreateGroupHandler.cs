using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Entity;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public CreateGroupHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

        var group = new Group
        {
            Name = request.Name,
            Description= request.Description,
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Groups.AddAsync(group);
        await _unitOfWork.SaveChangesAsync();

        var member = new GroupMember
        {
            GroupId = group.Id,
            UserId = userId,
            Role = GroupRole.Admin,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.GroupMembers.AddAsync(member);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Group created successfully");
    }
}