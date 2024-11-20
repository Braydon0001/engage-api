namespace Engage.Application.Services.TrainingFiles.Commands;

public class TrainingFileInsertCommand : IMapTo<TrainingFile>, IRequest<TrainingFile>
{
    public int TrainingId { get; set; }
    public int TrainingFileTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFileInsertCommand, TrainingFile>();
    }
}

public class TrainingFileInsertHandler : InsertHandler, IRequestHandler<TrainingFileInsertCommand, TrainingFile>
{
    public TrainingFileInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingFile> Handle(TrainingFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TrainingFileInsertCommand, TrainingFile>(command);

        _context.TrainingFiles.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class TrainingFileInsertValidator : AbstractValidator<TrainingFileInsertCommand>
{
    public TrainingFileInsertValidator()
    {
        RuleFor(e => e.TrainingId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.TrainingFileTypeId).NotEmpty().GreaterThan(0);
    }
}