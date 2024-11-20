using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities.LinkEntities;
using Engage.Domain.Enums;

namespace Engage.Application.Services.ImportSurveyStores
{
    public class AccountNoImport
    {
        public string AccountNo { get; set; }
    }

    public class ImportSurveyStoreDto : IMapFrom<ImportSurveyStore>
    {
        public int Id { get; set; }
        public int FileUploadId { get; set; }
        public ImportRowType ImportRowType { get; set; }
        public string ImportRowMessage { get; set; }
        public int RowNo { get; set; }
        public int SurveyId { get; set; }
        public int? StoreId { get; set; }
        public string AccountNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImportSurveyStore, ImportSurveyStoreDto>()
                 .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ImportSurveyStoreId));
        }
    }

    public class ImportSurveyStoreListDto : IMapFrom<ImportSurveyStore>
    {
        public int Id { get; set; }
        public int ImportFileId { get; set; }
        public int RowNo { get; set; }
        public ImportRowType ImportRowType { get; set; }
        public string ImportRowMessage { get; set; }
        public int SurveyId { get; set; }
        public int? StoreId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreFormat { get; set; }
        public string EngageRegion { get; set; }
        public string AccountNumber { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImportSurveyStore, ImportSurveyStoreListDto>()
                 .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ImportSurveyStoreId))
                 .ForMember(d => d.StoreCode, opts => opts.MapFrom(s => s.Store.Code))
                 .ForMember(d => d.StoreName, opts => opts.MapFrom(s => s.Store.Name))
                 .ForMember(d => d.StoreFormat, opts => opts.MapFrom(s => s.Store.StoreFormat.Name))
                 .ForMember(d => d.EngageRegion, opts => opts.MapFrom(s => s.Store.EngageRegion.Name));
        }
    }

    public class SurveyStoreListDto : IMapFrom<SurveyStore>
    {
        public int SurveyId { get; set; }
        public int? StoreId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreFormat { get; set; }
        public string EngageRegion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyStore, SurveyStoreListDto>()
                 .ForMember(d => d.StoreCode, opts => opts.MapFrom(s => s.Store.Code))
                 .ForMember(d => d.StoreName, opts => opts.MapFrom(s => s.Store.Name))
                 .ForMember(d => d.StoreFormat, opts => opts.MapFrom(s => s.Store.StoreFormat.Name))
                 .ForMember(d => d.EngageRegion, opts => opts.MapFrom(s => s.Store.EngageRegion.Name));
        }
    }
}
