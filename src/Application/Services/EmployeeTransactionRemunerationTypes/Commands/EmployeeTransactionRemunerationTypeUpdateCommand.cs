namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Commands;

public class EmployeeTransactionRemunerationTypeUpdateCommand : IMapTo<EmployeeTransactionRemunerationType>, IRequest<EmployeeTransactionRemunerationType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionRemunerationTypeUpdateCommand, EmployeeTransactionRemunerationType>();
    }
}

public record EmployeeTransactionRemunerationTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeTransactionRemunerationTypeUpdateCommand, EmployeeTransactionRemunerationType>
{
    public async Task<EmployeeTransactionRemunerationType> Handle(EmployeeTransactionRemunerationTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeTransactionRemunerationTypes.SingleOrDefaultAsync(e => e.EmployeeTransactionRemunerationTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeTransactionRemunerationTypeValidator : AbstractValidator<EmployeeTransactionRemunerationTypeUpdateCommand>
{
    public UpdateEmployeeTransactionRemunerationTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).MaximumLength(120);
    }
}