using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetMyGroupsHandler : IRequestHandler<GetMyGroupsQuery, Result<List<GroupDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public GetMyGroupsHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<List<GroupDto>>> Handle(GetMyGroupsQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var groups = await _unitOfWork.GroupMembers
            .GetQueryable()
            .Include(x => x.Group)
            .Where(x => x.UserId == userId)
            .Select(x => new GroupDto
            {
                Id = x.Group.Id,
                Name = x.Group.Name,
                Description = x.Group.Description,
                CreatedByUserId = x.Group.CreatedByUserId
            })
            .ToListAsync(cancellationToken);

        return Result<List<GroupDto>>.Success(groups);
    }
}