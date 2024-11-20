using Engage.Application.Services.FileTypes.Models;

namespace Engage.Application.Services.FileTypes.Queries;

public class FileTypeOptionsQuery : GetQuery, IRequest<List<FileTypeDto>>
{
}

public class FileTypeOptionsHandler : BaseQueryHandler, IRequestHandler<FileTypeOptionsQuery, List<FileTypeDto>>
{
    public FileTypeOptionsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<FileTypeDto>> Handle(FileTypeOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.FileTypes.OrderBy(e => e.Name)
                                               .ProjectTo<FileTypeDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        return new List<FileTypeDto>(entities);
    }
}