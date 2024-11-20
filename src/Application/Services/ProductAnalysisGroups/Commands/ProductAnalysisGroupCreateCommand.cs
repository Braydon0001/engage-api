namespace Engage.Application.Services.ProductAnalysisGroups.Commands;

public class ProductAnalysisGroupCreateCommand : ProductAnalysisGroupCommand, IMapTo<ProductAnalysisGroup>, IRequest<OperationStatus>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisGroupCreateCommand, ProductAnalysisGroup>();
    }
}

public class ProductAnalysisGroupCreateHandler : BaseCreateCommandHandler, IRequestHandler<ProductAnalysisGroupCreateCommand, OperationStatus>
{
    public ProductAnalysisGroupCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductAnalysisGroupCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductAnalysisGroupCreateCommand, ProductAnalysisGroup>(command);
        _context.ProductAnalysisGroups.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}

public class ProductAnalysisGroupCreateValidator : ProductAnalysisGroupValidator<ProductAnalysisGroupCreateCommand>
{
    public ProductAnalysisGroupCreateValidator()
    {
    }
}