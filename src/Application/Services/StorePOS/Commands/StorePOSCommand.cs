namespace Engage.Application.Services.StorePOS.Commands;

public class StorePOSCommand : IMapTo<Domain.Entities.StorePOS>
{
    public int StoreId { get; set; }
    public int StorePOSTypeId { get; set; }
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
        profile.CreateMap<StorePOSCommand, Domain.Entities.StorePOS>();
    }
}
