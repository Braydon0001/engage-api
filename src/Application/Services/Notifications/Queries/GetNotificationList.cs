using Engage.Application.Services.Notifications.Models;

namespace Engage.Application.Services.Notifications.Queries
{
    public class GetNotificationListQuery : GetQuery, IRequest<ListResult<NotificationListDto>>
    {
        public bool? IsNational { get; set; }
        public DateTime? TimezoneDate { get; set; }
    }

    public class GetNotificationListQueryHandler : BaseQueryHandler, IRequestHandler<GetNotificationListQuery, ListResult<NotificationListDto>>
    {
        public GetNotificationListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<NotificationListDto>> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
        {
            // A notification is national if it has no regions 
            bool? isNational = request.IsNational ?? null;
            DateTime? timezoneDate = request.TimezoneDate ?? null;

            var entities = await _context.Notifications.Where(e => timezoneDate == null || timezoneDate.Value >= e.StartDate && timezoneDate.Value <= e.EndDate)
                                                       .Where(e => isNational == null || (isNational.Value ? !e.NotificationRegions.Any() : e.NotificationRegions.Any()))
                                                       .Include(e => e.NotificationRegions)
                                                       .ThenInclude(e => e.EngageRegion)
                                                       .OrderByDescending(d => d.NotificationId)
                                                       .ProjectTo<NotificationListDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return new ListResult<NotificationListDto>(entities);
        }
    }
}
