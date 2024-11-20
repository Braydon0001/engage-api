using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class SurveyAnswerPhoto : BaseAuditableEntity
    {
        public int SurveyAnswerPhotoId { get; set; }
        public int SurveyAnswerId { get; set; }
        public string FileName { get; set; }
        public string Folder { get; set; }

        // Navigation Properties
        public SurveyAnswer SurveyAnswer { get; set; }        
    }
}
