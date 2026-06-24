using System.Net.Http.Json;
using Commun.Server.API.Models;

namespace Commun.Client.CLI.Services;

public class DeviceService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public DeviceService(string baseUrl)
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
    }

    public async Task<IEnumerable<DeviceDto>> GetDevicesAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/devices");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<DeviceDto>>() ?? Enumerable.Empty<DeviceDto>();
    }

    public async Task<DeviceDto?> GetDeviceAsync(string id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/devices/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<DeviceDto>();
    }

    public async Task<DeviceDto> CreateDeviceAsync(CreateDeviceDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/devices", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<DeviceDto>())!;
    }

    public async Task DeleteDeviceAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/devices/{id}");
        response.EnsureSuccessStatusCode();
    }
}
