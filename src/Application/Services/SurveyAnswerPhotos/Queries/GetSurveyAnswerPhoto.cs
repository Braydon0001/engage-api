using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.SurveyAnswerPhotos.Models;
using Engage.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Engage.Application.Services.SurveyAnswerPhotos.Queries
{
    public class GetSurveyAnswerPhotoQuery : GetByIdQuery, IRequest<SurveyAnswerPhotoDto>
    { }

    public class GetSurveyAnswerPhotoDtoQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveyAnswerPhotoQuery, SurveyAnswerPhotoDto>
    {
        public GetSurveyAnswerPhotoDtoQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<SurveyAnswerPhotoDto> Handle(GetSurveyAnswerPhotoQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SurveyAnswerPhotos.FirstOrDefaultAsync(x => x.SurveyAnswerPhotoId == request.Id, cancellationToken);

            return _mapper.Map<SurveyAnswerPhoto, SurveyAnswerPhotoDto>(entity);
        }
    }
}
