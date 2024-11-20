namespace Engage.Application.Services.StoreConcepts.Commands;

public class StoreConceptValidator<T> : AbstractValidator<T> where T : StoreConceptCommand
{
    public StoreConceptValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.EngageDepartmentId).GreaterThan(0).NotEmpty();
    }
}

public class CreateEngageSubGroupValidator : StoreConceptValidator<CreateStoreConceptCommand>
{
    public CreateEngageSubGroupValidator()
    {
    }
}

public class UpdateStoreConceptValidator : StoreConceptValidator<UpdateStoreConceptCommand>
{
    public UpdateStoreConceptValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
