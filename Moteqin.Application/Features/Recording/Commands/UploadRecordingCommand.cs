using MediatR;
using Microsoft.AspNetCore.Http;

public class UploadRecordingCommand : IRequest<Result<string>>
{
    public IFormFile File { get; set; }

    public int AyahIdFrom { get; set; }
    public int AyahIdTo { get; set; }

    public int Duration { get; set; }
}