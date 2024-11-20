using Engage.Application.Services.Mobile.Database.Models;
using Geolocation;
using System.Globalization;

namespace Engage.Application.Services.Stores.Queries;

public class GetStoreListByLocationQuery : IRequest<List<StoreListDto>>
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public int? EmployeeId { get; set; }

}

public class GetStoreListByLocationQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreListByLocationQuery, List<StoreListDto>>
{
    public GetStoreListByLocationQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreListDto>> Handle(GetStoreListByLocationQuery request, CancellationToken cancellationToken)
    {
        Coordinate origin = new Coordinate() { Latitude = request.Lat, Longitude = request.Lon };
        CoordinateBoundaries boundaries = new CoordinateBoundaries(origin, 15, DistanceUnit.Kilometers);

        double minLatitude = boundaries.MinLatitude;
        double maxLatitude = boundaries.MaxLatitude;
        double minLongitude = boundaries.MinLongitude;
        double maxLongitude = boundaries.MaxLongitude;



        List<int> employeeRegions = new List<int>();
        List<int> employeeStoreConceptIds = new List<int>();

        if (request.EmployeeId.HasValue && request.EmployeeId > 0)
        {
            employeeRegions = _context.EmployeeRegions
                                             .Where(e => e.EmployeeId == request.EmployeeId)
                                             .Select(s => s.EngageRegionId)
                                             .ToList();

            var employeeDepartmentIds = await _context.EmployeeDepartments.Where(e => e.EmployeeId == request.EmployeeId)
                                                                 .Select(e => e.EngageDepartmentId).Distinct().ToListAsync(cancellationToken);
            employeeStoreConceptIds = await _context.StoreConcepts.Where(e => employeeDepartmentIds.Contains(e.EngageDepartmentId))
                                                                 .Select(e => e.Id).Distinct()
                                                                 .ToListAsync(cancellationToken);
        }


        // Join the tables on StakeholderId and not Primary Location.

        var results = await _context.Stores.Join(_context.Locations,
        store => store.StakeholderId,
        location => location.StakeholderId,
        (store, location) => new { store, location })
            .Where(x => x.store.Disabled == false)
            .Where(x => x.location.Disabled == false)
            .Where(s => s.location.Lat >= minLatitude && s.location.Lat <= maxLatitude)
            .Where(s => s.location.Long >= minLatitude && s.location.Long <= maxLongitude)
            .Select(x => new StoreListDto
            {
                Id = x.store.StoreId,
                StoreFormatId = x.store.StoreFormatId,
                Code = x.store.Code,
                Name = x.store.Name,
                EngageRegionName = x.store.EngageRegion.Name,
                StoreImageUrl = x.store.StoreImageUrl,
                StoreTypeImageUrl = x.store.StoreImageUrl,
                AddressLineOne = x.location.AddressLineOne,
                AddressLineTwo = x.location.AddressLineTwo,
                Suburb = x.location.Suburb,
                City = x.location.City,
                Province = x.location.Province,
                PostCode = x.location.PostCode,
                //Email = x.store.PrimaryContact.PrimaryEmailContactItem.Value,
                //Mobile = x.store.PrimaryContact.PrimaryMobileContactItem.Value,
                Lat = x.location.Lat.HasValue ? CheckCoOrdinate(x.location.Lat.Value, true).ToFloat() : null,
                Long = x.location.Long.HasValue ? CheckCoOrdinate(x.location.Long.Value, false).ToFloat() : null,
                Distance = GeoCalculator.GetDistance(
                        origin.Latitude,
                        origin.Longitude,
                        CheckCoOrdinate(x.location.Lat.Value, true),
                        CheckCoOrdinate(x.location.Long.Value, false),
                        2,
                        DistanceUnit.Kilometers),
                Contacts = x.store.StoreContacts.Where(e => e.Store.StoreId == x.store.StoreId).Select(o => new MobileStoreContactDto() { Name = o.FullName, Title = o.EntityContactType.Name, Email = o.EmailAddress1, PhoneNumber = o.MobilePhone }).ToList(),
                DistributionCentres = x.store.DCAccounts.OrderBy(e => e.AccountNumber).Select(e => new OptionDto() { Name = $"{e.DistributionCenter.Name} - {e.AccountNumber}", Id = e.DistributionCenterId }).ToList(),
                EngageRegionId = x.store.EngageRegionId,
            })

            .ToListAsync(cancellationToken);

        results = results
                .Where(s => s.Distance < 15)
                .OrderBy(x => x.Distance)
                .Take(20)
                .ToList();

        if (request.EmployeeId.HasValue && request.EmployeeId > 0)
        {
            foreach (var result in results)
            {
                var attributesWithScores = await GetStoreAttributes(employeeStoreConceptIds, result.Id, cancellationToken);
                result.Concepts = attributesWithScores.Concepts;
                result.StorePerformancePercent = attributesWithScores.RobotScoresAverage;
            }

            if (!employeeRegions.Contains(7)) // 7 is for all regions.
            {
                results = results.Where(x => employeeRegions.Contains(x.EngageRegionId)).ToList();
            }
        }

        return results.ToList();
    }

