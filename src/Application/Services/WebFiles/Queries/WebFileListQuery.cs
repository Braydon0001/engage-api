namespace Engage.Application.Services.WebFiles.Queries;

public class WebFileListQuery : GetQuery, IRequest<ListResult<WebFileDto>>
{
    public int? WebFileCategoryId { get; set; }
    public int? FileTypeId { get; set; }
    public bool? IsNPrinted { get; set; }
}

public class WebFileListHandler : BaseQueryHandler, IRequestHandler<WebFileListQuery, ListResult<WebFileDto>>
{
    public WebFileListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<WebFileDto>> Handle(WebFileListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.WebFiles.AsQueryable().AsNoTracking();

        if (query.WebFileCategoryId.HasValue)
        {
            queryable = queryable.Where(e => e.WebFileCategoryId == query.WebFileCategoryId);
        }

        if (query.FileTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.FileTypeId == query.FileTypeId);
        }

        if (query.IsNPrinted.HasValue && query.IsNPrinted.Value == true)
        {
            queryable = queryable.Where(e => e.NPrintingId != null);
        }

        if (query.IsNPrinted.HasValue && query.IsNPrinted.Value == false)
        {
            queryable = queryable.Where(e => e.NPrintingId == null);
        }

        var entities = await queryable.OrderBy(e => e.WebFileId)
                                      .ProjectTo<WebFileDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        entities = entities.Select(file =>
        {
            if (file.Files != null && file.Files.Count == 1)
            {
                var jsonFile = file.Files[0];
                file.FileUrl = jsonFile.Url;
                file.FileName = jsonFile.Name;
            }

            return file;
        }).ToList();

        return new ListResult<WebFileDto>(entities);
    }
}