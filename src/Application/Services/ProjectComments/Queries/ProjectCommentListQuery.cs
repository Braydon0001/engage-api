namespace Engage.Application.Services.ProjectComments.Queries;

public class ProjectCommentListQuery : IRequest<List<ProjectCommentDto>>
{
    public int ProjectId { get; set; }
}

public record ProjectCommentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCommentListQuery, List<ProjectCommentDto>>
{
    public async Task<List<ProjectCommentDto>> Handle(ProjectCommentListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectComments.AsQueryable().AsNoTracking().Where(e => e.ProjectId == query.ProjectId);

        var data = await queryable.OrderByDescending(e => e.Created)
                              .ProjectTo<ProjectCommentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        if (data.Count > 0)
        {
            foreach (var item in data)
            {
                var user = await Context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == item.CreatedBy.ToLower(), cancellationToken);
                if (user != null)
                {
                    var employee = await Context.Employees.FirstOrDefaultAsync(e => e.UserId == user.UserId, cancellationToken);
                    if (employee != null)
                    {
                        item.UserPhotoUrl = employee.Files?.Where(e => e.Type.ToLower() == "photo").FirstOrDefault()?.Url;
                    }
                }
            }
        }


        return data;
    }
}