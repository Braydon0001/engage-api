namespace Engage.Application.Services.IncidentTypes.Queries;

public class IncidentTypeOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class IncidentTypeOptionsQueryHandler : BaseQueryHandler, IRequestHandler<IncidentTypeOptionsQuery, List<OptionDto>>
{
    public IncidentTypeOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(IncidentTypeOptionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.IncidentTypes.Where(e => e.Disabled == false)
                                           .OrderBy(e => e.Name)
                                           .Select(e => new OptionDto(e.IncidentTypeId, e.Name))
                                           .ToListAsync(cancellationToken);
    }
}
