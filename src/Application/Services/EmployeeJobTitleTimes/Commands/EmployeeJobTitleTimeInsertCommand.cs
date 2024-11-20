namespace Engage.Application.Services.EmployeeJobTitleTimes.Commands;

public class EmployeeJobTitleTimeInsertCommand : IMapTo<EmployeeJobTitleTime>, IRequest<EmployeeJobTitleTime>
{
    public int EmployeeJobTitleId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTimeInsertCommand, EmployeeJobTitleTime>();
    }
}

public record EmployeeJobTitleTimeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTimeInsertCommand, EmployeeJobTitleTime>
{
    public async Task<EmployeeJobTitleTime> Handle(EmployeeJobTitleTimeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<EmployeeJobTitleTimeInsertCommand, EmployeeJobTitleTime>(command);

        Context.EmployeeJobTitleTimes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeJobTitleTimeInsertValidator : AbstractValidator<EmployeeJobTitleTimeInsertCommand>
{
    public EmployeeJobTitleTimeInsertValidator()
    {
        RuleFor(e => e.EmployeeJobTitleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(300);
    }
}