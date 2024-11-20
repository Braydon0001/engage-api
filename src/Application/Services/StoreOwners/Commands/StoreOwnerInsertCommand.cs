// auto-generated
namespace Engage.Application.Services.StoreOwners.Commands;

public class StoreOwnerInsertCommand : IMapTo<StoreOwner>, IRequest<StoreOwner>
{
    public int StoreId { get; set; }
    public int StoreGroupId { get; set; }
    public int StoreOwnerTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Note { get; set; }
    public string Name { get; set; }
    public bool IsPrimaryOwner { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreOwnerInsertCommand, StoreOwner>();
    }
}

public class StoreOwnerInsertHandler : InsertHandler, IRequestHandler<StoreOwnerInsertCommand, StoreOwner>
{
    public StoreOwnerInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreOwner> Handle(StoreOwnerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreOwnerInsertCommand, StoreOwner>(command);

        _context.StoreOwners.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class StoreOwnerInsertValidator : AbstractValidator<StoreOwnerInsertCommand>
{
    public StoreOwnerInsertValidator()
    {
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreOwnerTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate);
        RuleFor(e => e.Note).MaximumLength(1000);
        RuleFor(e => e.Name).MaximumLength(1000);
    }
}