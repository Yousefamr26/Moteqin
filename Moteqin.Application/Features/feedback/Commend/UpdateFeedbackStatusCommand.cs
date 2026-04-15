using MediatR;
using Moteqin.Domain.Entity;

public class UpdateFeedbackStatusCommand : IRequest<Result<string>>
{
    public int FeedbackId { get; set; }
    public FeedbackStatus Status { get; set; }
}