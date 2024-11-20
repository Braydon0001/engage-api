// auto-generated
namespace Engage.Application.Services.SupplierContracts.Commands;

public class SupplierContractInsertCommand : IMapTo<SupplierContract>, IRequest<SupplierContract>
{
    public int SupplierId { get; set; }
    public int SupplierContractTypeId { get; set; }
    public int? SupplierContractSubGroupId { get; set; }
    public int? SupplierContractGroupId { get; set; }
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
        profile.CreateMap<SupplierContractInsertCommand, SupplierContract>();
    }
}

public class SupplierContractInsertHandler : InsertHandler, IRequestHandler<SupplierContractInsertCommand, SupplierContract>
{
    public SupplierContractInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContract> Handle(SupplierContractInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.StartDate > command.EndDate)
        {
            throw new Exception("Start Date must be before end date");
        }

        var entity = _mapper.Map<SupplierContractInsertCommand, SupplierContract>(command);

        _context.SupplierContracts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierContractInsertValidator : AbstractValidator<SupplierContractInsertCommand>
{
    public SupplierContractInsertValidator()
    {
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractSubGroupId);
        RuleFor(e => e.SupplierContractGroupId);
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