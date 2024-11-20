using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Engage.Application.Services.StoreSurveys.Models;

namespace Engage.Application.Services.StoreSurveys
{
    public static class StoreSurveyTransforms
    {
        /// <summary>
        /// Transform flat EmployeeStoreSurvey records, for a single employee and single store,  
        /// into a tree  graph that groups surveys by supplier.  
        /// </summary>
        /// 
        /// Example. These flat records (In JSON format) are transformed this tree.
        /// 
        ///     "data": [
        ///     {
        ///         "id": 1,
        ///         "employeeId": 1,
        ///         "storeId": 1,
        ///         "storeName": "Spar Glenwood",
        ///         "surveyId": 1,
        ///         "surveyTitle": "Survey 1",
        ///         "surveyDate": "2020-04-20T00:00:00",
        ///         "supplierId": 1,
        ///         "supplierName": "Supplier One",
        ///         "engageBrandId": 2,
        ///         "engageBrandName": "Engage Brand 2"
        ///    },
        ///    {
        ///         "id": 2,
        ///         "employeeId": 1,
        ///         "storeId": 1,
        ///         "storeName": "Spar Glenwood",
        ///         "surveyId": 2,
        ///         "surveyTitle": "Survey 2",
        ///         "surveyDate": "2020-04-20T00:00:00",
        ///         "supplierId": 2,
        ///         "supplierName": "Supplier two",
        ///   },
        /// 
        ///  Resulting object graph has this shape (In JSON format):
        ///     "employeeId": 1,
        ///     "storeId": 1,
        ///     "storeName": "Spar Glenwood",
        ///     "suppliers": [
        ///         {
        ///             "supplierId": 1,
        ///             "supplierName": "Supplier One",
        ///             "surveyCount": 2,
        ///             "isCompleted": true,
        ///             "surveys": [
        ///                 {
        ///                     "surveyId": 1,
        ///                     "surveyTitle": "Survey 1",
        ///                     ...   
        ///       { 
        ///             "supplierId": 2,
        ///             "supplierName": "Supplier Two",
        ///             "surveyCount": 2,
        ///             "isCompleted": false,
        ///             "surveys": [
        ///                 {
        ///                     "surveyId": 2,
        ///                     "surveyTitle": "Survey 2",
        ///                      ...                     
        ///
        public static StoreSurveysDto GroupBySupplier(IMapper mapper, List<StoreSurveyResult> storeSurveysResults, string storeName = "")
        {
            if (storeSurveysResults == null || storeSurveysResults.Count == 0)
            {
                return null;
            }

            var firstStoreSurvey = storeSurveysResults.FirstOrDefault();

            var dto = new StoreSurveysDto()
            {
                EmployeeId = firstStoreSurvey.EmployeeId,
                StoreId = firstStoreSurvey.StoreId,
                StoreName = !string.IsNullOrWhiteSpace(storeName) ? storeName : firstStoreSurvey.StoreName,
                Suppliers = new List<SupplierSurveysDto>()
            };

            SupplierSurveysDto supplierSurveys = null;

            foreach (var storeSurveysResult in storeSurveysResults)
            {
                var storeSurvey = mapper.Map<StoreSurveyResult, StoreSurveyDto>(storeSurveysResult);

                if (supplierSurveys == null ||
                    supplierSurveys.SupplierId != storeSurveysResult.SupplierId)
                {
                    supplierSurveys = new SupplierSurveysDto()
                    {
                        SupplierId = storeSurveysResult.SupplierId,
                        SupplierName = storeSurveysResult.SupplierName,
                        Surveys = new List<StoreSurveyDto>()
                    };
                    dto.Suppliers.Add(supplierSurveys);
                };

                supplierSurveys.Surveys.Add(storeSurvey);
            }

            foreach (var suppliersurveys in dto.Suppliers)
            {
                suppliersurveys.SurveyCount = suppliersurveys.Surveys.Count;
            }

            dto.SuppliersCount = dto.Suppliers.Count;
            
            return dto;
        }
    }

}
