﻿using Engage.Application.Services.SurveyFormEngageDepartments.Commands;


namespace Engage.WebApi.Controllers
{
    public partial class SurveyFormEngageDepartmentController : BaseController
    {
        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(SurveyFormEngageDepartmentBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyFormId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(SurveyFormEngageDepartmentBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyFormId));
        }
    }
}