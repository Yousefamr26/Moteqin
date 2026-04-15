public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<Error> Errors { get; set; } = new();

    public bool IsFailure => !IsSuccess;

    public static Result<T> Success(T data, string message = "Success")
        => new()
        {
            IsSuccess = true,
            Data = data,
            Message = message
        };

    public static Result<T> Failure(string message, List<Error> errors = null)
        => new()
        {
            IsSuccess = false,
            Message = message,
            Errors = errors ?? new List<Error>()
        };
}