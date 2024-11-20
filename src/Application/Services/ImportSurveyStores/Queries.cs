using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.ImportSurveyStores
{
    // Queries
    public class ImportSurveyStoreQuery : GetByIdQuery, IRequest<ImportSurveyStoreDto>
    {
    }

    public class ImportSurveyStoresQuery : GetQuery, IRequest<ListResult<ImportSurveyStoreListDto>>
    {
        public int? ImportFileId { get; set; }
        public int? SurveyId { get; set; }
    }

    // Handlers
    public class ImportSurveyStoreQueryHandler : BaseQueryHandler, IRequestHandler<ImportSurveyStoreQuery, ImportSurveyStoreDto>
    {
        public ImportSurveyStoreQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ImportSurveyStoreDto> Handle(ImportSurveyStoreQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.ImportSurveyStores
                                .Where(e => e.ImportSurveyStoreId == query.Id)
                                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<ImportSurveyStore, ImportSurveyStoreDto>(entity);
        }
    }

    public class ImportSurveyStoresQueryHandler : BaseQueryHandler, IRequestHandler<ImportSurveyStoresQuery, ListResult<ImportSurveyStoreListDto>>
    {
        public ImportSurveyStoresQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<ImportSurveyStoreListDto>> Handle(ImportSurveyStoresQuery query, CancellationToken cancellationToken)
        {
            var entities = await _context.ImportSurveyStores
                                .Where(e => e.ImportFileId == (query.ImportFileId ?? e.ImportFileId) &&
                                            e.SurveyId == (query.SurveyId ?? e.SurveyId))
                                .OrderBy(e => e.ImportFileId)
                                    .ThenBy(e => e.RowNo)
                                .ProjectTo<ImportSurveyStoreListDto>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

            return new ListResult<ImportSurveyStoreListDto>
            {
                Data = entities,
                Count = entities.Count
            };
        }
    }
}
