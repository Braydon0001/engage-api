using Engage.Domain.Enums;

namespace Engage.Domain.Entities.Views
{
    /**********************************************************************
    * GetStoreSurveysQuery
    ***********************************************************************/

    public class StoreSurveysView
    {
        public int SurveyId { get; set; }
        public int StoreId { get; set; }
        public int EmployeeId { get; set; }
        public int EngageSubGroupId { get; set; }

        //Navigation Properties
        public Survey Survey { get; set; }
        public Store Store { get; set; }
        public Employee Employee { get; set; }
        public EngageSubGroup EngageSubGroup { get; set; }
    }

    // No longer used?  
    public class SurveysByEmployeeStoreView : StoreSurveysView { }

    public class SurveysByEmployeePerRegionView2 : StoreSurveysView { }
    public class SurveysByEmployeePerStoreView : StoreSurveysView { }

    /**********************************************************************
     SurveysByEmployeeQuery 
    ***********************************************************************/
    public class SurveysByEmployeeView
    {
        public int SurveyId { get; set; }
        public int StoreId { get; set; }
        public int EmployeeId { get; set; }
        public int EngageSubGroupId { get; set; }
        
        //Navigation Properties
        public Survey Survey { get; set; }
        public Store Store { get; set; }
        public Employee Employee { get; set; }
        public EngageSubGroup EngageSubGroup { get; set; }
    }

    // Views where, one or more, employees are explicily assigned a survey. 
    // (surveyemployees table)
    public class SurveysByEmployeePerStoreView_ : SurveysByEmployeeView { }
    public class SurveysByEmployeePerStoreFormatView : SurveysByEmployeeView { }
    public class SurveysByEmployeePerRegionView : SurveysByEmployeeView { }

    // Views where, one or more, employees are assigned a survey with their sub group.
    // (employeestores table: employeesstores.subgroupid = survey.subgroupid )      
    public class SurveysByEmployeeSubGroupPerStoreView : SurveysByEmployeeView { }
    public class SurveysByEmployeeSubGroupPerStoreFormatView : SurveysByEmployeeView { }
    public class SurveysByEmployeeSubGroupPerRegionView : SurveysByEmployeeView { }        
}
