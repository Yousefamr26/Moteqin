using MediatR;
using Moteqin.Domain.Common;

public class GetMyGroupsQuery : IRequest<Result<List<GroupDto>>>
{
}