using Domain;

namespace ServerApplication.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<ISettingsService> _lazySettingsService;
    private readonly Lazy<ICategoryService> _lazyCategoryService;
    private readonly Lazy<IPlaceService> _lazyPlaceService;

    public ServiceManager(ApplicationContext appCtx)
    {
        // appCtx.Database.EnsureDeleted();
        // appCtx.Database.EnsureCreated();
        _lazyUserService = new Lazy<IUserService>(() => new UserService(appCtx));
        _lazySettingsService = new Lazy<ISettingsService>(() => new SettingsService(appCtx));
        _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(appCtx));
        _lazyPlaceService = new Lazy<IPlaceService>(() => new PlaceService(appCtx));
    }

    public IUserService UserService => _lazyUserService.Value;
    public ISettingsService SettingsService => _lazySettingsService.Value;
    public ICategoryService CategoryService => _lazyCategoryService.Value;
    public IPlaceService PlaceService => _lazyPlaceService.Value;
}