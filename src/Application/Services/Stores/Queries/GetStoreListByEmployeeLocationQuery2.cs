using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.Stores.Models;
using Geolocation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.Stores.Queries
{
    public class GetStoreListByLocationQuery2 : IRequest<ListResult<StoreListDto>>
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class GetStoreListByLocationQuery2Handler : BaseQueryHandler, IRequestHandler<GetStoreListByLocationQuery2, ListResult<StoreListDto>>
    {
        public GetStoreListByLocationQuery2Handler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<StoreListDto>> Handle(GetStoreListByLocationQuery2 request, CancellationToken cancellationToken)
        {
            Coordinate origin = new Coordinate() { Latitude = request.Lat, Longitude = request.Lon };
            CoordinateBoundaries boundaries = new CoordinateBoundaries(origin, 30, DistanceUnit.Kilometers);

            double minLatitude = boundaries.MinLatitude;
            double maxLatitude = boundaries.MaxLatitude;
            double minLongitude = boundaries.MinLongitude;
            double maxLongitude = boundaries.MaxLongitude;

            var results = await _context.Stores
                            .Where(s => s.Disabled == false)
                            .Where(s => s.PrimaryLocation.Lat >= minLatitude && s.PrimaryLocation.Lat <= maxLatitude)
                            .Where(s => s.PrimaryLocation.Long >= minLatitude && s.PrimaryLocation.Long <= maxLongitude)
                            .ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            results.ForEach(s => s.Distance = GeoCalculator.GetDistance(origin.Latitude, origin.Longitude, Convert.ToDouble(s.Lat), Convert.ToDouble(s.Long)));

            var entities = results.Where(s => s.Distance < 25).OrderBy(x => x.Distance).ToList();

            return new ListResult<StoreListDto>()
            {
                Data = entities,
                Count = entities.Count
            };

        }
    }
}
