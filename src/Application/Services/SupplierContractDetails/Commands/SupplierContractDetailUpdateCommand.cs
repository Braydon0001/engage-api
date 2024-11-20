// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Commands;

public class SupplierContractDetailUpdateCommand : IMapTo<SupplierContractDetail>, IRequest<SupplierContractDetail>
{
    public int Id { get; set; }
    public int SupplierContractId { get; set; }
    public int SupplierContractDetailTypeId { get; set; }
    public int? EngageRegionId { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public float? RangeStartAmount { get; set; }
    public float? RangeEndAmount { get; set; }
    public string GlCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public bool SaveChanges { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetailUpdateCommand, SupplierContractDetail>();
    }
}

public class SupplierContractDetailUpdateHandler : UpdateHandler, IRequestHandler<SupplierContractDetailUpdateCommand, SupplierContractDetail>
{
    public SupplierContractDetailUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractDetail> Handle(SupplierContractDetailUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierContractDetails.SingleOrDefaultAsync(e => e.SupplierContractDetailId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        if (command.RangeStartAmount != null && command.RangeStartAmount > command.RangeEndAmount)
        {
            throw new Exception("Range start amount must be less than end amount");
        }

        var mappedEntity = _mapper.Map(command, entity);

        if (command.SaveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        return mappedEntity;
    }
}

public class UpdateSupplierContractDetailValidator : AbstractValidator<SupplierContractDetailUpdateCommand>
{
    public UpdateSupplierContractDetailValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractDetailTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Amount).NotEmpty();
        RuleFor(e => e.RangeStartAmount);
        RuleFor(e => e.RangeEndAmount);
        RuleFor(e => e.GlCode).MaximumLength(100);
        RuleFor(e => e.GlSubCode).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(220);
        RuleFor(e => e.Reference1).MaximumLength(100);
    }
}