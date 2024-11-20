using Engage.Application.Services.StorePOSQuestions.Models;

namespace Engage.Application.Services.StorePOSQuestions.Queries;

public class GetStorePOSQuestionVmQuery : GetByIdQuery, IRequest<StorePOSQuestionVm>
{

}

public class GetStorePOSQuestionVmQueryHandler : BaseListQueryHandler, IRequestHandler<GetStorePOSQuestionVmQuery, StorePOSQuestionVm>
{
    public GetStorePOSQuestionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StorePOSQuestionVm> Handle(GetStorePOSQuestionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StorePOSQuestions.Include(e => e.Store)
                                                     .Include(e => e.StorePOSType)
                                                     .SingleAsync(e => e.StorePOSQuestionId == request.Id, cancellationToken);

        return _mapper.Map<StorePOSQuestion, StorePOSQuestionVm>(entity);
    }
}
