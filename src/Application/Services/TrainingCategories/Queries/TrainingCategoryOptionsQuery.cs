namespace Engage.Application.Services.TrainingCategories.Queries;

public class TrainingCategoryOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class TrainingCategoryOptionsQueryHandler : IRequestHandler<TrainingCategoryOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public TrainingCategoryOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(TrainingCategoryOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.TrainingCategories.Where(e => e.Disabled == false)
                                                   .OrderBy(e => e.Name)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.TrainingCategoryId, e.Name))
                       .ToList();
    }
}