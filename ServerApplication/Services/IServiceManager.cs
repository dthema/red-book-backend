namespace ServerApplication.Services;

public interface IServiceManager
{
    IUserService UserService { get; }
    ISettingsService SettingsService { get; }
    ICategoryService CategoryService { get; }
    IPlaceService PlaceService { get; }
    IPlacesChainService PlacesChainService { get; }
    IGeoService GeoService { get; }
}