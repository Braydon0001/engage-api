namespace Engage.Application.Services.ProductAnalysisGroups.Commands;

public class ProductAnalysisGroupUpdateCommand : ProductAnalysisGroupCommand, IMapTo<ProductAnalysisGroup>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductAnalysisGroupUpdateCommand, ProductAnalysisGroup>();
    }
}

public class ProductAnalysisGroupUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<ProductAnalysisGroupUpdateCommand, OperationStatus>
{
    public ProductAnalysisGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductAnalysisGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductAnalysisGroups.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}

public class ProductAnalysisGroupUpdateValidator : ProductAnalysisGroupValidator<ProductAnalysisGroupUpdateCommand>
{
    public ProductAnalysisGroupUpdateValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}