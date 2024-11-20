namespace Engage.Application.Services.ProductAnalysisDivisions.Commands;

public class ProductAnalysisDivisionCreateCommand : ProductAnalysisDivisionCommand, IMapTo<ProductAnalysisDivision>, IRequest<OperationStatus>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisDivisionCreateCommand, ProductAnalysisDivision>();
    }
}

public class ProductAnalysisDivisionCreateHandler : BaseCreateCommandHandler, IRequestHandler<ProductAnalysisDivisionCreateCommand, OperationStatus>
{
    public ProductAnalysisDivisionCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductAnalysisDivisionCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductAnalysisDivisionCreateCommand, ProductAnalysisDivision>(command);
        _context.ProductAnalysisDivisions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}

public class ProductAnalysisDivisionCreateValidator : ProductAnalysisDivisionValidator<ProductAnalysisDivisionCreateCommand>
{
    public ProductAnalysisDivisionCreateValidator()
    {
    }
}