using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SurveyAnswerPhotos.Models
{
    public class SurveyAnswerPhotoListItemDtoBySurvey : IMapFrom<SurveyAnswerPhoto>
    {
        public int Id { get; set; }        
        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyAnswerPhoto, SurveyAnswerPhotoListItemDtoBySurvey>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerPhotoId));
        }
    }
}
