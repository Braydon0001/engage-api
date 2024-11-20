using Engage.Application.Services.Vacancies.Models;

namespace Engage.Application.Services.Vacancies.Queries;

public class VacanciesQuery : GetQuery, IRequest<ListResult<VacancyDto>>
{
}

public class VacanciesQueryHandler : BaseQueryHandler, IRequestHandler<VacanciesQuery, ListResult<VacancyDto>>
{
    public VacanciesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<VacancyDto>> Handle(VacanciesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Vacancies.OrderBy(e => e.VacancyId)
                                                .ProjectTo<VacancyDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<VacancyDto>(entities);
    }
}
