namespace Engage.Application.Services.ProductAnalyses.Commands;

public class ProductAnalysisUpdateCommand : ProductAnalysisCommand, IMapTo<ProductAnalysis>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisUpdateCommand, ProductAnalysis>();
    }
}

public class ProductAnalysisUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<ProductAnalysisUpdateCommand, OperationStatus>
{
    public ProductAnalysisUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductAnalysisUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductAnalyses.SingleAsync(x => x.ProductAnalysisId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ProductAnalysisId;
        return opStatus;
    }
}

public class ProductAnalysisUpdateValidator : ProductAnalysisValidator<ProductAnalysisUpdateCommand>
{
    public ProductAnalysisUpdateValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}