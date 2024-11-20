// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Commands;

public class SupplierSubContractDetailUpdateCommand : IMapTo<SupplierSubContractDetail>, IRequest<SupplierSubContractDetail>
{
    public int Id { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public int SupplierSubContractDetailTypeId { get; set; }
    public string Detail { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubContractDetailUpdateCommand, SupplierSubContractDetail>();
    }
}

public class SupplierSubContractDetailUpdateHandler : UpdateHandler, IRequestHandler<SupplierSubContractDetailUpdateCommand, SupplierSubContractDetail>
{
    public SupplierSubContractDetailUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractDetail> Handle(SupplierSubContractDetailUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSubContractDetails.SingleOrDefaultAsync(e => e.SupplierSubContractDetailId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierSubContractDetailValidator : AbstractValidator<SupplierSubContractDetailUpdateCommand>
{
    public UpdateSupplierSubContractDetailValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierSubContractTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Detail).MaximumLength(200);
        RuleFor(e => e.Note).MaximumLength(200);
    }
}