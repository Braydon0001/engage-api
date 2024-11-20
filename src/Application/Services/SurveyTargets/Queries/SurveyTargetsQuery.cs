using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.SurveyTargets.Queries;

public class SurveyTargetsQuery : IRequest<SurveyTargets>
{
    public int Id { get; set; }
}

public class SurveyTargetsHandler : IRequestHandler<SurveyTargetsQuery, SurveyTargets>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SurveyTargetsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SurveyTargets> Handle(SurveyTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleOrDefaultAsync(e => e.SurveyId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await _context.SurveyTargets.AsQueryable().AsNoTracking().Where(e => e.SurveyId == query.Id).ToListAsync(cancellationToken);

        var employeeJobTitleIds = entities.OfType<SurveyEmployeeJobTitleTarget>().Select(e => e.EmployeeJobTitleId).ToList();
        var employeeIds = entities.OfType<SurveyEmployeeTarget>().Select(e => e.EmployeeId).ToList();
        var engageRegionIds = entities.OfType<SurveyEngageRegionTarget>().Select(e => e.EngageRegionId).ToList();
        var storeFormatIds = entities.OfType<SurveyStoreFormatTarget>().Select(e => e.StoreFormatId).ToList();
        var storeIds = entities.OfType<SurveyStoreTarget>().Select(e => e.StoreId).ToList();

        var employeeJobTitles = await _context.EmployeeJobTitles.Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId)).ProjectTo<EmployeeJobTitleDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employees = await _context.Employees.Where(e => employeeIds.Contains(e.EmployeeId)).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var enageRegions = await _context.EngageRegions.Where(e => engageRegionIds.Contains(e.Id)).ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var storeFormats = await _context.StoreFormats.Where(e => storeFormatIds.Contains(e.Id)).ProjectTo<StoreFormatDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var stores = await _context.Stores.Where(e => storeIds.Contains(e.StoreId)).ProjectTo<StoreDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new SurveyTargets(employeeJobTitles, employees, enageRegions, storeFormats, stores);
    }
}
