using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class LeaveGroupHandler : IRequestHandler<LeaveGroupCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public LeaveGroupHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var member = await _unitOfWork.GroupMembers
            .FindAsync(x => x.GroupId == request.GroupId && x.UserId == userId);

        var entity = member.FirstOrDefault();

        if (entity == null)
            return Result<string>.Failure("Not a member");

        _unitOfWork.GroupMembers.Delete(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Left group successfully");
    }
}