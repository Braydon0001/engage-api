using Engage.Application.Services.FileUploads.Models;

namespace Engage.Application.Services.FileUploads.Queries;

public class FileUploadVmQuery : IRequest<FileUploadVm>
{
    public int Id { get; set; }
}

public class FileUploadVmQueryHandler : BaseQueryHandler, IRequestHandler<FileUploadVmQuery, FileUploadVm>
{
    public FileUploadVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<FileUploadVm> Handle(FileUploadVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileUploads.SingleAsync(e => e.FileUploadId == request.Id, cancellationToken);

        return _mapper.Map<FileUpload, FileUploadVm>(entity);
    }
}
