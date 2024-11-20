namespace Engage.Application.Services.WebEvents.Commands
{
    public class WebEventCreateCommand : WebEventCommand, IRequest<OperationStatus>
    {
    }

    public class WebEventCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<WebEventCreateCommand, OperationStatus>
    {
        public WebEventCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
        {
        }

        public async Task<OperationStatus> Handle(WebEventCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<WebEventCreateCommand, WebEvent>(command);
            _context.WebEvents.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
