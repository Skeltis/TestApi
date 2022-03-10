using Microsoft.Extensions.Logging;

namespace TestApp.BLL.Services;

public abstract class BaseService<T>
{
    protected readonly ILogger<T> Logger;

    protected BaseService(ILogger<T> log)
    {
        Logger = log ?? throw new ArgumentNullException(nameof(log));
    }
}

