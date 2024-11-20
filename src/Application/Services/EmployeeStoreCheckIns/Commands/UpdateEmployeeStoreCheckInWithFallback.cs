using Geolocation;
using System.Globalization;

namespace Engage.Application.Services.EmployeeStoreCheckIns.Commands;

public class UpdateEmployeeStoreCheckInWithFallbackCommand : EmployeeStoreCheckInCommand, IRequest<OperationStatus>, IMapTo<EmployeeStoreCheckIn>
{
    public string CheckInUuid { get; set; }
    public DateTime? CheckOutTimezoneDate { get; set; }
    public float? CheckOutLat { get; set; }
    public float? CheckOutLong { get; set; }
    public float? CheckOutDistance { get; set; }
    public int EmployeeId { get; set; }
    public int? StoreId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateEmployeeStoreCheckInWithFallbackCommand, EmployeeStoreCheckIn>();
    }
}

public class UpdateEmployeeStoreCheckInWithFallbackCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeStoreCheckInWithFallbackCommand, OperationStatus>
{
    public UpdateEmployeeStoreCheckInWithFallbackCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeStoreCheckInWithFallbackCommand command, CancellationToken cancellationToken)
    {
        try
        {
            string lastCheckOutId = command.CheckInUuid;

            // Fetch the last check in for this employee, assign the CheckInUUID from the command to the entity.
            if (string.IsNullOrWhiteSpace(lastCheckOutId))
            {
                var lastEntry = await _context.EmployeeStoreCheckIns
                        .Where(s => s.EmployeeId == command.EmployeeId)
                        .OrderByDescending(s => s.EmployeeStoreCheckInId)
                        .FirstOrDefaultAsync(e => e.CheckOutUTCDate == null);

                if (lastEntry != null)
                {
                    command.CheckInUuid = lastEntry.CheckInUuid;
                    lastCheckOutId = lastEntry.CheckInUuid;
                }
                else
                {
                    throw new Exception("No Previous Check In Found");
                }
            }

            var entity = await _context.EmployeeStoreCheckIns
                 .FirstOrDefaultAsync(x => x.CheckInUuid == lastCheckOutId);

            if (entity == null && command.StoreId != null)
            {
                await _mediator.Send(new CreateEmployeeStoreCheckInCommand
                {
                    CheckInDistance = command.CheckOutDistance ?? 0,
                    CheckInLat = command.CheckOutLat ?? (float)-29.7374,
                    CheckInLong = command.CheckOutLong ?? (float)31.0648,
                    CheckInTimezoneDate = command.CheckOutTimezoneDate ?? DateTime.Now,
                    CheckInUuid = command.CheckInUuid,
                    EmployeeId = command.EmployeeId,
                    StoreId = (int)command.StoreId,
                });

                entity = await _context.EmployeeStoreCheckIns
                .FirstOrDefaultAsync(x => x.CheckInUuid == lastCheckOutId);
            }

            var store = await _context.Stores
                .Include(s => s.PrimaryLocation)
                .FirstOrDefaultAsync(s => s.StoreId == entity.StoreId);

            Coordinate origin = new Coordinate()
            {
                Latitude = Convert.ToDouble(command.CheckOutLat),
                Longitude = Convert.ToDouble(command.CheckOutLong)
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
                // Use the HQ as a default address.
                destination = new Coordinate()
                {
                    Latitude = -29.7374,
                    Longitude = 31.0648
                };
            }
            else
            {
                var lat = checkCoOrdinate(storeLocation.Lat.Value, true);
                var lng = checkCoOrdinate(storeLocation.Long.Value, false);
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

            var distance = 0f;

            try
            {
                distance = (float)GeoCalculator.GetDistance(origin, destination, 2, DistanceUnit.Kilometers);
            }
            catch (Exception)
            {
                distance = -1f;
            }

            entity.CheckOutUTCDate = DateTime.Now;
            entity.CheckOutDistance = distance;
            command.CheckOutDistance = distance;

            return await SaveChangesAsync(command, entity, nameof(EmployeeStoreCheckIns), entity.EmployeeStoreCheckInId, cancellationToken);
        }
        catch (Exception ex)
        {
            return OperationStatus.CreateFromException("Error updating checkout.", ex);
        }
    }

    double checkCoOrdinate(float value, bool isLat)
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
