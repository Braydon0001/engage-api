namespace Engage.Application.Services.StoreFormats.Queries;

public class StoreFormatDto : IMapFrom<StoreFormat>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreFormat, StoreFormatDto>();
    }
}