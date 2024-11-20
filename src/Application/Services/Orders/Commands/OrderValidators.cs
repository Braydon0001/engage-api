namespace Engage.Application.Services.Orders.Commands;

public class OrderValidators<T> : AbstractValidator<T> where T : OrderCommand
{
    public OrderValidators()
    {
        RuleFor(x => x.OrderTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.DistributionCenterId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0);
        RuleFor(x => x.OrderDate).NotEmpty();
        RuleFor(x => x.DeliveryDate).GreaterThanOrEqualTo(x => x.OrderDate);
        RuleFor(x => x.SubmittedDate).GreaterThanOrEqualTo(x => x.OrderDate);
        RuleFor(x => x.ProcessedDate).GreaterThanOrEqualTo(x => x.SubmittedDate);
        RuleFor(x => x.DCAccountNo).MaximumLength(120);
        RuleFor(x => x.OrderNo).MaximumLength(30);
        RuleFor(x => x.OrderReference).MaximumLength(220);
        RuleFor(x => x.Comment).MaximumLength(300);
        RuleFor(x => x.Note).MaximumLength(300);
        RuleFor(x => x.VATNumber).MaximumLength(100);
        RuleFor(x => x.AccountNumber).MaximumLength(100);
        RuleFor(x => x.Email).MaximumLength(200);
        RuleFor(x => x.Contact).MaximumLength(200);
        RuleFor(x => x.Address).MaximumLength(1000);
        RuleForEach(x => x.EngageDepartmentIds).GreaterThan(0);
    }
}

public class CreateOrderValidator : OrderValidators<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
    }
}

public class UpdateOrderValidator : OrderValidators<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}

public class UpdateOrderDateValidator : AbstractValidator<UpdateOrderDateCommand>
{
    public UpdateOrderDateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderDate).NotEmpty();
    }
}

public class UpdateOrderDeliveryDateValidator : AbstractValidator<UpdateOrderDeliveryDateCommand>
{
    public UpdateOrderDeliveryDateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.DeliveryDate).NotEmpty();
    }
}

public class UpdateOrderReferenceValidator : AbstractValidator<UpdateOrderReferenceCommand>
{
    public UpdateOrderReferenceValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderReference).MaximumLength(220);
    }
}

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderStatusId).GreaterThan(0).NotEmpty();
    }
}

public class BatchUpdateOrderStatusValidator : AbstractValidator<BatchUpdateOrderStatusCommand>
{
    public BatchUpdateOrderStatusValidator()
    {
        RuleForEach(x => x.Ids).GreaterThan(0);
        RuleFor(x => x.OrderStatusId).GreaterThan(0).NotEmpty();
    }
}
