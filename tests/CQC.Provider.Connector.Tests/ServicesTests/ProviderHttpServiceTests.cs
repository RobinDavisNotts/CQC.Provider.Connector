using System.Net;
using CQC.Provider.Connector.Core.Entities.Options;
using CQC.Provider.Connector.Data.Services;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using Shouldly;
using Xunit;

namespace CQC.Provider.Connector.Tests.ServicesTests;

public class ProviderHttpServiceTests
{
    [Fact]
    public async Task Providers_With_Id_Endpoint_Returns_Forbidden_Without_Api_Key_Header()
    {
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<CQCApiOptions>>();

        mockHttp.When("https://api.service.cqc.org.uk/public/v1/providers/0")
            .WithHeaders(new Dictionary<string, string>
            {
                { "Ocp-Apim-Subscription-Key", string.Empty }
            })
            .Throw(new HttpRequestException("Forbidden", null, HttpStatusCode.Forbidden));

        mockOptions.Setup(x => x.Value).Returns(new CQCApiOptions()
        {
            BaseUri = "https://api.service.cqc.org.uk/public/v1/",
            ApiKey = string.Empty
        });
        

        var sut = new ProviderHttpService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetProviderByIdAsync("0");
        
        result.IsFailed.ShouldBeTrue();
        result.Errors.ShouldContain(x => x.Message == "Forbidden");
    }
    
    [Fact]
    public async Task Providers_With_Id_Endpoint_Returns_Not_Found_With_Incorrect_Id()
    {
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<CQCApiOptions>>();

        mockHttp.When("https://api.service.cqc.org.uk/public/v1/providers/0")
            .WithHeaders(new Dictionary<string, string>
            {
                { "Ocp-Apim-Subscription-Key", "123" }
            })
            .Throw(new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        mockOptions.Setup(x => x.Value).Returns(new CQCApiOptions()
        {
            BaseUri = "https://api.service.cqc.org.uk/public/v1/",
            ApiKey = "123"
        });
        

        var sut = new ProviderHttpService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetProviderByIdAsync("0");
        
        result.IsFailed.ShouldBeTrue();
        result.Errors.ShouldContain(x => x.Message == "Not Found");
    }
    
    [Fact]
    public async Task Providers_With_Id_Endpoint_Returns_Ok_And_Provider_With_Valid_Api_Key_And_Id()
    {
        var mockHttp = new MockHttpMessageHandler();
        var mockOptions = new Mock<IOptions<CQCApiOptions>>();

        mockHttp.When("https://api.service.cqc.org.uk/public/v1/providers/1234")
            .WithHeaders(new Dictionary<string, string>
            {
                { "Ocp-Apim-Subscription-Key", "123" }
            })
            .Respond("application/json", "{\"providerId\":\"1234\"}");

        mockOptions.Setup(x => x.Value).Returns(new CQCApiOptions()
        {
            BaseUri = "https://api.service.cqc.org.uk/public/v1/",
            ApiKey = "123"
        });
        

        var sut = new ProviderHttpService(mockHttp.ToHttpClient(), mockOptions.Object);

        var result = await sut.GetProviderByIdAsync("1234");
        
        result.IsSuccess.ShouldBeTrue();
        result.Value.ProviderId.ShouldBe("1234");
    }
}