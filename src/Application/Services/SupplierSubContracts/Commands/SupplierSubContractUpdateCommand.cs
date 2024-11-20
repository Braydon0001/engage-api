// auto-generated
namespace Engage.Application.Services.SupplierSubContracts.Commands;

public class SupplierSubContractUpdateCommand : IMapTo<SupplierSubContract>, IRequest<SupplierSubContract>
{
    public int Id { get; set; }
    public int SupplierContractId { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public string Name { get; set; }
    public string Reference1 { get; set; }
    public string GlMainCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractUpdateCommand, SupplierSubContract>();
    }
}

public class SupplierSubContractUpdateHandler : UpdateHandler, IRequestHandler<SupplierSubContractUpdateCommand, SupplierSubContract>
{
    public SupplierSubContractUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContract> Handle(SupplierSubContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSubContracts.SingleOrDefaultAsync(e => e.SupplierSubContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierSubContractValidator : AbstractValidator<SupplierSubContractUpdateCommand>
{
    public UpdateSupplierSubContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierSubContractTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).MaximumLength(100);
        RuleFor(e => e.Reference1).MaximumLength(100);
        RuleFor(e => e.GlMainCode).MaximumLength(100);
        RuleFor(e => e.GlSubCode).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(220);
    }
}