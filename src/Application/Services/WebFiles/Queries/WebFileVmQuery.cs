namespace Engage.Application.Services.WebFiles.Queries;

public record WebFileVmQuery(int Id) : IRequest<WebFileVm>
{
}

public class WebFileVmHandler : BaseQueryHandler, IRequestHandler<WebFileVmQuery, WebFileVm>
{
    public WebFileVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebFileVm> Handle(WebFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebFiles.AsQueryable().AsNoTracking();

        var entity = await queryable.Include(e => e.WebFileCategory)
                                    .ThenInclude(e => e.WebFileGroup)
                                    .Include(e => e.FileType)
                                    .Include(e => e.TargetStrategy)
                                    .Include(e => e.Employee)
                                    .Include(e => e.Store)
                                    .SingleOrDefaultAsync(e => e.WebFileId == query.Id, cancellationToken);

        var vm = _mapper.Map<WebFile, WebFileVm>(entity);

        if (entity.Files != null && entity.Files.Count == 1)
        {
            var file = entity.Files[0];
            vm.FileUrl = file.Url;
            vm.FileName = file.Name;
        }

        return entity == null ? null : vm;
    }
}