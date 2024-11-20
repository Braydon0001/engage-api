namespace Engage.Application.Services.TrainingFiles.Commands;

public class TrainingFileUpdateCommand : IMapTo<TrainingFile>, IRequest<TrainingFile>
{
    public int Id { get; set; }
    public int TrainingId { get; set; }
    public int TrainingFileTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFileUpdateCommand, TrainingFile>();
    }
}

public class TrainingFileUpdateHandler : UpdateHandler, IRequestHandler<TrainingFileUpdateCommand, TrainingFile>
{
    public TrainingFileUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingFile> Handle(TrainingFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingFiles.SingleOrDefaultAsync(e => e.TrainingFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateTrainingFileValidator : AbstractValidator<TrainingFileUpdateCommand>
{
    public UpdateTrainingFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.TrainingId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.TrainingFileTypeId).NotEmpty().GreaterThan(0);
    }
}