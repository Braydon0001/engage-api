using Engage.Application.Services.FileTypes.Models;

namespace Engage.Application.Services.FileTypes.Queries;

public class FileTypeVmQuery : GetByIdQuery, IRequest<FileTypeVm>
{
}

public class FileTypeVmHandler : BaseQueryHandler, IRequestHandler<FileTypeVmQuery, FileTypeVm>
{
    public FileTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<FileTypeVm> Handle(FileTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileTypes.SingleOrDefaultAsync(e => e.FileTypeId == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(FileType), request.Id);
        }

        return _mapper.Map<FileType, FileTypeVm>(entity);
    }
}
