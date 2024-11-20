using System.Text.Json.Serialization;

namespace Engage.Domain.Entities.Json;

public class JsonThemeSetting
{
    public string Name { get; set; }
    public string Label { get; set; }
    public JsonActiveColor ActiveColor { get; set; }
    public JsonCssVars CssVars { get; set; }
}

public class JsonActiveColor
{
    public string Light { get; set; }
    public string Dark { get; set; }
}

public class JsonCssVars
{
    public JsonThemeValues Light { get; set; }
    public JsonThemeValues Dark { get; set; }
}

public class JsonThemeValues
{
    [JsonPropertyName("background")]
    public string Background { get; set; }

    [JsonPropertyName("foreground")]
    public string Foreground { get; set; }

    [JsonPropertyName("card")]
    public string Card { get; set; }

    [JsonPropertyName("card-foreground")]
    public string CardForeground { get; set; }

    [JsonPropertyName("popover")]
    public string Popover { get; set; }

    [JsonPropertyName("popover-foreground")]
    public string PopoverForeground { get; set; }

    [JsonPropertyName("primary")]
    public string Primary { get; set; }

    [JsonPropertyName("primary-foreground")]
    public string PrimaryForeground { get; set; }

    [JsonPropertyName("secondary")]
    public string Secondary { get; set; }

    [JsonPropertyName("secondary-foreground")]
    public string SecondaryForeground { get; set; }

    [JsonPropertyName("muted")]
    public string Muted { get; set; }

    [JsonPropertyName("muted-foreground")]
    public string MutedForeground { get; set; }

    [JsonPropertyName("accent")]
    public string Accent { get; set; }

    [JsonPropertyName("accent-foreground")]
    public string AccentForeground { get; set; }

    [JsonPropertyName("destructive")]
    public string Destructive { get; set; }

    [JsonPropertyName("destructive-foreground")]
    public string DestructiveForeground { get; set; }

    [JsonPropertyName("border")]
    public string Border { get; set; }

    [JsonPropertyName("input")]
    public string Input { get; set; }

    [JsonPropertyName("ring")]
    public string Ring { get; set; }

    [JsonPropertyName("radius")]
    public string Radius { get; set; }


    [JsonPropertyName("header")]
    public string Header { get; set; }

    [JsonPropertyName("panel")]
    public string Panel { get; set; }

    [JsonPropertyName("option")]
    public string Option { get; set; }
     
    [JsonPropertyName("option-hover")]
    public string OptionHover { get; set; }
}

//public class JsonThemeValues
//{
//    public string Background { get; set; }
//    public string Foreground { get; set; }
//    public string Card { get; set; }
//    public string CardForeground { get; set; }
//    public string Popover { get; set; }
//    public string PopoverForeground { get; set; }
//    public string Primary { get; set; }
//    public string PrimaryForeground { get; set; }
//    public string Secondary { get; set; }
//    public string SecondaryForeground { get; set; }
//    public string Muted { get; set; }
//    public string MutedForeground { get; set; }
//    public string Accent { get; set; }
//    public string AccentForeground { get; set; }
//    public string Destructive { get; set; }
//    public string DestructiveForeground { get; set; }
//    public string Border { get; set; }
//    public string Input { get; set; }
//    public string Ring { get; set; }
//    public string Radius { get; set; }
//}
