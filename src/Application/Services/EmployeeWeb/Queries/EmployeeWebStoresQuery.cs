using Engage.Application.Services.DCAccounts.Models;
using Engage.Application.Services.EmployeeWeb.Models;
using Store = Engage.Application.Services.EmployeeWeb.Models.Store;

namespace Engage.Application.Services.EmployeeWeb.Queries;

public record EmployeeWebStoresQuery(int EmployeeId) : IRequest<EmployeeWebStoresVm>
{
}

public class EmployeeWebStoresHandler : BaseQueryHandler, IRequestHandler<EmployeeWebStoresQuery, EmployeeWebStoresVm>
{
    public EmployeeWebStoresHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeWebStoresVm> Handle(EmployeeWebStoresQuery request, CancellationToken cancellationToken)
    {
        var employeeStoreIds = await _context.EmployeeStores.Where(e => e.EmployeeId == request.EmployeeId)
                                                    .Select(e => e.StoreId)
                                                    .Distinct()
                                                    .ToListAsync(cancellationToken);

        if (employeeStoreIds == null)
        {
            return new EmployeeWebStoresVm()
            {
            };
        }

        var employeeStores = await _context.Stores.Include(e => e.StoreStoreConceptPerformances)
                                          .Include(e => e.Stakeholder)
                                          .ThenInclude(x => x.Locations)
                                          .Include(e => e.PrimaryContact)
                                          .Include(e => e.StoreCluster)
                                          .Where(e => employeeStoreIds.Contains(e.StoreId))
                                          .ToListAsync(cancellationToken);

        var employeeDepartmentIds = await _context.EmployeeDepartments.Where(e => e.EmployeeId == request.EmployeeId)
                                                                      .Select(e => e.EngageDepartmentId)
                                                                      .ToListAsync(cancellationToken);

        var employeeStoreConceptIds = await _context.StoreConcepts.Where(e => employeeDepartmentIds.Contains(e.EngageDepartmentId))
                                                                  .Select(e => e.Id)
                                                                  .ToListAsync(cancellationToken);

        var stores = new List<Store>();

        foreach (var store in employeeStores)
        {
            var address = "";
            var addressLineOne = "";
            var addressLineTwo = "";
            var suburb = "";
            var city = "";
            var province = "";
            var postCode = "";
            var phone = "";

            if (store.PrimaryContact != null && store.PrimaryContact.PrimaryEmailContactItem != null)
            {
                phone = store.PrimaryContact.PrimaryEmailContactItem.Value;
            }

            var primaryLocation = store.Stakeholder.Locations.Where(e => e.IsPrimaryAddress == true).FirstOrDefault();
            if (primaryLocation != null)
            {
                address = primaryLocation.AddressLineOne + " " + primaryLocation.AddressLineTwo;
                addressLineOne = primaryLocation.AddressLineOne;
                addressLineTwo = primaryLocation.AddressLineTwo;
                suburb = primaryLocation.Suburb;
                city = primaryLocation.City;
                province = primaryLocation.Province;
                postCode = primaryLocation.PostCode;
            }

            var dcAccounts = _context.DCAccounts.Where(e => e.StoreId == store.StoreId)
                                                .ProjectTo<DCAccountDto>(_mapper.ConfigurationProvider)
                                                .ToList();

            var robots = await _context.StoreConceptLevels.Where(e => e.StoreId == store.StoreId && employeeStoreConceptIds.Contains(e.StoreConceptId))
                                                          .Select(o => new Robot()
                                                          {
                                                              Name = o.StoreConcept.Name,
                                                              Level = o.Level,
                                                          })
                                                          .ToListAsync(cancellationToken);

            var robotScores = await _context.StoreConceptLevels.Where(e => e.StoreId == store.StoreId && employeeStoreConceptIds.Contains(e.StoreConceptId) && e.Level > 0)
                                                               .Select(e => e.Score)
                                                               .ToListAsync(cancellationToken);
            var robotScoreTotal = 0.0;
            var robotScoresAverage = 0.0;

            if (robotScores != null && robotScores.Count > 0)
            {
                foreach (var score in robotScores)
                {
                    robotScoreTotal += score;
                }

                robotScoresAverage = robotScoreTotal / robotScores.Count;
            }

            var employeeStore = new Store()
            {
                StoreId = store.StoreId,
                Name = store.Name,
                Address = address,
                AddressLineOne = addressLineOne,
                AddressLineTwo = addressLineTwo,
                Suburb = suburb,
                City = city,
                Province = province,
                PostCode = postCode,
                Phone = phone,
                ImageUrl = store.StoreImageUrl,
                IsHalaal = store.IsHalaal,
                StoreCluster = store.StoreCluster,
                DCAccounts = dcAccounts,
                Robots = robots,
                StorePerformancePercent = robotScoresAverage,
            };
            stores.Add(employeeStore);
        }

        return new EmployeeWebStoresVm()
        {
            Stores = stores
        };
    }
}
