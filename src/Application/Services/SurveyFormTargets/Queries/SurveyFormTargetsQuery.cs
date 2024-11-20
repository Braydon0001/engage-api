using Engage.Application.Services.EmployeeDivisions.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.SurveyFormTargets.Queries;

public class SurveyFormTargetsQuery : IRequest<SurveyFormTargets>
{
    public int Id { get; set; }
}

public class SurveyFormTargetsHandler : IRequestHandler<SurveyFormTargetsQuery, SurveyFormTargets>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SurveyFormTargetsHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SurveyFormTargets> Handle(SurveyFormTargetsQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyForms.SingleOrDefaultAsync(e => e.SurveyFormId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var entities = await _context.SurveyFormTargets.AsQueryable().AsNoTracking().Where(e => e.SurveyFormId == query.Id).ToListAsync(cancellationToken);

        var employeeIds = entities.OfType<SurveyFormEmployee>().Select(e => e.EmployeeId).ToList();
        var employeeEngageRegionIds = entities.OfType<SurveyFormEmployeeEngageRegion>().Select(e => e.EmployeeEngageRegionId).ToList();
        var engageDepartmentIds = entities.OfType<SurveyFormEngageDepartment>().Select(e => e.EngageDepartmentId).ToList();
        var employeeJobTitleIds = entities.OfType<SurveyFormEmployeeJobTitle>().Select(e => e.EmployeeJobTitleId).ToList();
        var employeeDivisionIds = entities.OfType<SurveyFormEmployeeDivision>().Select(e => e.EmployeeDivisionId).ToList();

        var storeIds = entities.OfType<SurveyFormStore>().Select(e => e.StoreId).ToList();
        var storeEngageRegionIds = entities.OfType<SurveyFormStoreEngageRegion>().Select(e => e.StoreEngageRegionId).ToList();
        var storeClusterIds = entities.OfType<SurveyFormStoreCluster>().Select(e => e.StoreClusterId).ToList();
        var storeFormatIds = entities.OfType<SurveyFormStoreFormat>().Select(e => e.StoreFormatId).ToList();
        var storeLSMIds = entities.OfType<SurveyFormStoreLSM>().Select(e => e.StoreLSMId).ToList();
        var storeTypeIds = entities.OfType<SurveyFormStoreType>().Select(e => e.StoreTypeId).ToList();

        var employees = await _context.Employees.Where(e => employeeIds.Contains(e.EmployeeId)).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employeeEngageRegions = await _context.EngageRegions.Where(e => employeeEngageRegionIds.Contains(e.Id)).ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var engageDepartments = await _context.EngageDepartments.Where(e => engageDepartmentIds.Contains(e.Id)).ProjectTo<EngageDepartmentDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employeeJobTitles = await _context.EmployeeJobTitles.Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId)).ProjectTo<EmployeeJobTitleDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var employeeDivisions = await _context.EmployeeDivisions.Where(e => employeeDivisionIds.Contains(e.EmployeeDivisionId)).ProjectTo<EmployeeDivisionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        var stores = await _context.Stores.Where(e => storeIds.Contains(e.StoreId)).ProjectTo<StoreDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var storeEngageRegions = await _context.EngageRegions.Where(e => storeEngageRegionIds.Contains(e.Id)).ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        var storeClusters = await _context.StoreClusters.Where(e => storeClusterIds.Contains(e.Id)).Select(e => new OptionDto() { Name = e.Name, Id = e.Id, Disabled = e.Disabled }).ToListAsync(cancellationToken);
        var storeFormats = await _context.StoreFormats.Where(e => storeFormatIds.Contains(e.Id)).Select(e => new OptionDto() { Name = e.Name, Id = e.Id, Disabled = e.Disabled }).ToListAsync(cancellationToken);
        var storeLSMs = await _context.StoreLSMs.Where(e => storeLSMIds.Contains(e.Id)).Select(e => new OptionDto() { Name = e.Name, Id = e.Id, Disabled = e.Disabled }).ToListAsync(cancellationToken);
        var storeTypes = await _context.StoreTypes.Where(e => storeTypeIds.Contains(e.Id)).Select(e => new OptionDto() { Name = e.Name, Id = e.Id, Disabled = e.Disabled }).ToListAsync(cancellationToken);


        return new SurveyFormTargets(employees, employeeEngageRegions, engageDepartments, employeeJobTitles, employeeDivisions, stores, storeEngageRegions, storeClusters, storeFormats, storeLSMs, storeTypes);
    }
}
