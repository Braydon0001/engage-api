namespace Engage.Application.Services.EngageSubCategories.Commands;

public class CreateEngageSubCategoryCommand : EngageSubCategoryCommand, IRequest<OperationStatus>
{
}

public class CreateEngageSubCategoryCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageSubCategoryCommand, OperationStatus>
{
    public CreateEngageSubCategoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageSubCategoryCommand, EngageSubCategory>(request);
        _context.EngageSubCategories.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
