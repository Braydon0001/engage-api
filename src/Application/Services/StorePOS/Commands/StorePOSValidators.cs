namespace Engage.Application.Services.StorePOS.Commands;

public class StorePOSValidator<T> : AbstractValidator<T> where T : StorePOSCommand
{
    public StorePOSValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StorePOSTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.A0PosterQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.A1PosterQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.A2PosterQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.A3BuntingQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AisleBladesQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.HangingMobilesQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ShelfStripsQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ShelfTalkersQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TentCardsQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TableClothsQty).GreaterThanOrEqualTo(0);
        RuleFor(x => x.WobblersQty).GreaterThanOrEqualTo(0);
    }
}

public class CreateStorePOSValidator : StorePOSValidator<CreateStorePOSCommand>
{
    public CreateStorePOSValidator()
    {
    }
}

public class UpdateStorePOSValidator : StorePOSValidator<UpdateStorePOSCommand>
{
    public UpdateStorePOSValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
