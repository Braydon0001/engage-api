// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Commands;

public class SupplierSalesLeadInsertCommand : IMapTo<SupplierSalesLead>, IRequest<SupplierSalesLead>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string KnownAs { get; set; }
    public string EmailAddress { get; set; }
    public string ContactNumber { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSalesLeadInsertCommand, SupplierSalesLead>();
    }
}

public class SupplierSalesLeadInsertHandler : InsertHandler, IRequestHandler<SupplierSalesLeadInsertCommand, SupplierSalesLead>
{
    public SupplierSalesLeadInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSalesLead> Handle(SupplierSalesLeadInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierSalesLeadInsertCommand, SupplierSalesLead>(command);

        _context.SupplierSalesLeads.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierSalesLeadInsertValidator : AbstractValidator<SupplierSalesLeadInsertCommand>
{
    public SupplierSalesLeadInsertValidator()
    {
        RuleFor(e => e.FirstName);
        RuleFor(e => e.LastName);
        RuleFor(e => e.KnownAs).MaximumLength(100);
        RuleFor(e => e.EmailAddress).MaximumLength(100);
        RuleFor(e => e.ContactNumber).MaximumLength(100);
    }
}