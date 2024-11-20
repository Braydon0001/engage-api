using Engage.Application.Services.StorePOSQuestions.Models;

namespace Engage.Application.Services.StorePOSQuestions.Queries;

public class GetStorePOSQuestionsQuery : GetQuery, IRequest<ListResult<StorePOSQuestionDto>>
{
    public int StoreId { get; set; }
}

public class GetStorePOSQuestionsQueryHandler : BaseQueryHandler, IRequestHandler<GetStorePOSQuestionsQuery, ListResult<StorePOSQuestionDto>>
{
    public GetStorePOSQuestionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StorePOSQuestionDto>> Handle(GetStorePOSQuestionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.StorePOSQuestions.Where(e => e.StoreId == request.StoreId)
                                                       .OrderBy(e => e.StorePOSQuestionId)
                                                       .ProjectTo<StorePOSQuestionDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

        return new ListResult<StorePOSQuestionDto>
        {
            Count = entities.Count,
            Data = entities
        };
    }
}
