namespace Engage.Application.Services.StorePOS.Commands;

public class UpdateStorePOSCommand : StorePOSCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateStorePOSCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStorePOSCommand, OperationStatus>
{
    public UpdateStorePOSCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateStorePOSCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StorePOS.SingleAsync(x => x.StorePOSId == request.Id);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StorePOSId;
        return opStatus;
    }
}
