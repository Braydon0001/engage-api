namespace Engage.Application.Services.StorePOSQuestions.Commands;

public class StorePOSFreezerQuestionCommand : IMapTo<StorePOSFreezerQuestion>
{
    public int StoreId { get; set; }
    public int StorePOSTypeId { get; set; }
    public int StorePOSFreezerTypeId { get; set; }
    public bool IsWobblers { get; set; }
    public string WobblersComment { get; set; }
    public bool IsFreezerDecals { get; set; }
    public string FreezerDecalsComment { get; set; }
    public bool IsShelfTalker { get; set; }
    public string ShelfTalkerComment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StorePOSFreezerQuestionCommand, StorePOSFreezerQuestion>();
    }
}
