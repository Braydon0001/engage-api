namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderOptionsSearchQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public record ProjectStakeholderOptionsSearchHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStakeholderOptionsSearchQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(ProjectStakeholderOptionsSearchQuery request, CancellationToken cancellationToken)
    {
        var projectStakeholders = await Context.ProjectStakeholderUsers
                                               .AsNoTracking()
                                               .Where(e => e.ProjectId == request.ProjectId)
                                               .Select(e => e.UserId)
                                               .ToListAsync(cancellationToken);

        var usersQuaryable = Context.Users.AsNoTracking().AsQueryable()
                                          .Where(e => !projectStakeholders.Contains(e.UserId));

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            usersQuaryable = usersQuaryable.Where(e => EF.Functions.Like(e.FirstName, $"%{request.Search}%")
                                                        || EF.Functions.Like(e.LastName, $"%{request.Search}%")
                                                        || EF.Functions.Like(e.Email, $"%{request.Search}%")
                                                        );
        }

        //var storeContacts = Context.StoreContacts.AsNoTracking().AsQueryable();
        //if (!string.IsNullOrWhiteSpace(request.Search))
        //{
        //}

        var users = await usersQuaryable.Take(100)
                                        .Select(e => new OptionDto { Id = e.UserId, Name = $"{e.FullName} - {e.Email}" })
                                        .ToListAsync(cancellationToken);

        return users;
    }
}
