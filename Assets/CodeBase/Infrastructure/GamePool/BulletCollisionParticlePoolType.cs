using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePool
{
  public class BulletCollisionParticlePoolType : IGameObjectPoolType
  {
    private readonly IGameFactory _gameFactory;

    public BulletCollisionParticlePoolType(IGameFactory gameFactory) => 
      _gameFactory = gameFactory;

    public async Task<GameObject> CreateObject() => 
      await _gameFactory.CreateBulletCollisionSparks();

    public void SetPositionAndActive(Vector3 position, GameObject gameObject, GameObject parent = null)
    {
      gameObject.transform.position = position;
      gameObject.SetActive(true);
    }
  }
}