// auto-generated
namespace Engage.Application.Services.StoreOwners.Commands;

public class StoreOwnerUpdateCommand : IMapTo<StoreOwner>, IRequest<StoreOwner>
{
    public int Id { get; set; }
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
        profile.CreateMap<StoreOwnerUpdateCommand, StoreOwner>();
    }
}

public class StoreOwnerUpdateHandler : UpdateHandler, IRequestHandler<StoreOwnerUpdateCommand, StoreOwner>
{
    public StoreOwnerUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreOwner> Handle(StoreOwnerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreOwners.SingleOrDefaultAsync(e => e.StoreOwnerId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreOwnerValidator : AbstractValidator<StoreOwnerUpdateCommand>
{
    public UpdateStoreOwnerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreOwnerTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate);
        RuleFor(e => e.Note).MaximumLength(1000);
        RuleFor(e => e.Name).MaximumLength(1000);
    }
}