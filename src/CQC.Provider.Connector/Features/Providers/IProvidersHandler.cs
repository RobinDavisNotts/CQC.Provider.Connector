using CQC.Provider.Connector.Core.Dto;
using FluentResults;

namespace CQC.Provider.Connector.Features.Providers;

public interface IProvidersHandler
{
    public Task<Result<ProviderDto>> GetProviderById(string id);
    public Task<Result<ProviderListDto[]>> GetAllProviders();
}