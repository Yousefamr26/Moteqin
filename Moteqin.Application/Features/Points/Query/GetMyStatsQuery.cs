using MediatR;
using Moteqin.Domain.Common;

public class GetMyStatsQuery : IRequest<Result<MyStatsDto>>
{
}