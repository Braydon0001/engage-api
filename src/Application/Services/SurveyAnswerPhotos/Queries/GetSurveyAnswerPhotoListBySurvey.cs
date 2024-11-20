using Engage.Application.Services.SurveyAnswerPhotos.Models;

namespace Engage.Application.Services.SurveyAnswerPhotos.Queries;

public class GetSurveyAnswerPhotoListQueryBySurvey : GetQuery, IRequest<PaginatedListResult<SurveyAnswerPhotoListItemDtoBySurvey>>
{
    public int SurveyId { get; set; }
}

public record GetSurveyAnswerPhotoListBySurveyQueryHandler(IAppDbContext Context) : IRequestHandler<GetSurveyAnswerPhotoListQueryBySurvey, PaginatedListResult<SurveyAnswerPhotoListItemDtoBySurvey>>
{
    public async Task<PaginatedListResult<SurveyAnswerPhotoListItemDtoBySurvey>> Handle(GetSurveyAnswerPhotoListQueryBySurvey request, CancellationToken cancellationToken)
    {
        var (search, filters, orderBy, orderDirection, pageNo, pageSize, skip) = request;

        var query = Context.SurveyInstances.Where(e => e.SurveyId == request.SurveyId)
                                            .Join(Context.SurveyAnswers,
                                                    surveyInstance => surveyInstance.SurveyInstanceId,
                                                    surveyAnswer => surveyAnswer.SurveyInstanceId,
                                                    (surveyInstance, surveyAnswer) => surveyAnswer.SurveyAnswerId)
                                            .Join(Context.SurveyAnswerPhotos,
                                                    surveyAnswerId => surveyAnswerId,
                                                    surveyAnswerPhoto => surveyAnswerPhoto.SurveyAnswerId,
                                                    (surveyAnswerId, surveyAnswerPhoto) => surveyAnswerPhoto);

        var photos = await query.Skip(skip)
                                .Take(pageSize)
                                .OrderBy(e => e.SurveyAnswerPhotoId)
                                .ToListAsync(cancellationToken);

        var rowCount = await query.CountAsync(cancellationToken);

        var photoDtos = photos.Select(photo => new SurveyAnswerPhotoListItemDtoBySurvey
        {
            Id = photo.SurveyAnswerPhotoId,
            ImageUrl = $"https://ensource.insightconsulting.co.za/surveyphotos{photo.Folder[photo.Folder.IndexOf('/')..]}{photo.FileName[photo.FileName.LastIndexOf('/')..]}"
        })
        .ToList();

        return new PaginatedListResult<SurveyAnswerPhotoListItemDtoBySurvey>(photoDtos, PaginationResult.Create(pageNo, pageSize, rowCount));

    }
}
