using Engage.Application.Services.EmployeeContractRegions.Queries;
using Engage.Application.Services.EmployeeEmployeeBadges.Models;
using Engage.Application.Services.EmployeePopiConsents.Models;
using Engage.Application.Services.EmployeeRegions.Queries;
using Engage.Application.Services.Employees.Models;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeeVmQuery : IRequest<EmployeeVm>
{
    public int Id { get; set; }
    public bool IsContract { get; set; } = false;
}

public class EmployeeVmQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeVmQuery, EmployeeVm>
{
    private readonly IMultiTenantContextAccessor _multiTenantContextAccessor;
    private readonly IMediator _mediator;
    public EmployeeVmQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IMultiTenantContextAccessor multiTenantContextAccessor) : base(context, mapper)
    {
        _mediator = mediator;
        _multiTenantContextAccessor = multiTenantContextAccessor;
    }

    public async Task<EmployeeVm> Handle(EmployeeVmQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var entity = await _context.Employees.IgnoreQueryFilters()
                                             .Where(e => EF.Property<string>(e, "TenantId") == _multiTenantContextAccessor.MultiTenantContext.TenantInfo.Id)
                                             .Include(x => x.EmployeeDisabledType)
                                             .Include(x => x.EmployeeIdentificationType)
                                             .Include(x => x.EmployeeState)
                                             .Include(x => x.EmployeeIncentiveTier)
                                             .Include(x => x.EmployeeCitzenship)
                                             .Include(x => x.EmployeeLanguage)
                                             .Include(x => x.EmployeeJobTitle)
                                             .Include(x => x.MaritalStatus)
                                             .Include(x => x.EmployeeNationality)
                                             .Include(x => x.EmployeeRace)
                                             .Include(x => x.EmployeeGender)
                                             .Include(x => x.NextOfKinType)
                                             .Include(x => x.EmployeeTitle)
                                             .Include(x => x.UniformSize)
                                             .Include(x => x.EmployeePassportNationality)
                                             .Include(x => x.EmployeePersonType)
                                             .Include(x => x.EmployeeSDLExemption)
                                             .Include(x => x.EmployeeTaxStatus)
                                             .Include(x => x.EmployeeUIFExemption)
                                             .Include(x => x.EmployeeStandardIndustryGroupCode)
                                             .Include(x => x.EmployeeStandardIndustryCode)
                                             .Include(x => x.Manager)
                                             .Include(x => x.LeaveManager)
                                             .Include(x => x.CostCenterManager)
                                             .Include(x => x.CostCenterManager)
                                             .Include(x => x.EmployeeJobTitleTime)
                                             .Include(x => x.EmployeeJobTitleType)
                                             .Include(x => x.EmployeeTerminationReason)
                                             .Include(x => x.EmployeeCostCenters)
                                             .ThenInclude(x => x.CostCenter)
                                             .Include(x => x.EmployeeDepartments)
                                             .ThenInclude(x => x.EngageDepartment)
                                             .Include(x => x.EmployeeRegions)
                                             .ThenInclude(x => x.EngageRegion)
                                             .Include(x => x.EngageSubRegion)
                                             .Include(x => x.EmployeeHealthConditions)
                                             .ThenInclude(x => x.EmployeeHealthCondition)
                                             .Include(x => x.EmployeeDivisions)
                                             .ThenInclude(x => x.EmployeeDivision)
                                             .Include(x => x.EmployeeJobTitles)
                                             .ThenInclude(x => x.EmployeeJobTitle)
                                             .Include(x => x.EmployeeTerminationHistories)
                                             .ThenInclude(x => x.EmployeeTerminationReason)
                                             .Include(x => x.EmployeeFiles)
                                             .SingleAsync(x => x.EmployeeId == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception("Employee not found");
        }

        if (!engageRegionIds.Contains(7))
        {
            var isInRegion = await _context.EmployeeRegions.Where(e => e.EmployeeId == entity.EmployeeId && engageRegionIds.Contains(e.EngageRegionId))
                                                           .AnyAsync(cancellationToken);

            if (!isInRegion)
            {
                throw new Exception("This Employee is not in your Region");
            }
        }

        if (request.IsContract)
        {
            var contractRegionIds = await _mediator.Send(new EmployeeContractRegionsQuery(), cancellationToken);
            if (contractRegionIds.Count != 0)
            {
                var hasPermission = await _context.EmployeeRegions.Where(e => e.EmployeeId == entity.EmployeeId && contractRegionIds.Contains(e.EngageRegionId))
                                                                  .AnyAsync(cancellationToken);

                if (!hasPermission)
                {
                    throw new Exception("You do not have permission to view this Employee's Contract.");
                }
            }
            else
            {
                throw new Exception("You do not have permission to view this Employee's Contract.");
            }
        }

        var vm = _mapper.Map<Employee, EmployeeVm>(entity);

        vm.PrimaryBankDetailId = await _context.EmployeeBankDetails.Where(e => e.EmployeeId == request.Id && e.IsPrimary == true && e.Disabled == false)
                                                                   .Select(e => e.EmployeeBankDetailId)
                                                                   .SingleOrDefaultAsync(cancellationToken);

        vm.EmployeeAddressId = await _context.EmployeeAddresses.Where(e => e.EmployeeId == request.Id && e.Disabled == false)
                                                               .Select(e => e.EmployeeAddressId)
                                                               .SingleOrDefaultAsync(cancellationToken);

        vm.EmployeePayRateId = await _context.EmployeePayRates.Where(e => e.EmployeeId == request.Id && e.Disabled == false)
                                                               .Select(e => e.EmployeePayRateId)
                                                               .SingleOrDefaultAsync(cancellationToken);

        vm.EmployeePensionId = await _context.EmployeePensions.Where(e => e.EmployeeId == request.Id && e.Disabled == false)
                                                               .Select(e => e.EmployeePensionId)
                                                               .SingleOrDefaultAsync(cancellationToken);

        var stores = await _context.EmployeeStores.Where(e => e.EmployeeId == request.Id && e.Disabled == false)
                                                  .GroupBy(e => new { e.StoreId, e.Store.Name })
                                                  .Select(e => new OptionDto(e.Key.StoreId, e.Key.Name))
                                                   .ToListAsync(cancellationToken);

        vm.EmployeeBadges = await _context.EmployeeEmployeeBadges.Where(e => e.EmployeeId == request.Id)
                                                                 .ProjectTo<EmployeeEmployeeBadgeDto>(_mapper.ConfigurationProvider)
                                                                 .ToListAsync(cancellationToken);

        vm.EmployeePopiConsents = await _context.EmployeePopiConsents.Where(e => e.EmployeeId == request.Id)
                                                                     .ProjectTo<EmployeePopiConsentDto>(_mapper.ConfigurationProvider)
                                                                     .ToListAsync(cancellationToken);

        vm.HasCoolerBoxes = await _context.EmployeeCoolerBoxes.Where(e => e.EmployeeId == request.Id).AnyAsync(cancellationToken);

        vm.HasVehicles = await _context.EmployeeVehicles.Where(e => e.EmployeeId == request.Id).AnyAsync(cancellationToken);

        vm.HasAssets = await _context.EmployeeAssets.Where(e => e.EmployeeId == request.Id).AnyAsync(cancellationToken);

        if (stores.Count > 0)
        {
            stores = stores.OrderBy(e => e.Name).ToList();
        }
        vm.EmployeeStoreStoreIds = stores;

        //If not contract query, do not include contract in data object
        if (!request.IsContract)
        {
            vm.FileEmploymentContract = new List<JsonFile>();
        }

        return vm;
    }
}
