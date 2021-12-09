using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePool
{
  public interface IGameObjectPoolType
  {
    Task<GameObject> CreateObject();
    void SetPositionAndActive(Vector3 position, GameObject gameObject, GameObject parent = null);
  }
}