namespace Engage.Application.Services.Options.Models;

public class OptionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public OptionDto()
    {
    }

    public OptionDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class CascadingOptionDto : OptionDto
{
    public int ParentId { get; set; }
    public CascadingOptionDto()
    {
    }

    public CascadingOptionDto(int id, int parentId, string name)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
    }
}

public class DependentOptionDto : OptionDto
{
    public int ParentId { get; set; }
}
