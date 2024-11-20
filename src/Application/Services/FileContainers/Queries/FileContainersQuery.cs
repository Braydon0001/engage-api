using Engage.Application.Services.FileContainers.Models;

namespace Engage.Application.Services.FileContainers.Queries
{
    public class FileContainersQuery : GetQuery, IRequest<ListResult<FileContainerDto>>
    {
    }

    public class FileContainersQueryHandler : BaseQueryHandler, IRequestHandler<FileContainersQuery, ListResult<FileContainerDto>>
    {
        public FileContainersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<FileContainerDto>> Handle(FileContainersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.FileContainers.OrderBy(e => e.Name)
                                                        .ProjectTo<FileContainerDto>(_mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);

            return new ListResult<FileContainerDto>(entities);
        }
    }
}
