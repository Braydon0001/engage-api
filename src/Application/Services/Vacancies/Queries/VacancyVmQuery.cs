using Engage.Application.Services.Vacancies.Models;

namespace Engage.Application.Services.Vacancies.Queries;

public class VacancyVmQuery : GetByIdQuery, IRequest<VacancyVm>
{
}

public class GetVacancyVmQueryHandler : BaseQueryHandler, IRequestHandler<VacancyVmQuery, VacancyVm>
{
    public GetVacancyVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<VacancyVm> Handle(VacancyVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vacancies.SingleOrDefaultAsync(x => x.VacancyId == request.Id, cancellationToken);
        return _mapper.Map<Vacancy, VacancyVm>(entity);
    }
}
