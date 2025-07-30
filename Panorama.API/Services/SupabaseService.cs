using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Panorama.API.Models;

using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
namespace Panorama.API.Services;

public class SupabaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public SupabaseService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL") ?? configuration["SUPABASE_URL"] ?? string.Empty;
        _apiKey = Environment.GetEnvironmentVariable("SUPABASE_SERVICE_ROLE_KEY") ?? configuration["SUPABASE_SERVICE_ROLE_KEY"] ?? string.Empty;

        if (!string.IsNullOrEmpty(_apiKey))
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("apikey"))
                _httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }
    }

    public async Task<IEnumerable<Flow>> GetFlowsAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/rest/v1/flows?select=*");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Flow>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Flow>();
    }

    public async Task<Flow?> CreateFlowAsync(Flow flow)
    {
        var content = new StringContent(JsonSerializer.Serialize(flow), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/rest/v1/flows", content);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<IEnumerable<Flow>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return items?.FirstOrDefault();
    }

    public async Task<IEnumerable<FlowValidationItem>> GetValidationItemsAsync(Guid flowId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/rest/v1/flow_validation_items?select=*&flow_id=eq.{flowId}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<FlowValidationItem>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<FlowValidationItem>();
    }

    public async Task SaveValidationAsync(FlowValidation validation)
    {
        var validationContent = new StringContent(JsonSerializer.Serialize(validation), Encoding.UTF8, "application/json");
        var valResponse = await _httpClient.PostAsync($"{_baseUrl}/rest/v1/flow_validations", validationContent);
        valResponse.EnsureSuccessStatusCode();

        if (validation.Items != null && validation.Items.Any())
        {
            var itemsContent = new StringContent(JsonSerializer.Serialize(validation.Items), Encoding.UTF8, "application/json");
            var itemsResponse = await _httpClient.PostAsync($"{_baseUrl}/rest/v1/flow_validation_items", itemsContent);
            itemsResponse.EnsureSuccessStatusCode();
        }
    }
}
