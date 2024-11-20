using System.Threading;
using System.Threading.Tasks;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.SurveyAnswerPhotos.Models;
using MediatR;

namespace Engage.Application.Services.SurveyAnswerPhotos.Queries
{
    public class GetSurveyAnswerPhotoViewModelQuery : GetByNullableIdQuery, IRequest<SurveyAnswerPhotoVM>
    {
    }

    public class GetEmployeeStoreSurveyPhotoViewModelQueryHandler : BaseViewModelQueryHandler, IRequestHandler<GetSurveyAnswerPhotoViewModelQuery, SurveyAnswerPhotoVM>
    {
        public GetEmployeeStoreSurveyPhotoViewModelQueryHandler(IMediator mediator) : base(mediator) { }

        public async Task<SurveyAnswerPhotoVM> Handle(GetSurveyAnswerPhotoViewModelQuery request, CancellationToken cancellationToken)
        {
            var vm = new SurveyAnswerPhotoVM();

            if (request.Id.HasValue)
            {
                vm.SurveyAnswerPhoto = await _mediator.Send(new GetSurveyAnswerPhotoQuery() { Id = request.Id.Value });
            }
                        
            return vm;
        }
    }
}
