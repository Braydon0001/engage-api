namespace Engage.Application.Services.EmployeeJobTitleTypes.Commands;

public class EmployeeJobTitleTypeInsertCommand : IMapTo<EmployeeJobTitleType>, IRequest<EmployeeJobTitleType>
{
    public int EmployeeJobTitleId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTypeInsertCommand, EmployeeJobTitleType>();
    }
}

public record EmployeeJobTitleTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTypeInsertCommand, EmployeeJobTitleType>
{
    public async Task<EmployeeJobTitleType> Handle(EmployeeJobTitleTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<EmployeeJobTitleTypeInsertCommand, EmployeeJobTitleType>(command);

        Context.EmployeeJobTitleTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeJobTitleTypeInsertValidator : AbstractValidator<EmployeeJobTitleTypeInsertCommand>
{
    public EmployeeJobTitleTypeInsertValidator()
    {
        RuleFor(e => e.EmployeeJobTitleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(300);
    }
}