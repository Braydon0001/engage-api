namespace Engage.Application.Models.Configuration
{
    public class FeatureSwitchOptions
    {
        public bool IsNewSurveyTargeting { get; set; }
        public bool NewRejectClaimEmail { get; set; }
        public bool NewDisputeClaimEmail { get; set; }
        public bool NewPayClaimEmail { get; set; }
        public bool NewApproveClaimReminderEmail { get; set; }
        public bool NewClaimFloatWarningEmail { get; set; }
        public bool IsEmailEmployeeTermination { get; set; }
        public bool EnableMasterProductOnSurvey { get; set; }
        public bool SendContactReportCompleteEmail { get; set; }
        public bool IsClerkAuthentication { get; set; }
        public bool IsClerkOrganizations { get; set; }
        public bool NewContactReport { get; set; }
    }
}
