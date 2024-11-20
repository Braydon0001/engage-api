using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SurveyAnswerPhotos.Commands
{
    public class SurveyAnswerPhotoCommand : IMapTo<SurveyAnswerPhoto>
    {
        public int SurveyAnswerId { get; set; }
        public string FileName { get; set; }
        public string Folder { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<SurveyAnswerPhotoCommand, SurveyAnswerPhoto>();
        }
    }
}
