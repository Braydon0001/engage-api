namespace Engage.Application.Services.Organogram;

public class OrganogramTreeNodeDto
{
    public string Id { get; set; }
    public int? EntityId { get; set; }
    public int? EntityParentId { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public Dictionary<string, string> LabelValues { get; set; }
    public ICollection<OrganogramTreeNodeDto> Children { get; set; }
    public int Level { get; set; }
    public bool IsSummary { get; set; }
    public OrganogramTreeNodeConfig Config { get; set; }
}


public class OrganogramTreeNodeConfig
{
    public BaseOrganogramTreeNodeConfig SummaryNode { get; set; }
    public BaseOrganogramTreeNodeConfig StandardNode { get; set; }
    public BaseOrganogramTitleConfig TitleConfig { get; set; }
    public OrganogramSubTitleConfig SubTitleConfig { get; set; }

}

public class BaseOrganogramTreeNodeConfig
{
    public BaseOrganogramLabelValueConfig LabelValueConfig { get; set; }
    public BaseOrganogramTitleConfig TitleConfig { get; set; }
}

public class BaseOrganogramLabelValueConfig
{
    public ICollection<String> ExcludedLabels { get; set; }
    public bool IsLabelsHidden { get; set; }
    public bool IsLabelValuesHidden { get; set; }
}

public class OrganogramSubTitleConfig: BaseOrganogramTitleConfig
{
    public bool IsHidden { get; set; }
}

public class BaseOrganogramTitleConfig
{
    public OrganogramTitleVariant Variant { get; set; }
}

public enum OrganogramTitleVariant
{
    Variant1=1,
    Variant2=2,
}