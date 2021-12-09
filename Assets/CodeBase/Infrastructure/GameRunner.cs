using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    [SerializeField] private LoadScene LoadScene;
    [SerializeField] private GameObject GameBootstrapPrefab;

    private void Awake()
    {
      GameBootstrap gameBootstrap = FindObjectOfType<GameBootstrap>();
      if (!gameBootstrap)
      {
        GameObject go = Instantiate(GameBootstrapPrefab);
        go.GetComponent<GameBootstrap>().Load(LoadScene);
        return;
      }
      gameBootstrap.Load(LoadScene);
    }
  }
}