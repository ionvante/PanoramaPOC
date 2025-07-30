using System;
using System.Collections.Generic;

namespace Panorama.API.Models;

public class FlowValidation
{
    public Guid Id { get; set; }
    public Guid FlowId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Status { get; set; }
    public IEnumerable<FlowValidationItem>? Items { get; set; }
}
