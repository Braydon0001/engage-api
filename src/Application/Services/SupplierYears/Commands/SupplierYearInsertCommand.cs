namespace Engage.Application.Services.SupplierYears.Commands;

public class SupplierYearInsertCommand : IMapTo<SupplierYear>, IRequest<SupplierYear>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierYearInsertCommand, SupplierYear>();
    }
}

public class SupplierYearInsertHandler : InsertHandler, IRequestHandler<SupplierYearInsertCommand, SupplierYear>
{
    public SupplierYearInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierYear> Handle(SupplierYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SupplierYearInsertCommand, SupplierYear>(command);

        _context.SupplierYears.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SupplierYearInsertValidator : AbstractValidator<SupplierYearInsertCommand>
{
    public SupplierYearInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}

