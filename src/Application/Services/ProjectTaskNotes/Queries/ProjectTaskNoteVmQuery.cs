namespace Engage.Application.Services.ProjectTaskNotes.Queries;

public record ProjectTaskNoteVmQuery(int Id) : IRequest<ProjectTaskNoteVm>;

public record ProjectTaskNoteVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskNoteVmQuery, ProjectTaskNoteVm>
{
    public async Task<ProjectTaskNoteVm> Handle(ProjectTaskNoteVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskNotes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProjectTask);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectTaskNoteId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectTaskNoteVm>(entity);
    }
}