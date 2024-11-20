using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Domain.Entities
{
    public class EmailTemplateVariableClaimNumber : BaseAuditableEntity
    {
        public int EmailTemplateVariableClaimNumberId { get; set; }
        public int EmailHistoryTemplateVariableId { get; set; }
        public string ClaimNo { get; set; }
        public decimal Amount { get; set; }
        public EmailHistoryTemplateVariable EmailHistoryTemplateVariable { get; set; }
    }
}
