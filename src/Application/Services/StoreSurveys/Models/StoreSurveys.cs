using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Engage.Application.Mappings;
using Engage.Domain.Entities;
using Engage.Domain.Entities.Views;

namespace Engage.Application.Services.StoreSurveys.Models
{

    public class StoreSurveyResultBase
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyNote { get; set; }
        public DateTime SurveyDate { get; set; }
        public OptionDto EngageMasterProductId { get; set; }
        public bool EnableMasterProductOnSurvey { get; set; }
        public int EngageSubGroupId { get; set; }
        public string EngageSubGroupName { get; set; }
        public int EngageBrandId { get; set; }
        public string EngageBrandName { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsRecurring { get; set; }
        public ICollection<StoreSurveyQuestionDto> Questions { get; set; }
    }

    public class StoreSurveyResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeeStoreView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeeStoreView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyNote, opt => opt.MapFrom(s => s.Survey.Note))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageMasterProductId, opt => opt.MapFrom(s => s.Survey.EngageMasterProductId.HasValue ? new OptionDto(s.Survey.EngageMasterProductId.Value, s.Survey.EngageMasterProduct.Name) : null))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.IsRequired, opt => opt.MapFrom(s => s.Survey.IsRequired))
                .ForMember(d => d.IsDisabled, opt => opt.MapFrom(s => s.Survey.IsDisabled))
                .ForMember(d => d.IsRecurring, opt => opt.MapFrom(s => s.Survey.IsRecurring))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
                
        }
    }

    #region View to StoreSurveyResult Mappings 
        
    // GetTargetedSurveys
    public class SurveyResult : StoreSurveyResultBase, IMapFrom<Survey>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Survey, StoreSurveyResult>()
                //.ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class StoreSurveyPerStoreResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeePerStoreView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeePerStoreView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class StoreSurveyPerRegionResult2 : StoreSurveyResultBase, IMapFrom<SurveysByEmployeePerRegionView2>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeePerRegionView2, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class SurveysByEmployeePerRegionResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeePerRegionView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeePerRegionView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class SurveysByEmployeePerStoreResult_ : StoreSurveyResultBase, IMapFrom<SurveysByEmployeePerStoreView_>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeePerStoreView_, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class SurveysByEmployeePerStoreFormatResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeePerStoreFormatView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeePerStoreFormatView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class SurveysByEmployeeSubGroupPerRegionResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeeSubGroupPerRegionView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeeSubGroupPerRegionView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class SurveysByEmployeeSubGroupPerStoreResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeeSubGroupPerStoreView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeeSubGroupPerStoreView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    public class SurveysByEmployeeSubGroupPerStoreFormatResult : StoreSurveyResultBase, IMapFrom<SurveysByEmployeeSubGroupPerStoreFormatView>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveysByEmployeeSubGroupPerStoreFormatView, StoreSurveyResult>()
                .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
                .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
                .ForMember(d => d.SurveyTitle, opt => opt.MapFrom(s => s.Survey.Title))
                .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
                .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
                .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name))
                .ForMember(d => d.Questions, opt => opt.MapFrom(s => s.Survey.SurveyQuestions.OrderBy(s => s.DisplayOrder)));
        }
    }

    #endregion

    public class StoreSurveysDto
    {
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int SuppliersCount { get; set; }
        public ICollection<SupplierSurveysDto> Suppliers { get; set; }
    }

    public class SupplierSurveysDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int SurveyCount { get; set; }
        public ICollection<StoreSurveyDto> Surveys { get; set; }
    }

    public class StoreSurveyDto : IMapFrom<StoreSurveyResult>
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public string SurveyNote { get; set; }
        public DateTime SurveyDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int EngageSubGroupId { get; set; }
        public string EngageSubGroupName { get; set; }
        public int EngageBrandId { get; set; }
        public string EngageBrandName { get; set; }
        public OptionDto EngageMasterProductId { get; set; }
        public bool EnableMasterProductOnSurvey { get; set; }
        public string QuestionCount { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsRecurring { get; set; }
        public ICollection<StoreSurveyQuestionDto> Questions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreSurveyResult, StoreSurveyDto>()
                .ForMember(d => d.QuestionCount, opt => opt.MapFrom(s => s.Questions.Count));
        }
    }
}
