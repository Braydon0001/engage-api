using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.SurveyAnswerPhotos.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.SurveyAnswerPhotos.Queries
{
    public class GetSurveyAnswerPhotoListQuery : GetQuery, IRequest<ListResult<SurveyAnswerPhotoListItemDto>>
    {
        public int? EmployeeStoreSurveyAnswerId { get; set; }       
    }

    public class GetSurveyAnswerPhotoListQueryHandler : BaseQueryHandler, IRequestHandler<GetSurveyAnswerPhotoListQuery, ListResult<SurveyAnswerPhotoListItemDto>>
    {
        public GetSurveyAnswerPhotoListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<SurveyAnswerPhotoListItemDto>> Handle(GetSurveyAnswerPhotoListQuery request, CancellationToken cancellationToken)
        {
            var photos = await _context.SurveyAnswerPhotos
                .Where(x => x.SurveyAnswerId == (request.EmployeeStoreSurveyAnswerId ?? x.SurveyAnswerId))     
                .ProjectTo<SurveyAnswerPhotoListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ListResult<SurveyAnswerPhotoListItemDto>
            {
                Data = photos,
                Count = photos.Count
            };
        }
    }
}
