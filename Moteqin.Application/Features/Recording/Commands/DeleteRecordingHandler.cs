using MediatR;
using Microsoft.AspNetCore.Hosting;
using Moteqin.Domain.Interfaces;

public class DeleteRecordingHandler
    : IRequestHandler<DeleteRecordingCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env;

    public DeleteRecordingHandler(
        IUnitOfWork unitOfWork,
        IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Result<string>> Handle(DeleteRecordingCommand request, CancellationToken cancellationToken)
    {
        
        var recording = await _unitOfWork.Recordings.GetByIdAsync(request.RecordingId);

        if (recording == null)
            return Result<string>.Failure("Recording not found");

        
        var filePath = Path.Combine(
            _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
            recording.FileUrl.TrimStart('/')
        );

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

       
        _unitOfWork.Recordings.Delete(recording);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Success("Recording deleted successfully");
    }
}