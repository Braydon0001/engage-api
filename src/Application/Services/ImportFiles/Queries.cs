using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.ImportFiles
{
    // Queries
    public class ImportFileQuery : GetByIdQuery, IRequest<ImportFileDto>
    {
    }

    public class ImportFilesQuery : GetQuery, IRequest<ListResult<ImportFileListDto>>
    {
        public List<int> FileUploadIds { get; set; }

        public bool? IsConfirmed { get; set; }
    }

    // Handlers 
    public class ImportFileQueryHandler : BaseQueryHandler, IRequestHandler<ImportFileQuery, ImportFileDto>
    {
        public ImportFileQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ImportFileDto> Handle(ImportFileQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.ImportFiles.FirstOrDefaultAsync(e => e.ImportFileId == query.Id, cancellationToken);

            return _mapper.Map<ImportFile, ImportFileDto>(entity);
        }
    }

    public class ImportFilesQueryHandler : BaseQueryHandler, IRequestHandler<ImportFilesQuery, ListResult<ImportFileListDto>>
    {
        public ImportFilesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<ImportFileListDto>> Handle(ImportFilesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _context.ImportFiles
                                    .Where(e => query.FileUploadIds != null && query.FileUploadIds.Count > 0
                                                    ? query.FileUploadIds.Contains(e.ImportFileId)
                                                    : e.ImportFileId == e.ImportFileId &&
                                                query.IsConfirmed.HasValue
                                                    ? query.IsConfirmed.Value == true
                                                        ? e.ConfirmedDate != null
                                                        : e.ConfirmedDate == null
                                                    : e.ConfirmedDate == e.ConfirmedDate)
                                    .ProjectTo<ImportFileListDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

            return new ListResult<ImportFileListDto>
            {
                Data = entities,
                Count = entities.Count
            };
        }
    }
}
