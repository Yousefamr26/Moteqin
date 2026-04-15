public static class ResultExtensions
{
    public static Result<T> ToSuccess<T>(this T data, string message = "Success")
        => Result<T>.Success(data, message);

    public static Result<T> ToFailure<T>(this string message)
        => Result<T>.Failure(message);

    public static Error ToError(this string message, string code = "GENERAL_ERROR")
        => new Error(code, message);
}