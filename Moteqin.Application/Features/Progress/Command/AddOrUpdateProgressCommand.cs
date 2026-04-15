using MediatR;

public class AddOrUpdateProgressCommand : IRequest<Result<string>>
{
    public int AyahId { get; set; }
    public ProgressStatus Status { get; set; }

    public string? UserId { get; set; } 
}