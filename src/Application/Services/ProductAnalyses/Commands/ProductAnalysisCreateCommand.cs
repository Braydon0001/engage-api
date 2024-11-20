namespace Engage.Application.Services.ProductAnalyses.Commands;

public class ProductAnalysisCreateCommand : ProductAnalysisCommand, IMapTo<ProductAnalysis>, IRequest<OperationStatus>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisCreateCommand, ProductAnalysis>();
    }
}

public class ProductAnalysisCreateHandler : BaseCreateCommandHandler, IRequestHandler<ProductAnalysisCreateCommand, OperationStatus>
{
    public ProductAnalysisCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductAnalysisCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductAnalysisCreateCommand, ProductAnalysis>(command);
        _context.ProductAnalyses.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ProductAnalysisId;
        return opStatus;
    }
}

public class ProductAnalysisCreateValidator : ProductAnalysisValidator<ProductAnalysisCreateCommand>
{
    public ProductAnalysisCreateValidator()
    {
    }
}