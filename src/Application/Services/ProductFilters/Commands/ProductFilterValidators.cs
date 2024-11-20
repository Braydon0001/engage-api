namespace Engage.Application.Services.ProductFilters.Commands;

public class CreateProductFiltersValidator : AbstractValidator<CreateProductFiltersCommand>
{
    public CreateProductFiltersValidator()
    {
        RuleForEach(x => x.ProductIds).GreaterThan(0).NotEmpty();
    }
}

public class ImportProductFiltersValidator : AbstractValidator<ImportProductFiltersCommand>
{
    public ImportProductFiltersValidator()
    {
        RuleFor(x => x.FileUploadId).GreaterThan(0).NotEmpty();
    }
}

public class RemoveProductFilterValidator : AbstractValidator<RemoveProductFilterCommand>
{
    public RemoveProductFilterValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}

public class RemoveProductFiltersValidator : AbstractValidator<RemoveProductFiltersCommand>
{
    public RemoveProductFiltersValidator()
    {
        RuleFor(x => x.Filter).NotEmpty();
    }
}

public class UploadProductFiltersValidator : AbstractValidator<UploadProductFiltersCommand>
{
    public UploadProductFiltersValidator()
    {
        RuleFor(x => x.File).NotEmpty();
    }
}
