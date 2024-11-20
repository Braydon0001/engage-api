namespace Engage.Application.Services.StorePOSFreezerQuestions.Commands;

public class StorePOSQuestionValidator<T> : AbstractValidator<T> where T : StorePOSQuestionCommand
{
    public StorePOSQuestionValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StorePOSTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FridgeDecalsComment).MaximumLength(1000);
        RuleFor(x => x.FloorDecalsComment).MaximumLength(1000);
        RuleFor(x => x.FSUDecalsComment).MaximumLength(1000);
        RuleFor(x => x.FSUDecalsPaidComment).MaximumLength(1000);
        RuleFor(x => x.ShelfStripsComment).MaximumLength(1000);
        RuleFor(x => x.AisleBladesComment).MaximumLength(1000);
        RuleFor(x => x.StandeeComment).MaximumLength(1000);
        RuleFor(x => x.EntryBoxComment).MaximumLength(1000);
        RuleFor(x => x.BaseWrapComment).MaximumLength(1000);
        RuleFor(x => x.GondolaHeaderComment).MaximumLength(1000);
        RuleFor(x => x.HangingMobilesComment).MaximumLength(1000);
        RuleFor(x => x.PollUpBannerComment).MaximumLength(1000);
        RuleFor(x => x.ParaciteUnitsComment).MaximumLength(1000);
        RuleFor(x => x.SensorSleavesComment).MaximumLength(1000);
        RuleFor(x => x.NeckTagsComment).MaximumLength(1000);
    }
}

public class CreateStorePOSQuestionValidator : StorePOSQuestionValidator<CreateStorePOSQuestionCommand>
{
    public CreateStorePOSQuestionValidator()
    {
    }
}

public class UpdateStorePOSQuestionValidator : StorePOSQuestionValidator<UpdateStorePOSQuestionCommand>
{
    public UpdateStorePOSQuestionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
