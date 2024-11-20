using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.StoreSurveys.Models;
using Engage.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.StoreSurveys.Queries
{
    public class GetStoreSurveyQuery : IRequest<DataResult<StoreSurveyDto>>
    {
        public int SurveyInstanceId { get; set; }
    }

    public class GetStoreSurveyQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreSurveyQuery, DataResult<StoreSurveyDto>>
    {
        public GetStoreSurveyQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<DataResult<StoreSurveyDto>> Handle(GetStoreSurveyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SurveyInstances
                 .Include(x => x.Survey)
                    .ThenInclude(x => x.EngageSubGroup)
                 .Include(x => x.Survey)
                    .ThenInclude(x => x.Supplier)
                 .Include(x => x.Survey)
                    .ThenInclude(x => x.EngageBrand)
                 .Include(x => x.SurveyAnswers)
                    .ThenInclude(x => x.SurveyQuestion)
                       .ThenInclude(x => x.QuestionType)
                 .Include(x => x.SurveyAnswers)
                    .ThenInclude(x => x.SurveyQuestion)
                       .ThenInclude(x => x.SurveyQuestionOptions)
                 .Include(x => x.SurveyAnswers)
                    .ThenInclude(x => x.SurveyQuestion)
                       .ThenInclude(x => x.SurveyQuestionFalseReasons)
                            .ThenInclude(x => x.QuestionFalseReason)
                 .Include(x => x.SurveyAnswers)
                    .ThenInclude(x => x.SurveyAnswerOptions)
                       .ThenInclude(x => x.SurveyQuestionOption)
                 .Include(x => x.SurveyAnswers)
                    .ThenInclude(x => x.SurveyAnswerPhotos)
                 .FirstOrDefaultAsync(x => x.SurveyInstanceId == request.SurveyInstanceId, cancellationToken);

            var relatedDataEntity = _mapper.Map<SurveyInstance, StoreSurveyResult>(entity);
            var transformedEntity = _mapper.Map<StoreSurveyResult, StoreSurveyDto>(relatedDataEntity);

            return new DataResult<StoreSurveyDto>()
            {
                Data = transformedEntity
            };
        }
    }
}
