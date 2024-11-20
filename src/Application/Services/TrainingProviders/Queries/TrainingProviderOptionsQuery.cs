namespace Engage.Application.Services.TrainingProviders.Queries;

public class TrainingProviderOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class TrainingProviderOptionsQueryHandler : IRequestHandler<TrainingProviderOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public TrainingProviderOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(TrainingProviderOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.TrainingProviders.Where(e => e.Disabled == false)
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.TrainingProviderId, e.Name))
                       .ToList();
    }
}