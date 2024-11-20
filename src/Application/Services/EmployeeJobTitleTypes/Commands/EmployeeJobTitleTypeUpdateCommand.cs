namespace Engage.Application.Services.EmployeeJobTitleTypes.Commands;

public class EmployeeJobTitleTypeUpdateCommand : IMapTo<EmployeeJobTitleType>, IRequest<EmployeeJobTitleType>
{
    public int Id { get; set; }
    public int EmployeeJobTitleId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleTypeUpdateCommand, EmployeeJobTitleType>();
    }
}

public record EmployeeJobTitleTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeJobTitleTypeUpdateCommand, EmployeeJobTitleType>
{
    public async Task<EmployeeJobTitleType> Handle(EmployeeJobTitleTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.EmployeeJobTitleTypes.SingleOrDefaultAsync(e => e.EmployeeJobTitleTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeJobTitleTypeValidator : AbstractValidator<EmployeeJobTitleTypeUpdateCommand>
{
    public UpdateEmployeeJobTitleTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeJobTitleId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(300);
    }
}