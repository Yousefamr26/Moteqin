using MediatR;
using Moteqin.Domain.Common;

public class GetMyRankQuery : IRequest<Result<MyRankDto>>
{
}