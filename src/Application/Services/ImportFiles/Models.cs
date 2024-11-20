using AutoMapper;
using Engage.Application.Mappings;
using System;

namespace Engage.Application.Services.ImportFiles
{
    public class ImportFileDto : IMapFrom<ImportFile>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? RejectedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImportFile, ImportFileDto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ImportFileId));
        }
    }

    public class ImportFileListDto : IMapFrom<ImportFile>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? RejectedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImportFile, ImportFileListDto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ImportFileId));
        }
    }
}
