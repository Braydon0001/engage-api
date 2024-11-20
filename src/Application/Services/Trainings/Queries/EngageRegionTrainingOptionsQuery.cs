namespace Engage.Application.Services.Trainings.Queries;

public class EngageRegionTrainingOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    //public int EngageRegionId { get; set; }
    public List<int> EngageRegionIds { get; set; }
}

public class EngageRegionTrainingOptionsQueryHandler : IRequestHandler<EngageRegionTrainingOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EngageRegionTrainingOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EngageRegionTrainingOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Trainings.Where(e => request.EngageRegionIds.Contains(e.EngageRegionId.Value) &&//e.EngageRegionId == request.EngageRegionId && 
                                                        e.Disabled == false
                                                    )
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.TrainingId, e.Name))
                       .ToList();
    }
}