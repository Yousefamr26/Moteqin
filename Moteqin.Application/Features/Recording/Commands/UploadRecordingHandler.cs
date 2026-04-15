using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moteqin.Domain.Interfaces;
using Moteqin.Domain.Entity;

public class UploadRecordingHandler
    : IRequestHandler<UploadRecordingCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IWebHostEnvironment _env;

    public UploadRecordingHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContext,
        IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
        _env = env;
    }

    public async Task<Result<string>> Handle(UploadRecordingCommand request, CancellationToken cancellationToken)
    {
      
        var userId = _httpContext.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result<string>.Failure("Unauthorized");

       
        var ayahsExist = await _unitOfWork.Ayahs.FindAsync(x =>
            x.Id >= request.AyahIdFrom &&
            x.Id <= request.AyahIdTo);

        if (!ayahsExist.Any())
            return Result<string>.Failure("Invalid Ayah range");

        
        var webRoot = _env.WebRootPath
                      ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        var uploadsFolder = Path.Combine(webRoot, "recordings");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

       
        var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        var fileUrl = $"/recordings/{fileName}";

       
        var recording = new Recording
        {
            UserId = userId,
            AyahIdFrom = request.AyahIdFrom,
            AyahIdTo = request.AyahIdTo,
            FileUrl = fileUrl,
            Duration = request.Duration,
            Status = RecordingStatus.Pending
        };

        await _unitOfWork.Recordings.AddAsync(recording);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Recording uploaded successfully");
    }
}