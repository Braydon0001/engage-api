namespace Engage.Application.Services.SupplierAllowanceContracts.Commands;

public class SupplierAllowanceContractInsertCommand : IMapTo<SupplierAllowanceContract>, IRequest<SupplierAllowanceContract>
{
    public int SupplierId { get; set; }
    public int SupplierSalesLeadId { get; set; }
    public string Name { get; set; }
    public string NCircularReference { get; set; }
    public string EncoreReference { get; set; }
    public string Vendor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierAllowanceContractInsertCommand, SupplierAllowanceContract>();
    }
}

public class SupplierAllowanceContractInsertHandler : InsertHandler, IRequestHandler<SupplierAllowanceContractInsertCommand, SupplierAllowanceContract>
{
    public SupplierAllowanceContractInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierAllowanceContract> Handle(SupplierAllowanceContractInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierAllowanceContractInsertCommand, SupplierAllowanceContract>(command);

        _context.SupplierAllowanceContracts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierAllowanceContractInsertValidator : AbstractValidator<SupplierAllowanceContractInsertCommand>
{
    public SupplierAllowanceContractInsertValidator()
    {
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierSalesLeadId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name);
        RuleFor(e => e.NCircularReference).MaximumLength(100);
        RuleFor(e => e.EncoreReference).MaximumLength(100);
        RuleFor(e => e.Vendor).MaximumLength(100);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        RuleFor(e => e.Comment);
        RuleFor(e => e.Note);
    }
}