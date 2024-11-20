using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.SurveyInstances.Queries;

public class SurveyInstanceWebVmQuery : IRequest<SurveyInstanceWebVm>
{
    public int Id { get; set; }
}
public class SurveyInstanceWebVmHandler : BaseQueryHandler, IRequestHandler<SurveyInstanceWebVmQuery, SurveyInstanceWebVm>
{
    public SurveyInstanceWebVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyInstanceWebVm> Handle(SurveyInstanceWebVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyInstances.SingleAsync(x => x.SurveyInstanceId == request.Id, cancellationToken);

        return _mapper.Map<SurveyInstance, SurveyInstanceWebVm>(entity);
    }
}
