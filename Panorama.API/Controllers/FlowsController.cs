using Microsoft.AspNetCore.Mvc;
using Panorama.API.Models;
using System.Collections.Generic;
using Panorama.API.Services;

namespace Panorama.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : ControllerBase
{
    private readonly SupabaseService _service;
    public FlowsController(SupabaseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flow>>> GetFlows()
    {
        var flows = await _service.GetFlowsAsync();
        return Ok(flows);
    }

    [HttpPost]
    public async Task<ActionResult<Flow>> CreateFlow([FromBody] Flow flow)
    {
        var created = await _service.CreateFlowAsync(flow);
        return CreatedAtAction(nameof(GetFlows), new { id = created?.Id }, created);
    }

    [HttpGet("{id}/validations")]
    public async Task<ActionResult<IEnumerable<FlowValidationItem>>> GetValidations(Guid id)
    {
        var items = await _service.GetValidationItemsAsync(id);
        return Ok(items);
    }

    [HttpPost("{id}/validate")]
    public async Task<ActionResult<FlowValidation>> Validate(Guid id)
    {
        var validation = new FlowValidation
        {
            Id = Guid.NewGuid(),
            FlowId = id,
            CreatedAt = DateTime.UtcNow,
            Status = "validated",
            Items = new List<FlowValidationItem>
            {
                new FlowValidationItem
                {
                    Id = Guid.NewGuid(),
                    FlowValidationId = Guid.NewGuid(),
                    Field = "example",
                    OriginalValue = "A",
                    NewValue = "B",
                    Difference = "changed"
                }
            }
        };

        // Replace validation id for items
        foreach (var item in validation.Items!)
            item.FlowValidationId = validation.Id;

        await _service.SaveValidationAsync(validation);
        return Ok(validation);
    }
}
