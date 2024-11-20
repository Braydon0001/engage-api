namespace Engage.Application.Services.Settings.Models;

public class SettingDto
{
    public int SettingId { get; set; }
    public string SettingName { get; set; }
    public string NormalizedSettingName { get; set; }
    public string Value { get; set; }
}
