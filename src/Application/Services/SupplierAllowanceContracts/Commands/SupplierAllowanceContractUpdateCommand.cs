namespace Engage.Application.Services.SupplierAllowanceContracts.Commands;

public class SupplierAllowanceContractUpdateCommand : IMapTo<SupplierAllowanceContract>, IRequest<SupplierAllowanceContract>
{
    public int Id { get; set; }
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
        profile.CreateMap<SupplierAllowanceContractUpdateCommand, SupplierAllowanceContract>();
    }
}

public class SupplierAllowanceContractUpdateHandler : UpdateHandler, IRequestHandler<SupplierAllowanceContractUpdateCommand, SupplierAllowanceContract>
{
    public SupplierAllowanceContractUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierAllowanceContract> Handle(SupplierAllowanceContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowanceContracts.SingleOrDefaultAsync(e => e.SupplierAllowanceContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierAllowanceContractValidator : AbstractValidator<SupplierAllowanceContractUpdateCommand>
{
    public UpdateSupplierAllowanceContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
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