using Domain;

namespace ServerApplication.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<ISettingsService> _lazySettingsService;
    private readonly Lazy<ICategoryService> _lazyCategoryService;
    private readonly Lazy<IPlaceService> _lazyPlaceService;
    private readonly Lazy<IPlacesChainService> _lazyPlaceChainService;
    private readonly Lazy<IGeoService> _lazyGeoService;

    public ServiceManager(ApplicationContext appCtx)
    {
        _lazyUserService = new Lazy<IUserService>(() => new UserService(appCtx));
        _lazySettingsService = new Lazy<ISettingsService>(() => new SettingsService(appCtx));
        _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(appCtx));
        _lazyPlaceService = new Lazy<IPlaceService>(() => new PlaceService(appCtx));
        _lazyPlaceChainService = new Lazy<IPlacesChainService>(() => new PlacesChainService(appCtx));
        _lazyGeoService = new Lazy<IGeoService>(() => new GeoService(appCtx));
    }

    public IUserService UserService => _lazyUserService.Value;
    public ISettingsService SettingsService => _lazySettingsService.Value;
    public ICategoryService CategoryService => _lazyCategoryService.Value;
    public IPlaceService PlaceService => _lazyPlaceService.Value;
    public IPlacesChainService PlacesChainService => _lazyPlaceChainService.Value;
    public IGeoService GeoService => _lazyGeoService.Value;
}