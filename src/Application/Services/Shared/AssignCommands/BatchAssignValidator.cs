using FluentValidation;

namespace Engage.Application.Services.Shared.AssignCommands
{
    public class BatchAssignCommandValidator : AbstractValidator<BatchAssignCommand> 
    {
        public BatchAssignCommandValidator()
        {
            RuleFor(e => e.Mapping).NotEmpty();
            RuleFor(e => e.ToId).GreaterThan(0).NotEmpty();
            RuleForEach(e => e.AssignedIds).GreaterThan(0).NotEmpty(); 
        }
    }
}
