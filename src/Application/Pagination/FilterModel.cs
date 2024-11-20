namespace Engage.Application.Pagination;

public class FilterModel
{
    public string FilterType { get; set; }
    public string Type { get; set; }
    public string Filter { get; set; }
    public string FilterTo { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<int> Values { get; set; }
}

public static class FilterType
{
    public const string SET = "SET";
    public const string NUMBER = "NUMBER";
    public const string TEXT = "TEXT";
    public const string DATE = "DATE";
    public const string BOOLEAN = "BOOLEAN";
}

public static class TextOperator
{
    public const string EQUALS = "EQUALS";
    public const string NOT_EQUAL = "NOTEQUAL";
    public const string CONTAINS = "CONTAINS";
    public const string NOT_CONTAINS = "NOTCONTAINS";
    public const string STARTS_WITH = "STARTSWITH";
    public const string ENDS_WITH = "ENDSWITH";
}

public static class NumberOperator
{
    public const string EQUALS = "EQUALS";
    public const string NOT_EQUAL = "NOTEQUAL";
    public const string LESS_THAN = "LESSTHAN";
    public const string LESS_THAN_OR_EQUAL = "LESSTHANOREQUAL";
    public const string GREATER_THAN = "GREATERTHAN";
    public const string GREATER_THAN_OR_EQUAL = "GREATERTHANOREQUAL";
    public const string IN_RANGE = "INRANGE";
}

public enum NumberFilterType
{
    INT,
    DECIMAL
}

public static class DateOperator
{
    public const string EQUALS = "EQUALS";
    public const string NOT_EQUAL = "NOTEQUAL";
    public const string LESS_THAN = "LESSTHAN";
    public const string GREATER_THAN = "GREATERTHAN";
    public const string IN_RANGE = "INRANGE";
}    

