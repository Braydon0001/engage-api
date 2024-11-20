using FluentValidation;

namespace Engage.Application.Services.SubWarehouses.Commands
{
    public class SubWarehouseValidator<T> : AbstractValidator<T> where T: SubWarehouseCommand
    {
        public SubWarehouseValidator()
        {
            RuleFor(e => e.Name).MaximumLength(120).NotEmpty();
        }
    }

    class CreateSubWarehouseValidator : SubWarehouseValidator<CreateSubWarehouseCommand>
    {
        public CreateSubWarehouseValidator()
        {

        }
    }

    class UpdateSubWarehouseValidator : SubWarehouseValidator<UpdateSubWarehouseCommand>
    {
        public UpdateSubWarehouseValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
        }
    }
}
