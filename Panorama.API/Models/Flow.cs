using System;

namespace Panorama.API.Models;

public class Flow
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Shift { get; set; }
    public DateTime Date { get; set; }
    public string? CreatedBy { get; set; }
}
