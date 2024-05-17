using CQC.Provider.Connector.Core.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CQC.Provider.Connector.Features.Providers;

public static class ProvidersEndpoint
{
    public static void AddProvidersEndpoints(this IEndpointRouteBuilder app)
    {
        var providerGroup = app.MapGroup("providers").WithTags("Providers");

        providerGroup.MapGet("/", GetProviders);
        providerGroup.MapGet("/{id}", GetProvidersById);
    }

    private static async Task<Ok<ProviderListDto[]>> GetProviders(IProvidersHandler handler)
    {
        var response = await handler.GetAllProviders();

        return TypedResults.Ok(response.Value);
    }

    private static async Task<Ok<ProviderDto>> GetProvidersById(string id, IProvidersHandler handler)
    {
        var response = await handler.GetProviderById(id);

        return TypedResults.Ok(response.Value);
    }
}