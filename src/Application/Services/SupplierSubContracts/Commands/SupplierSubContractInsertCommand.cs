// auto-generated
namespace Engage.Application.Services.SupplierSubContracts.Commands;

public class SupplierSubContractInsertCommand : IMapTo<SupplierSubContract>, IRequest<SupplierSubContract>
{
    public int SupplierContractId { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public string Name { get; set; }
    public string Reference1 { get; set; }
    public string GlMainCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractInsertCommand, SupplierSubContract>();
    }
}

public class SupplierSubContractInsertHandler : InsertHandler, IRequestHandler<SupplierSubContractInsertCommand, SupplierSubContract>
{
    public SupplierSubContractInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContract> Handle(SupplierSubContractInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierSubContractInsertCommand, SupplierSubContract>(command);
        
        _context.SupplierSubContracts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierSubContractInsertValidator : AbstractValidator<SupplierSubContractInsertCommand>
{
    public SupplierSubContractInsertValidator()
    {
        RuleFor(e => e.SupplierContractId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierSubContractTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).MaximumLength(100);
        RuleFor(e => e.Reference1).MaximumLength(100);
        RuleFor(e => e.GlMainCode).MaximumLength(100);
        RuleFor(e => e.GlSubCode).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(220);
    }
}