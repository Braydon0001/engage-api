using Engage.Application.Services.FileTypes.Models;

namespace Engage.Application.Services.FileTypes.Queries;

public class FileTypesQuery : GetQuery, IRequest<ListResult<FileTypeDto>>
{
}

public class WebFileTypesHandler : BaseQueryHandler, IRequestHandler<FileTypesQuery, ListResult<FileTypeDto>>
{
    public WebFileTypesHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<FileTypeDto>> Handle(FileTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.FileTypes.OrderBy(e => e.Name)
                                               .ProjectTo<FileTypeDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        return new ListResult<FileTypeDto>(entities);
    }
}