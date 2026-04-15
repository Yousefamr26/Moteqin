using MediatR;
using Moteqin.Domain.Common;

public class LeaveGroupCommand : IRequest<Result<string>>
{
    public int GroupId { get; set; }
}