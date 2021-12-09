using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Task<GameObject> CreateCharacter(ScreenSide screenSide);
    Task<GameObject> CreateShootParticle();
    Task<GameObject> CreateBloodParticle();
    Task<GameObject> CreateBullet();
    Task<GameObject> CreateBulletCollisionSparks();
    SpawnMarker GetSpawnMarker(ScreenSide screenSide);
    void LoadBonus(string bonusAssetPath);
    void InstantiateBonus(string bonusAssetPath);
    void CreateShieldEffect(Transform transform);
  }
}