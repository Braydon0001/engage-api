using Engage.Application.Services.StorePOSFreezerQuestions.Models;

namespace Engage.Application.Services.StorePOSFreezerQuestions.Queries;

public class GetStorePOSFreezerQuestionVmQuery : GetByIdQuery, IRequest<StorePOSFreezerQuestionVm>
{

}

public class GetStorePOSFreezerQuestionVmQueryHandler : BaseListQueryHandler, IRequestHandler<GetStorePOSFreezerQuestionVmQuery, StorePOSFreezerQuestionVm>
{
    public GetStorePOSFreezerQuestionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StorePOSFreezerQuestionVm> Handle(GetStorePOSFreezerQuestionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StorePOSFreezerQuestions.Include(e => e.Store)
                                                            .Include(e => e.StorePOSType)
                                                            .Include(e => e.StorePOSFreezerType)
                                                            .SingleAsync(e => e.StorePOSFreezerQuestionId == request.Id, cancellationToken);

        return _mapper.Map<StorePOSFreezerQuestion, StorePOSFreezerQuestionVm>(entity);
    }
}
