using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;

namespace Engage.Application.Services.SurveyAnswerPhotos.Models
{
    public class SurveyAnswerPhotoDto : IMapFrom<SurveyAnswerPhoto>
    {
        public int Id { get; set; }
        public int SurveyAnswerId { get; set; }
        public string FileName { get; set; }
        public string Folder { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyAnswerPhoto, SurveyAnswerPhotoDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyAnswerPhotoId)); 
        }
    }
    
    public class EmployeeStoreSurveyPhotoDto2 : IMapFrom<SurveyAnswerPhoto>
    {
        public int EmployeeStoreSurveyPhotoId { get; set; }
        public string FileName { get; set; }
        public string RelativeUrl { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyAnswerPhoto, EmployeeStoreSurveyPhotoDto2>()
                .ForMember(d => d.RelativeUrl, opt => opt.MapFrom(s => $"{s.Folder}/{s.FileName}") ); 
        }
    }
}
