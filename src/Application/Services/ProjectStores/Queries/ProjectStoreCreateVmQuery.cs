using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectStakeholders.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public record ProjectStoreCreateVmQuery() : IRequest<ProjectStoreCreateVm>;

public class ProjectStoreCreateVmHandler : BaseQueryHandler, IRequestHandler<ProjectStoreCreateVmQuery, ProjectStoreCreateVm>
{
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    public ProjectStoreCreateVmHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _mediator = mediator;
        _user = user;
    }

    public async Task<ProjectStoreCreateVm> Handle(ProjectStoreCreateVmQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email.ToLower() == _user.UserName.ToLower(), cancellationToken)
                            ?? throw new Exception("User not found");

            var currentUser = new ProjectStakeholderSearchOption
            {
                Id = user.UserId,
                Name = $"{user.FullName} - {user.Email}",
                Identifier = "user",

            };

            ProjectStoreCreateVm data = new()
            {
                ProjectAssignedTo = new List<ProjectStakeholderSearchOption> { currentUser },
                StakeholderIds = new List<ProjectStakeholderSearchOption> { currentUser },
                EngageBrandIds = [],
                ProjectOwnerId = new OptionDto(user.UserId, user.FullName)
            };

            return data;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}