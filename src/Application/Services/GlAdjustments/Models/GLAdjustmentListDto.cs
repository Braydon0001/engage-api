namespace Engage.Application.Services.GLAdjustments.Models;

public class GlAdjustmentListDto : IMapFrom<GLAdjustment>
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int GLCode { get; set; }
    public string GLDescription { get; set; }
    public DateTime TransactionDate { get; set; }
    public string DocumentNo { get; set; }
    public double DebitValue { get; set; }
    public double CreditValue { get; set; }
    public string Description { get; set; }
    public string Invoice { get; set; }
    public string Account { get; set; }
    public string SupplierName { get; set; }
    public string GLAdjustmentTypeName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAdjustment, GlAdjustmentListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.GLAdjustmentId))
            .ForMember(e => e.SupplierName, opt => opt.MapFrom(d => d.Supplier.Name))
            .ForMember(e => e.GLAdjustmentTypeName, opt => opt.MapFrom(d => d.GLAdjustmentType.Name));
    }
}
