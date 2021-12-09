using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.Assets
{
  public class AssetProvider : IAsset
  {
    private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();

    public AssetProvider() => 
      InitAddressable();

    public async Task<GameObject> Instantiate(string address) => 
      await Addressables.InstantiateAsync(address).Task;

    public async Task<GameObject> Instantiate(string address, Transform parent) => 
      await Addressables.InstantiateAsync(address, parent).Task;

    public async Task<GameObject> Instantiate(string address, Vector3 position, Quaternion quaternion) => 
      await Addressables.InstantiateAsync(address, position, quaternion).Task;

    public async Task<T> Load<T>(string address) where T : class
    {
      if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
        return completedHandle.Result as T;

      return await RunWithCacheOnComplete(
        Addressables.LoadAssetAsync<T>(address),
        address);
    }

    private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
    {
      handle.Completed += completeHandle => { _completedCache[cacheKey] = completeHandle; };

      return await handle.Task;
    }


    private void InitAddressable() => 
      Addressables.InitializeAsync();
  }
}