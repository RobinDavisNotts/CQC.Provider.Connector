using FluentResults;

namespace CQC.Provider.Connector.Core.Services;

public interface IProviderHttpService
{
    public Task<Result<Entities.Provider?>> GetProviderByIdAsync(string id);
    public Task<Result<Entities.Provider[]?>> GetAllProviders();
}