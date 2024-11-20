

namespace Engage.Application.Services.PayrollYears.Commands;

public class PayrollYearUpdateCommand : IMapTo<PayrollYear>, IRequest<PayrollYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PayrollYearUpdateCommand, PayrollYear>();
    }
}

public class PayrollYearUpdateHandler :UpdateHandler, IRequestHandler<PayrollYearUpdateCommand, PayrollYear>
{
    public PayrollYearUpdateHandler (IAppDbContext context, IMapper mapper) : base (context, mapper)
    {
    }

    public async Task<PayrollYear> Handle(PayrollYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.PayrollYears.SingleOrDefaultAsync(e=>e.PayrollYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryYearValidator : AbstractValidator<PayrollYearUpdateCommand>
{
    public UpdateInventoryYearValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Name);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}