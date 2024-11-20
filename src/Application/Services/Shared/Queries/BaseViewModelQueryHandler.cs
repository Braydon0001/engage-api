namespace Engage.Application.Services.Shared.Queries;

public abstract class BaseViewModelQueryHandler
{
    protected readonly IMediator _mediator;

    protected BaseViewModelQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

}
