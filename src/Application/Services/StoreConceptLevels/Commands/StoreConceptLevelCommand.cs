namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelCommand : IMapTo<StoreConceptLevel>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int StoreConceptId { get; set; }
    public string StoreConceptName { get; set; }
    public int Level { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptLevelCommand, StoreConceptLevel>();
    }
}

