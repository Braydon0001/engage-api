namespace Engage.Application.Services.TrainingTypes.Queries;

public class TrainingTypeOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class TrainingTypeOptionsQueryHandler : IRequestHandler<TrainingTypeOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public TrainingTypeOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(TrainingTypeOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.TrainingTypes.Where(e => e.Disabled == false)
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.TrainingTypeId, e.Name))
                       .ToList();
    }
}