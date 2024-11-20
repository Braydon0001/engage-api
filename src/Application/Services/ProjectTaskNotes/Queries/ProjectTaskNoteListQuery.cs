namespace Engage.Application.Services.ProjectTaskNotes.Queries;

public class ProjectTaskNoteListQuery : IRequest<List<ProjectTaskNoteDto>>
{
    public int? ProjectTaskId { get; init; }
}

public record ProjectTaskNoteListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskNoteListQuery, List<ProjectTaskNoteDto>>
{
    public async Task<List<ProjectTaskNoteDto>> Handle(ProjectTaskNoteListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskNotes.AsQueryable().AsNoTracking();

        if (query.ProjectTaskId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectTaskId == query.ProjectTaskId.Value);
        }

        var data = await queryable.OrderByDescending(e => e.ProjectTaskNoteId)
                                  .ProjectTo<ProjectTaskNoteDto>(Mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        if (data.Count > 0)
        {
            foreach (var item in data)
            {
                var user = await Context.Users.FirstOrDefaultAsync(e => e.Email == item.CreatedBy, cancellationToken);
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