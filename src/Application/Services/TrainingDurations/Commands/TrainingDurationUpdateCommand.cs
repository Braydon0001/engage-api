namespace Engage.Application.Services.TrainingDurations.Commands;

public class TrainingDurationUpdateCommand : IMapTo<TrainingDuration>, IRequest<TrainingDuration>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingDurationUpdateCommand, TrainingDuration>();
    }
}
public record TrainingDurationUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<TrainingDurationUpdateCommand, TrainingDuration>
{
    public async Task<TrainingDuration> Handle(TrainingDurationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.TrainingDurations.SingleOrDefaultAsync(e => e.TrainingDurationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class TrainingDurationUpdateValidator : AbstractValidator<TrainingDurationUpdateCommand>
{
    public TrainingDurationUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}