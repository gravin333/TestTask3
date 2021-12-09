namespace CodeBase.Infrastructure.Services
{
  public class AllServices
  {
    private static AllServices _instance;
    public static AllServices Container => _instance ??= new AllServices();

    public void RegisterSingle<TService>(TService service) => 
      Implementation<TService>.InstanceService = service;

    public TService Single<TService>() => 
      Implementation<TService>.InstanceService;

    private static class Implementation<TService>
    {
      public static TService InstanceService;
    }
  }
}