namespace Engage.Application.Services.ProductFilters.Models;

public class ProductFilterVm : IMapFrom<ProductFilter>
{
    public int Id { get; set; }
    public OptionDto EngageVariantProductId { get; set; }
    public string Barcode { get; set; }
    public int FileUploadId { get; set; }
    public string FileUploadName { get; set; }
    public string Filter { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductFilter, ProductFilterVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductFilterId))
            .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.EngageVariantProductId.HasValue
                                                                 ? new OptionDto(s.EngageVariantProductId.Value, s.EngageVariantProduct.Name)
                                                                 : null))
            .ForMember(d => d.FileUploadName, opt => opt.MapFrom(s => s.FileUpload.FileName));

    }
}
