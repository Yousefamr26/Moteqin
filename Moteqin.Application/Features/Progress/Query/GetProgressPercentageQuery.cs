using MediatR;
using Moteqin.Domain.Common;

public class GetProgressPercentageQuery : IRequest<Result<double>>
{
    public string UserId { get; set; }

    public GetProgressPercentageQuery(string userId)
    {
        UserId = userId;
    }
}