using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePool
{
  public interface IGamePoolService : IService
  {
    void SetPositionAndActive(Vector3 position, GameObject parent = null);
  }
}