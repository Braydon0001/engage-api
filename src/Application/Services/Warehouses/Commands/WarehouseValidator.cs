namespace Engage.Application.Services.Warehouses.Commands
{
    public class WarehouseValidator<T> : AbstractValidator<T> where T : WarehouseCommand
    {
        public WarehouseValidator()
        {
            RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
            RuleFor(x => x.DCId).GreaterThan(0);
        }
    }

    public class CreateWarehouseValidator : WarehouseValidator<CreateWarehouseCommand>
    {
    }

    public class UpdateWarehouseValidator : WarehouseValidator<UpdateWarehouseCommand>
    {
        public UpdateWarehouseValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
