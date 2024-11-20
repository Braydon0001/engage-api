namespace Engage.Application.Services.ProjectNotes.Queries;

public class ProjectNoteListQuery : IRequest<List<ProjectNoteDto>>
{
    public int ProjectId { get; init; }
}

public record ProjectNoteListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectNoteListQuery, List<ProjectNoteDto>>
{
    public async Task<List<ProjectNoteDto>> Handle(ProjectNoteListQuery query, CancellationToken cancellationToken)
    {
        if (query.ProjectId < 1)
        {
            throw new Exception("Project not found");
        }

        var queryable = Context.ProjectNotes.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.ProjectId == query.ProjectId);

        var data = await queryable.OrderByDescending(e => e.ProjectNoteId)
                                  .ProjectTo<ProjectNoteDto>(Mapper.ConfigurationProvider)
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

        //return await queryable.OrderByDescending(e => e.ProjectNoteId)
        //                      .ProjectTo<ProjectNoteDto>(Mapper.ConfigurationProvider)
        //                      .ToListAsync(cancellationToken);
    }
}