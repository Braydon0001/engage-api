namespace Engage.Application.Services.SupplierYears.Commands;

public class SupplierYearUpdateCommand : IMapTo<SupplierYear>, IRequest<SupplierYear>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierYearUpdateCommand, SupplierYear>();
    }
}

public class SupplierYearUpdateHandler : UpdateHandler, IRequestHandler<SupplierYearUpdateCommand, SupplierYear>
{
    public SupplierYearUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierYear> Handle(SupplierYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierYears.SingleOrDefaultAsync(e => e.SupplierYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSupplierYear : AbstractValidator<SupplierYearUpdateCommand>
{
    public UpdateSupplierYear()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);

    }
}
