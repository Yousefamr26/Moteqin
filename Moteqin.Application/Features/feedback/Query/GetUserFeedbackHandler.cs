using MediatR;
using Moteqin.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class GetUserFeedbackHandler
    : IRequestHandler<GetUserFeedbackQuery, Result<List<FeedbackDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserFeedbackHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<FeedbackDto>>> Handle(GetUserFeedbackQuery request, CancellationToken cancellationToken)
    {
       
        var feedbacks = await _unitOfWork.Feedbacks
            .GetAllWithRecordingAsync(); 

        
        var data = feedbacks
            .Where(x => x.Recording != null && x.Recording.UserId == request.UserId)
            .Select(x => new FeedbackDto
            {
                RecordingId = x.RecordingId,
                Comment = x.CommentText,
                Status = x.Status.ToString()
            })
            .ToList();

        return Result<List<FeedbackDto>>.Success(data);
    }
}