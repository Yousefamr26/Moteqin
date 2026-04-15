public class Result
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public List<Error> Errors { get; set; } = new();

    public bool IsFailure => !IsSuccess;

    public static Result Success(string message = "Success")
        => new()
        {
            IsSuccess = true,
            Message = message
        };

    public static Result Failure(string message, List<Error> errors = null)
        => new()
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<Error>()
        };
}