using Geolocation;
using System.Globalization;

namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class CreateEmployeeStoreCheckInCommand : EmployeeStoreCheckInCommand, IRequest<OperationStatus>, IMapTo<EmployeeStoreCheckIn>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public DateTime CheckInTimezoneDate { get; set; }
    public float CheckInLat { get; set; }
    public float CheckInLong { get; set; }
    public float CheckInDistance { get; set; }
    public string CheckInUuid { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateEmployeeStoreCheckInCommand, EmployeeStoreCheckIn>();
    }
}

public class CreateEmployeeStoreCheckInCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeStoreCheckInCommand, OperationStatus>
{
    public CreateEmployeeStoreCheckInCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(CreateEmployeeStoreCheckInCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var store = await _context.Stores
                    .Include(s => s.PrimaryLocation)
                    .FirstOrDefaultAsync(s => s.StoreId == command.StoreId, cancellationToken);

            if (command.CheckInUuid == null)
            {
                Guid g = Guid.NewGuid();
                command.CheckInUuid = "v1-" + g.ToString();
            }

            Coordinate origin = new Coordinate()
            {
                Latitude = command.CheckInLat,
                Longitude = command.CheckInLong
            };

            // find the primary address.
            var storeLocation = await _context.Locations
                .FirstOrDefaultAsync(x =>
                    x.StakeholderId == store.StakeholderId &&
                    x.IsPrimaryAddress == true);

            // no primary, find any other address.
            if (storeLocation == null)
            {
                storeLocation = await _context.Locations
                .FirstOrDefaultAsync(x =>
                    x.StakeholderId == store.StakeholderId);
            }

            Coordinate destination;

            if (storeLocation == null || !storeLocation.Lat.HasValue || !storeLocation.Long.HasValue)
            {
                destination = new Coordinate()
                {
                    Latitude = -29.7374,
                    Longitude = 31.0648
                };
            }
            else
            {
                var lat = CheckCoOrdinate(storeLocation.Lat.Value, true);
                var lng = CheckCoOrdinate(storeLocation.Long.Value, false);
                if (lat == 0 || lng == 0)
                {
                    destination = new Coordinate()
                    {
                        Latitude = -29.7374,
                        Longitude = 31.0648
                    };
                }
                else
                {
                    destination = new Coordinate()
                    {
                        Latitude = Convert.ToDouble(lat),
                        Longitude = Convert.ToDouble(lng)
                    };
                }
            }

            float distance = 0f;

            try
            {
                distance = (float)GeoCalculator.GetDistance(origin, destination, 2, DistanceUnit.Kilometers);
            }
            catch (Exception)
            {
                distance = -1f;
            }

            var entity = _mapper.Map<CreateEmployeeStoreCheckInCommand, EmployeeStoreCheckIn>(command);

            //entity.CheckInUTCDate = DateTime.UtcNow;
            entity.CheckInUTCDate = DateTime.Now;
            entity.CheckInDistance = distance;
            _context.EmployeeStoreCheckIns.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmployeeStoreCheckInId;
            return opStatus;
        }
        catch (Exception ex)
        {
            return OperationStatus.CreateFromException("Error checking in.", ex);
        }
    }


    double CheckCoOrdinate(float value, bool isLat)
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
}
