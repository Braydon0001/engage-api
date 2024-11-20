namespace Engage.Application.Services.StorePOS.Commands;

public class CreateStorePOSCommand : StorePOSCommand, IRequest<OperationStatus>
{

}

public class CreateStorePOSCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStorePOSCommand, OperationStatus>
{
    public CreateStorePOSCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateStorePOSCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateStorePOSCommand, Domain.Entities.StorePOS>(request);
        _context.StorePOS.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StorePOSId;
        return opStatus;
    }
}
