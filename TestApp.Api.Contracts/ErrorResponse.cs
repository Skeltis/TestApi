namespace TestApp.Api.Contracts;

public class ErrorResponse
{
    public ErrorResponse(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