    static double CheckCoOrdinate(float value, bool isLat)
    {
        double dbl;
        var sValue = value.ToString()
                        .Replace(".", "")
                        .Replace(",", "");

        if (isLat)
        {
            if (!sValue.StartsWith("-"))
            {
                sValue = "-" + sValue;
            }
            if (sValue.Length > 3)
            {
                sValue = sValue.Insert(3, ",");
            }

            if (double.TryParse(sValue, CultureInfo.CreateSpecificCulture("en-ZA"), out dbl))
            {
                if (dbl < -23 && dbl > -35)
                {
                    return dbl;
                }
            }
        }
        else
        {
            if (sValue.Length > 2)
            {
                sValue = sValue.Insert(2, ",");
            }
            if (double.TryParse(sValue, CultureInfo.CreateSpecificCulture("en-ZA"), out dbl))
            {
                if (dbl > 16 && dbl < 33)
                {
                    return dbl;
                }
            }
        }

        return 0;
    }
    private async Task<StorePerformaceWithConcepts> GetStoreAttributes(List<int> employeeStoreConceptIds, int storeId, CancellationToken cancellationToken)
    {
        var storeRobots = new List<StoreConceptWithAttributesMobile>();
        foreach (var storeConceptId in employeeStoreConceptIds)
        {
            var performance = await _context.StoreConceptLevels.Where(e => e.StoreId == storeId && e.StoreConceptId == storeConceptId).FirstOrDefaultAsync(cancellationToken);
            var concept = await _context.StoreConcepts.Where(e => e.Id == storeConceptId).FirstOrDefaultAsync(cancellationToken);
            var attributes = await _context.StoreConceptAttributes.Where(e => e.StoreConceptId == storeConceptId).ToListAsync(cancellationToken);
            var conceptAttributes = new List<StoreConceptAttributeMobile>();
            foreach (var attr in attributes)
            {
                var attributeValue = await _context.StoreConceptAttributeValues.Where(e => e.StoreConceptAttributeId == attr.StoreConceptAttributeId && e.StoreId == storeId).FirstOrDefaultAsync(cancellationToken);
                if (attributeValue != null)
                {
                    var attribute = new StoreConceptAttributeMobile()
                    {
                        StoreConceptAttributeId = attr.StoreConceptAttributeId,
                        Name = attr.Name,
                        Detail = attributeValue.Value,
                    };
                    conceptAttributes.Add(attribute);
                }
            }
            var robotWithAttr = new StoreConceptWithAttributesMobile()
            {
                StoreConceptId = storeConceptId,
                Name = concept.Name,
                Level = performance == null ? 0 : performance.Level,
                Attributes = conceptAttributes,
                Target = performance == null ? 0 : performance.Target,
                Actual = performance == null ? 0 : performance.Actual,
                Score = performance == null ? 0 : performance.Score,
            };
            storeRobots.Add(robotWithAttr);
        }

        var robotScores = await _context.StoreConceptLevels.Where(e => e.StoreId == storeId && employeeStoreConceptIds.Contains(e.StoreConceptId) && e.Level > 0)
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

        return new StorePerformaceWithConcepts()
        {
            RobotScoresAverage = robotScoresAverage,
            Concepts = storeRobots
        };
    }
}

public class StorePerformaceWithConcepts
{
    public double RobotScoresAverage { get; set; }
    public List<StoreConceptWithAttributesMobile> Concepts { get; set; }
}


