using Domain;

namespace ServerApplication.Services.Implementations;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _lazyUserService;


    public ServiceManager(ApplicationContext appCtx)
    {
        _lazyUserService = new(() => new UserService(appCtx));
    }

    public IUserService UserService => _lazyUserService.Value;
}