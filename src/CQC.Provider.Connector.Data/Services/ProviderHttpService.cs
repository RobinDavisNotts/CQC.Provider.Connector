using System.Net.Http.Json;
using CQC.Provider.Connector.Core.Entities;
using CQC.Provider.Connector.Core.Entities.Options;
using CQC.Provider.Connector.Core.Services;
using FluentResults;
using Microsoft.Extensions.Options;

namespace CQC.Provider.Connector.Data.Services;

public class ProviderHttpService : IProviderHttpService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<CQCApiOptions> _options;

    public ProviderHttpService(HttpClient httpClient, IOptions<CQCApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options;

        _httpClient.BaseAddress = new Uri(options.Value.BaseUri);
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", options.Value.ApiKey);
    }

    public async Task<Result<Core.Entities.Provider?>> GetProviderByIdAsync(string id)
    {
        try
        {
            var apiResult = await _httpClient.GetFromJsonAsync<Core.Entities.Provider>($"providers/{id}");

            return Result.Ok(apiResult);
        }
        catch (HttpRequestException exception)
        {
            return Result.Fail(exception.Message);
        }
    }

    public async Task<Result<Core.Entities.Provider[]?>> GetAllProviders()
    {
        try
        {
            var apiResult = await _httpClient.GetFromJsonAsync<ProviderListResponse>($"providers?page=1&perPage=5");

            return Result.Ok(apiResult.Providers);
        }
        catch (HttpRequestException exception)
        {
            return Result.Fail(exception.Message);
        }
    }
}