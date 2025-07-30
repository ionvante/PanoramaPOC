namespace Panorama.API.Models;

public class FlowValidationItem
{
    public Guid Id { get; set; }
    public Guid FlowValidationId { get; set; }
    public string? Field { get; set; }
    public string? OriginalValue { get; set; }
    public string? NewValue { get; set; }
    public string? Difference { get; set; }
}
