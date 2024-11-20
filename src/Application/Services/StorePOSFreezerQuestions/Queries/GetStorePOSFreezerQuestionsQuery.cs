using Engage.Application.Services.StorePOSFreezerQuestions.Models;

namespace Engage.Application.Services.StorePOSFreezerQuestions.Queries;

public class GetStorePOSFreezerQuestionsQuery : GetQuery, IRequest<ListResult<StorePOSFreezerQuestionDto>>
{
    public int StoreId { get; set; }
}

public class GetStorePOSFreezerQuestionsQueryHandler : BaseQueryHandler, IRequestHandler<GetStorePOSFreezerQuestionsQuery, ListResult<StorePOSFreezerQuestionDto>>
{
    public GetStorePOSFreezerQuestionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StorePOSFreezerQuestionDto>> Handle(GetStorePOSFreezerQuestionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.StorePOSFreezerQuestions.Where(e => e.StoreId == request.StoreId)
                                                       .OrderBy(e => e.StorePOSFreezerQuestionId)
                                                       .ProjectTo<StorePOSFreezerQuestionDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

        return new ListResult<StorePOSFreezerQuestionDto>
        {
            Count = entities.Count,
            Data = entities
        };
    }
}
