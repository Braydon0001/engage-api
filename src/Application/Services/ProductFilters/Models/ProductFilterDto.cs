namespace Engage.Application.Services.ProductFilters.Models;

public class ProductFilterDto : IMapFrom<ProductFilter>
{
    public int Id { get; set; }
    public int? EngageVariantProductId { get; set; }
    public string EngageVariantProductName { get; set; }
    public string Barcode { get; set; }
    public int FileUploadId { get; set; }
    public string FileUploadName { get; set; }
    public string Filter { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductFilter, ProductFilterDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductFilterId))
            .ForMember(d => d.FileUploadName, opt => opt.MapFrom(s => s.FileUpload.FileName));

    }
}
