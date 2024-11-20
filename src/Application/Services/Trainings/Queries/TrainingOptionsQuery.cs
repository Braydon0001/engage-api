using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Trainings.Queries;

public class TrainingOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class TrainingOptionsQueryHandler : IRequestHandler<TrainingOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public TrainingOptionsQueryHandler(IAppDbContext context, IMediator mediator, IUserService user)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(TrainingOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Trainings.Where(e => e.Disabled == false && e.Deleted == false);

        if (string.IsNullOrWhiteSpace(_user.UserName))
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);
            if (!engageRegionIds.Contains(7))
            {
                queryable = queryable.Where(e => engageRegionIds.Contains(e.EngageRegionId.Value));
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            if (int.TryParse(request.Search, out int intId))
            {
                queryable = queryable.Where(e => e.TrainingId.ToString().Contains(intId.ToString()));
            }
            else
            {
                queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
            }
        }

        return await queryable.Select(e => new OptionDto(e.TrainingId, e.TrainingId + ": " + e.Name + GetDateDisplay(e.StartDate, e.EndDate) + " - " + e.EngageRegion.Name))
                              .ToListAsync(cancellationToken);
    }

    public static string GetDateDisplay(DateTime startDate, DateTime endDate)
    {
        return " (" + startDate.ToString("MMM") + " " + startDate.Day + " - " + endDate.ToString("MMM") + " " + endDate.Day + ")";
    }
}