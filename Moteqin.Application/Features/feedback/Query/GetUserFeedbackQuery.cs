using MediatR;

public record GetUserFeedbackQuery(string UserId)
    : IRequest<Result<List<FeedbackDto>>>;