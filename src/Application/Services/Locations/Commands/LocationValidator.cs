namespace Engage.Application.Services.Locations.Commands;

public class LocationValidator<T> : AbstractValidator<T> where T : LocationCommand
{
    public LocationValidator()
    {
        RuleFor(x => x.LocationTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageLocationId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Province).MaximumLength(120).NotEmpty();
        RuleFor(x => x.City).MaximumLength(120).NotEmpty();
        RuleFor(x => x.BusinessUnit).MaximumLength(120);
        RuleFor(x => x.AddressLineOne).MaximumLength(220).NotEmpty();
        RuleFor(x => x.AddressLineTwo).MaximumLength(220).NotEmpty();
        RuleFor(x => x.Suburb).MaximumLength(120);
        RuleFor(x => x.PostCode).MaximumLength(30);
    }
}

public class CreateLocationValidator : LocationValidator<CreateLocationCommand>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.StakeholderType).IsInEnum();
        RuleFor(x => x.EntityId).NotEmpty();
    }
}

public class UpdateLocationValidator : LocationValidator<UpdateLocationCommand>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
