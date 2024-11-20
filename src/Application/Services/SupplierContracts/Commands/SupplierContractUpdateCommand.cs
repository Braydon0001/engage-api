// auto-generated
namespace Engage.Application.Services.SupplierContracts.Commands;

public class SupplierContractUpdateCommand : IMapTo<SupplierContract>, IRequest<SupplierContract>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public int SupplierContractTypeId { get; set; }
    public int? SupplierContractGroupId { get; set; }
    public int? SupplierContractSubGroupId { get; set; }
    public int SupplierContactId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Vendor { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public string Reference2 { get; set; }
    public bool IsEngage { get; set; }
    public bool IsEncore { get; set; }
    public bool IsEngine { get; set; }
    public bool IsSpar { get; set; }
    public bool IsTops { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractUpdateCommand, SupplierContract>();
    }
}

public class SupplierContractUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractUpdateCommand, SupplierContract>
{
    public SupplierContractUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContract> Handle(SupplierContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContracts.SingleOrDefaultAsync(e => e.SupplierContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierContractValidator : AbstractValidator<SupplierContractUpdateCommand>
{
    public UpdateSupplierContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractGroupId);
        RuleFor(e => e.SupplierContractSubGroupId);
        RuleFor(e => e.SupplierContactId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
        RuleFor(e => e.Vendor).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(220);
        RuleFor(e => e.Reference1).MaximumLength(100);
        RuleFor(e => e.Reference2).MaximumLength(100);
    }
}