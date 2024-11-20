namespace Engage.Application.Services.StoreCycles.Commands;

public class StoreCycleUpdateCommand : IMapTo<StoreCycle>, IRequest<StoreCycle>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int EngageDepartmentId { get; set; }
    public int StoreCycleOperationId { get; set; }
    public int FrequencyTypeId { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreCycleUpdateCommand, StoreCycle>();
    }
}

public class StoreCycleUpdateHandler : UpdateHandler, IRequestHandler<StoreCycleUpdateCommand, StoreCycle>
{
    public StoreCycleUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreCycle> Handle(StoreCycleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreCycles.SingleOrDefaultAsync(e => e.StoreCycleId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreCycleValidator : AbstractValidator<StoreCycleUpdateCommand>
{
    public UpdateStoreCycleValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageDepartmentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreCycleOperationId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FrequencyTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}