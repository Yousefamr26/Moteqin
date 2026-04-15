using MediatR;
using Microsoft.AspNetCore.Http;
using Moteqin.Domain.Entity;
using Moteqin.Domain.Interfaces;
using System.Security.Claims;

public class AddOrUpdateProgressHandler
    : IRequestHandler<AddOrUpdateProgressCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public AddOrUpdateProgressHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<Result<string>> Handle(AddOrUpdateProgressCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

     
        var ayah = await _unitOfWork.Ayahs.GetByIdAsync(request.AyahId);

        if (ayah == null)
            return Result<string>.Failure("Ayah not found");

       
        var existing = (await _unitOfWork.UserProgresses
            .FindAsync(x => x.UserId == userId && x.AyahId == request.AyahId))
            .FirstOrDefault();

     
        if (existing == null)
        {
            var progress = new UserProgress
            {
                UserId = userId,
                AyahId = request.AyahId,
                Status = request.Status,
                MemorizedAt = request.Status == ProgressStatus.Memorized ? DateTime.UtcNow : null,
                LastReviewedAt = request.Status == ProgressStatus.Reviewing ? DateTime.UtcNow : null
            };

            await _unitOfWork.UserProgresses.AddAsync(progress);
        }
        else
        {
            existing.Status = request.Status;

            if (request.Status == ProgressStatus.Memorized)
                existing.MemorizedAt = DateTime.UtcNow;

            if (request.Status == ProgressStatus.Reviewing)
                existing.LastReviewedAt = DateTime.UtcNow;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Progress updated successfully");
    }
}