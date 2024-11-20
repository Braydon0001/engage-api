namespace Engage.Application.Services.WebEvents.Commands;

public class WebEventUpdateCommand : WebEventCommand, IRequest<OperationStatus>
{
}

public class WebEventUpdateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<WebEventUpdateCommand, OperationStatus>
{
    public WebEventUpdateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(WebEventUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebEvents.SingleAsync(x => x.WebEventId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
