namespace Engage.Application.Services.StoreConceptAttributeValues.Models;

public class StoreConceptAttributeValueGroup
{
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int ConceptId { get; set; }
    public string ConceptName { get; set; }
    public List<Attribute> Attributes { get; set; }

}

public class Attribute
{
    public int StoreConceptAttributeValueId { get; set; }
    public int AttributeId { get; set; }
    public string AttributeName { get; set; }
    public int AttributeType { get; set; }
    public string Value { get; set; }
}
