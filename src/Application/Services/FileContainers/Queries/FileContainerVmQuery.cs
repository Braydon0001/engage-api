using Engage.Application.Services.FileContainers.Models;

namespace Engage.Application.Services.FileContainers.Queries;

public class FileContainerVmQuery : GetByIdQuery, IRequest<FileContainerVm>
{
}

public class FileContainerVmQueryHandler : BaseQueryHandler, IRequestHandler<FileContainerVmQuery, FileContainerVm>
{
    public FileContainerVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<FileContainerVm> Handle(FileContainerVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileContainers.SingleAsync(e => e.FileContainerId == request.Id, cancellationToken);

        return _mapper.Map<FileContainer, FileContainerVm>(entity);
    }
}
