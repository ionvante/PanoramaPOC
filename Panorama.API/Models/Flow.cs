using System;
using System.Text.Json.Serialization;

namespace Panorama.API.Models;

public class Flow
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("turno")]
    public string? Shift { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Date { get; set; }

    [JsonPropertyName("creado_por")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("estado")]
    public string? Status { get; set; }
}
