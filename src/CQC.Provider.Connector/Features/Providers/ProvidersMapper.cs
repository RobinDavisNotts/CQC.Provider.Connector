using CQC.Provider.Connector.Core.Dto;

namespace CQC.Provider.Connector.Features.Providers;

public static class ProvidersMapper
{
    public static ProviderListDto MapFromProviderEntityToListDto(Core.Entities.Provider entity)
    {
        return new ProviderListDto
        {
            ProviderId = entity.ProviderId,
            ProviderName = entity.ProviderName
        };
    }
    
    public static ProviderDto MapFromProviderEntity(Core.Entities.Provider entity)
    {
        return new ProviderDto
        {
            ProviderId = entity.ProviderId,
            LocationIds = entity.LocationIds,
            OrganisationType = entity.OrganisationType,
            OwnershipType = entity.OwnershipType,
            Type = entity.Type,
            Name = entity.Name,
            BrandId = entity.BrandId,
            BrandName = entity.BrandName,
            RegistrationStatus = entity.RegistrationStatus,
            RegistrationDate = entity.RegistrationDate,
            CompaniesHouseNumber = entity.CompaniesHouseNumber,
            CharityNumber = entity.CharityNumber,
            Website = entity.Website,
            PostalAddressLine1 = entity.PostalAddressLine1,
            PostalAddressLine2 = entity.PostalAddressLine2,
            PostalAddressTownCity = entity.PostalAddressTownCity,
            PostalAddressCounty = entity.PostalAddressCounty,
            Region = entity.Region,
            PostalCode = entity.PostalCode,
            Uprn = entity.Uprn,
            OnspdLatitude = entity.OnspdLatitude,
            OnspdLongitude = entity.OnspdLongitude,
            MainPhoneNumber = entity.MainPhoneNumber,
            InspectionDirectorate = entity.InspectionDirectorate,
            Constituency = entity.Constituency,
            LocalAuthority = entity.Constituency,
            LastInspection = entity.LastInspection
        };
    }

    public static Core.Entities.Provider MapFromProviderDto(ProviderDto dto, DateTime expiryDate)
    {
        return new Core.Entities.Provider
        {
            ProviderId = dto.ProviderId,
            LocationIds = dto.LocationIds,
            OrganisationType = dto.OrganisationType,
            OwnershipType = dto.OwnershipType,
            Type = dto.Type,
            Name = dto.Name,
            BrandId = dto.BrandId,
            BrandName = dto.BrandName,
            RegistrationStatus = dto.RegistrationStatus,
            RegistrationDate = dto.RegistrationDate,
            CompaniesHouseNumber = dto.CompaniesHouseNumber,
            CharityNumber = dto.CharityNumber,
            Website = dto.Website,
            PostalAddressLine1 = dto.PostalAddressLine1,
            PostalAddressLine2 = dto.PostalAddressLine2,
            PostalAddressTownCity = dto.PostalAddressTownCity,
            PostalAddressCounty = dto.PostalAddressCounty,
            Region = dto.Region,
            PostalCode = dto.PostalCode,
            Uprn = dto.Uprn,
            OnspdLatitude = dto.OnspdLatitude,
            OnspdLongitude = dto.OnspdLongitude,
            MainPhoneNumber = dto.MainPhoneNumber,
            InspectionDirectorate = dto.InspectionDirectorate,
            Constituency = dto.Constituency,
            LocalAuthority = dto.Constituency,
            LastInspection = dto.LastInspection,
            ExpiryDate = expiryDate
        };
    }
}