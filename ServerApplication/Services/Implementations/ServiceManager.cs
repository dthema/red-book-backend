using Domain;

namespace ServerApplication.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<ISettingsService> _lazySettingsService;

    public ServiceManager(ApplicationContext appCtx)
    {
        // appCtx.Database.EnsureDeleted();
        // appCtx.Database.EnsureCreated();
        _lazyUserService = new(() => new UserService(appCtx));
        _lazySettingsService = new(() => new SettingsService(appCtx));
    }

    public IUserService UserService => _lazyUserService.Value;
    public ISettingsService SettingsService => _lazySettingsService.Value;
}