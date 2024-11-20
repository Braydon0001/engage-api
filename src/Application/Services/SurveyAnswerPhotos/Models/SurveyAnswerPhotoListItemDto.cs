using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SurveyAnswerPhotos.Models
{
    public class SurveyAnswerPhotoListItemDto : IMapFrom<SurveyAnswerPhoto>
    {
        public int Id { get; set; }
        public int SurveyAnswerId { get; set; }
        public string FileName { get; set; }
        public string Folder { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyAnswerPhoto, SurveyAnswerPhotoListItemDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerPhotoId));
        }
    }
}
