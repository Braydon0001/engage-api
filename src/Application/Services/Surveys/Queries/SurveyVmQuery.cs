using Engage.Application.Services.Surveys.Models;

namespace Engage.Application.Services.Surveys.Queries;

public class SurveyVmQuery : IRequest<SurveyVm>
{
    public int Id { get; set; }
}

public class SurveyVmQueryHandler : BaseQueryHandler, IRequestHandler<SurveyVmQuery, SurveyVm>
{

    public SurveyVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyVm> Handle(SurveyVmQuery query, CancellationToken cancellationToken)
    {
        var survey = await _context.Surveys.Include(e => e.SurveyType)
                                           .Include(e => e.EngageSubGroup)
                                           .Include(e => e.Supplier)
                                           .Include(e => e.EngageBrand)
                                           .Include(e => e.EngageMasterProduct)
                                           .Include(e => e.SurveyEngageRegions)
                                           .ThenInclude(e => e.EngageRegion)
                                           .SingleAsync(e => e.SurveyId == query.Id, cancellationToken);

        if (survey.EngageMasterProduct != null)
        {
            survey.EngageMasterProduct.Name = survey.EngageMasterProduct.Name + " - " + survey.EngageMasterProduct.Code;
        }

        var surveyVm = _mapper.Map<Survey, SurveyVm>(survey);
        return surveyVm;
    }
}
