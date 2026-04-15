using MediatR;

public class AddUserActivityCommand : IRequest<Result<bool>>
{
    public int Points { get; set; }
    public string Reason { get; set; }
}