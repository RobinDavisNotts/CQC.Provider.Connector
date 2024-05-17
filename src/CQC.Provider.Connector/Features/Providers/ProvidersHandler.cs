using CQC.Provider.Connector.Core.Dto;
using CQC.Provider.Connector.Core.Repositories;
using CQC.Provider.Connector.Core.Services;
using FluentResults;

namespace CQC.Provider.Connector.Features.Providers;

public class ProvidersHandler(IProviderRepository providerRepository, IProviderHttpService providerHttpService) : IProvidersHandler
{
    public async Task<Result<ProviderDto>> GetProviderById(string id)
    {
        var existingProviderResult = await providerRepository.GetProviderByIdAsync(id);

        if (existingProviderResult is { IsSuccess: true, Value: not null })
        {
            return Result.Ok(ProvidersMapper.MapFromProviderEntity(existingProviderResult.Value));
        }
        
        var providerApiResult = await providerHttpService.GetProviderByIdAsync(id);

        if (providerApiResult is { IsSuccess: true, Value: not null })
        {
            //Store in the database with todays date
            await providerRepository.AddProviderAsync(providerApiResult.Value);

            return Result.Ok(ProvidersMapper.MapFromProviderEntity(providerApiResult.Value));
        }

        return Result.Fail(providerApiResult.Errors);
    }

    public async Task<Result<ProviderListDto[]>> GetAllProviders()
    {
        var allProvidersResult = await providerHttpService.GetAllProviders();

        if (allProvidersResult is { IsSuccess: true, Value: not null })
        {
            return Result.Ok(allProvidersResult.Value.Select(ProvidersMapper.MapFromProviderEntityToListDto).ToArray());
        }
        
        return Result.Fail(allProvidersResult.Errors);
    }
}