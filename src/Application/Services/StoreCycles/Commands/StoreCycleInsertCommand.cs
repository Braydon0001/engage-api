namespace Engage.Application.Services.StoreCycles.Commands;

public class StoreCycleInsertCommand : IMapTo<StoreCycle>, IRequest<StoreCycle>
{
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
        profile.CreateMap<StoreCycleInsertCommand, StoreCycle>();
    }
}

public class StoreCycleInsertHandler : InsertHandler, IRequestHandler<StoreCycleInsertCommand, StoreCycle>
{
    public StoreCycleInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreCycle> Handle(StoreCycleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreCycleInsertCommand, StoreCycle>(command);

        _context.StoreCycles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class StoreCycleInsertValidator : AbstractValidator<StoreCycleInsertCommand>
{
    public StoreCycleInsertValidator()
    {
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageDepartmentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreCycleOperationId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FrequencyTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}