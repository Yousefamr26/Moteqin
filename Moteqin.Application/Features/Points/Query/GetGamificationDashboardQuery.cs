using MediatR;
using Moteqin.Domain.Common;

public class GetGamificationDashboardQuery : IRequest<Result<GamificationDashboardDto>>
{
}