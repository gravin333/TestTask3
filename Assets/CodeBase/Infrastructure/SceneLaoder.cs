using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
  public class SceneLoader
  {

    public async void LoadScene(string sceneAssetPath, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name.Equals(sceneAssetPath))
      {
        onLoaded?.Invoke();
        return;
      }

      AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(sceneAssetPath);
      await asyncOperationHandle.Task;
      onLoaded?.Invoke();
    }
  }
}