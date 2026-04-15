using MediatR;

public class JoinGroupCommand : IRequest<Result<string>>
{
    public int GroupId { get; set; }
}