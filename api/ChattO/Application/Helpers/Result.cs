namespace Application.Helpers;

public class Result<T>
{
    public bool IsSuccessful { get; }

    public string Message { get; }

    public T Data { get; }

    public Result(bool isSuccessful, T data)
    {
        IsSuccessful = isSuccessful;
        Data = data;
    }

    public Result(bool isSuccessful, string message)
    {
        IsSuccessful = isSuccessful;
        Message = message;
    }

    public Result(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }
    
}

public static class Result
{
    public static Result<T> Success<T>(T data) => new(true, data);
    public static Result<T> Success<T>() => new(true);
    public static Result<T> Failure<T>(string message) => new(false, message);
}
