﻿namespace Application.Helpers;

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
