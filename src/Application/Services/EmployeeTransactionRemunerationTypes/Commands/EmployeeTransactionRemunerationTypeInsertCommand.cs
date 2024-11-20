namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Commands;

public class EmployeeTransactionRemunerationTypeInsertCommand : IMapTo<EmployeeTransactionRemunerationType>, IRequest<EmployeeTransactionRemunerationType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionRemunerationTypeInsertCommand, EmployeeTransactionRemunerationType>();
    }
}

public record EmployeeTransactionRemunerationTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeTransactionRemunerationTypeInsertCommand, EmployeeTransactionRemunerationType>
{
    public async Task<EmployeeTransactionRemunerationType> Handle(EmployeeTransactionRemunerationTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<EmployeeTransactionRemunerationTypeInsertCommand, EmployeeTransactionRemunerationType>(command);
        
        Context.EmployeeTransactionRemunerationTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeTransactionRemunerationTypeInsertValidator : AbstractValidator<EmployeeTransactionRemunerationTypeInsertCommand>
{
    public EmployeeTransactionRemunerationTypeInsertValidator()
    {
        RuleFor(e => e.Name).MaximumLength(120);
    }
}