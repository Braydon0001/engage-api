namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuValidator<T> : AbstractValidator<T> where T : OrderSkuCommand
{
    public OrderSkuValidator()
    {
        RuleFor(x => x.OrderSkuTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderSkuStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.DCProductId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Code).MaximumLength(30);
        RuleFor(x => x.Description).MaximumLength(220);
        RuleFor(x => x.Note).MaximumLength(300);
    }
}

public class CreateOrderSkuValidator : OrderSkuValidator<CreateOrderSkuCommand>
{
    public CreateOrderSkuValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).NotEmpty();
    }
}

public class CreateOrderSkuDescriptionCommandValidator : AbstractValidator<CreateOrderSkuDescriptionCommand>
{
    public CreateOrderSkuDescriptionCommandValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(220).NotEmpty();
    }
}

public class CreateOrderSkusValidator : AbstractValidator<CreateOrderSkusCommand>
{
    public CreateOrderSkusValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).NotEmpty();
        RuleForEach(x => x.DcProductIds).GreaterThan(0);
    }
}

public class UpdateOrderSkuValidator : OrderSkuValidator<OrderSkuUpdateCommand>
{
    public UpdateOrderSkuValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
public class UpdateOrderSkuQuantityTypeValidator : AbstractValidator<OrderSkuQuantityTypeUpdateCommand>
{
    public UpdateOrderSkuQuantityTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderQuantityTypeId).GreaterThanOrEqualTo(0).NotEmpty();
    }
}

public class UpdateOrderSkuNoteValidator : AbstractValidator<OrderSkuNoteUpdateCommand>
{
    public UpdateOrderSkuNoteValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(300);
    }
}
