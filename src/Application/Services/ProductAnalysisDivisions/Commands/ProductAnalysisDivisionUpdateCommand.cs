namespace Engage.Application.Services.ProductAnalysisDivisions.Commands;

public class ProductAnalysisDivisionUpdateCommand : ProductAnalysisDivisionCommand, IMapTo<ProductAnalysisDivision>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisDivisionUpdateCommand, ProductAnalysisDivision>();
    }
}

public class ProductAnalysisDivisionUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<ProductAnalysisDivisionUpdateCommand, OperationStatus>
{
    public ProductAnalysisDivisionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductAnalysisDivisionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductAnalysisDivisions.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}

public class ProductAnalysisDivisionUpdateValidator : ProductAnalysisDivisionValidator<ProductAnalysisDivisionUpdateCommand>
{
    public ProductAnalysisDivisionUpdateValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}