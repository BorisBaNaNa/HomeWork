public class AllServices
{
    private class Implementation<TService> where TService : IService
    {
        public static TService Instance;
    }

    public static AllServices Instance => _instance ??= new AllServices();

    private static AllServices _instance;

    public static void RegisterService_<TService>(TService instanceObj) where TService : IService 
        => Instance.RegisterService(instanceObj);

    public static TService GetService_<TService>() where TService : IService 
        => Instance.GetService<TService>();

    public void RegisterService<TService>(TService instanceObj) where TService : IService
        => Implementation<TService>.Instance = instanceObj;

    public TService GetService<TService>() where TService : IService
        => Implementation<TService>.Instance;
}