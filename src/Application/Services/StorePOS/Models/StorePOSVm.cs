namespace Engage.Application.Services.StorePOS.Models;

public class StorePOSVm : IMapFrom<Domain.Entities.StorePOS>
{
    public int Id { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto StorePOSTypeId { get; set; }
    public int A0PosterQty { get; set; }
    public int A1PosterQty { get; set; }
    public int A2PosterQty { get; set; }
    public int A3BuntingQty { get; set; }
    public int AisleBladesQty { get; set; }
    public int HangingMobilesQty { get; set; }
    public int ShelfStripsQty { get; set; }
    public int ShelfTalkersQty { get; set; }
    public int TentCardsQty { get; set; }
    public int TableClothsQty { get; set; }
    public int WobblersQty { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.StorePOS, StorePOSVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StorePOSId))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)))
            .ForMember(d => d.StorePOSTypeId, opt => opt.MapFrom(s => new OptionDto(s.StorePOSTypeId, s.StorePOSType.Name)));
    }
}
