namespace Engage.Application.Services.ProjectNotes.Queries;

public record ProjectNoteVmQuery(int Id) : IRequest<ProjectNoteVm>;

public record ProjectNoteVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectNoteVmQuery, ProjectNoteVm>
{
    public async Task<ProjectNoteVm> Handle(ProjectNoteVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectNotes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectNoteId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectNoteVm>(entity);
    }
}