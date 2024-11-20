namespace Engage.Application.Services.TrainingDurations.Commands;

public class TrainingDurationInsertCommand : IMapTo<TrainingDuration>, IRequest<TrainingDuration>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingDurationInsertCommand, TrainingDuration>();
    }
}

public record TrainingDurationInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<TrainingDurationInsertCommand, TrainingDuration>
{
    public async Task<TrainingDuration> Handle(TrainingDurationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<TrainingDurationInsertCommand, TrainingDuration>(command);

        Context.TrainingDurations.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class TrainingDurationInsertValidator : AbstractValidator<TrainingDurationInsertCommand>
{
    public TrainingDurationInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}