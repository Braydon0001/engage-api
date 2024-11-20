// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Commands;

public class SupplierSubContractDetailInsertCommand : IMapTo<SupplierSubContractDetail>, IRequest<SupplierSubContractDetail>
{
    public int SupplierSubContractTypeId { get; set; }
    public string Detail { get; set; }
    public string Note { get; set; }
    public int SupplierSubContractDetailTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetailInsertCommand, SupplierSubContractDetail>();
    }
}

public class SupplierSubContractDetailInsertHandler : InsertHandler, IRequestHandler<SupplierSubContractDetailInsertCommand, SupplierSubContractDetail>
{
    public SupplierSubContractDetailInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractDetail> Handle(SupplierSubContractDetailInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierSubContractDetailInsertCommand, SupplierSubContractDetail>(command);

        _context.SupplierSubContractDetails.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierSubContractDetailInsertValidator : AbstractValidator<SupplierSubContractDetailInsertCommand>
{
    public SupplierSubContractDetailInsertValidator()
    {
        RuleFor(e => e.SupplierSubContractTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Detail).MaximumLength(200);
        RuleFor(e => e.Note).MaximumLength(200);
    }
}