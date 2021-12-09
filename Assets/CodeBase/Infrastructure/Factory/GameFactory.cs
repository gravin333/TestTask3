using System;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Assets;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAsset _asset;
    private SpawnMarker[] _spawnMarkers;

    public GameFactory(IAsset asset)
    {
      _asset = asset;
    }

    public async Task<GameObject> CreateCharacter(ScreenSide screenSide)
    {
      GetSpawnMarkers();
      if (TryFindMarkerOfSide(screenSide, out SpawnMarker spawnMarker))
        return await _asset.Instantiate(GameAssetPath.Character, spawnMarker.transform);
      throw new ArgumentNullException(nameof(spawnMarker));
    }

    public async Task<GameObject> CreateShootParticle() => 
      await InstantiatePrefab(ParticleAssetPath.Shoot);

    public async Task<GameObject> CreateBloodParticle() => 
      await InstantiatePrefab(ParticleAssetPath.Blood);

    public async Task<GameObject> CreateBullet() => 
      await InstantiatePrefab(GameAssetPath.Bullet);

    public async Task<GameObject> CreateBulletCollisionSparks() => 
      await InstantiatePrefab(ParticleAssetPath.CollisionSparks);

    public SpawnMarker GetSpawnMarker(ScreenSide screenSide) => 
      _spawnMarkers.First(item => item.ScreenSide == screenSide);

    public void LoadBonus(string bonusAssetPath) => 
      _asset.Load<GameObject>(bonusAssetPath);

    public void InstantiateBonus(string bonusAssetPath) => 
      _asset.Instantiate(bonusAssetPath);

    public void CreateShieldEffect(Transform transform) => 
      _asset.Instantiate(GameEffectAssetPath.ShieldEffect, transform);

    private async Task<GameObject> InstantiatePrefab(string assetPath)
    {
      var prefab = await _asset.Load<GameObject>(assetPath);
      GameObject gameObject = Object.Instantiate(prefab);
      gameObject.SetActive(false);
      return gameObject;
    }

    private bool TryFindMarkerOfSide(ScreenSide screenSide, out SpawnMarker spawnMarker)
    {
      SpawnMarker marker = GetSpawnMarker(screenSide);

      if (marker != null)
      {
        spawnMarker = marker;
        return true;
      }

      spawnMarker = null;
      return false;
    }

    private void GetSpawnMarkers() => 
      _spawnMarkers ??= Object.FindObjectsOfType<SpawnMarker>();
  }
}