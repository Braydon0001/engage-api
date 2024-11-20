namespace Engage.Application.Services.TrainingFileTypes.Commands;

public class TrainingFileTypeUpdateCommand : IMapTo<TrainingFileType>, IRequest<TrainingFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFileTypeUpdateCommand, TrainingFileType>();
    }
}

public class TrainingFileTypeUpdateHandler : UpdateHandler, IRequestHandler<TrainingFileTypeUpdateCommand, TrainingFileType>
{
    public TrainingFileTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingFileType> Handle(TrainingFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingFileTypes.SingleOrDefaultAsync(e => e.TrainingFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryYearValidator : AbstractValidator<TrainingFileTypeUpdateCommand>
{
    public UpdateInventoryYearValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(300);
    }
}