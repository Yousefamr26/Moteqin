using MediatR;

public class CreateGroupCommand : IRequest<Result<string>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}