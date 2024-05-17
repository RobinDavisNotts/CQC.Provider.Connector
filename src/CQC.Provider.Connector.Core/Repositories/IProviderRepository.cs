using FluentResults;

namespace CQC.Provider.Connector.Core.Repositories;

public interface IProviderRepository
{
    public Task<Result> AddProviderAsync(Entities.Provider provider);
    public Task<Result<Entities.Provider?>> GetProviderByIdAsync(string id);

}