using Engage.Application.Services.DCAccounts.Models;
using Engage.Application.Services.EmployeeWeb.Models;
using Attribute = Engage.Application.Services.EmployeeWeb.Models.Attribute;
using StoreContact = Engage.Application.Services.EmployeeWeb.Models.StoreContact;

namespace Engage.Application.Services.EmployeeWeb.Queries;

public record EmployeeWebStoreQuery(int StoreId, int EmployeeId) : IRequest<EmployeeWebStore>
{
}

public class EmployeeWebStoreHandler : BaseQueryHandler, IRequestHandler<EmployeeWebStoreQuery, EmployeeWebStore>
{
    public EmployeeWebStoreHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeWebStore> Handle(EmployeeWebStoreQuery request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores.Include(e => e.PrimaryLocation)
                                         .Include(e => e.PrimaryContact)
                                         .Include(e => e.StoreCluster)
                                         .Where(e => e.StoreId == request.StoreId)
                                         .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("No Store");

        var employeeDepartmentIds = await _context.EmployeeDepartments.Where(e => e.EmployeeId == request.EmployeeId)
                                                                      .Select(e => e.EngageDepartmentId)
                                                                      .ToListAsync(cancellationToken);

        var employeeStoreConceptIds = await _context.StoreConcepts.Where(e => employeeDepartmentIds.Contains(e.EngageDepartmentId))
                                                                  .Select(e => e.Id)
                                                                  .ToListAsync(cancellationToken);
        var storeRobots = new List<RobotWithAttributes>();

        foreach (var storeConceptId in employeeStoreConceptIds)
        {
            var level = await _context.StoreConceptLevels.Where(e => e.StoreId == store.StoreId && e.StoreConceptId == storeConceptId).Select(e => e.Level).FirstOrDefaultAsync(cancellationToken);
            var actual = await _context.StoreConceptLevels.Where(e => e.StoreId == store.StoreId && e.StoreConceptId == storeConceptId).Select(e => e.Actual).FirstOrDefaultAsync(cancellationToken);
            var target = await _context.StoreConceptLevels.Where(e => e.StoreId == store.StoreId && e.StoreConceptId == storeConceptId).Select(e => e.Target).FirstOrDefaultAsync(cancellationToken);
            var score = await _context.StoreConceptLevels.Where(e => e.StoreId == store.StoreId && e.StoreConceptId == storeConceptId).Select(e => e.Score).FirstOrDefaultAsync(cancellationToken);
            var concept = await _context.StoreConcepts.Where(e => e.Id == storeConceptId).FirstOrDefaultAsync(cancellationToken);
            var attributes = await _context.StoreConceptAttributes.Where(e => e.StoreConceptId == storeConceptId).ToListAsync(cancellationToken);
            var conceptAttributes = new List<Attribute>();
            foreach (var attr in attributes)
            {
                var attributeValue = await _context.StoreConceptAttributeValues.Where(e => e.StoreConceptAttributeId == attr.StoreConceptAttributeId && e.StoreId == request.StoreId).FirstOrDefaultAsync(cancellationToken);
                if (attributeValue != null)
                {
                    var attribute = new Attribute()
                    {
                        Id = attr.StoreConceptAttributeId,
                        Name = attr.Name,
                        Detail = attributeValue.Value,
                    };
                    conceptAttributes.Add(attribute);
                }
            }
            var robotWithAttr = new RobotWithAttributes()
            {
                Name = concept.Name,
                Level = level,
                Attributes = conceptAttributes,
                Target = target,
                Actual = actual,
                Score = score,
            };
            storeRobots.Add(robotWithAttr);
        }

        var contact = "";
        var location = "";
        var AddressLineOne = "";
        var AddressLineTwo = "";
        var Suburb = "";
        var City = "";
        var Province = "";
        var PostCode = "";
        var lat = new float();
        var lng = new float();
        var gps = new List<float>();

        if (store.PrimaryContact != null && store.PrimaryContact.PrimaryEmailContactItem != null)
        {
            contact = store.PrimaryContact.PrimaryEmailContactItem.Value;
        }

        if (store.PrimaryLocation != null)
        {
            location = store.PrimaryLocation.AddressLineOne + " " + store.PrimaryLocation.AddressLineTwo;
            AddressLineOne = store.PrimaryLocation.AddressLineOne;
            AddressLineTwo = store.PrimaryLocation.AddressLineTwo;
            Suburb = store.PrimaryLocation.Suburb;
            City = store.PrimaryLocation.City;
            Province = store.PrimaryLocation.Province;
            PostCode = store.PrimaryLocation.PostCode;
            lat = (float)store.PrimaryLocation.Lat;
            lng = (float)store.PrimaryLocation.Long;
            gps.Add(lat);
            gps.Add(lng);
        }

        var contacts = _context.StoreContacts.Where(e => e.StoreId == request.StoreId)
                                             .Include(e => e.EntityContactType)
                                             .Select(o => new StoreContact()
                                             {
                                                 Name = o.FullName,
                                                 Title = o.EntityContactType.Name,
                                                 Email = o.EmailAddress1,
                                                 PhoneNumber = o.MobilePhone
                                             })
                                             .ToList();

        var dcAccounts = _context.DCAccounts.Where(e => e.StoreId == request.StoreId)
                                            .ProjectTo<DCAccountDto>(_mapper.ConfigurationProvider)
                                            .ToList();

        return new EmployeeWebStore()
        {
            Phone = contact,
            StoreId = store.StoreId,
            Name = store.Name,
            IsHalaal = store.IsHalaal,
            DCAccounts = dcAccounts,
            StoreCluster = store.StoreCluster,
            Address = location,
            AddressLineOne = AddressLineOne,
            AddressLineTwo = AddressLineTwo,
            Suburb = Suburb,
            City = City,
            Province = Province,
            PostCode = PostCode,
            ImageUrl = store.StoreImageUrl,
            Robots = storeRobots,
            Gps = gps,
            Contacts = contacts,
        };
    }
}