// auto-generated

namespace Engage.Application.Services.SupplierContractDetails.Commands;

public class SupplierContractDetailInsertCommand : IMapTo<SupplierContractDetail>, IRequest<SupplierContractDetail>
{
    public int SupplierContractId { get; set; }
    public int SupplierContractDetailTypeId { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public float? RangeStartAmount { get; set; }
    public float? RangeEndAmount { get; set; }
    public string GlCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractDetailInsertCommand, SupplierContractDetail>();
    }
}

public class SupplierContractDetailInsertHandler : InsertHandler, IRequestHandler<SupplierContractDetailInsertCommand, SupplierContractDetail>
{
    public SupplierContractDetailInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractDetail> Handle(SupplierContractDetailInsertCommand command, CancellationToken cancellationToken)
    {
        SupplierContractDetail entity = null;

        // Regional Split  
        var regionIds = command.EngageRegionIds;
        if (command.SupplierContractDetailTypeId == 3 && regionIds != null && regionIds.Count >= 1)
        {
            var splitAmount = (float)Math.Round(command.Amount / regionIds.Count, 2);

            foreach (var regionId in regionIds)
            {
                entity = _mapper.Map<SupplierContractDetailInsertCommand, SupplierContractDetail>(command);
                entity.EngageRegionId = regionId;
                entity.Amount = splitAmount;
                _context.SupplierContractDetails.Add(entity);
            }

        }
        else
        {
            entity = _mapper.Map<SupplierContractDetailInsertCommand, SupplierContractDetail>(command);
            _context.SupplierContractDetails.Add(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return entity;

    }
}

public class SupplierContractDetailInsertValidator : AbstractValidator<SupplierContractDetailInsertCommand>
{
    public SupplierContractDetailInsertValidator()
    {
        RuleFor(e => e.SupplierContractId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierContractDetailTypeId).NotEmpty().GreaterThan(0);
        RuleForEach(e => e.EngageRegionIds).GreaterThan(0);
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