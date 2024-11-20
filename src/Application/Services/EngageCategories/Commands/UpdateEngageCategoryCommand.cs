namespace Engage.Application.Services.EngageCategories.Commands;

public class UpdateEngageCategoryCommand : EngageCategoryCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEngageCategoryCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageCategoryCommand, OperationStatus>
{
    public UpdateEngageCategoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEngageCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageCategories.SingleAsync(e => e.Id == request.Id, cancellationToken);
        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
