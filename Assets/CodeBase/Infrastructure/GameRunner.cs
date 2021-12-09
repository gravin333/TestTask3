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
      if (!FindObjectOfType<GameBootstrap>())
      {
        GameObject instantiate = Instantiate(GameBootstrapPrefab);
        instantiate.GetComponent<GameBootstrap>().Load(LoadScene);
      }

      gameObject.SetActive(false);
    }
  }
}