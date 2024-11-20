namespace Engage.Application.Services.EngageCategories.Commands;

public class CreateEngageCategoryCommand : EngageCategoryCommand, IRequest<OperationStatus>
{
}

public class CreateEngageCategoryCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageCategoryCommand, OperationStatus>
{
    public CreateEngageCategoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageCategoryCommand, EngageCategory>(request);
        _context.EngageCategories.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
