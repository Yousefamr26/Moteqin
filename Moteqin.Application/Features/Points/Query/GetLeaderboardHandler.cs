using MediatR;
using Moteqin.Domain.Common;
using Moteqin.Domain.Interfaces;

public class GetLeaderboardHandler : IRequestHandler<GetLeaderboardQuery, Result<List<LeaderboardDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLeaderboardHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<LeaderboardDto>>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.Points
            .GetLeaderboardAsync(request.Take);

        return Result<List<LeaderboardDto>>.Success(data);
    }
}