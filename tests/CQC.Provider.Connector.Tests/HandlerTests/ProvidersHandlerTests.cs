using CQC.Provider.Connector.Core.Repositories;
using CQC.Provider.Connector.Core.Services;
using CQC.Provider.Connector.Data.Services;
using CQC.Provider.Connector.Features.Providers;
using FluentResults;
using Moq;
using Shouldly;
using Xunit;

namespace CQC.Provider.Connector.Tests.HandlerTests;

public class ProvidersHandlerTests
{
    [Fact]
    public async Task When_Calling_GetProviderById_With_Entity_In_Database_No_Calls_To_Api_Are_Made()
    {
        var mockRepository = new Mock<IProviderRepository>();
        var mockHttpService = new Mock<IProviderHttpService>();

        mockRepository.Setup(x => x.GetProviderByIdAsync("123"))
            .ReturnsAsync(Result.Ok(new Core.Entities.Provider { ProviderId = "123" }));
        
        var sut = new ProvidersHandler(mockRepository.Object, mockHttpService.Object);

        var result = await sut.GetProviderById("123");
        
        mockHttpService.Verify(x => x.GetProviderByIdAsync("123"), Times.Never);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ProviderId.ShouldBe("123");
    }
    
    [Fact]
    public async Task When_Calling_GetProviderById_With_Entity_Not_In_Database_Call_To_Api_Are_Made()
    {
        var mockRepository = new Mock<IProviderRepository>();
        var mockHttpService = new Mock<IProviderHttpService>();

        mockRepository.Setup(x => x.GetProviderByIdAsync("123"))
            .ReturnsAsync(Result.Ok<Core.Entities.Provider>(null));

        mockHttpService.Setup(x => x.GetProviderByIdAsync("123"))
            .ReturnsAsync(Result.Ok(new Core.Entities.Provider { ProviderId = "123" }));
        
        var sut = new ProvidersHandler(mockRepository.Object, mockHttpService.Object);

        var result = await sut.GetProviderById("123");
        
        mockHttpService.Verify(x => x.GetProviderByIdAsync("123"), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ProviderId.ShouldBe("123");
    }
}