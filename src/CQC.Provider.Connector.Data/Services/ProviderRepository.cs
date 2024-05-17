using CQC.Provider.Connector.Core.Repositories;
using CQC.Provider.Connector.Data.Contexts;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CQC.Provider.Connector.Data.Services;

public class ProviderRepository(ProviderContext dbContext) : IProviderRepository
{
    public async Task<Result> AddProviderAsync(Core.Entities.Provider provider)
    {
        try
        {
            provider.ExpiryDate = DateTime.Now.AddMonths(1);

            dbContext.Providers.Add(provider);
            await dbContext.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result<Core.Entities.Provider?>> GetProviderByIdAsync(string id)
    {
        try
        {
            var provider = await dbContext.Providers.FirstOrDefaultAsync(x => x.ProviderId == id);

            return Result.Ok(provider);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }
}