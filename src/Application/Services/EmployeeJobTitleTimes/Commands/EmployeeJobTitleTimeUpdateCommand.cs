namespace Engage.Application.Services.EmployeeJobTitleTimes.Commands;

public class EmployeeJobTitleTimeUpdateCommand : IMapTo<EmployeeJobTitleTime>, IRequest<EmployeeJobTitleTime>
{
    public int Id { get; set; }
    public int EmployeeJobTitleId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTimeUpdateCommand, EmployeeJobTitleTime>();
    }
}

public record EmployeeJobTitleTimeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTimeUpdateCommand, EmployeeJobTitleTime>
{
    public async Task<EmployeeJobTitleTime> Handle(EmployeeJobTitleTimeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeJobTitleTimes.SingleOrDefaultAsync(e => e.EmployeeJobTitleTimeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeJobTitleTimeValidator : AbstractValidator<EmployeeJobTitleTimeUpdateCommand>
{
    public UpdateEmployeeJobTitleTimeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(300);
    }
}