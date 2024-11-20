namespace Engage.Application.Services.TrainingFileTypes.Commands;

public class TrainingFileTypeInsertCommand : IMapTo<TrainingFileType>, IRequest<TrainingFileType>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFileTypeInsertCommand, TrainingFileType>();
    }
}

public class TrainingFileTypeInsertHandler : InsertHandler, IRequestHandler<TrainingFileTypeInsertCommand, TrainingFileType>
{
    public TrainingFileTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<TrainingFileType> Handle(TrainingFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TrainingFileTypeInsertCommand, TrainingFileType>(command);
        _context.TrainingFileTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryYearInsertValidator : AbstractValidator<TrainingFileTypeInsertCommand>
{
    public InventoryYearInsertValidator()
    {
        RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(300);
    }
}