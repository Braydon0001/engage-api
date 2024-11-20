using MediatR.Pipeline;

namespace Engage.Application.Behaviours;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly IUserService _user;

    public RequestLogger(ILogger<TRequest> logger, IUserService user)
    {
        _logger = logger;
        _user = user;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task Process(TRequest request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var requestName = typeof(TRequest).Name;
        var userName = _user.UserName;

        _logger.LogInformation("Engage Request: {Name} {@UserName} {@Request}", requestName, userName, request);
    }
}
