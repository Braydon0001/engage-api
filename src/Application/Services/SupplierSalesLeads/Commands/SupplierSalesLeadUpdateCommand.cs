// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Commands;

public class SupplierSalesLeadUpdateCommand : IMapTo<SupplierSalesLead>, IRequest<SupplierSalesLead>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string KnownAs { get; set; }
    public string EmailAddress { get; set; }
    public string ContactNumber { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSalesLeadUpdateCommand, SupplierSalesLead>();
    }
}

public class SupplierSalesLeadUpdateHandler : UpdateHandler, IRequestHandler<SupplierSalesLeadUpdateCommand, SupplierSalesLead>
{
    public SupplierSalesLeadUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSalesLead> Handle(SupplierSalesLeadUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierSalesLeads.SingleOrDefaultAsync(e => e.SupplierSalesLeadId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierSalesLeadValidator : AbstractValidator<SupplierSalesLeadUpdateCommand>
{
    public UpdateSupplierSalesLeadValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FirstName);
        RuleFor(e => e.LastName);
        RuleFor(e => e.KnownAs).MaximumLength(100);
        RuleFor(e => e.EmailAddress).MaximumLength(100);
        RuleFor(e => e.ContactNumber).MaximumLength(100);
    }
}