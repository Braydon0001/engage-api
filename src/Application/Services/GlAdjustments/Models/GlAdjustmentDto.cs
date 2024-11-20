namespace Engage.Application.Services.GlAdjustments.Models;

public class GLAdjustmentDto : IMapFrom<GLAdjustment>
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
    public int SupplierId { get; set; }
    public int GLAdjustmentTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GLAdjustment, GLAdjustmentDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.GLAdjustmentId));
    }
}
