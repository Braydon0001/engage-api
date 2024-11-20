namespace Engage.Application.Services.StoreFilters.Commands;

public class CreateStoreFiltersValidator : AbstractValidator<CreateStoreFiltersCommand>
{
    public CreateStoreFiltersValidator()
    {
        RuleForEach(x => x.StoreIds).GreaterThan(0).NotEmpty();
    }
}

public class ImportStoreFiltersValidator : AbstractValidator<ImportStoreFiltersCommand>
{
    public ImportStoreFiltersValidator()
    {
        RuleFor(x => x.FileUploadId).GreaterThan(0).NotEmpty();
    }
}

public class RemoveStoreFilterValidator : AbstractValidator<RemoveStoreFilterCommand>
{
    public RemoveStoreFilterValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}

public class RemoveStoreFiltersValidator : AbstractValidator<RemoveStoreFiltersCommand>
{
    public RemoveStoreFiltersValidator()
    {
        RuleFor(x => x.Filter).NotEmpty();
    }
}

public class UploadStoreFiltersValidator : AbstractValidator<UploadStoreFiltersCommand>
{
    public UploadStoreFiltersValidator()
    {
        RuleFor(x => x.File).NotEmpty();
    }
}
