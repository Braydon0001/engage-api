namespace Engage.Application.Services.StorePOSQuestions.Commands;

public class StorePOSFreezerQuestionValidator<T> : AbstractValidator<T> where T : StorePOSFreezerQuestionCommand
{
    public StorePOSFreezerQuestionValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StorePOSTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StorePOSFreezerTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.WobblersComment).MaximumLength(1000);
        RuleFor(x => x.FreezerDecalsComment).MaximumLength(1000);
        RuleFor(x => x.ShelfTalkerComment).MaximumLength(1000);
    }
}

public class CreateStorePOSFreezerQuestionValidator : StorePOSFreezerQuestionValidator<CreateStorePOSFreezerQuestionCommand>
{
    public CreateStorePOSFreezerQuestionValidator()
    {
    }
}

public class UpdateStorePOSFreezerQuestionValidator : StorePOSFreezerQuestionValidator<UpdateStorePOSFreezerQuestionCommand>
{
    public UpdateStorePOSFreezerQuestionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
