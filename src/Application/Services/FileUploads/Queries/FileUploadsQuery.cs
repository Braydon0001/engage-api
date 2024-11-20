using Engage.Application.Services.FileUploads.Models;

namespace Engage.Application.Services.FileUploads.Queries;

public class FileUploadsQuery : IRequest<ListResult<FileUploadDto>>
{

}

public class FileUploadsQueryHandler : BaseQueryHandler, IRequestHandler<FileUploadsQuery, ListResult<FileUploadDto>>
{
    public FileUploadsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<FileUploadDto>> Handle(FileUploadsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.FileUploads.OrderByDescending(e => e.FileUploadId)
                                                 .ProjectTo<FileUploadDto>(_mapper.ConfigurationProvider)
                                                 .ToListAsync(cancellationToken);

        return new ListResult<FileUploadDto>(entities);
    }
}
